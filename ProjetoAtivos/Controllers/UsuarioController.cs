﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoAtivos.Control;

namespace ProjetoAtivos.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioControl ctlUsuario = new UsuarioControl();

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult Gravar(int Codigo, string Login, string Senha, int TipoUsuario, int CodigoPessoa, int Operacao)
        {
            int Retorno;
            Retorno = ctlUsuario.Gravar(Codigo, Login, Senha, TipoUsuario, CodigoPessoa, Operacao);

            switch (Retorno)
            {
                case 1:
                    return Json("");

                case -10:
                    return Json("Usuario Ja Cadastrado!");

                case -20:
                    return Json("Erro ao Gravar o Registro!");

                case -30:
                    return Json("Pessoa Ja Possui um Usuario Cadastrado!");

                case -40:
                    return Json("Não e Possivel Alterar o Ultimo Adminstrador");
            }

            return Json("Erro ao Gravar o Registro!");
        }

        public JsonResult ObterUsuarios(string Chave, string Filtro)
        {
            var Lista = ctlUsuario.ObterUsuarios(Chave, Filtro);
            List<object> Dados = new List<object>();

            if (Lista != null)
            {
                foreach (var L in Lista)
                {
                    Dados.Add(new
                    {
                        Codigo = L.GetCodigo(),
                        Email = L.GetLogin(),
                        Senha = L.GetSenha(),
                        PesCodigo = L.GetPessoa().GetCodigo(),
                        PesNome = L.GetPessoa().GetNome(),
                        Nivel = L.GetTipoUsuario().GetCodigo()  //codigo representa o nivel
                  
                    });
                }
            }


            return Dados == null ? Json("") : Json(Dados);
        }
        public JsonResult BuscarUsuario(int Codigo)
        {
            List<object> Dado = new List<object>();
            var L = ctlUsuario.BuscarUsuario(Codigo);
            Dado.Add(new
            {
                Codigo = L.GetCodigo(),
                Email = L.GetLogin(),
                Senha = L.GetSenha(),
                CodigoPessoa = L.GetPessoa().GetCodigo(),
                Nome = L.GetPessoa().GetNome(),
                Cpf = L.GetPessoa().GetCodigo(),
                Nivel = L.GetTipoUsuario().GetCodigo()  //codigo representa o nivel
            });
            return Dado == null ? Json("") : Json(Dado);
        }
        public JsonResult Excluir(int Codigo)
        {
            int Retorno = 0;

            Retorno = ctlUsuario.Excluir(Codigo);

            switch (Retorno)
            {
                case 1:
                    return Json("");

                case -10:
                    return Json("Não e Possivel Excluir o Ultimo Administrador!");

                case -20:
                    return Json("Erro ao Excluir o Registro!");
            }

            return Json("Erro ao Excluir o Registro!");
        }
    }
}