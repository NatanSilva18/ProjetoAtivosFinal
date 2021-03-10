using Microsoft.AspNetCore.Mvc;
using ProjetoAtivos.Control;
using ProjetoAtivos.Filter;
using System;
using System.Collections.Generic;

namespace ProjetoAtivos.Controllers
{
    [FiltroBanco]
    [FiltroSession]
    public class PessoaController : Controller
    {
        private static PessoaControl ctlPessoa = new PessoaControl();

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult Gravar(int Codigo, string Matricula, string Nome, string Email, string Cargo, string Telefone, string Telefone2, Boolean Ativo, string EndLogradouro, int EndNumero, string EndReferencia, string EndBairro, string EndCep, string EndCidade, string EndEstado, int Operacao)
        {
            int Retorno = 0;

            Retorno = ctlPessoa.Gravar(Codigo, Matricula, Nome, Email, Cargo, Telefone, Telefone2, Ativo, EndLogradouro, EndNumero, EndReferencia, EndBairro, EndCep, EndCidade, EndEstado);

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
            if (ctlPessoa.ExcluirLogico(Codigo))
                return Json("");
            else
                return Json("Não Foi Possivel Excluir o Registro!");
        }
        public JsonResult Ativar(int Codigo)
        {
            if (ctlPessoa.Ativar(Codigo))
                return Json("");
            else
                return Json("Não Foi Possivel Ativar o Registro!");
        }
        public JsonResult BuscarPessoa(int Codigo)
        {
            object Dado = new object();
            var L = ctlPessoa.BuscarPessoa(Codigo);
            Dado = (new
            {
                Codigo = L.GetCodigo(),
                Matricula = L.GetMatricula(),
                Nome = L.GetNome(),
                Email = L.GetEmail(),
                Cargo = L.GetCargo(),
                Telefone = L.GetTelefone1(),
                Telefone2 = L.GetTelefone2(),
                StAtivo = L.GetStAtivo(),
                Logradouro = L.GetEndereco().GetLogradouro(),
                Numero = L.GetEndereco().GetNumero(),
                Referencia = L.GetEndereco().GetReferencia(),
                Bairro = L.GetEndereco().GetBairro(),
                Cep = L.GetEndereco().GetCep(),
                Cidade = L.GetEndereco().GetCidade(),
                Estado = L.GetEndereco().GetEstado()
            });

            return Dado == null ? Json("") : Json(Dado);
        }
        public JsonResult ObterPessoas(string Chave, string Filtro, int Ativo)
        {
            var Lista = ctlPessoa.ObterPessoas(Chave, Filtro, Ativo);
            List<object> Dados = new List<object>();

            if (Lista != null)
            {
                foreach (var L in Lista)
                {
                    Dados.Add(new
                    {
                        Codigo = L.GetCodigo(),
                        Matricula = L.GetMatricula(),
                        Nome = L.GetNome(),
                        Email = L.GetEmail(),
                        Cargo = L.GetCargo(),
                        Telefone = L.GetTelefone1(),
                        Telefone2 = L.GetTelefone2(),
                        StAtivo = L.GetStAtivo(),
                        Endereco = L.GetEndereco().GetCodigo()
                    });
                }
            }

            return Dados == null ? Json("") : Json(Dados);
        }
    }
}