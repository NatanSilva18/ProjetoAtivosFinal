
using ProjetoAtivos.DAO;
using Survey.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ProjetoAtivos.Models
{
    public class Transferencia
    {
        private int Codigo;
        private string Observacao;

        private DateTime DtAbertura;
        private DateTime DtFechamento;
        private Motivo Motivo;
        private Filial FilialOrigem;
        private Filial FilialDestino;
        private bool Status;

        public List<Ativo> Ativos { get; set; }
        public List<Documento> Documentos { get; set; }
        public Aprovacao AprovacaoGerente { get; set; }
        public Aprovacao AprovacaoDestino { get; set; }
        public Transferencia()
        {
            Status = true;
            Documentos = new List<Documento>();
            this.Codigo = 0;
            this.Observacao = "";
            this.Motivo = new Motivo();
            this.FilialOrigem = new Filial();
            this.FilialDestino = new Filial();
            this.DtAbertura = DateTime.Now;
            this.DtFechamento = DateTime.MinValue;
        }
        public Transferencia(int Codigo)
        {
            Status = true;
            Documentos = new List<Documento>();
            this.Codigo = Codigo;
            this.Observacao = "";
            this.Motivo = new Motivo();
            this.FilialOrigem = new Filial();
            this.FilialDestino = new Filial();
            this.DtAbertura = DateTime.Now;
            this.DtFechamento = DateTime.MinValue;
        }
        public Transferencia(int Codigo, string Observacao, String Status, DateTime dtAbertura, DateTime dtFechamento, int Motivo, int FilialOrigem, int FilialDestino, int Ativo)
        {
            this.Status = true;
            Documentos = new List<Documento>();
            this.Codigo = Codigo;
            this.Observacao = Observacao;
            this.DtAbertura = dtAbertura;
            this.DtFechamento = dtFechamento;
            this.Motivo = new Motivo(Motivo);
            this.FilialOrigem = new Filial(FilialOrigem);
            this.FilialDestino = new Filial(FilialDestino);
        }
        public Transferencia(int Codigo, string Observacao, bool Status, DateTime dtAbertura, DateTime dtFechamento, int MotivoCod, string MotDescricao, Boolean MotAtivo, int FilialOrigem, string RazaoOrigem, Boolean FilAtivoOrigem, int FilialDestino, string RazaoDestino, Boolean FilAtivoDestino, int Ativo, string DescricaoAtivo, Boolean StAtivoAtivo)
        {
            Documentos = new List<Documento>();
            this.Codigo = Codigo;
            this.Observacao = Observacao;
            this.Status = Status;
            this.DtAbertura = dtAbertura;
            this.DtFechamento = dtFechamento;
            this.Motivo = new Motivo(MotivoCod); this.Motivo.SetDescricao(MotDescricao); this.Motivo.SetStAtivo(MotAtivo);
            this.FilialOrigem = new Filial(FilialOrigem); this.FilialOrigem.SetRazao(RazaoOrigem); this.FilialOrigem.SetStAtivo(FilAtivoOrigem);
            this.FilialDestino = new Filial(FilialDestino); this.FilialDestino.SetRazao(RazaoDestino); this.FilialDestino.SetStAtivo(FilAtivoDestino);

        }

        public int GetCodigo()
        {
            return this.Codigo;
        }
        public void SetCodigo(int Codigo)
        {
            this.Codigo = Codigo;
        }
        public string GetObservacao()
        {
            return this.Observacao;
        }
        public void SetObservacao(string Observacao)
        {
            this.Observacao = Observacao;
        }

        public bool GetStatus()
        {
            return this.Status;
        }
        public void SetStatus(bool status)
        {
            this.Status = status;
        }
        public Motivo GetMotivo()
        {
            return this.Motivo;
        }
        public void SetMotivo(Motivo Motivo)
        {
            this.Motivo = Motivo;
        }
        public DateTime GetdtAbertura()
        {
            return this.DtAbertura;
        }
        public void SetdtAbertura(DateTime dtAbertura)
        {
            this.DtAbertura = dtAbertura;
        }
        public DateTime GetdtFechamento()
        {
            return this.DtFechamento;
        }
        public void SetdtFechamento(DateTime dtFechamento)
        {
            this.DtFechamento = dtFechamento;
        }
        public Filial GetFilialOrigem()
        {
            return this.FilialOrigem;
        }
        public void SetFilialOrigem(Filial FilialOrigem)
        {
            this.FilialOrigem = FilialOrigem;
        }
        public Filial GetFilialDestino()
        {
            return this.FilialDestino;
        }
        public void SetFilialDestino(Filial FilialDestino)
        {
            this.FilialDestino = FilialDestino;
        }
        public List<Ativo> GetAtivos()
        {
            return this.Ativos;
        }
        public void SetAtivos(List<Ativo> Ativos)
        {
            this.Ativos = Ativos;
        }

        private string MontaCorpo(Transferencia t, bool aprovacao)
        {
            string body = @"<div class='modal-body'>
    <div class='container-fluid'>

        <div class='tab-pane' id='nav-confirmarAprov' role='tabpanel' aria-labelledby='nav-confirmar-tab'>
            <div class='form-group' id='confirmarCamposAprov'>        
                <h2> Origem</h2>            
                <div class='row'>                
                    <div class='col-lg-6'>                    
                        <div class='form-group'>                        
                            <label>Filial de Origem:<b>" + t.GetFilialOrigem().GetRazao() + @"</b></label>                    
                        </div>                
                    </div>                
                    <div class='col-lg-6'>                    
                        <div class='form-group'>                        
                            <label>Responsavel Origem:<b>" + t.GetFilialOrigem().GetResponsavel().GetNome() + @"</b></label>                    
                        </div>                
                    </div>            
                </div>            
                <hr>            
                <h2>Destino</h2>            
                <div class='row'>                
                    <div class='col-lg-6'>                    
                        <div class='form-group'>                        
                            <label>Filial de Destino:<b>" + t.GetFilialDestino().GetRazao() + @"</b></label>                    
                        </div>                
                    </div>                
                    <div class='col-lg-6'>                    
                        <div class='form-group'>                        
                            <label>Responsavel Destino:<b>" + t.GetFilialDestino().GetResponsavel().GetNome() + @"</b></label>                    
                        </div>                
                    </div>            
                </div>
            </div>
            <div id='tbbConfirmarItensAprov' style='' class='card mb-4'>
                <div class='card-header'>
                    <i class='fa fa-table'></i>
                    <span><b>Ativos Transferencia</b></span>
                </div>
                <div class='card-body'>
                    <div class='table-responsive-xl table-responsive-sm table-responsive-md'>
                        <table id='tableConfirmarItensAprov' class='table table-sm table-hover' style='width:100%'>
                            <thead>                             
                                <tr class='thead-light'>                                              
                                    <th scope='col'>Placa</th>                                
                                    <th scope='col'>Descrição</th>                                
                                    <th scope='col'>Estado</th>                                   
                                </tr>                                
                            </thead>                                
                            <tbody>";
            for (int i = 0; i < t.Ativos.Count; i++)
            {
                body += @"
                                <tr id = 'prov1' class='galeria'>
                                    
                                    <td align='center'>
                                        " + t.Ativos[i].GetPlaca() + @"
                                    </td>
                                    <td align='center'>
                                        " + t.Ativos[i].GetDescricao() + @"
                                    </td>
                                    <td align='center'>
                                        " + t.Ativos[i].GetEstado() + @"
                                    </td>
                                </tr>";
            }

            body += @"</tbody>
                        </table>
                    </div>
                </div>
            </div>";


            if (aprovacao)
            {
                body += @"
            <div id = 'InfoAprovGerente'>
 
                 <hr>
 
                 <h2> Aprovação Gerente </h2>
   
                   <div class='row'>                        
                    <div class='col-lg-6'>                                
                        <div class='form-group'>                                    
                            <label>Data Aprovação:<b>" + t.AprovacaoGerente.DataInsercao.ToShortDateString() + @"</b></label>                                
                        </div>                            
                    </div>                            
                    <div class='col-lg-6'>                                
                        <div class='form-group'>                                    
                            <label>Gerente Responsavel:<b>" + t.GetFilialOrigem().GetRegional().GetPessoa().GetNome() + @"</b></label>                                
                        </div>                        
                    </div>                  
                </div>                
                <div class='row'>                        
                    <div class='col-lg-6'>                                
                        <div class='form-group'>                                    
                            <label>Observação da Aprovação:<b> " + t.AprovacaoGerente.Observacao + @"</b></label>                                
                        </div>                            
                    </div>                
                </div>  
            </div>";
            }

            body += @"
        </div>
    </div>
</div>";

            return body;
        }

        public Boolean Gravar()
        {
            if (this.DtAbertura != null)
            {
                bool ok = new TransferenciaDAO().Gravar(this);
                if (ok)
                {
                    this.FilialOrigem = new FilialDAO().BuscarFilialEmail(this.FilialOrigem.GetCodigo());
                    this.FilialDestino = new FilialDAO().BuscarFilialEmail(this.FilialDestino.GetCodigo());

                    string dest = FilialOrigem.GetRegional().GetPessoa().GetEmail();
                    string dest2 = FilialDestino.GetRegional().GetPessoa().GetEmail();


                    var result = EnviarEmail("m2ntech.20@gmail.com", "ParebemSystem", dest, "PareBem Aprovação de Ativos", "Olá, Existe uma Aprovação de Ativos Pendente. <br> Filial de Origem: " + this.FilialOrigem.GetRazao() + " <br>Filial Destino:" + this.FilialDestino.GetRazao() + "<br> Por favor Faça o login. Para Aprovação.... <a href='http://www.m2nsolutions.com.br'>ParebemSystem</a>");

                    var result2 = EnviarEmail("m2ntech.20@gmail.com", "ParebemSystem", dest, "PareBem Aprovação de Ativos", "Olá, Existe uma Aprovação de Ativos Pendente. <br> Filial de Origem: " + this.FilialOrigem.GetRazao() + " <br>Filial Destino:" + this.FilialDestino.GetRazao() + "<br> Por favor Faça o login. Para Aprovação.... <a href='http://www.m2nsolutions.com.br'>ParebemSystem</a>");
                }

                return ok;
            }
            else
                return false;
        }

        public Transferencia BuscarTransferencia(int Codigo)
        {
            if (Codigo > 0)
                return new TransferenciaDAO().BuscarTransferencia(Codigo);
            else
                return null;
        }

        public List<object> ObterTransferencias(int Origem, int Destino, int Ativo, int Regiao, int Filial)
        {
            return new TransferenciaDAO().ObterTransferencias(Origem, Destino, Ativo, Regiao, Filial);
        }

        public bool Aprovar(int Transf, string Obs, int Pessoa)
        {
            AprovacaoGerente = new Aprovacao()
            {
                DataInsercao = DateTime.Now,
                Observacao = Obs,
                Responsável = new Pessoa(Pessoa),
                Status = true
            };

            this.Codigo = Transf;

            bool ok = new TransferenciaDAO().Aprovar(this);

            if (ok)
            {
                Transferencia t = BuscarTransferencia(Transf);
                t.AprovacaoGerente = AprovacaoGerente;
                t.Ativos = new AtivoDAO().BuscarAtivos(t);

                t.FilialOrigem = new FilialDAO().BuscarFilialEmail(t.FilialOrigem.GetCodigo());
                t.FilialDestino = new FilialDAO().BuscarFilialEmail(t.FilialDestino.GetCodigo());

                string dest = t.FilialOrigem.GetRegional().GetPessoa().GetEmail();
                string dest2 = t.FilialDestino.GetRegional().GetPessoa().GetEmail();

                var result = EnviarEmail("m2ntech.20@gmail.com", "ParebemSystem", dest, "Pare bem Ativos - Aprovação Origem", "Ativo Aprovado com Sucesso, Seguirá para se aprovado no Destino...");
                var result2 = EnviarEmail("m2ntech.20@gmail.com", "ParebemSystem", dest, "Pare bem Ativos - Aprovação Origem", "Ativo Aprovado com Sucesso, Seguirá para se aprovado no Destino...");
            }

            return ok;
        }
        internal bool Receber(Localizacao loc)
        {
            if (new TransferenciaDAO().Receber(this, loc))
            {
                Transferencia Transf = new Transferencia().BuscarTransferencia(this.Codigo);
                this.FilialDestino = Transf.GetFilialDestino();
                this.FilialOrigem = Transf.GetFilialOrigem();


                this.FilialOrigem = new FilialDAO().BuscarFilialEmail(this.FilialOrigem.GetCodigo());
                this.FilialDestino = new FilialDAO().BuscarFilialEmail(this.FilialDestino.GetCodigo());

                string dest = FilialOrigem.GetRegional().GetPessoa().GetEmail();
                string dest2 = FilialDestino.GetRegional().GetPessoa().GetEmail();


                var result = EnviarEmail("m2ntech.20@gmail.com", "ParebemSystem", dest, "PareBem Aprovação de Ativos", "Transferenica Aprovada com Sucesso... O Ativo ja se encontrada no local de destino!");

                var result2 = EnviarEmail("m2ntech.20@gmail.com", "ParebemSystem", dest2, "Pare Bem Aprovação de Ativos", "Transferenica Aprovada com Sucesso... O Ativo ja se encontrada no local de destino!");

                return true;
            }
            else
                return false;
        }
        internal void EnviarNotificação()
        {
            string corpo = @"";
        }
        public string EnviarEmail(string emailFrom, string nomeFrom, string emailSend, string assunto, string texto)
        {
            //Gerando o objeto da mensagem
            MailMessage msg = new MailMessage();
            //Remetente
            msg.From = new MailAddress(emailFrom, nomeFrom);
            //Destinatários
            msg.To.Add(emailSend);
            //Assunto
            msg.Subject = assunto;
            //Texto a ser enviado
            msg.Body = texto;
            msg.IsBodyHtml = true;

            //Gerando o objeto para envio da mensagem (Exemplo pelo Gmail)
            SmtpClient client = new SmtpClient();
            client.EnableSsl = true;
            client.UseDefaultCredentials = true;
            client.Credentials = new NetworkCredential("m2ntech.20@gmail.com", "NAtAN38S23@**");
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                client.Send(msg);
                return "";
            }
            catch (Exception ex)
            {
                return "Falha: " + ex.Message;
            }
            finally
            {
                msg.Dispose();
            }
        }
        public object StatusTransferencias()
        {
            return new TransferenciaDAO().StatusTransferencias();
        }

    }
}
