using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace curso {
    public class TraceActionFilterAttribute : ActionFilterAttribute {
        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            Debug.WriteLine($"Iniciando {filterContext.ActionDescriptor.ControllerDescriptor.ControllerName} -> {filterContext.ActionDescriptor.ActionName}");
            base.OnActionExecuting(filterContext);
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext) {
            Debug.WriteLine($"Ejecutado {filterContext.ActionDescriptor.ControllerDescriptor.ControllerName} -> {filterContext.ActionDescriptor.ActionName}");
            base.OnActionExecuted(filterContext);
        }
    }

    public class TraceActionFilter : IActionFilter {
        public void OnActionExecuted(ActionExecutedContext filterContext) {
            Debug.WriteLine($"Iniciando {filterContext.ActionDescriptor.ControllerDescriptor.ControllerName} -> {filterContext.ActionDescriptor.ActionName}");
        }

        public void OnActionExecuting(ActionExecutingContext filterContext) {
            Debug.WriteLine($"Ejecutado {filterContext.ActionDescriptor.ControllerDescriptor.ControllerName} -> {filterContext.ActionDescriptor.ActionName}");
        }
    }
}