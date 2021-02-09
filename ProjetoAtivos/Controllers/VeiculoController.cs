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
    public class VeiculoController : Controller
    {
        private static AtivoControl ctlAtivo = new AtivoControl();
        public IActionResult Index()
        {
            return View();
        }

        private IHostingEnvironment _env;

        public VeiculoController(IHostingEnvironment env)
        {
            _env = env;
        }

        public JsonResult Gravar(int Codigo, int Regional, int Filial, int Placa, string Tag, string Estado, string Observacao, string Descricao, int TipoAtivo, string Marca, string NumeroSerie, string Modelo, double Valor, string Imagem, string Latitude, string Longitude, int CodigoNota, string NumeroNota, double ValorNota, DateTime DataEmissao, string Fornecedor, string Cnpj, string NomeAnexo, string Anexo, string Cor, string PlacaVeiculo, string CRLV, string DUT, string FIPE, string ModeloV)
        {
            int Retorno = ctlAtivo.Gravar(Codigo, Regional, Filial, Placa, Tag, Estado, Observacao, Descricao, TipoAtivo, Marca, NumeroSerie, Modelo, Valor, Imagem, Latitude, Longitude, CodigoNota, NumeroNota, ValorNota, DataEmissao, Fornecedor, Cnpj, NomeAnexo, Anexo, Cor, PlacaVeiculo, CRLV, DUT, FIPE, ModeloV);
            if (Retorno == 1)
                return Json("");
            else
            {
                if (Retorno == 0)
                    return Json("Erro ao Gravar o Registro");
                else
                {
                    if (Retorno == -10)
                        return Json("Insira uma Imagem!");
                    else
                        return Json("Ativo ja Cadastrada com a Placa Informada");

                }
            }
        }

        public JsonResult ObterVeiculos(string Chave, string Filtro, int Ativo, int Regiao, int Filial)
        {

            List<object> Dados = ctlAtivo.ObterAtivos(Chave, Filtro, Ativo, Regiao, Filial, true);

            return Dados == null ? Json("") : Json(Dados);
        }
    }
}
