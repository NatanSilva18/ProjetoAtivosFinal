using ProjetoAtivos.Models;
using Survey.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAtivos.DAO
{
    public class TransferenciaDAO
    {
        private Banco b;

        internal TransferenciaDAO()       //pegando instancia do objeto banco
        {
            b = Banco.GetInstance();
        }
        internal List<Transferencia> TableToList(DataTable dt)
        {
            List<Transferencia> dados = null;
            if (dt != null && dt.Rows.Count > 0)
                dados = (from DataRow row in dt.Rows
                         select new Transferencia(
                                          Convert.ToInt32(row["transf_codigo"]),
                                          row["transf_observacao"].ToString(),
                                          Convert.ToBoolean(row["transf_stativo"]),
                                          Convert.ToDateTime(row["transf_dtabertura"]),
                                          Convert.ToDateTime(row["transf_dtfechamento"]),
                                          Convert.ToInt32(row["mot_codigo"]),
                                          row["mot_descricao"].ToString(),
                                          Convert.ToBoolean(row["mot_stativo"]),
                                          Convert.ToInt32(row["fil_codigo"]),
                                          row["fil_descricao"].ToString(),
                                          Convert.ToBoolean(row["fil_stativo"]),
                                          Convert.ToInt32(row["fil_codigo"]),
                                          row["fil_descricao"].ToString(),
                                          Convert.ToBoolean(row["fil_stativo"]),
                                          Convert.ToInt32(row["ati_codigo"]),
                                          row["ati_descricao"].ToString(),
                                          Convert.ToBoolean(row["ati_stativo"])
                         )).ToList();

            return dados;
        }

        internal Transferencia TableToObj(DataTable dt)
        {
            Transferencia dados = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                dados = new Transferencia(
                                          Convert.ToInt32(dt.Rows[0]["transf_codigo"]),
                                          dt.Rows[0]["transf_observacao"].ToString(),
                                          Convert.ToBoolean(dt.Rows[0]["transf_stativo"]),
                                          Convert.ToDateTime(dt.Rows[0]["transf_dtabertura"]),
                                          DateTime.MinValue,
                                          Convert.ToInt32(dt.Rows[0]["mot_codigo"]),
                                          dt.Rows[0]["mot_descricao"].ToString(),
                                          Convert.ToBoolean(dt.Rows[0]["mot_stativo"]),
                                          Convert.ToInt32(dt.Rows[0]["fil_codigo"]),
                                          dt.Rows[0]["fil_razao"].ToString(),
                                          Convert.ToBoolean(dt.Rows[0]["fil_stativo"]),
                                          Convert.ToInt32(dt.Rows[0]["fil_codigoD"]),
                                          dt.Rows[0]["fil_razaoD"].ToString(),
                                          Convert.ToBoolean(dt.Rows[0]["fil_stativo"]),
                                          0, "", true);


                dados.AprovacaoDestino = dt.Rows[0]["aprdes_codigo"] != DBNull.Value ? new Aprovacao() { Codigo = Convert.ToInt32(dt.Rows[0]["aprdes_codigo"]) }:null;
                dados.AprovacaoGerente = dt.Rows[0]["apr_codigo"] != DBNull.Value ? new Aprovacao() { Codigo = Convert.ToInt32(dt.Rows[0]["apr_codigo"]) } : null;
                dados.GetFilialDestino().SetResponsavel(new Pessoa(0,dt.Rows[0]["RespDestino"].ToString()));
                dados.GetFilialOrigem().SetResponsavel(new Pessoa(0, dt.Rows[0]["RespOrigem"].ToString()));

                if (dados.AprovacaoDestino != null)
                {
                    dados.AprovacaoDestino.Responsável = new Pessoa(0, dt.Rows[0]["AprovanteDest"].ToString());
                    dados.AprovacaoDestino.Observacao = dt.Rows[0]["obsAprovDestino"].ToString();
                    dados.AprovacaoDestino.DataInsercao = Convert.ToDateTime(dt.Rows[0]["dtAprovDestino"]);
                }

                if (dados.AprovacaoGerente != null)
                {
                    dados.AprovacaoGerente.Responsável = new Pessoa(0, dt.Rows[0]["GerenteAprov"].ToString());
                    dados.AprovacaoGerente.Observacao = dt.Rows[0]["obsAprovGerente"].ToString();
                    dados.AprovacaoGerente.DataInsercao = Convert.ToDateTime(dt.Rows[0]["dtAprovGerente"]);
                }
            }

            return dados;
        }

       
        internal List<object> TableToListCompleta(DataTable dt)
        {
            List<object> Dados = new List<object>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Dados.Add(new
                    {
                        Codigo = Convert.ToInt32(dt.Rows[i]["transf_codigo"]),
                        Observacao = dt.Rows[i]["transf_observacao"].ToString(),
                        Status = Convert.ToBoolean(dt.Rows[i]["transf_stativo"]),
                        dtAbertura = Convert.ToDateTime(dt.Rows[i]["transf_dtabertura"]),
                        dtFechamento = dt.Rows[i]["transf_dtfechamento"].ToString(),
                        Motivo = dt.Rows[i]["mot_codigo"].ToString(),
                        MotivoDesc= dt.Rows[i]["mot_Descricao"].ToString(),
                        FilialOrigem = dt.Rows[i]["fil_codigo"].ToString(),
                        FilialDestino = dt.Rows[i]["fil_codigo"].ToString(),
                        RazaoOrigem = dt.Rows[i]["origem"].ToString(),
                        RazaoDestino = dt.Rows[i]["destino"].ToString(),
                        dtRecebimento = dt.Rows[i]["aprovacaoDestino"].ToString(),
                        dtAprovacao = dt.Rows[i]["aprovacaoGerente"].ToString(),
                    });
                }
            }

            return Dados == null ? null : Dados;
        }
        internal Boolean Gravar(Transferencia transferencia)
        {
            b.getComandoSQL().Parameters.Clear();

            if (transferencia.GetCodigo() == 0)
            {
                b.getComandoSQL().CommandText = @"insert into tranferencia (transf_observacao, transf_stativo, transf_dtabertura, transf_dtfechamento, mot_codigo, fil_codigo, fil_codigo_destino) values(@observacao, @status, @dtabertura, @dtfechamento, @motivo, @filial, @filialDestino);
                SELECT LAST_INSERT_ID();";

            }
            b.getComandoSQL().Parameters.AddWithValue("@observacao", transferencia.GetObservacao());
            b.getComandoSQL().Parameters.AddWithValue("@status", transferencia.GetStatus());
            b.getComandoSQL().Parameters.AddWithValue("@dtabertura", transferencia.GetdtAbertura());            
            b.getComandoSQL().Parameters.AddWithValue("@motivo", transferencia.GetMotivo().GetCodigo());
            b.getComandoSQL().Parameters.AddWithValue("@filialDestino", transferencia.GetFilialDestino().GetCodigo());
            b.getComandoSQL().Parameters.AddWithValue("@filial", transferencia.GetFilialOrigem().GetCodigo());

            if(transferencia.GetdtFechamento() == DateTime.MinValue)
                b.getComandoSQL().Parameters.AddWithValue("@dtfechamento", DBNull.Value);
            else
                b.getComandoSQL().Parameters.AddWithValue("@dtfechamento", transferencia.GetdtFechamento());

            bool ok = true;
            int codT = 0;
            ok = b.ExecutaComando(true, out codT) == 1;

            if(ok)
            {
                for (int i = 0; ok && i < transferencia.Ativos.Count; i++)
                {
                    ok = transferencia.Ativos[i].Imagens[0].Gravar(transferencia.GetCodigo());

                    if (ok)
                        ok = GravarItem(transferencia.Ativos[i].Imagens[0], codT);
                }

                for (int i = 0;ok && i < transferencia.Documentos.Count; i++)
                {
                    ok = transferencia.Documentos[i].Gravar(codT);

                    if (ok)
                        ok = transferencia.Documentos[i].GravarArquivo();
                }

                if (!ok)
                    Documento.ExcluirArquivos(transferencia.Documentos);

                b.FinalizaTransacao(ok);
            }
            return ok;
        }


        internal bool GravarItem(Imagem imagem, int transf)
        {
            
            Boolean OK = false;

            b.getComandoSQL().Parameters.Clear();

                b.getComandoSQL().CommandText = @"
INSERT INTO itens_ativos
           (transf_codigo
           ,ati_codigo
           ,img_codigo)
     VALUES
           (@transf
           ,@ativo
           ,@img);


";
            
            b.getComandoSQL().Parameters.AddWithValue("@img", imagem.GetCodigo());
            b.getComandoSQL().Parameters.AddWithValue("@ativo", imagem.GetAtivo().GetCodigo());
            b.getComandoSQL().Parameters.AddWithValue("@transf", transf);

            OK = b.ExecutaComando(true) == 1;


            return OK;
        }

        public List<object> ObterTransferencias(int Origem, int Destino, int Ativo, int Regiao, int Filial)
        {
            b.getComandoSQL().Parameters.Clear();

            if(Origem != 0)
            {
                b.getComandoSQL().CommandText = @"select t.transf_codigo, t.transf_observacao, t.transf_stativo, t.transf_dtabertura, ifnull(t.transf_dtfechamento, ' ') as transf_dtfechamento, 
                                              m.mot_codigo, m.mot_descricao, fo.fil_codigo, fd.fil_codigo,  fo.fil_razao as origem, fd.fil_razao as destino
                                              , ifnull(aprdes_dtinsercao,' ') as aprovacaoDestino, ifnull(apr_dtinsercao,' ') as aprovacaoGerente
                                              from tranferencia t
                                              inner join Motivo m on m.mot_codigo = t.mot_codigo
                                              inner join Filial fo on fo.fil_codigo = t.fil_codigo
                                              inner join Filial fd on fd.fil_codigo = t.fil_codigo_destino
                                              left outer join Aprovacao_Destino ad on ad.aprdes_codigo = t.aprdes_codigo
                                              left outer join Aprovacao_Gerente ag on ag.apr_codigo = t.apr_codigo
                                              where t.fil_codigo = @origem
                                              order by t.transf_dtabertura desc;";
                b.getComandoSQL().Parameters.AddWithValue("@origem", Origem);


                if (Destino != 0)
                {
                   //mexer aqui
                }

            }
            else
            {

                if (Destino != 0)
                {
                    b.getComandoSQL().CommandText = @"select t.transf_codigo, t.transf_observacao, t.transf_stativo, t.transf_dtabertura, ifnull(t.transf_dtfechamento, ' ') as transf_dtfechamento, 
                                              m.mot_codigo, m.mot_descricao, fo.fil_codigo, fd.fil_codigo,  fo.fil_razao as origem, fd.fil_razao as destino
                                              , ifnull(aprdes_dtinsercao,' ') as aprovacaoDestino, ifnull(apr_dtinsercao,' ') as aprovacaoGerente
                                              from tranferencia t
                                              inner join Motivo m on m.mot_codigo = t.mot_codigo
                                              inner join Filial fo on fo.fil_codigo = t.fil_codigo
                                              inner join Filial fd on fd.fil_codigo = t.fil_codigo_destino
                                              left outer join Aprovacao_Destino ad on ad.aprdes_codigo = t.aprdes_codigo
                                              left outer join Aprovacao_Gerente ag on ag.apr_codigo = t.apr_codigo
                                              where t.fil_codigo_destino = @destino
                                              order by t.transf_dtabertura desc;";
                    b.getComandoSQL().Parameters.AddWithValue("@destino", Destino);
                }
                else
                {
                    b.getComandoSQL().CommandText = @"select t.transf_codigo, t.transf_observacao, t.transf_stativo, t.transf_dtabertura, ifnull(t.transf_dtfechamento, ' ') as transf_dtfechamento, 
                                              m.mot_codigo, m.mot_descricao, fo.fil_codigo, fd.fil_codigo,  fo.fil_razao as origem, fd.fil_razao as destino
                                              , ifnull(aprdes_dtinsercao,' ') as aprovacaoDestino, ifnull(apr_dtinsercao,' ') as aprovacaoGerente
                                              from tranferencia t
                                              inner join Motivo m on m.mot_codigo = t.mot_codigo
                                              inner join Filial fo on fo.fil_codigo = t.fil_codigo
                                              inner join Filial fd on fd.fil_codigo = t.fil_codigo_destino
                                              left outer join Aprovacao_Destino ad on ad.aprdes_codigo = t.aprdes_codigo
                                              left outer join Aprovacao_Gerente ag on ag.apr_codigo = t.apr_codigo
                                              order by t.transf_dtabertura desc;";
                }

            }

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToListCompleta(dt);
            else
                return null;
        }


        internal Transferencia BuscarTransferencia(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select t.transf_codigo, t.transf_observacao, t.transf_stativo, t.transf_dtabertura, t.transf_dtfechamento, m.mot_codigo, m.mot_descricao, m.mot_stativo, f.fil_codigo, f.fil_razao, f.fil_stativo, 
                                                ff.fil_codigo as fil_codigoD, ff.fil_razao as fil_razaoD, ff.fil_stativo as fil_stativoD, p.pes_nome as RespOrigem , pp.pes_nome as RespDestino
                                              , t.aprdes_codigo, t.apr_codigo, pad.pes_nome as AprovanteDest, pag.pes_nome as GerenteAprov, ad.aprdes_dtinsercao as dtAprovDestino, ag.apr_dtinsercao as dtAprovGerente
                                              , ifnull(ad.aprdes_observacao,'') as obsAprovDestino, ifnull(ag.apr_observacao,'') as obsAprovGerente
                                              from tranferencia t
                                              inner join Motivo m on m.mot_codigo = t.mot_codigo
                                              inner join Filial f on f.fil_codigo = t.fil_codigo
                                              inner join Filial ff on ff.fil_codigo = t.fil_codigo_destino
                                              inner join Pessoa p on p.pes_codigo = f.pes_codigo
                                              inner join Pessoa pp on pp.pes_codigo = ff.pes_codigo
                                              left outer join Aprovacao_Destino ad on ad.aprdes_codigo = t.aprdes_codigo
                                              left outer join Aprovacao_Gerente ag on ag.apr_codigo = t.apr_codigo
                                              left outer join Pessoa pad on pad.pes_codigo = ad.pes_codigo
                                              left outer join Pessoa pag on pag.pes_codigo = ag.pes_codigo
                                              where t.transf_codigo = @codigo;";

            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
            {
                return TableToObj(dt);


            }
            else
                return null;
        }

        internal bool Aprovar(Transferencia Transf)
        {
            Boolean OK = false;

            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"
                                                INSERT INTO Aprovacao_Gerente
                                                           (apr_observacao, apr_dtinsercao, apr_stativo, pes_codigo)
                                                     VALUES
                                                           (@obs, @data,@ativo, @pessoa);
                                                            SELECT LAST_INSERT_ID();";

            b.getComandoSQL().Parameters.AddWithValue("@obs", Transf.AprovacaoGerente.Observacao);
            b.getComandoSQL().Parameters.AddWithValue("@data", Transf.AprovacaoGerente.DataInsercao);
            b.getComandoSQL().Parameters.AddWithValue("@ativo", Transf.AprovacaoGerente.Status);
            b.getComandoSQL().Parameters.AddWithValue("@pessoa", Transf.AprovacaoGerente.Responsável.GetCodigo());

            int cod = 0;
            OK = b.ExecutaComando(true, out cod) == 1;

            if(OK)
            {
                b.getComandoSQL().Parameters.Clear();

                b.getComandoSQL().CommandText = @"
                                                update tranferencia set apr_codigo = @aprov where transf_codigo = @cod;";

                b.getComandoSQL().Parameters.AddWithValue("@aprov", cod);
                b.getComandoSQL().Parameters.AddWithValue("@cod", Transf.GetCodigo());
                OK = b.ExecutaComando(true) == 1;
            }

            b.FinalizaTransacao(OK);

            return OK;
        }

        internal bool Receber(Transferencia Transf, Localizacao loc)
        {
            Boolean OK = false;
            int CodigoImagem = 0;

            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"INSERT INTO Aprovacao_Destino
                                                           (aprdes_observacao
                                                           ,aprdes_dtinsercao
                                                           ,aprdes_stativo
                                                           ,pes_codigo)
                                                     VALUES
                                                           (@obs
                                                           ,@data
                                                           ,@ativo
                                                           ,@pessoa);
                                                            SELECT LAST_INSERT_ID();";

            b.getComandoSQL().Parameters.AddWithValue("@obs", Transf.AprovacaoDestino.Observacao);
            b.getComandoSQL().Parameters.AddWithValue("@data", Transf.AprovacaoDestino.DataInsercao);
            b.getComandoSQL().Parameters.AddWithValue("@ativo", Transf.AprovacaoDestino.Status);
            b.getComandoSQL().Parameters.AddWithValue("@pessoa", Transf.AprovacaoDestino.Responsável.GetCodigo());

            int cod = 0;
            OK = b.ExecutaComando(true, out cod) == 1;

            if (OK)
            {
                b.getComandoSQL().Parameters.Clear();

                b.getComandoSQL().CommandText = @"update tranferencia set aprdes_codigo = @aprov where transf_codigo = @cod;";

                b.getComandoSQL().Parameters.AddWithValue("@aprov", cod);
                b.getComandoSQL().Parameters.AddWithValue("@cod", Transf.GetCodigo());
                OK = b.ExecutaComando(true) == 1;

                if (OK)
                {
                    for (int i = 0; OK && i < Transf.AprovacaoDestino.Ativos.Count; i++)
                    {
                        OK = Transf.AprovacaoDestino.Ativos[i].Imagens[0].Gravar(loc, Transf.GetCodigo());

                        if (OK)
                            OK = GravarItem(Transf.AprovacaoDestino.Ativos[i].Imagens[0], Transf.GetCodigo());

                        if (OK)
                            OK = Transf.AprovacaoDestino.Ativos[i].AtualizarSala();

                        b.getComandoSQL().Parameters.Clear();       //muda a localização

                        b.getComandoSQL().CommandText = @"  select i.img_codigo as Codigo from localizacao l
                                                            inner join imagem i on l.img_codigo = i.img_codigo
                                                            inner join Ativos a on i.ati_codigo = a.ati_codigo
                                                            where a.ati_codigo = @codigo and i.transf_codigo is null;";     
                        //tentei fazer update com inner n deu bom 

                        b.getComandoSQL().Parameters.AddWithValue("@codigo", Transf.AprovacaoDestino.Ativos[i].GetCodigo());

                        DataTable dt = b.ExecutaSelect(true);
                        if (dt.Rows.Count > 0)
                            CodigoImagem = Convert.ToInt32(dt.Rows[0]["Codigo"]);
                        else
                            OK = false;

                        if (OK)
                        {
                            b.getComandoSQL().Parameters.Clear();       //muda a localização

                            b.getComandoSQL().CommandText = @"  update localizacao set localizacao.loca_latitude = @latitude, 
                                                                        localizacao.loca_longitude = @longitude where img_codigo = @codigoImagem;
";
                            //tentei fazer update com inner n deu bom 

                            b.getComandoSQL().Parameters.AddWithValue("@codigoImagem", CodigoImagem);
                            b.getComandoSQL().Parameters.AddWithValue("@latitude", loc.GetLatitude());
                            b.getComandoSQL().Parameters.AddWithValue("@longitude", loc.GetLongitude());

                            OK = b.ExecutaComando(true) == 1;
                        }
                        //gravar a localizacao
                    }
                }

            }

            b.FinalizaTransacao(OK);

            return OK;
        }
    }
}
