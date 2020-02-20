using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SCGEX_TRAINING.Data;
using SCGEX_TRAINING.Models;
using SCGEX_TRAINING.Services;
using SCGEX_TRAINING.ViewModels;

namespace SCGEX_TRAINING.Controllers
{
    public class MachinesController : Controller
    {
        private readonly AppDb db;
        private readonly IEnumerable<ILogWriter> logs;

        public MachinesController(AppDb db, IEnumerable<ILogWriter> logs)
        {
            this.db = db;
            this.logs = logs;
        }

        [Route("[controller]/{id}")]
        public IActionResult Index(int id)
        {
            var m = db.Machines.Find(id);

            if(m == null) { return NotFound(); }

            db.Slot.Where(x => x.Machine.Id == m.Id && x.Price <= m.TotalAmount && x.Quantity >= 1).ToList();

            //db.Entry(m).Collection(x => x.Slots).Query().Where(x => x.Price >= m.TotalAmount).Load(); // Manual Loading

            var q = from mx in db.Machines
                    orderby mx.Id
                    select new SelectListItem { Value = mx.Id.ToString(), Text = $"Machine #{mx.Id}" };

            ViewBag.MachinesList = new SelectList(q, "Value", "Text", id);
            return View(m);
        }

        [Route("[controller]/[action]/{id}")]
        public IActionResult Accepts(int id, decimal amount)
        {
            var m = db.Machines.Find(id);

            if (m == null) { return NotFound(); }

            try
                {
                    m.AcceptCoin(amount);
                    db.SaveChanges();
                    //var log = new FileLogWriter();
                    foreach (var log in logs)
                    log.Write($"Machine #{id} has accepted {amount} Baht");
                    TempData["AcceptResult"] = "Saved";
                }
            catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }

            return RedirectToAction("Index", new { id = id });
        }

        [HttpPost("[controller]/[action]/{id}")]
        public IActionResult Clear(int id)
        {
            var m = db.Machines.Find(id);

            m.ClearCoin();
            db.SaveChanges(); 
            foreach (var log in logs)
            log.Write($"Machine #{id} has Clear Coin");

            return RedirectToAction("Index", new { id = id });
        }

        [HttpPost("[controller]/[action]/{id}")]
        public IActionResult SwitchOff(int id) 
        {
            var m = db.Machines.Find(id);

            m.SwitchMachineOff();
            db.SaveChanges();

            return RedirectToAction("Index", new { id = id });
        }

        [HttpPost("[controller]/[action]/{id}")]
        public IActionResult SwitchOn(int id)
        {
            var m = db.Machines.Find(id);

            m.SwitchMachineOn();
            db.SaveChanges();

            return RedirectToAction("Index", new { id = id });
        }

        [HttpGet("[controller]/[action]/{id}")]
        public IActionResult AddProducts(int id)
        {
            var vm = new MachinesAddProductsVM();
            vm.MachineId = id;
            vm.ExpiredDate = DateTime.Today.AddDays(7);
            ViewBag.ProductInfoCodeList = new SelectList(
                db.ProductInfos,
                nameof(ProductInfo.Code),
                nameof(ProductInfo.Name));
            return View(vm);
        }

        [HttpPost("[controller]/[action]/{id}")]
        public async Task<IActionResult> AddProducts(int id, MachinesAddProductsVM item)
        {
            if (!ModelState.IsValid)

            {
                ViewBag.ProductInfoCodeList = new SelectList(
                   db.ProductInfos,
                   nameof(ProductInfo.Code),
                   nameof(ProductInfo.Name));
                return View(item);
            }

            var m = await db.Machines
                .Include(x => x.Slots) // Eagerly Loading
                .SingleOrDefaultAsync(x => x.Id == id);

            var pi = await db.ProductInfos.FindAsync(item.ProductInfoCode);
            m.AddProducts(pi, item.Quantity, item.ExpiredDate);
            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { id });
        }
    }
}