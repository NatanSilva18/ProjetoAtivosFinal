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
    public class AtivoController : Controller
    {
        private static AtivoControl ctlAtivo = new AtivoControl();

        public IActionResult Index()
        {
            return View();
        }
        private IHostingEnvironment _env;

        public AtivoController(IHostingEnvironment env)
        {
            _env = env;
        }

        private void Resize(Stream img, int novaLargura, string nomeArquivo, out string base64)
        {
            // Cria um objeto de imagem baseado no stream do arquivo enviado
            Bitmap originalBMP = new Bitmap(img);

            // Calcula a nova dimensao da imagem
            int origWidth = originalBMP.Width;
            int origHeight = originalBMP.Height;
            double sngRatio = (double)novaLargura / origWidth;
            int newWidth = novaLargura;
            int newHeight = Convert.ToInt32(origHeight * sngRatio);

            // Cria uma nova imagem a partir da imagem original
            Bitmap newBMP = new Bitmap(originalBMP, newWidth, newHeight);
            newBMP.SetResolution(1024, 1024);
            newBMP.Save(nomeArquivo, ImageFormat.Jpeg);

            // Converter imagem em base64 para gravar como string no banco de dados
            MemoryStream ms = new MemoryStream();
            newBMP.Save(ms, ImageFormat.Jpeg);
            byte[] imageBytes = ms.ToArray();
            base64 = Convert.ToBase64String(imageBytes);

            // Retira os objetos da memória
            originalBMP.Dispose();
            newBMP.Dispose();
        }

        [HttpPost]
        public JsonResult ReceberAnexo(IFormCollection form)
        {
            List<object> retorno = new List<object>();

            int id = 0;
            int.TryParse(form["id"], out id);
            string nome = form["nome"];

            bool apenasImagens = form.Keys.Contains("apenasImagens");

            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    var extensoesPermitidas = new[] { ".jpg", ".gif", ".png", ".jpeg", ".tiff", ".svg", ".jfif", ".pdf", ".doc", ".docx" };
                    if(apenasImagens)
                        extensoesPermitidas = new[] { ".jpg", ".gif", ".png", ".jpeg", ".tiff", ".svg", ".jfif"};
                    for (int i = 0; i < Request.Form.Files.Count; i++)
                    {
                        //Recepcionando cada arquivo
                        var arquivo = Request.Form.Files[i];
                        if (arquivo != null && arquivo.Length > 0 &&
                            arquivo.Length <= 100048576) //Maximo 1MB
                        {
                            string extensaoArquivo =
                                                Path.GetExtension(arquivo.FileName).ToLower();
                            if (extensoesPermitidas.Contains(extensaoArquivo))
                            {
                                var nomeArquivo = string.Format("{0}-{1}-{2}",
                                    id, i, arquivo.FileName);

                                var caminho = _env.WebRootPath + "\\img";
                                caminho = Path.Combine(caminho, nomeArquivo);

                                //Gravar o arquivo no servidor
                                //using (var stream = new FileStream(caminho, FileMode.Create))
                                //{
                                //    arquivo.CopyTo(stream);
                                //}

                                string base64 = "";
                                var img = new MemoryStream();
                                arquivo.CopyTo(img);
                                Request.Form.Files[i].CopyTo(img);
                                base64 = Convert.ToBase64String(img.ToArray());

                                if (apenasImagens)
                                    base64 = "data:" + Request.Form.Files[i].ContentType + ";base64," + base64;

                                //ctlimg.Gravar(0, base64, DateTime.now(), CodigoAtivo);    //grava no banco
                                retorno.Add(new { Id = i, Dados = base64, Nome = arquivo.FileName });
                            }
                            else
                                retorno.Add(new { Id = -2, Dados = "Formato inválido de arquivo." });
                        }
                        else
                            retorno.Add(new { Id = -1, Dados = "Tamanho inválido de arquivo." });
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.Add(new { Id = -10, Dados = ex.Message });
            }

            return Json(retorno);
        }

        [HttpPost]
        public JsonResult ReceberDados(IFormCollection form)
        {
            List<object> retorno = new List<object>();

            int id = 0;
            int.TryParse(form["id"], out id);
            string nome = form["nome"];

            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    var extensoesPermitidas = new[] { ".jpg", ".gif", ".png", ".jpeg", ".tiff", ".svg", ".jfif" };
                    for (int i = 0; i < Request.Form.Files.Count; i++)
                    {
                        //Recepcionando cada arquivo
                        var arquivo = Request.Form.Files[i];
                        if (arquivo != null && arquivo.Length > 0 &&
                            arquivo.Length <= 10048576) //Maximo 1MB
                        {
                            string extensaoArquivo =
                                                Path.GetExtension(arquivo.FileName).ToLower();
                            if (extensoesPermitidas.Contains(extensaoArquivo))
                            {
                                var nomeArquivo = string.Format("{0}-{1}-{2}",
                                    id, i, arquivo.FileName);

                                var caminho = _env.WebRootPath + "\\img";
                                caminho = Path.Combine(caminho, nomeArquivo);

                                //Gravar o arquivo no servidor
                                //using (var stream = new FileStream(caminho, FileMode.Create))
                                //{
                                //    arquivo.CopyTo(stream);
                                //}

                                string base64 = "";
                                var img = new MemoryStream();
                                arquivo.CopyTo(img);
                                Resize(img, 1024, caminho, out base64);
                                //ctlimg.Gravar(0, base64, DateTime.now(), CodigoAtivo);    //grava no banco
                                retorno.Add(new { Id = i, Dados = base64 });
                            }
                            else
                                retorno.Add(new { Id = -2, Dados = "Formato inválido de arquivo." });
                        }
                        else
                            retorno.Add(new { Id = -1, Dados = "Tamanho inválido de arquivo." });
                    }
                }
            }
            catch (Exception ex)
            {
                retorno.Add(new { Id = -10, Dados = ex.Message });
            }

            return Json(retorno);
        }
        public JsonResult Gravar(int Codigo, int Regional, int Filial, int Sala, int Placa, string Tag, string Estado, string Observacao, string Descricao, int TipoAtivo, string Marca, string NumeroSerie, string Modelo, double Valor, string Imagem, string Latitude, string Longitude, int CodigoNota, string NumeroNota, double ValorNota, DateTime DataEmissao, string Fornecedor, string Cnpj, string NomeAnexo, string Anexo)
        {            
            int Retorno = ctlAtivo.Gravar(Codigo, Regional, Filial, Sala, Placa, Tag, Estado, Observacao, Descricao, TipoAtivo, Marca, NumeroSerie, Modelo, Valor, Imagem, Latitude, Longitude, CodigoNota, NumeroNota, ValorNota, DataEmissao, Fornecedor, Cnpj,  NomeAnexo,  Anexo);
            if (Retorno == 1)
                return Json("");
            else
            {
                if (Retorno == 0)
                    return Json("Erro ao Gravar o Registro");
                else
                {
                    if(Retorno == -10)
                        return Json("Insira uma Imagem!");
                    else
                            return Json("Ativo ja Cadastrada com a Placa Informada");

                }
            }
        }

        public JsonResult GravarVeiculo(int Codigo, int Regional, int Filial, int Placa, string Tag, string Estado, string Observacao, string Descricao, int TipoAtivo, string Marca, string NumeroSerie, string Modelo, double Valor, string Imagem, string Latitude, string Longitude, int CodigoNota, string NumeroNota, double ValorNota, DateTime DataEmissao, string Fornecedor, string Cnpj, string NomeAnexo, string Anexo, string Cor, string PlacaVeiculo, string CRLV, string DUT, string FIPE)
        {
            int Retorno = ctlAtivo.Gravar(Codigo, Regional, Filial, Placa, Tag, Estado, Observacao, Descricao, TipoAtivo, Marca, NumeroSerie, Modelo, Valor, Imagem, Latitude, Longitude, CodigoNota, NumeroNota, ValorNota, DataEmissao, Fornecedor, Cnpj, NomeAnexo, Anexo, Cor, PlacaVeiculo, CRLV, DUT, FIPE);
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

        public JsonResult ObterAtivos(string Chave, string Filtro, int Ativo, int Regiao, int Filial)
        {

            List<object> Dados = ctlAtivo.ObterAtivos(Chave, Filtro, Ativo,  Regiao,  Filial);

            return Dados == null ? Json("") : Json(Dados);
        }
        [FiltroSession]
        public JsonResult ObterAtivosPlaca(int Placa)
        {
            List<object> Dados = ctlAtivo.ObterAtivosPlaca(Placa);

            return Dados == null ? Json("") : Json(Dados);
        }
        public JsonResult BuscarAtivos(int Local)
        {
            var Lista = ctlAtivo.BuscarAtivos(Local);
            List<object> Dados = new List<object>();

            if (Lista != null)
            {
                foreach (var L in Lista)
                {
                    Dados.Add(new
                    {
                        Codigo = L.GetCodigo(),
                        Descricao = L.GetDescricao(),
                        Placa = L.GetPlaca()
                    });
                }
            }

            return Dados == null ? Json("") : Json(Dados);
        }

        public JsonResult ExcluirLogico(int Codigo)
        {
            if (ctlAtivo.ExcluirLogico(Codigo))
                return Json("");
            else
                return Json("Não Foi Possivel Excluir o Ativo!");
        }

        public JsonResult Ativar(int Codigo)
        {
            if (ctlAtivo.Ativar(Codigo))
                return Json("");
            else
                return Json("Não Foi Possivel Ativar o Ativo!");
        }

        public JsonResult BuscarAtivo(int Codigo)
        {
            object Dado = new object();
            var L = ctlAtivo.BuscarAtivo(Codigo);
            object[] imagens = null;


            if (L != null)
            {
                if(L.Imagens != null)
                {
                    imagens = new object[L.Imagens.Count];

                    for (int i = 0; i < L.Imagens.Count; i++)
                    {
                        imagens[i] = (new { Foto = L.Imagens[i].GetFoto(), DataInsercao = L.Imagens[i].GetDataInsercao()});
                    }
                }
         

                Dado = (new
                {
                    Codigo = L.GetCodigo(),
                    Descricao = L.GetDescricao(),
                    Estado = L.GetEstado(),
                    Marca = L.GetMarca(),
                    Modelo = L.GetModelo(),
                    NumeroSerie = L.GetNumeroSerie(),
                    Observacao = L.GetObservacao(),
                    Placa = L.GetPlaca(),
                    SalaCodigo = L.GetSala().GetCodigo(),
                    TpAtivoCodigo = L.GetTipoAtivo().GetCodigo(),
                    Valor = L.GetValor(),
                    Tag = L.GetTag(),
                    StAtivo = L.GetStAtivo(),
                    NotaFiscal = L.GetNota(),
                    Imagens = imagens,
                    Anexo = L.GetAnexo()

                });
            }


            return Dado == null ? Json("") : Json(Dado);
        }
        public JsonResult BuscarLocalizacao(int Codigo)
        {
            object Dado = new object();
            var l = ctlAtivo.BuscarLocalizacao(Codigo);
            if (l != null)
            {
                Dado = (new
                {
                    Latitude = l.GetLatitude(),
                    Longitude = l.GetLongitude()

                });
            }

            return Dado == null ? Json("") : Json(Dado);
        }
        public JsonResult ObterValores(int Regional, int Filial)
        {
            var Lista = ctlAtivo.ObterValores(Regional, Filial);
           
            return Lista == null ? Json("") : Json(Lista);
        }
        public JsonResult ObterRelatorioImagem(int Regional, int Filial)
        {
            var Lista = ctlAtivo.ObterRelatorioImagem(Regional, Filial);

            return Lista == null ? Json("") : Json(Lista);
        }

        public JsonResult ObterImagens(int Codigo)
        {
            var Lista = ctlAtivo.ObterImagens(Codigo);

            return Lista == null ? Json("") : Json(Lista);
        }

        public FileResult BaixarAnexo(int id)
        {
            var a = ctlAtivo.BuscaAnexo(id);
            byte[] fileBytes = System.IO.File.ReadAllBytes(a.Local);
            string fileName = a.Nome;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }

}