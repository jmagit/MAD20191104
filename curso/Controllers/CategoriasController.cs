using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using domain;

namespace curso.Controllers {
    public class CategoriasShortDTO {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public class CategoriasController : ApiController {
        private AWEntities db = new AWEntities();

        // GET: api/Categorias
        public IQueryable<CategoriasShortDTO> GetCategories() {
            return db.Categories
                .Where(o => o.MainCategory == null)
                .Select(o => new CategoriasShortDTO() { Name = o.Name, Id = o.ProductCategoryID });
        }

        // GET: api/Categorias/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult GetCategory(int id) {
            Category category = db.Categories.FirstOrDefault(o => o.ProductCategoryID == id && o.ParentProductCategoryID == null);
            if (category == null) {
                return NotFound();
            }

            return Ok(category.SubCategories);
        }

        // PUT: api/Categorias/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCategory(int id, Category category) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if (id != category.ProductCategoryID) {
                return BadRequest();
            }

            db.Entry(category).State = EntityState.Modified;

            try {
                db.SaveChanges();
            } catch (DbUpdateConcurrencyException) {
                if (!CategoryExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Categorias
        [ResponseType(typeof(Category))]
        public IHttpActionResult PostCategory(Category category) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            db.Categories.Add(category);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = category.ProductCategoryID }, category);
        }

        // DELETE: api/Categorias/5
        [ResponseType(typeof(Category))]
        public IHttpActionResult DeleteCategory(int id) {
            Category category = db.Categories.Find(id);
            if (category == null) {
                return NotFound();
            }

            db.Categories.Remove(category);
            db.SaveChanges();

            return Ok(category);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id) {
            return db.Categories.Count(e => e.ProductCategoryID == id) > 0;
        }
    }
}