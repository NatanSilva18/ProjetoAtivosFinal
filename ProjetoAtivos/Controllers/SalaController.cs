using Microsoft.AspNetCore.Mvc;
using ProjetoAtivos.Control;
using ProjetoAtivos.Filter;
using System;
using System.Collections.Generic;

namespace ProjetoAtivos.Controllers
{
    [FiltroBanco]
    [FiltroSession]
    public class SalaController : Controller
    {
        private static SalaControl ctlSala = new SalaControl();

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult Gravar(int Codigo, string Descricao, Boolean StAtivo, int Filial)
        {
            if (ctlSala.Gravar(Codigo, Descricao, StAtivo, Filial))
                return Json("");
            else
                return Json("Erro ao Gravar o Registro!");
        }

        public JsonResult ExcluirLogico(int Codigo)
        {
            if (ctlSala.ExcluirLogico(Codigo))
                return Json("");
            else
                return Json("Não Foi Possivel Excluir o Registro!");
        }
        public JsonResult Ativar(int Codigo)
        {
            if (ctlSala.Ativar(Codigo))
                return Json("");
            else
                return Json("Não Foi Possivel Ativar o Registro!");
        }
        public JsonResult BuscarSala(int Codigo)
        {
            object Dado = new object();
            var L = ctlSala.BuscarSala(Codigo);
            Dado = (new
            {
                Codigo = L.GetCodigo(),
                Descricao = L.GetDescricao(),
                StAtivo = L.GetStAtivo(),
                FilCodigo = L.GetFilial().GetCodigo(),
                FilRazao = L.GetFilial().GetRazao()

            });

            return Dado == null ? Json("") : Json(Dado);
        }
        public JsonResult BuscarSalas(int Codigo)
        {
            var Lista = ctlSala.BuscarSalas(Codigo);
            List<object> Dados = new List<object>();

            if (Lista != null)
            {
                foreach (var L in Lista)
                {
                    Dados.Add(new
                    {
                        Codigo = L.GetCodigo(),
                        Descricao = L.GetDescricao(),
                        StAtivo = L.GetStAtivo(),
                        FilCodigo = L.GetFilial().GetCodigo(),
                        FilRazao = L.GetFilial().GetRazao(),
                        FilAtivo = L.GetFilial().GetStativo()
                    });
                }
            }

            return Dados == null ? Json("") : Json(Dados);
        }
        public JsonResult ObterSalas(string Chave, string Filtro, int Ativo)
        {
            var Lista = ctlSala.ObterSalas(Chave, Filtro, Ativo);
            List<object> Dados = new List<object>();

            if (Lista != null)
            {
                foreach (var L in Lista)
                {
                    Dados.Add(new
                    {
                        Codigo = L.GetCodigo(),
                        Descricao = L.GetDescricao(),
                        StAtivo = L.GetStAtivo(),
                        FilCodigo = L.GetFilial().GetCodigo(),
                        FilRazao = L.GetFilial().GetRazao(),
                        FilAtivo = L.GetFilial().GetStativo()
                    });
                }
            }

            return Dados == null ? Json("") : Json(Dados);
        }
    }
}