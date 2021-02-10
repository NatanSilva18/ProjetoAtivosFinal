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
        private static AtivoControl ctlAtivo = new AtivoControl();

        public IActionResult Index()
        {
            return View();
        }
        private IHostingEnvironment _env;

        public InventarioController(IHostingEnvironment env)
        {
            _env = env;
        }
    }
}
