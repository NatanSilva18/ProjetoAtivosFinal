using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoAtivos.Control;
using ProjetoAtivos.Filter;

namespace ProjetoAtivos.Controllers
{
    [FiltroSession]
    public class MotivoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private static MotivoControl ctlMotivo = new MotivoControl();

        public JsonResult Gravar(int Codigo, string Descricao, Boolean StAtivo)
        {
            if (ctlMotivo.Gravar(Codigo, Descricao, StAtivo))
                return Json("");
            else
                return Json("Erro ao Gravar o Registro!");
        }

        public JsonResult ObterMotivos(string Chave, string Filtro, int Ativo)
        {
            var Lista = ctlMotivo.ObterMotivos(Chave, Filtro, Ativo);
            List<object> Dados = new List<object>();

            if (Lista != null)
            {
                foreach (var L in Lista)
                {
                    Dados.Add(new
                    {
                        Codigo = L.GetCodigo(),
                        Descricao = L.GetDescricao(),
                        StAtivo = L.GetStAtivo()
                    });
                }
            }

            return Dados == null ? Json("") : Json(Dados);
        }

        public JsonResult ExcluirLogico(int Codigo)
        {
            if (ctlMotivo.ExcluirLogico(Codigo))
                return Json("");
            else
                return Json("Não Foi Possivel Excluir o Registro!");
        }
        public JsonResult Ativar(int Codigo)
        {
            if (ctlMotivo.Ativar(Codigo))
                return Json("");
            else
                return Json("Não Foi Possivel Ativar o Registro!");
        }
        public JsonResult BuscarMotivo(int Codigo)
        {
            object Dado = new object();
            var L = ctlMotivo.BuscarMotivo(Codigo);
            Dado = (new
            {
                Codigo = L.GetCodigo(),
                Descricao = L.GetDescricao(),
                StAtivo = L.GetStAtivo(),

            });

            return Dado == null ? Json("") : Json(Dado);
        }
    }
}