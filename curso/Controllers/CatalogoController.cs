using domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace curso.Controllers {
    public class CatalogoController : Controller {
        private AWEntities db = new AWEntities();

        // GET: Catalogo
        public ActionResult Index() {
            ViewBag.categorias = (new CategoriasController()).GetCategories().ToList();
            return View();
        }
        public PartialViewResult Subcategorias(int id) {
            Category category = db.Categories.Find(id);
            //if (category == null) {
            //    return NotFound();
            //}

            return PartialView("_Subcategorias", category.SubCategories);
        }
    }
}