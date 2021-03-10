using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoAtivos.Control;
using ProjetoAtivos.Filter;
using ProjetoAtivos.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjetoAtivos.Controllers
{
    [FiltroBanco]
    [FiltroSession]
    public class TransferenciaController : Controller
    {
        private static TransferenciaControl ctlTransferencia = new TransferenciaControl();
        private IHostingEnvironment _env;

        public TransferenciaController(IHostingEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        /*public JsonResult Gravar(int Codigo, string Observacao, string Status, DateTime dtAbertura, DateTime dtFechamento, int Motivo, int FilialOrigem, int FilialDestino, int Ativo)
        {
            if (ctlTransferencia.Gravar(Codigo, Observacao, Status, dtAbertura, dtFechamento, Motivo, FilialOrigem, FilialDestino, Ativo))
                return Json("");
            else
                return Json("Erro ao Gravar o Registro");
        }*/

        public JsonResult ObterTransferencias(int Origem, int Destino, int Ativo, int Regiao, int Filial)
        {

            List<object> Dados = ctlTransferencia.ObterTransferencias(Origem, Destino, Ativo, Regiao, Filial);

            return Dados == null ? Json("") : Json(Dados);
        }

        public JsonResult BuscarTransferencia(int Codigo)
        {
            
            object Dado = new object();
            var L = ctlTransferencia.BuscarTransferencia(Codigo);
            if (L != null)
            {
                var ativos = new AtivoControl().ObterAtivosTransf(Codigo);
                var docs = ctlTransferencia.BuscaDocs(Codigo);
                Dado = (new
                {
                    Codigo = L.GetCodigo(),
                    Observaocao = L.GetObservacao(),
                    Status = L.GetStatus(),
                    dtAbertura = L.GetdtAbertura(),
                    dtFechamento = L.GetdtFechamento(),
                    Motivo = L.GetMotivo().GetCodigo(),
                    FilialOrigem = L.GetFilialOrigem().GetRazao(),
                    FilialDestino = L.GetFilialDestino().GetRazao(),
                    CodigoFilialDestino = L.GetFilialDestino().GetCodigo(),
                    RespOrigem = L.GetFilialOrigem().GetResponsavel().GetNome(),
                    RespDestino = L.GetFilialDestino().GetResponsavel().GetNome(),
                    AprovDestino = L.AprovacaoDestino != null ? L.AprovacaoDestino.Codigo : 0,
                    AprovGerente = L.AprovacaoGerente != null ? L.AprovacaoGerente.Codigo : 0,
                    AprovanteDestino = L.AprovacaoDestino != null ? L.AprovacaoDestino.Responsável.GetNome() : "",
                    AprovanteGerente = L.AprovacaoGerente != null ? L.AprovacaoGerente.Responsável.GetNome() : "",
                    DtAprovDestino = L.AprovacaoDestino != null ? L.AprovacaoDestino.DataInsercao.ToShortDateString() : "",
                    DtAprovGerente = L.AprovacaoGerente != null ? L.AprovacaoGerente.DataInsercao.ToShortDateString() : "",
                    ObsAprovDestino = L.AprovacaoDestino != null ? L.AprovacaoDestino.Observacao : "",
                    ObsAprovGerente = L.AprovacaoGerente != null ? L.AprovacaoGerente.Observacao : "",
                    Ativos = ativos,
                    Docs = docs,
                    obsrecusa = L.GetObsRecusa(),
                    pessoarecusa = L.PessoaRecusa != null ? L.PessoaRecusa.GetNome() + " - " + L.PessoaRecusa.GetCargo() : null
                }); 
            }


            return Dado == null ? Json("") : Json(Dado);
        }

        [HttpPost]
        public JsonResult ReceberDados(IFormCollection form)
        {
            List<object> retorno = new List<object>();

            int id = 0;
            int.TryParse(form["id"], out id);
            // string nome = form["nome"];

            try
            {
                if (Request.Form.Files.Count > 0)
                {
                    var extensoesPermitidas = new[] { ".doc", ".docx", ".txt", ".pdf" };
                    for (int i = 0; i < Request.Form.Files.Count; i++)
                    {
                        //Recepcionando cada arquivo
                        var arquivo = Request.Form.Files[i];
                        if (arquivo != null && arquivo.Length > 0 &&
                            arquivo.Length <= 8048576) //Maximo 1MB
                        {
                            string extensaoArquivo =
                                                Path.GetExtension(arquivo.FileName).ToLower();
                            if (extensoesPermitidas.Contains(extensaoArquivo))
                            {
                                var nomeArquivo = string.Format("{0}-{1}-{2}",
                                    id, i, arquivo.FileName);

                                var caminho = _env.WebRootPath + "\\Docs";
                                caminho = Path.Combine(caminho, nomeArquivo);

                                //Gravar o arquivo no servidor
                                //using (var stream = new FileStream(caminho, FileMode.Create))
                                //{
                                //    arquivo.CopyTo(stream);
                                //}

                                string base64 = "";
                                var img = new MemoryStream();
                                arquivo.CopyTo(img);
                                base64 = Convert.ToBase64String(img.ToArray());

                                
                                //ctlimg.Gravar(0, base64, DateTime.now(), CodigoAtivo);    //grava no banco
                                retorno.Add(new { Id = i, Dados = base64, Content = arquivo.ContentType, Nome = arquivo.FileName, Extensao = extensaoArquivo, Tamanho = arquivo.Length });
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
        public JsonResult Gravar(int Origem, int Destino, int Motivo, string Descricao, string[] Docs, string[] Nome, string[] Content, int[] Ativos, string[] Imgs)
        {
            if (ctlTransferencia.Gravar(Origem, Destino, Motivo, Descricao, Docs, Nome, Content, Ativos, Imgs, _env.WebRootPath + "\\Docs"))
            {
                return Json("");
            }
            else
                return Json("Erro");

            //_env.WebRootPath + "\\Docs"
        }

        [HttpPost]
        public JsonResult Aprovar(int Transf, string Obs)
        {
            
            int pessoa = Convert.ToInt32(Request.Cookies["Pessoa"]);

            if (ctlTransferencia.Aprovar(Transf, Obs, pessoa))
            {
                return Json("");
            }
            else
                return Json("Erro");
        }

        [HttpPost]
        public JsonResult Recusar(int Transf, string Obs)
        {

            int pessoa = Convert.ToInt32(Request.Cookies["Pessoa"]);

            if (ctlTransferencia.Recusar(Transf, Obs, pessoa))
            {
                return Json("");
            }
            else
                return Json("Erro");
        }

        public FileResult BaixarDocumento(int Codigo)
        {
            var doc = ctlTransferencia.BuscaDoc(Codigo);
            byte[] fileBytes = System.IO.File.ReadAllBytes(doc.Caminho);
            string fileName = doc.Nome;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [HttpPost]
        public JsonResult Receber(int Codigo, string Obs, int[] Ativos, string[] Imgs, string Latitude, string Longitude, int[] Salas)
        {

            int pessoa = Convert.ToInt32(Request.Cookies["Pessoa"]);

            if (ctlTransferencia.Receber(Codigo, Obs, Ativos, Imgs, pessoa, Latitude, Longitude, Salas))
            {
                return Json("");
            }
            else
                return Json("Erro");

            //_env.WebRootPath + "\\Docs"
        }
    }
}