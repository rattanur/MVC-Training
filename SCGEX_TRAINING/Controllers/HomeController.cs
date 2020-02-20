using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SCGEX_TRAINING.Data;
using SCGEX_TRAINING.Models;
using SCGEX_TRAINING.Services;

namespace SCGEX_TRAINING.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDb db;
        private readonly IHostingEnvironment env;
        private readonly IEnumerable<ILogWriter> logs;

        public HomeController(AppDb db, IHostingEnvironment env, IEnumerable<ILogWriter> logs)
        {
            this.db = db;
            this.env = env;
            this.logs = logs;
        }

        public IActionResult Index()
        {
            // Language Integrated Query = LINQ
            // LINQ query syntax
            var q = from c in db.Categories
                    where c.Name.StartsWith("B")
                    orderby c.Name
                    select c;

            var items = q.ToList();

            //return new ViewAsPdf(items);

            return View(items);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // call method on url {~/home/circlearea?radius?3
        public ContentResult CircleArea(double radius)
        {
            var area = Math.PI * Math.Pow(radius, 2);

            var r = new ContentResult();
            r.Content = area.ToString();

            return r;
        }

        public IActionResult TestReturnString()
        {
            return Content("😄 <b>Happy<b/> ~~~", "text/html", Encoding.Unicode);
        }

        public IActionResult TestReturnStatus(string id)
        {
            var p = db.ProductInfos.Find(id);

            if (p == null) { return NotFound(new ProblemDetails { Title = "Product Not Found", Detail = $"ERROR ID '{id}' NOT FOUND" }); }

            return Json(p);
        }

        public IActionResult TestReturnPictureRoot()
        {
            return File("~/Files/PIC_001.jpg", "image/jpeg");
        }

        public IActionResult TestReturnPicture()
        {
            var filePath = Path.Combine(env.ContentRootPath, "Files", "PIC_001.jpg");
            foreach (ILogWriter log in logs)
            {
                log.Write($"Someone Watch Training_Picture");
            }
            return PhysicalFile(filePath, "image/jpg");
        }

        public IActionResult TestReturnPDF()
        {
            var filePath = Path.Combine(env.ContentRootPath, "Files", "Handbook.pdf");
            foreach (ILogWriter log in logs)
            {
                log.Write($"Someone Watch HandBook");
            }
            return PhysicalFile(filePath, "application/pdf");
        }

        public IActionResult TestReturnPDFDownload()
        {
            var filePath = Path.Combine(env.ContentRootPath, "Files", "Handbook.pdf");
            return PhysicalFile(filePath, "application/pdf", "HandBook.pdf");
        }
    }
}
