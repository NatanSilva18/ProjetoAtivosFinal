using System;
using System.Linq;
using System.Web.Http.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Survey.DAL;

namespace ProjetoAtivos.Filter 
{
    public class FiltroSession : FilterAttribute, Microsoft.AspNetCore.Mvc.Filters.IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            bool login = context.ActionDescriptor.RouteValues.Values.Contains("Login") && context.ActionDescriptor.RouteValues.Values.Contains("Index");


            if (!login && context.HttpContext.Session.GetString("Usuario") == null)
            {
                context.Result = new RedirectResult("/Login/Logout");
            }
            else
                if(login && context.HttpContext.Session.GetString("Usuario") != null)
                    context.Result = new RedirectResult("/Home/Index");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }
    }
}
