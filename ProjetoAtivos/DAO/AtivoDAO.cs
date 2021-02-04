using ProjetoAtivos.Models;
using Survey.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ProjetoAtivos.DAO
{
    public class AtivoDAO
    {
        private Banco b;

        internal AtivoDAO()       //pegando instancia do objeto banco
        {
            b = Banco.GetInstance();
        }
        internal List<Ativo> TableToList(DataTable dt)
        {
            List<Ativo> dados = null;
            if (dt != null && dt.Rows.Count > 0)
                dados = (from DataRow row in dt.Rows
                         select new Ativo(
                                          Convert.ToInt32(row["ati_codigo"]),
                                          Convert.ToInt32(row["ati_placa"]),
                                          row["ati_descricao"].ToString(),
                                          row["ati_estado"].ToString(),
                                          row["ati_observacao"].ToString(),
                                          row["ati_tag"].ToString(),
                                          row["ati_marca"].ToString(),
                                          row["ati_modelo"].ToString(),
                                          row["ati_numeroserie"].ToString(),
                                          Convert.ToBoolean(row["ati_stativo"]),
                                          Convert.ToDouble(row["ati_valor"]),
                                          Convert.ToInt32(row["tpa_codigo"]),
                                          row["tpa_descricao"].ToString(),
                                          Convert.ToDouble(row["tpa_valor"]),
                                          Convert.ToInt32(row["sal_codigo"]),
                                          row["sal_descricao"].ToString(),
                                          row["nt_codigo"] == DBNull.Value? 0: Convert.ToInt32(row["nt_codigo"])

                         )
                         {
                             Veiculo = row["ve_codigo"] != DBNull.Value ? new VeiculoDAO().Buscar(Convert.ToInt32(row["ati_codigo"])) : null
                         }
                         ).ToList();

            return dados;
        }
        internal List<object> TableToListAtivos(DataTable dt)
        {
            List<object> Dados = new List<object>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Dados.Add(new
                    {
                        Codigo = Convert.ToInt32(dt.Rows[i]["ati_codigo"]),
                        Latitude = DBNull.Value.Equals(dt.Rows[i]["loca_latitude"]) ? "" : dt.Rows[i]["loca_latitude"].ToString(),
                        Longitude = DBNull.Value.Equals(dt.Rows[i]["loca_longitude"]) ? "" : dt.Rows[i]["loca_longitude"].ToString(),
                        Imagem = DBNull.Value.Equals(dt.Rows[i]["img_imagem"]) ? "" : Encoding.UTF8.GetString((byte[])dt.Rows[i]["img_imagem"]),
                        Placa = Convert.ToInt32(dt.Rows[i]["ati_placa"]),
                        Descricao = dt.Rows[i]["ati_descricao"].ToString(),
                        Estado = dt.Rows[i]["ati_estado"].ToString(),
                        Razao = dt.Rows[i]["fil_razao"].ToString(),
                        StAtivo = Convert.ToBoolean(dt.Rows[i]["ati_stativo"]),
                        NotaFiscal = dt.Rows[i]["nt_codigo"].ToString(),
                        ValorAtivo = dt.Rows[i]["ati_valor"].ToString(),
                        ValorNota = dt.Rows[i]["nt_valor"].ToString(),
                    });
                }
            }

            return Dados == null ? null : Dados;
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
                        Codigo = Convert.ToInt32(dt.Rows[i]["ati_codigo"]),
                        Latitude = DBNull.Value.Equals(dt.Rows[i]["loca_latitude"]) ? "": dt.Rows[i]["loca_latitude"].ToString(),
                        Longitude = DBNull.Value.Equals(dt.Rows[i]["loca_longitude"]) ? "" : dt.Rows[i]["loca_longitude"].ToString(),
                        Imagem = DBNull.Value.Equals(dt.Rows[i]["img_imagem"])  ? "": Encoding.UTF8.GetString((byte[])dt.Rows[i]["img_imagem"]),
                        Placa = Convert.ToInt32(dt.Rows[i]["ati_placa"]),
                        Descricao = dt.Rows[i]["ati_descricao"].ToString(),
                        Estado = dt.Rows[i]["ati_estado"].ToString(),
                        Razao = dt.Rows[i]["fil_razao"].ToString(),
                        StAtivo = Convert.ToBoolean(dt.Rows[i]["ati_stativo"])
                    });
                }
            }

            return Dados == null ? null : Dados;
        }
        internal List<object> TableToListSemFt(DataTable dt)
        {
            List<object> Dados = new List<object>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Dados.Add(new
                    {
                        Codigo = Convert.ToInt32(dt.Rows[i]["ati_codigo"]),
                        Placa = Convert.ToInt32(dt.Rows[i]["ati_placa"]),
                        Descricao = dt.Rows[i]["ati_descricao"].ToString(),
                        Estado = dt.Rows[i]["ati_estado"].ToString(),
                        Razao = dt.Rows[i]["fil_razao"].ToString(),
                        StAtivo = Convert.ToBoolean(dt.Rows[i]["ati_stativo"])
                    });
                }
            }

            return Dados == null ? null : Dados;
        }
        internal int Gravar(Ativo Ativo, List<Imagem> Imagens, Localizacao Localizacao)
        {
            int Codigo = 0, NotaFiscal = 0;
            int j = 0;
            Boolean OK = false;

            try
            {
                NotaFiscal = Ativo.GetNota().Gravar();
                if (NotaFiscal >= 0)  //deu bom gravar nota ou alterar
                {
                    
                    b.getComandoSQL().Parameters.Clear();

                    if (Ativo.GetCodigo() == 0)
                    {
                        if (NotaFiscal > 0)
                        {
                            b.getComandoSQL().CommandText = @"insert into Ativos (ati_placa, ati_descricao, ati_estado, ati_observacao, ati_tag, ati_marca, ati_modelo, ati_numeroserie, ati_stativo, ati_valor, tpa_codigo, sal_codigo, nt_codigo) 
                   values(@placa, @descricao, @estado, @observacao, @tag, @marca, @modelo, @numserie, @ativo, @valor, @tpativo, @sala, @nota);
                SELECT LAST_INSERT_ID();";
                            b.getComandoSQL().Parameters.AddWithValue("@nota", NotaFiscal);
                            b.getComandoSQL().Parameters.AddWithValue("@valor", Ativo.GetNota().ValorNota);    //se tiver nota fiscal pega valor da nota

                        }
                        else
                        {
                            b.getComandoSQL().CommandText = @"insert into Ativos (ati_placa, ati_descricao, ati_estado, ati_observacao, ati_tag, ati_marca, ati_modelo, ati_numeroserie, ati_stativo, ati_valor, tpa_codigo, sal_codigo) 
                   values(@placa, @descricao, @estado, @observacao, @tag, @marca, @modelo, @numserie, @ativo, @valor, @tpativo, @sala);
                SELECT LAST_INSERT_ID();";
                            b.getComandoSQL().Parameters.AddWithValue("@valor", Ativo.GetNota().ValorNota);      //se n tiver pegar valor do tipo_ativo
                        }

                        b.getComandoSQL().Parameters.AddWithValue("@sala", Ativo.GetSala().GetCodigo());
           
                    }
                    else
                    {
                        if (NotaFiscal > 0)
                        {
                            b.getComandoSQL().CommandText = @"UPDATE Ativos SET ati_descricao = @descricao, ati_estado = @estado, ati_observacao = @observacao, ati_tag = @tag, ati_marca = @marca, ati_modelo = @modelo, ati_numeroSerie = @numserie, ati_stativo = @ativo, tpa_codigo = @tpativo, ati_valor = @valor, ati_placa = @placa, nt_codigo = @nota WHERE ati_codigo = @codigo;";
                            b.getComandoSQL().Parameters.AddWithValue("@nota", NotaFiscal);
                            b.getComandoSQL().Parameters.AddWithValue("@valor", Ativo.GetNota().ValorNota);    //se tiver nota altera o valor pelo valor da nota
                        }
                        else
                        {
                            b.getComandoSQL().CommandText = @"UPDATE Ativos SET ati_descricao = @descricao, ati_estado = @estado, ati_observacao = @observacao, ati_tag = @tag, ati_marca = @marca, ati_modelo = @modelo, ati_numeroSerie = @numserie, ati_stativo = @ativo, tpa_codigo = @tpativo, ati_valor = @valor, ati_placa = @placa WHERE ati_codigo = @codigo;";// se n tiver pelo tipo_ativo

                            b.getComandoSQL().Parameters.AddWithValue("@valor", Ativo.GetNota().ValorNota);

                        }

                        b.getComandoSQL().Parameters.AddWithValue("@codigo", Ativo.GetCodigo());



                    }
                    b.getComandoSQL().Parameters.AddWithValue("@placa", Ativo.GetPlaca());
                    b.getComandoSQL().Parameters.AddWithValue("@descricao", Ativo.GetDescricao());
                    b.getComandoSQL().Parameters.AddWithValue("@estado", Ativo.GetEstado());
                    b.getComandoSQL().Parameters.AddWithValue("@observacao", Ativo.GetObservacao());
                    b.getComandoSQL().Parameters.AddWithValue("@tag", Ativo.GetTag());
                    b.getComandoSQL().Parameters.AddWithValue("@marca", Ativo.GetMarca());
                    b.getComandoSQL().Parameters.AddWithValue("@modelo", Ativo.GetModelo());
                    b.getComandoSQL().Parameters.AddWithValue("@numserie", Ativo.GetNumeroSerie());
                    b.getComandoSQL().Parameters.AddWithValue("@ativo", Ativo.GetStAtivo());
                    b.getComandoSQL().Parameters.AddWithValue("@tpativo", Ativo.GetTipoAtivo().GetCodigo());

                    if (Ativo.GetCodigo() == 0)
                        OK = b.ExecutaComando(true, out Codigo) == 1;
                    else
                        OK = b.ExecutaComando(true) == 1;

                    if (OK)
                    {

                        if (Ativo.GetCodigo() != 0)    //n pode excluir e gravar dnv ... pq se n altera a localizacao  ... tem q fazer update 
                        {
                            Codigo = Ativo.GetCodigo();
                            Ativo.Imagens = new ImagemDAO().BuscarImagens(Ativo.GetCodigo());

                            if (Ativo.Imagens != null)
                            {
                                for (j = 0; OK && j < Ativo.Imagens.Count; j++)
                                {
                                    OK = Ativo.Imagens[j].Excluir();
                                }


                                for (j = 0; OK && j < Imagens.Count; j++)
                                {
                                    Ativo.SetCodigo(Codigo);
                                    Imagens[j].SetAtivo(Ativo);
                                    ImagemDAO ImagemDAO = new ImagemDAO();

                                    OK = ImagemDAO.Gravar(Imagens[j], Localizacao);
                                }
                            }
                            else
                            {
                                for (int i = 0; OK && i < Imagens.Count; i++)
                                {
                                    Ativo.SetCodigo(Codigo);
                                    Imagens[i].SetAtivo(Ativo);
                                    ImagemDAO ImagemDAO = new ImagemDAO();

                                    OK = ImagemDAO.Gravar(Imagens[i], Localizacao);
                                }
                            }

                        }
                        else //gravar
                        {
                            for (int i = 0; OK && i < Imagens.Count; i++)
                            {
                                Ativo.SetCodigo(Codigo);
                                Imagens[i].SetAtivo(Ativo);
                                ImagemDAO ImagemDAO = new ImagemDAO();

                                OK = ImagemDAO.Gravar(Imagens[i], Localizacao);
                            }
                        }

                        if(OK)
                        {
                            
                            if (Ativo.GetAnexo() != null)
                            {
                                Ativo.GetAnexo().Ativo = Ativo;
                                OK = Ativo.GetAnexo().Gravar();
                            }
                            else
                            {
                                if(new AnexoDAO().Buscar(Ativo.GetCodigo()) != null)
                                {
                                    OK = new AnexoDAO().Excluir(Ativo.GetCodigo());
                                }
                            }
                        }

                        if (OK && Ativo.Veiculo != null)
                        {
                            OK = Ativo.Veiculo.Gravar();
                        }


                        b.FinalizaTransacao(OK);

                    }
                    else
                        b.FinalizaTransacao(OK);


                }
                else
                    b.FinalizaTransacao(false);
            }
            catch (Exception e)
            {
                b.FinalizaTransacao(false);
            }

            return OK == true? 1: 0;
        }
        public List<Ativo> BuscarAtivos(int Filial)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select a.ati_codigo, a.ati_placa, a.ati_descricao, a.ati_estado, a.ati_observacao, a.ati_tag, a.ati_marca,           
                                                a.ati_modelo, a.ati_numeroserie, a.ati_stativo, a.ati_valor, tp.tpa_codigo, tp.tpa_descricao, tp.tpa_valor, s.sal_codigo, s.sal_descricao, a.nt_codigo
                                              from Ativos a
                                              inner join Tipo_Ativo tp on tp.tpa_codigo = a.tpa_codigo
                                              inner join Sala s on s.sal_codigo = a.sal_codigo
                                              where s.fil_codigo = @filial 
                                              group by  a.ati_codigo, a.ati_placa, a.ati_descricao, a.ati_estado, a.ati_observacao, a.ati_tag, a.ati_marca,        
                                                a.ati_modelo, a.ati_numeroserie, a.ati_stativo, a.ati_valor, tp.tpa_codigo, tp.tpa_descricao, s.sal_codigo, s.sal_descricao;";
            b.getComandoSQL().Parameters.AddWithValue("@filial", Filial);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }


       public List<object> ObterAtivosPlaca(int Placa)
        {
            List<object> Dados = new List<object>();
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select a.ati_placa, a.ati_descricao, f.fil_razao, r.reg_descricao, a.ati_stativo
                                              from Ativos a 
                                              inner join Sala s on s.sal_codigo = a.sal_codigo
                                              inner join Filial f on s.fil_codigo = f.fil_codigo
                                              inner join regional r on f.reg_codigo = r.reg_codigo
                                              where a.ati_placa = @placa order by a.ati_placa;";

            b.getComandoSQL().Parameters.AddWithValue("@placa", Placa);


            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
            {
               for(int i = 0; i < dt.Rows.Count; i++)
                {
                    Dados.Add(new
                    {
                        Placa = dt.Rows[i]["ati_placa"].ToString(),
                        Descricao = dt.Rows[i]["ati_descricao"].ToString(),
                        Status = Convert.ToBoolean(dt.Rows[i]["ati_stativo"]),
                        Filial = dt.Rows[i]["fil_razao"].ToString(),
                        Regional = dt.Rows[i]["reg_descricao"].ToString()
                    });
                }
                return Dados;
            }
            else
                return null;

        }

        public List<Ativo> BuscarAtivos(Transferencia t)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select a.ati_codigo, a.ati_placa, a.ati_descricao, a.ati_estado, a.ati_observacao, a.ati_tag, a.ati_marca,                                                                
                                                a.ati_modelo, a.ati_numeroserie, a.ati_stativo, a.ati_valor, tp.tpa_codigo, tp.tpa_descricao, tp.tpa_valor, s.sal_codigo, s.sal_descricao, , a.nt_codigo
                                              from Ativos a
                                              inner join Tipo_Ativo tp on tp.tpa_codigo = a.tpa_codigo
                                              inner join Sala s on s.sal_codigo = a.sal_codigo
                                              inner join itens_ativos ia on a.ati_codigo = ia.ati_codigo 
                                              where ia.transf_codigo = @transf;";
            b.getComandoSQL().Parameters.AddWithValue("@transf", t.GetCodigo());

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }
        public List<object> ObterAtivos(Transferencia t)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @" select a.ati_codigo, '' as loca_latitude, '' as loca_longitude, i.img_imagem, a.ati_placa, ati_descricao, ati_estado, f.fil_razao, a.ati_stativo
                                                from itens_ativos ia
                                                inner join Ativos a on a.ati_codigo = ia.ati_codigo 
                                                inner join imagem i on a.ati_codigo = i.ati_codigo
                                                inner join Sala s on s.sal_codigo = a.sal_codigo
                                                inner join Filial f on s.fil_codigo = f.fil_codigo
                                                where ia.transf_codigo = @transf and i.transf_codigo is null 
                                                group by a.ati_codigo
                                                order by a.ati_descricao;";

            b.getComandoSQL().Parameters.AddWithValue("@transf", t.GetCodigo());


            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToListCompleta(dt);
            else
                return null;

        }



        public List<object> ObterAtivos(string Chave, string Filtro, int Ativo, int Regiao, int Filial)
        {
            string Txt = "";

            b.getComandoSQL().CommandTimeout = 0;
            b.getComandoSQL().Parameters.Clear();



            if(Chave == null)
            {
                if(Filial > 0)
                {
                    b.getComandoSQL().CommandText = @"select a.ati_codigo, l.loca_latitude, l.loca_longitude, i.img_imagem, a.ati_placa, ati_descricao, ati_estado, f.fil_razao, a.ati_stativo, a.ati_valor, nf.nt_codigo, nf.nt_valor
                   from Ativos a 
                   LEFT join imagem i on a.ati_codigo = i.ati_codigo
                   LEFT join localizacao l on l.img_codigo = i.img_codigo
                   inner join Sala s on s.sal_codigo = a.sal_codigo
                   inner join Filial f on s.fil_codigo = f.fil_codigo
                   inner join Regional r on r.reg_codigo = f.reg_codigo
                   inner join nota_fiscal nf on nf.nt_codigo = a.nt_codigo  
                   where a.ati_stativo = @ativoONE  and f.reg_codigo = @regionalONE and f.fil_codigo = @filialONE
                   group by a.ati_codigo
                        union
                   select a.ati_codigo, l.loca_latitude, l.loca_longitude, i.img_imagem, a.ati_placa, ati_descricao, ati_estado,
                   f.fil_razao, a.ati_stativo, a.ati_valor, nf.nt_codigo, nf.nt_valor
                   from Ativos a 
                   RIGHT join imagem i on a.ati_codigo = i.ati_codigo
                   RIGHT join localizacao l on l.img_codigo = i.img_codigo
                   left join nota_fiscal nf on nf.nt_codigo = a.nt_codigo
                   inner join Sala s on s.sal_codigo = a.sal_codigo
                   inner join Filial f on s.fil_codigo = f.fil_codigo
                   inner join Regional r on r.reg_codigo = f.reg_codigo
                   where a.ati_stativo = @ativoTWO  and f.reg_codigo = @regionalTWO and f.fil_codigo = @filialTWO
                   group by a.ati_codigo;";

                    b.getComandoSQL().Parameters.AddWithValue("@filialONE", Filial);
                    b.getComandoSQL().Parameters.AddWithValue("@filialTWO", Filial);
                }
                else
                {
                    b.getComandoSQL().CommandText = @"select a.ati_codigo, l.loca_latitude, l.loca_longitude, i.img_imagem, a.ati_placa, ati_descricao, ati_estado, f.fil_razao, a.ati_stativo, a.ati_valor, nf.nt_codigo, nf.nt_valor
                   from Ativos a 
                   LEFT join imagem i on a.ati_codigo = i.ati_codigo
                   LEFT join localizacao l on l.img_codigo = i.img_codigo
                   inner join Sala s on s.sal_codigo = a.sal_codigo
                   inner join Filial f on s.fil_codigo = f.fil_codigo
                   inner join Regional r on r.reg_codigo = f.reg_codigo
                   inner join nota_fiscal nf on nf.nt_codigo = a.nt_codigo
                   where a.ati_stativo = @ativoONE  and f.reg_codigo = @regionalONE
                   group by a.ati_codigo
                        union
                   select a.ati_codigo, l.loca_latitude, l.loca_longitude, i.img_imagem, a.ati_placa, ati_descricao, ati_estado,
                   f.fil_razao, a.ati_stativo, a.ati_valor, nf.nt_codigo, nf.nt_valor
                   from Ativos a 
                   RIGHT join imagem i on a.ati_codigo = i.ati_codigo
                   RIGHT join localizacao l on l.img_codigo = i.img_codigo
                   left join nota_fiscal nf on nf.nt_codigo = a.nt_codigo
                   inner join Sala s on s.sal_codigo = a.sal_codigo
                   inner join Filial f on s.fil_codigo = f.fil_codigo
                   inner join Regional r on r.reg_codigo = f.reg_codigo
                   where a.ati_stativo = @ativoTWO  and f.reg_codigo = @regionalTWO
                   group by a.ati_codigo;";
                }
                b.getComandoSQL().Parameters.AddWithValue("@regionalONE", Regiao);
                b.getComandoSQL().Parameters.AddWithValue("@regionalTWO", Regiao);
            }
            else
            {
                if(Filtro == "Nome")
                {
                    if(Filial > 0)
                    {
                        b.getComandoSQL().CommandText = @"select a.ati_codigo, l.loca_latitude, l.loca_longitude, i.img_imagem, a.ati_placa, ati_descricao, ati_estado, f.fil_razao, a.ati_stativo, a.ati_valor, nf.nt_codigo, nf.nt_valor
                           from Ativos a 
                           LEFT join imagem i on a.ati_codigo = i.ati_codigo
                           LEFT join localizacao l on l.img_codigo = i.img_codigo
                           inner join Sala s on s.sal_codigo = a.sal_codigo
                           inner join Filial f on s.fil_codigo = f.fil_codigo
                           inner join Regional r on r.reg_codigo = f.reg_codigo
                           inner join nota_fiscal nf on nf.nt_codigo = a.nt_codigo
                           where a.ati_stativo = @ativoONE  and f.reg_codigo = @regionalONE and a.ati_descricao like @nomeONE and f.fil_codigo = @filialONE
                           group by a.ati_codigo
                                union
                           select a.ati_codigo, l.loca_latitude, l.loca_longitude, i.img_imagem, a.ati_placa, ati_descricao, ati_estado,
                           f.fil_razao, a.ati_stativo, a.ati_valor, nf.nt_codigo, nf.nt_valor
                           from Ativos a 
                           RIGHT join imagem i on a.ati_codigo = i.ati_codigo
                           RIGHT join localizacao l on l.img_codigo = i.img_codigo
                           left join nota_fiscal nf on nf.nt_codigo = a.nt_codigo
                           inner join Sala s on s.sal_codigo = a.sal_codigo
                           inner join Filial f on s.fil_codigo = f.fil_codigo
                           inner join Regional r on r.reg_codigo = f.reg_codigo
                           where a.ati_stativo = @ativoTWO  and f.reg_codigo = @regionalTWO and a.ati_descricao like @nomeTWO and f.fil_codigo = @filialTWO
                           group by a.ati_codigo";

                        b.getComandoSQL().Parameters.AddWithValue("@filialONE", Filial);
                        b.getComandoSQL().Parameters.AddWithValue("@filialTWO", Filial);
                    }
                    else
                    {
                        b.getComandoSQL().CommandText = @"select a.ati_codigo, l.loca_latitude, l.loca_longitude, i.img_imagem, a.ati_placa, ati_descricao, ati_estado, f.fil_razao, a.ati_stativo, a.ati_valor, nf.nt_codigo, nf.nt_valor
                           from Ativos a 
                           LEFT join imagem i on a.ati_codigo = i.ati_codigo
                           LEFT join localizacao l on l.img_codigo = i.img_codigo
                           inner join Sala s on s.sal_codigo = a.sal_codigo
                           inner join Filial f on s.fil_codigo = f.fil_codigo
                           inner join Regional r on r.reg_codigo = f.reg_codigo
                           inner join nota_fiscal nf on nf.nt_codigo = a.nt_codigo
                           where a.ati_stativo = @ativoONE  and f.reg_codigo = @regionalONE and a.ati_descricao like @nomeONE
                           group by a.ati_codigo
                                union
                           select a.ati_codigo, l.loca_latitude, l.loca_longitude, i.img_imagem, a.ati_placa, ati_descricao, ati_estado,
                           f.fil_razao, a.ati_stativo, a.ati_valor, nf.nt_codigo, nf.nt_valor
                           from Ativos a 
                           RIGHT join imagem i on a.ati_codigo = i.ati_codigo
                           RIGHT join localizacao l on l.img_codigo = i.img_codigo
                           left join nota_fiscal nf on nf.nt_codigo = a.nt_codigo
                           inner join Sala s on s.sal_codigo = a.sal_codigo
                           inner join Filial f on s.fil_codigo = f.fil_codigo
                           inner join Regional r on r.reg_codigo = f.reg_codigo
                           where a.ati_stativo = @ativoTWO  and f.reg_codigo = @regionalTWO and a.ati_descricao like @nomeTWO
                           group by a.ati_codigo";
                    }

                    b.getComandoSQL().Parameters.AddWithValue("@nomeONE", "%" + Chave + "%");
                    b.getComandoSQL().Parameters.AddWithValue("@nomeTWO", "%" + Chave + "%");
                    b.getComandoSQL().Parameters.AddWithValue("@regionalONE", Regiao);
                    b.getComandoSQL().Parameters.AddWithValue("@regionalTWO", Regiao);
                }

                if(Filtro == "Placa")
                {
                    if(Regiao > 0) //preencheu a regional
                    {
                        if(Filial > 0) //preencheu regional e filial
                        {
                            b.getComandoSQL().CommandText = @"select a.ati_codigo, l.loca_latitude, l.loca_longitude, i.img_imagem, a.ati_placa, ati_descricao, ati_estado, f.fil_razao, a.ati_stativo, a.ati_valor, nf.nt_codigo, nf.nt_valor
                           from Ativos a 
                           LEFT join imagem i on a.ati_codigo = i.ati_codigo
                           LEFT join localizacao l on l.img_codigo = i.img_codigo
                           inner join Sala s on s.sal_codigo = a.sal_codigo
                           inner join Filial f on s.fil_codigo = f.fil_codigo
                           inner join Regional r on r.reg_codigo = f.reg_codigo
                           inner join nota_fiscal nf on nf.nt_codigo = a.nt_codigo
                           where a.ati_stativo = @ativoONE  and f.reg_codigo = @regionalONE and f.fil_codigo = @filialONE and a.ati_placa like @placaOne
                           group by a.ati_codigo
                                union
                           select a.ati_codigo, l.loca_latitude, l.loca_longitude, i.img_imagem, a.ati_placa, ati_descricao, ati_estado,
                           f.fil_razao, a.ati_stativo, a.ati_valor, nf.nt_codigo, nf.nt_valor
                           from Ativos a 
                           RIGHT join imagem i on a.ati_codigo = i.ati_codigo
                           RIGHT join localizacao l on l.img_codigo = i.img_codigo
                           left join nota_fiscal nf on nf.nt_codigo = a.nt_codigo
                           inner join Sala s on s.sal_codigo = a.sal_codigo
                           inner join Filial f on s.fil_codigo = f.fil_codigo
                           inner join Regional r on r.reg_codigo = f.reg_codigo
                           where a.ati_stativo = @ativoTWO  and f.reg_codigo = @regionalTWO and f.fil_codigo = @filialTWO and a.ati_placa like @placaTwo
                           group by a.ati_codigo";

                            b.getComandoSQL().Parameters.AddWithValue("@filialONE", Filial);
                            b.getComandoSQL().Parameters.AddWithValue("@filialTWO", Filial);
                            b.getComandoSQL().Parameters.AddWithValue("@regionalONE", Regiao);
                            b.getComandoSQL().Parameters.AddWithValue("@regionalTWO", Regiao);

                            b.getComandoSQL().Parameters.AddWithValue("@placaOne", "%" + Chave + "%");
                            b.getComandoSQL().Parameters.AddWithValue("@placaTwo", "%" + Chave + "%");

                        }
                        else  //preencheu regional mas n filial
                        {
                            b.getComandoSQL().CommandText = @"select a.ati_codigo, l.loca_latitude, l.loca_longitude, i.img_imagem, a.ati_placa, ati_descricao, ati_estado, f.fil_razao, a.ati_stativo, a.ati_valor, nf.nt_codigo, nf.nt_valor
                           from Ativos a 
                           LEFT join imagem i on a.ati_codigo = i.ati_codigo
                           LEFT join localizacao l on l.img_codigo = i.img_codigo
                           inner join Sala s on s.sal_codigo = a.sal_codigo
                           inner join Filial f on s.fil_codigo = f.fil_codigo
                           inner join Regional r on r.reg_codigo = f.reg_codigo
                           inner join nota_fiscal nf on nf.nt_codigo = a.nt_codigo
                           where a.ati_stativo = @ativoONE  and f.reg_codigo = @regionalONE and a.ati_placa like @placaOne
                           group by a.ati_codigo
                                union
                           select a.ati_codigo, l.loca_latitude, l.loca_longitude, i.img_imagem, a.ati_placa, ati_descricao, ati_estado,
                           f.fil_razao, a.ati_stativo, a.ati_valor, nf.nt_codigo, nf.nt_valor
                           from Ativos a 
                           RIGHT join imagem i on a.ati_codigo = i.ati_codigo
                           RIGHT join localizacao l on l.img_codigo = i.img_codigo
                           left join nota_fiscal nf on nf.nt_codigo = a.nt_codigo
                           inner join Sala s on s.sal_codigo = a.sal_codigo
                           inner join Filial f on s.fil_codigo = f.fil_codigo
                           inner join Regional r on r.reg_codigo = f.reg_codigo
                           where a.ati_stativo = @ativoTWO  and f.reg_codigo = @regionalTWO and a.ati_placa like @placaTwo
                           group by a.ati_codigo";

                            b.getComandoSQL().Parameters.AddWithValue("@regionalONE", Regiao);
                            b.getComandoSQL().Parameters.AddWithValue("@regionalTWO", Regiao);

                            b.getComandoSQL().Parameters.AddWithValue("@placaOne", "%" + Chave + "%");
                            b.getComandoSQL().Parameters.AddWithValue("@placaTwo", "%" + Chave + "%");
                        }
                    }
                    else //nao preencheu
                    {
                        b.getComandoSQL().CommandText = @"select a.ati_codigo, l.loca_latitude, l.loca_longitude, i.img_imagem, a.ati_placa, ati_descricao, ati_estado, f.fil_razao, a.ati_stativo, a.ati_valor, nf.nt_codigo, nf.nt_valor
                           from Ativos a 
                           LEFT join imagem i on a.ati_codigo = i.ati_codigo
                           LEFT join localizacao l on l.img_codigo = i.img_codigo
                           inner join Sala s on s.sal_codigo = a.sal_codigo
                           inner join Filial f on s.fil_codigo = f.fil_codigo
                           inner join Regional r on r.reg_codigo = f.reg_codigo
                   inner join nota_fiscal nf on nf.nt_codigo = a.nt_codigo
                           where a.ati_stativo = @ativoONE and a.ati_placa like @placaOne
                           group by a.ati_codigo
                                union
                           select a.ati_codigo, l.loca_latitude, l.loca_longitude, i.img_imagem, a.ati_placa, ati_descricao, ati_estado,
                           f.fil_razao, a.ati_stativo, a.ati_valor, nf.nt_codigo, nf.nt_valor
                           from Ativos a 
                           RIGHT join imagem i on a.ati_codigo = i.ati_codigo
                           RIGHT join localizacao l on l.img_codigo = i.img_codigo
                           left join nota_fiscal nf on nf.nt_codigo = a.nt_codigo
                           inner join Sala s on s.sal_codigo = a.sal_codigo
                           inner join Filial f on s.fil_codigo = f.fil_codigo
                           inner join Regional r on r.reg_codigo = f.reg_codigo
                           where a.ati_stativo = @ativoTWO and a.ati_placa like @placaTwo
                           group by a.ati_codigo";

                        b.getComandoSQL().Parameters.AddWithValue("@placaOne", "%" + Chave + "%");
                        b.getComandoSQL().Parameters.AddWithValue("@placaTwo", "%" + Chave + "%");
                    }
                }
            }

            b.getComandoSQL().Parameters.AddWithValue("@ativoONE", Ativo);
            b.getComandoSQL().Parameters.AddWithValue("@ativoTWO", Ativo);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToListAtivos(dt);
            else
                return null;
        }


        internal Boolean ExcluirLogico(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"update Ativos set ati_stativo = 0 where ati_codigo = @codigo;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            return b.ExecutaComando() == 1;
        }

        internal Boolean Ativar(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"update Ativos set ati_stativo = 1 where ati_codigo = @codigo;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            return b.ExecutaComando() == 1;
        }


        internal Ativo BuscarAtivo(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select a.ati_codigo, a.ati_placa, a.ati_descricao, a.ati_estado, a.ati_observacao, a.ati_tag, a.ati_marca, a.ati_modelo,                                         a.ati_numeroserie, a.ati_stativo, a.ati_valor, tp.tpa_codigo, tp.tpa_descricao, tp.tpa_valor, s.sal_codigo, s.sal_descricao, a.nt_codigo from Ativos a
                                              inner join Tipo_Ativo tp on tp.tpa_codigo = a.tpa_codigo
                                              inner join Sala s on s.sal_codigo = a.sal_codigo
                                              where a.ati_codigo = @codigo and a.ati_stativo = 1;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
            {
                Ativo a = TableToList(dt).FirstOrDefault();
                a.Imagens = new ImagemDAO().BuscarImagens(Codigo, false);
                a.SetNota(new NotaFiscal().BuscarNota(a.GetNota().Codigo));
                a.SetAnexo(new AnexoDAO().Buscar(a.GetCodigo()));
                return a;
            }
            else
                return null;
        }
        internal int BuscarCountImagem()
        {
            int i = 1;
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select count(a.ati_codigo)
                                            from imagem i
                                            inner join Ativos a on i.ati_codigo = a.ati_codigo
                                            where a.ati_stativo = 1
                                            group by a.ati_codigo;";

            DataTable dt = b.ExecutaSelect();
            if (dt.Rows.Count > 0)
            {
                for (i = 1; i < dt.Rows.Count; i++)
                {
                   /*olha a gambi... kkkk*/
                }
                return i;
            }
            else
                return 0;
        }
        internal int BuscarCountTotal()
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().CommandText = @"  select count(a.ati_codigo) as Total
                                                from Ativos a
                                                where a.ati_stativo = 1;";
            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0]["Total"]);
            }
            else
                return 0;
        }
        internal object AtivosImagem()
        {
            object Dados = new object();
            Dados = new
            {
                Quantidade = BuscarCountImagem(),
                QuantidadeTotal = BuscarCountTotal() - BuscarCountImagem()
            };

            return Dados == null ? null : Dados;

        }

        internal object SomaAtivos()
        {
            object Dados = new object();

            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().CommandText = @"select ifnull(sum(a.ati_valor),0) as Valor 
                                              from Ativos a;";
            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
            {
                Dados = new
                {
                    ValorTotal = Convert.ToInt32(dt.Rows[0]["Valor"])
                };
            }
            else
            {
                Dados = new
                {
                    ValorTotal = Convert.ToInt32(dt.Rows[0]["Valor"])
                };
            }

            return Dados == null ? null : Dados;

        }

        internal bool AtualizarSala(Ativo a)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"update Ativos set sal_codigo = @sala where ati_codigo = @codigo;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", a.GetCodigo());
            b.getComandoSQL().Parameters.AddWithValue("@sala", a.GetSala().GetCodigo());

            return b.ExecutaComando(true) == 1;
        }
        internal bool AtualizarLocalizacao(Ativo a)
        {
            return true;
        }
        internal List<object> ObterValores(int Regional, int Filial)
        {
            List<object> Dados = new List<object>();

            b.getComandoSQL().Parameters.Clear();

            if(Filial > 0)
            {
                b.getComandoSQL().CommandText = @"select f.fil_razao, sum(a.ati_valor) as Valor, count(a.ati_codigo) as Quantidade
                                              from ativos a
                                              inner join sala s on a.sal_codigo = s.sal_codigo
                                              inner join filial f on s.fil_codigo = f.fil_codigo
                                              inner join regional r on f.reg_codigo = r.reg_codigo
                                              where r.reg_codigo = @regional and f.fil_codigo = @filial
                                              group by f.fil_razao;";
                b.getComandoSQL().Parameters.AddWithValue("@regional", Regional);
                b.getComandoSQL().Parameters.AddWithValue("@filial", Filial);

            }
            else
            {
                b.getComandoSQL().CommandText = @"select f.fil_razao, sum(a.ati_valor) as Valor, count(a.ati_codigo) as Quantidade
                                              from ativos a
                                              inner join sala s on a.sal_codigo = s.sal_codigo
                                              inner join filial f on s.fil_codigo = f.fil_codigo
                                              inner join regional r on f.reg_codigo = r.reg_codigo
                                              where r.reg_codigo = @regional
                                              group by f.fil_razao;";
                b.getComandoSQL().Parameters.AddWithValue("@regional", Regional);
            }

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Dados.Add(new
                    {
                       Razao = dt.Rows[i]["fil_razao"].ToString(),
                       ValorAcumulado = Convert.ToDouble(dt.Rows[i]["Valor"]),
                       QuantidadeAtivos = Convert.ToInt32(dt.Rows[i]["Quantidade"])
                    });
                }
                return Dados;
            }
            else
                return null;
        }
        internal List<object> ObterRelatorioImagem(int Regional, int Filial)
        {
            List<object> Dados = new List<object>();

            b.getComandoSQL().Parameters.Clear();

            if (Filial > 0)
            {
                b.getComandoSQL().CommandText = @"select r.reg_descricao, f.fil_razao, a.ati_placa, i.img_codigo
                                                from regional r
                                                inner join filial f on r.reg_codigo = f.reg_codigo
                                                inner join sala s on s.fil_codigo = f.fil_codigo
                                                inner join ativos a on a.sal_codigo = s.sal_codigo
                                                LEFT join imagem i on i.ati_codigo = a.ati_codigo
                                                where r.reg_codigo = @regional and f.fil_codigo = @filial and a.ati_stativo = 1
                                                group by a.ati_placa
                                                union
                                                select r.reg_descricao, f.fil_razao, a.ati_placa, i.img_codigo
                                                from regional r
                                                inner join filial f on r.reg_codigo = f.reg_codigo
                                                inner join sala s on s.fil_codigo = f.fil_codigo
                                                inner join ativos a on a.sal_codigo = s.sal_codigo
                                                RIGHT join imagem i on i.ati_codigo = a.ati_codigo
                                                where r.reg_codigo = @regional and f.fil_codigo = @filial and a.ati_stativo = 1";
                b.getComandoSQL().Parameters.AddWithValue("@regional", Regional);
                b.getComandoSQL().Parameters.AddWithValue("@filial", Filial);

            }
            else
            {
                b.getComandoSQL().CommandText = @"select r.reg_descricao, f.fil_razao, a.ati_placa, i.img_codigo
                                                from regional r
                                                inner join filial f on r.reg_codigo = f.reg_codigo
                                                inner join sala s on s.fil_codigo = f.fil_codigo
                                                inner join ativos a on a.sal_codigo = s.sal_codigo
                                                LEFT join imagem i on i.ati_codigo = a.ati_codigo
                                                where r.reg_codigo = @regional and a.ati_stativo = 1
                                                group by a.ati_placa
                                                union
                                                select r.reg_descricao, f.fil_razao, a.ati_placa, i.img_codigo
                                                from regional r
                                                inner join filial f on r.reg_codigo = f.reg_codigo
                                                inner join sala s on s.fil_codigo = f.fil_codigo
                                                inner join ativos a on a.sal_codigo = s.sal_codigo
                                                RIGHT join imagem i on i.ati_codigo = a.ati_codigo
                                                where r.reg_codigo = @regional and a.ati_stativo = 1";

                b.getComandoSQL().Parameters.AddWithValue("@regional", Regional);
            }

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Dados.Add(new
                    {
                        Regional = dt.Rows[i]["reg_descricao"].ToString(),
                        Filial = dt.Rows[i]["fil_razao"].ToString(),
                        Placa = dt.Rows[i]["ati_placa"].ToString(),
                        Imagem = dt.Rows[i]["img_codigo"].ToString()
                    });
                }
                return Dados;
            }
            else
                return null;
        }

        public List<object> ObterImagens(int Codigo)
        {
            List<object> Dados = new List<object>();
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select img_imagem, img_dtinsercao from imagem where ati_codigo = @codigo";

            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Dados.Add(new
                    {
                        Imagem = DBNull.Value.Equals(dt.Rows[i]["img_imagem"]) ? "" : Encoding.UTF8.GetString((byte[])dt.Rows[i]["img_imagem"]),
                        DataInsercao = Convert.ToDateTime(dt.Rows[i]["img_dtinsercao"])

                    });
                }
                return Dados;
            }
            else
                return null;

        }
    }
}
