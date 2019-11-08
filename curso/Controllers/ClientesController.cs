using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using domain;

namespace curso.Controllers {
    public class ClientesController : Controller {
        private AWEntities db = new AWEntities();
        //protected override void OnActionExecuting(ActionExecutingContext filterContext) {
        //    Debug.WriteLine($"{filterContext.ActionDescriptor.ControllerDescriptor.ControllerName} -> {filterContext.ActionDescriptor.ActionName}");
        //    //Debug.WriteLine(filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + " -> " + filterContext.ActionDescriptor.ActionName);
        //    base.OnActionExecuting(filterContext);
        //}

        // GET: Clientes
        public ActionResult Index(int page = 0, int size = 10, string buscar = "") {
            var q = db.Customers
                    .OrderBy(o => o.CustomerID);
            if (!string.IsNullOrWhiteSpace(buscar))
                q = q.Where(o => (o.FirstName + " " + o.MiddleName + " " + o.LastName).ToLower().Contains(buscar.ToLower())) as IOrderedQueryable<Customer>;

            ViewBag.CountPages = Math.Ceiling((Decimal)q.Count() / size);

            if (page >= ViewBag.CountPages)
                return HttpNotFound();
            ViewBag.NumPage = page;
            ViewBag.buscar = buscar;
            var rslt = q.Skip(page * size)
                    .Take(size)
                    .ToList();
            if (Request.IsAjaxRequest())
                return Json(rslt, JsonRequestBehavior.AllowGet);
            return View(rslt);
        }

        // GET: Clientes/Details/5
        //[TraceActionFilter]
        public ActionResult Details(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers
                .FirstOrDefault(o => o.CustomerID == id);
            if (customer == null) {
                return HttpNotFound();
            }
            return View(customer);
        }
        // [Route("Clientes/Details/{id}/Direcciones")]
        public ActionResult Direcciones(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null) {
                return HttpNotFound();
            }

            return View("Direcciones", customer.CustomerAddresses.Select(o => o.Address).ToList());
        }

        // GET: Clientes/Create
        //[TraceActionFilter]
        public ActionResult Create() {
            return View();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,NameStyle,Title,FirstName,MiddleName,LastName,Suffix,CompanyName,SalesPerson,EmailAddress,Phone,PasswordHash,PasswordSalt,rowguid,ModifiedDate")] Customer customer) {
            if (ModelState.IsValid) {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null) {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,NameStyle,Title,FirstName,MiddleName,LastName,Suffix,CompanyName,SalesPerson,EmailAddress,Phone,PasswordHash,PasswordSalt,rowguid,ModifiedDate")] Customer customer) {
            ModelState.AddModelError("", "Este es para el sumario");
            ModelState.AddModelError("NameStyle", "Este no es un tratamiento");
            //if (ModelState.IsValid) {
            //    db.Entry(customer).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            return View(customer);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null) {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
