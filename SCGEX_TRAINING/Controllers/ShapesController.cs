using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SCGEX_TRAINING.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCGEX_TRAINING.Controllers
{
    public class ShapesController : Controller
    {
        // GET: /<controller>/
        public IActionResult Circle(int radius, string color)
        {
            var s = $"This is a {color} cricle with radius {radius} cm.";

            ViewBag.Message = s;

            var c = new Circle();
            c.Radius = radius;
            c.Color = color;

            //return Content(s);
            return View(model: c);

        }

        public IActionResult Dot(int radius, string color)
        {

            var c = new Circle();
            c.Radius = radius;
            c.Color = color;

            return PartialView(model: c);

        }
    }
}
