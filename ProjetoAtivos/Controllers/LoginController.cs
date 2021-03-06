﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using ProjetoAtivos.Control;
using ProjetoAtivos.Filter;
using ProjetoAtivos.Models;
using ProjetoEstagio.Controls;
using System;

namespace ProjetoAtivos.Controllers
{    
    [FiltroBanco]
    public class LoginController : Controller
    {
        private static LoginControl ctlLogin = new LoginControl();
        private static FilialControl ctlFilial = new FilialControl();
        private static RegionalControl ctlRegiao = new RegionalControl();
        
        [FiltroSession]
        [FiltroBanco]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login(string Login, string Senha)
        {
            var Usuario = ctlLogin.Login(Login, Senha);
            if (Usuario != null) //grava a cookie e redirenciona pra tela inicial
            {
                /*var objFilial = ctlFilial.BuscarFilialPessoa(Usuario.GetPessoa().GetCodigo());
                var objRegiao = ctlRegiao.BuscarRegionalPessoa(Usuario.GetPessoa().GetCodigo());

                int filial = objFilial != null ? objFilial.GetCodigo() : 0;
                int regiao = objRegiao != null ? objRegiao.GetCodigo() : 0;

                if (Usuario.GetTipoUsuario().GetCodigo() == 1)
                    filial = regiao = 0;

                var regiaoFilial = 0;

                if (regiao == 0 && filial != 0)
                    regiaoFilial = objFilial.GetRegional().GetCodigo();*/

                        HttpContext.Session.SetString("Usuario", Usuario.GetLogin());
                        HttpContext.Session.SetString("CodigoUsuario", ""+Usuario.GetCodigo());
                        Response.Cookies.Append("NomeUsuario", Usuario.GetPessoa().GetNome());

                        /*Response.Cookies.Append("RegiaoFilial", regiaoFilial.ToString());   
                        Response.Cookies.Append("Filial", filial.ToString());
                        Response.Cookies.Append("Regiao", regiao.ToString());
                        Response.Cookies.Append("Usuario", Usuario.GetLogin());
                        Response.Cookies.Append("Pessoa", Usuario.GetPessoa().GetCodigo().ToString());
                        Response.Cookies.Append("Nivel", Convert.ToString(Usuario.GetTipoUsuario().GetNivel()));*/

                        //RedirectToAction("Index", "Home");

                        return Json(new { Status = true, msg = "" });

            }
            else
                return Json(new { Status = false, msg = "Usuario ou Senha Invalidos" });
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Usuario");
            HttpContext.Session.Remove("CodigoUsuario");

            Response.Cookies.Delete("Usuario");
            Response.Cookies.Delete("NomeUsuario");
            Response.Cookies.Delete("RegiaoFilial");
            Response.Cookies.Delete("Filial");
            Response.Cookies.Delete("Regiao");
            Response.Cookies.Delete("Pessoa");
            Response.Cookies.Delete("Nivel");

            return RedirectToAction("Index", "Login");
        }
    }
}