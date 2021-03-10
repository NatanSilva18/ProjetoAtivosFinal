using Microsoft.AspNetCore.Mvc;
using ProjetoAtivos.Control;
using ProjetoAtivos.Filter;
using System;
using System.Collections.Generic;

namespace ProjetoAtivos.Controllers
{
    [FiltroBanco]
    [FiltroSession]
    public class RegionalController : Controller
    {
        private static RegionalControl ctlRegional = new RegionalControl();

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult Gravar(int Codigo, string Descricao, Boolean StAtivo, int Operacao, int PesCodigo)
        {
            int Retorno = 0;

            Retorno = ctlRegional.Gravar(Codigo, Descricao, StAtivo, Operacao, PesCodigo);

            if (Retorno == 10)
                return Json("");
            else
            {
                if (Retorno == -10)
                    return Json("Erro ao Gravar o Registro!");
                else
                {
                    if (Retorno == -20)
                        return Json("Registro ja Cadastrado!");
                    else
                        return Json("Erro ao Gravar o Registro!");
                }
            }

        }
        public JsonResult ExcluirLogico(int Codigo)
        {
            if (ctlRegional.ExcluirLogico(Codigo))
                return Json("");
            else
                return Json("Não Foi Possivel Excluir o Registro!");
        }
        public JsonResult Ativar(int Codigo)
        {
            if (ctlRegional.Ativar(Codigo))
                return Json("");
            else
                return Json("Não Foi Possivel Ativar o Registro!");
        }
        public JsonResult BuscarRegional(int Codigo)
        {
            object Dado = new object();
            var L = ctlRegional.BuscarRegional(Codigo);
            Dado = (new
            {
                Codigo = L.GetCodigo(),
                Descricao = L.GetDescricao(),
                PesCodigo = L.GetPessoa().GetCodigo(),
                Nome = L.GetPessoa().GetNome(),
                StAtivo = L.GetStAtivo()
            });

            return Dado == null ? Json("") : Json(Dado);
        }
        public JsonResult ObterRegionais(string Chave, string Filtro, int Ativo)
        {
            var Lista = ctlRegional.ObterRegionais(Chave, Filtro, Ativo);
            List<object> Dados = new List<object>();

            if (Lista != null)
            {
                foreach (var L in Lista)
                {
                    Dados.Add(new
                    {
                        Codigo = L.GetCodigo(),
                        Descricao = L.GetDescricao(),
                        PesCodigo = L.GetPessoa().GetCodigo(),
                        Nome = L.GetPessoa().GetNome(),
                        StAtivo = L.GetStAtivo()
                    });
                }
            }

            return Dados == null ? Json("") : Json(Dados);
        }
       
    }
}