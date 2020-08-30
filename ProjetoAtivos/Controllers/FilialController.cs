using Microsoft.AspNetCore.Mvc;
using ProjetoAtivos.Control;
using ProjetoAtivos.Filter;
using System;
using System.Collections.Generic;

namespace ProjetoAtivos.Controllers
{
    [FiltroSession]
    public class FilialController : Controller
    {
        private static FilialControl ctlFilial = new FilialControl();

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult Gravar(int Codigo, string Razao, string Cnpj, Boolean StAtivo, int Endereco, string EndLogradouro, int EndNumero, string EndReferencia, string EndBairro, string EndCep, string EndCidade, string EndEstado, int Responsavel, int Regional, int Operacao)
        {
            int Retorno = 0;

            Retorno = ctlFilial.Gravar(Codigo, Razao, Cnpj, StAtivo, Endereco, EndLogradouro, EndNumero, EndReferencia, EndBairro, EndCep, EndCidade, EndEstado, Responsavel, Regional);

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
            if (ctlFilial.ExcluirLogico(Codigo))
                return Json("");
            else
                return Json("Não Foi Possivel Excluir o Registro!");
        }
        public JsonResult Ativar(int Codigo)
        {
            if (ctlFilial.Ativar(Codigo))
                return Json("");
            else
                return Json("Não Foi Possivel Ativar o Registro!");
        }
        public JsonResult BuscarFilial(int Codigo)
        {
            object Dado = new object();
            var L = ctlFilial.BuscarFilial(Codigo);
            Dado = (new
            {
                Codigo = L.GetCodigo(),
                Razao = L.GetRazao(),
                Cnpj = L.GetCnpj(),
                StAtivo = L.GetStativo(),
                Estado = L.GetEndereco().GetEstado(),
                Responsavel = L.GetResponsavel().GetNome(),
                RespCodigo = L.GetResponsavel().GetCodigo(),
                Regional = L.GetRegional().GetDescricao(),
                RegCodigo = L.GetRegional().GetCodigo(),
                Logradouro = L.GetEndereco().GetLogradouro(),
                Numero = L.GetEndereco().GetNumero(),
                Referencia = L.GetEndereco().GetReferencia(),
                Bairro = L.GetEndereco().GetBairro(),
                Cep = L.GetEndereco().GetCep(),
                Cidade = L.GetEndereco().GetCidade(),
                Endereco = L.GetEndereco().GetCodigo()
            });

            return Dado == null ? Json("") : Json(Dado);
        }
        public JsonResult BuscarFiliais(int Codigo)
        {
            var Lista = ctlFilial.BuscarFiliais(Codigo);
            List<object> Dados = new List<object>();

            if (Lista != null)
            {
                foreach (var L in Lista)
                {
                    Dados.Add(new
                    {
                        Codigo = L.GetCodigo(),
                        Razao = L.GetRazao(),
                        Cnpj = L.GetCnpj(),
                        StAtivo = L.GetStativo(),
                        Endereco = L.GetEndereco().GetCodigo(),
                        Responsavel = L.GetResponsavel().GetCodigo(),
                        Regional = L.GetRegional().GetCodigo()

                    });
                }
            }

            return Dados == null ? Json("") : Json(Dados);
        }
        public JsonResult ObterFiliais(string Chave, string Filtro, int Ativo)
        {
            var Lista = ctlFilial.ObterFiliais(Chave, Filtro, Ativo);
            List<object> Dados = new List<object>();

            if (Lista != null)
            {
                foreach (var L in Lista)
                {
                    Dados.Add(new
                    {
                        Codigo = L.GetCodigo(),
                        Razao = L.GetRazao(),
                        Cnpj = L.GetCnpj(),
                        StAtivo = L.GetStativo(),
                        Estado = L.GetEndereco().GetEstado(),
                        Responsavel = L.GetResponsavel().GetNome(),
                        RespCodigo = L.GetResponsavel().GetCodigo(),
                        Regional = L.GetRegional().GetDescricao(),
                        RegCodigo = L.GetRegional().GetCodigo(),
                        RegAtivo = L.GetRegional().GetStAtivo()
                    });
                }
            }

            return Dados == null ? Json("") : Json(Dados);
        }
    }
}