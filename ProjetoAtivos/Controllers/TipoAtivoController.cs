using Microsoft.AspNetCore.Mvc;
using ProjetoAtivos.Control;
using ProjetoAtivos.Filter;
using System;
using System.Collections.Generic;

namespace ProjetoAtivos.Controllers
{
    [FiltroSession]
    public class TipoAtivoController : Controller
    {
        private static TipoAtivoControl ctlTipoAtivo = new TipoAtivoControl();

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Gravar(int Codigo, string Descricao, double Valor, Boolean StAtivo)
        {
            if (ctlTipoAtivo.Gravar(Codigo, Descricao, Valor, StAtivo))
                return Json("");
            else
                return Json("Erro ao Gravar o Registro!");
        }

        public JsonResult ObterTiposAtivos(string Chave, string Filtro, int Ativo)
        {
            var Lista = ctlTipoAtivo.ObterTiposAtivos(Chave, Filtro, Ativo);
            List<object> Dados = new List<object>();

            if (Lista != null)
            {
                foreach (var L in Lista)
                {
                    Dados.Add(new
                    {
                        Codigo = L.GetCodigo(),
                        Descricao = L.GetDescricao(),
                        Valor = L.GetValor(),
                        StAtivo = L.GetStAtivo()
                    });
                }
            }

            return Dados == null ? Json("") : Json(Dados);
        }

        public JsonResult ExcluirLogico(int Codigo)
        {
            if (ctlTipoAtivo.ExcluirLogico(Codigo))
                return Json("");
            else
                return Json("Não Foi Possivel Excluir o Registro!");
        }
        public JsonResult Ativar(int Codigo)
        {
            if (ctlTipoAtivo.Ativar(Codigo))
                return Json("");
            else
                return Json("Não Foi Possivel Ativar o Registro!");
        }
        public JsonResult BuscarTipoAtivo(int Codigo)
        {
            object Dado = new object();
            var L = ctlTipoAtivo.BuscarTipoAtivo(Codigo);
            Dado = (new
            {
                Codigo = L.GetCodigo(),
                Descricao = L.GetDescricao(),
                Valor = L.GetValor(),
                StAtivo = L.GetStAtivo(),

            });

            return Dado == null ? Json("") : Json(Dado);
        }


    }
}
