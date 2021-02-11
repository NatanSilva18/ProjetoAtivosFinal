using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoAtivos.Control;
using ProjetoAtivos.Filter;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace ProjetoAtivos.Controllers
{
    [FiltroSession]
    public class InventarioController : Controller
    {
        private static InventarioControl ctlInv = new InventarioControl();

        public IActionResult Index()
        {
            return View();
        }
        private IHostingEnvironment _env;

        public InventarioController(IHostingEnvironment env)
        {
            _env = env;
        }

        public JsonResult Buscar(string DtIni, string DtFim, int Regiao, int Filial)
        {
            List<object> Dados = ctlInv.Buscar(DtIni, DtFim, Regiao, Filial);

            return Dados == null ? Json("") : Json(Dados);
        }
    }
}
