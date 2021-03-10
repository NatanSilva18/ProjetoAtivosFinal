using System;
using System.Linq;
using System.Web.Http.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjetoAtivos.Filter
{
    public class FiltroBanco : FilterAttribute, Microsoft.AspNetCore.Mvc.Filters.IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {            
            Survey.DAL.Banco b = Survey.DAL.Banco.GetInstance();
            b.FechaConexao();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    
    }
}
