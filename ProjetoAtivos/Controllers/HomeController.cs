using Microsoft.AspNetCore.Mvc;
using ProjetoAtivos.Control;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetoAtivos.Filter;

namespace ProjetoAtivos.Controllers
{
    [FiltroSession]
    public class HomeController : Controller
    {
        private static RegionalControl ctlRegional = new RegionalControl();
        private static AtivoControl ctlAtivo = new AtivoControl();
        private static TransferenciaControl ctlTransferencia = new TransferenciaControl();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public JsonResult Grafico()
        {
            List<object> Dados = ctlRegional.AtivosRegional();
            Dados.Add(null);
            Dados.Add(ctlAtivo.AtivosImagem());
            Dados.Add(ctlAtivo.SomaAtivos());
            Dados.Add(ctlTransferencia.StatusTransferencias());
            return Dados == null ? Json("") : Json(Dados);
        }

    }
}
