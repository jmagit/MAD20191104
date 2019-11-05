using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace curso.Controllers
{
    public class DemosController : Controller
    {
        // GET: Demos
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Despide() {
            return View("Adios");
        }

        [NonAction]
        public string SemiPublico() {
            return "algo";
        }
    }
}