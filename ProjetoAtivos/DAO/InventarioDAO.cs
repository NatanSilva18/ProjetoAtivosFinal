using ProjetoAtivos.Models;
using Survey.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAtivos.DAO
{
    public class InventarioDAO
    {
        private Banco b;

        internal InventarioDAO()       //pegando instancia do objeto banco
        {
            b = Banco.GetInstance();
        }

        internal List<Inventario> TableToList(DataTable dt)
        {
            List<Inventario> dados = null;
            if (dt != null && dt.Rows.Count > 0)
                dados = (from DataRow row in dt.Rows
                         select new Inventario()
                         {
                             Codigo = Convert.ToInt32(row["iv_codigo"]),
                             Data = Convert.ToDateTime(row["iv_data"]),
                             Filial = new FilialDAO().BuscarFilial(Convert.ToInt32(row["fil_codigo"])),
                             Ativo = new AtivoDAO().BuscarAtivo(Convert.ToInt32(row["ati_codigo"])),
                             Obs = row["iv_obs"].ToString(),
                             Imagem = null //new ImagemDAO().Buscar(Convert.ToInt32(row["fil_codigo"])),
                         }
                         ).ToList();

            return dados;
        }

        internal List<Object> TableToListO(DataTable dt)
        {
            List<object> Dados = new List<object>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Dados.Add(new
                    {
                        Codigo = Convert.ToInt32(dt.Rows[i]["iv_codigo"]),
                        Data = Convert.ToDateTime(dt.Rows[i]["iv_data"]).ToString("dd/MM/yyyy"),
                        Filial = "",//new FilialDAO().BuscarFilial(Convert.ToInt32(dt.Rows[i]["fil_codigo"])),
                        Ativo = "", //new AtivoDAO().BuscarObject(Convert.ToInt32(dt.Rows[i]["ati_codigo"]), foto),
                        Obs = dt.Rows[i]["iv_obs"].ToString(),
                        Imagem = ""//foto ? new ImagemDAO().Buscar(Convert.ToInt32(dt.Rows[i]["img_codigo"])).GetFoto() : ""                       
                    });
                }
            }

            return Dados == null ? null : Dados;
            
        }

        internal List<object> Buscar(string dtIni, string dtFim, int Regional, int Filial)
        {
            List<object> Dados = new List<object>();
            b.getComandoSQL().CommandTimeout = 0;
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().CommandText = @"select i.iv_codigo, i.iv_data, i.iv_obs, f.fil_razao, a.ati_codigo, a.ati_placa, a.ati_descricao, a.ati_estado, a.ati_stativo, a.ati_valor, nf.nt_codigo, nf.nt_valor
                                                from inventario i
                                         inner join ativos a on i.ati_codigo = a.ati_codigo
                                        left join nota_fiscal nf on a.nt_codigo = nf.nt_codigo
                                        inner join filial f on f.fil_codigo = i.fil_codigo
                                        inner join regional r on r.reg_codigo = f.reg_codigo
                                                where r.reg_codigo = @regional";

            b.getComandoSQL().Parameters.AddWithValue("@regional", Regional);


            if(dtIni != null)
            {
                if(dtFim != null)
                {
                    b.getComandoSQL().CommandText += " and i.iv_data >= @dtIni and i.iv_data <= @dtFim";
                    b.getComandoSQL().Parameters.AddWithValue("@dtIni", dtIni);
                    b.getComandoSQL().Parameters.AddWithValue("@dtFim", dtFim);
                }
                else
                {
                    b.getComandoSQL().CommandText += " and i.iv_data >= @dtIni and i.iv_data <= '2080-01-01'";
                    b.getComandoSQL().Parameters.AddWithValue("@dtIni", dtIni);
                }
            }
            else
            {
                if (dtFim != null)
                {
                    b.getComandoSQL().CommandText += " and i.iv_data >= '2000-01-01' and i.iv_data <= @dtFim";
                    b.getComandoSQL().Parameters.AddWithValue("@dtFim", dtFim);
                }
            }

            if (Filial != 0)
            {
                b.getComandoSQL().CommandText += " and i.fil_codigo = @fil";
                b.getComandoSQL().Parameters.AddWithValue("@fil", Filial);
            }

           
            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Dados.Add(new
                    {
                        Codigo = Convert.ToInt32(dt.Rows[i]["iv_codigo"]),
                        Data = Convert.ToDateTime(dt.Rows[i]["iv_data"]).ToString("dd/MM/yyyy"),
                        Filial = dt.Rows[i]["fil_razao"].ToString(),
                        Obs = dt.Rows[i]["iv_obs"].ToString(),
                        CodigoAtivo = Convert.ToInt32(dt.Rows[i]["ati_codigo"]),
                        Placa = Convert.ToInt32(dt.Rows[i]["ati_placa"]),
                        Descricao = dt.Rows[i]["ati_descricao"].ToString(),
                        Estado = dt.Rows[i]["ati_estado"].ToString(),
                        StAtivo = Convert.ToBoolean(dt.Rows[i]["ati_stativo"]),
                        NotaFiscal = dt.Rows[i]["nt_codigo"].ToString(),
                        ValorAtivo = dt.Rows[i]["ati_valor"].ToString(),
                        ValorNota = dt.Rows[i]["nt_valor"].ToString(),
                        Imagem = "~/img/semimagem.png"
                    });
                }

                return Dados;
            }
            else
                return null;
        }

        internal bool Gravar(Inventario Inv)
        {
            bool ok = true;

            ok = Inv.Imagem.Gravar(Inv.Localizacao);

            if(ok)
            {
                b.getComandoSQL().Parameters.Clear();


                b.getComandoSQL().CommandText = @"INSERT INTO `inventario`
                                                    (
                                                    `iv_data`,
                                                    `iv_obs`,
                                                    `ati_codigo`,
                                                    `img_codigo`,
                                                    `fil_codigo`)
                                                    VALUES
                                                    (
                                                    @iv_data,
                                                    @iv_obs,
                                                    @ati_codigo,
                                                    @img_codigo,
                                                    @fil_codigo);
                                                    ";


                b.getComandoSQL().Parameters.AddWithValue("@iv_data", Inv.Data);
                b.getComandoSQL().Parameters.AddWithValue("@iv_obs", Inv.Obs);
                b.getComandoSQL().Parameters.AddWithValue("@ati_codigo", Inv.Ativo.GetCodigo());
                b.getComandoSQL().Parameters.AddWithValue("@img_codigo", Inv.Imagem.GetCodigo());
                b.getComandoSQL().Parameters.AddWithValue("@fil_codigo", Inv.Filial.GetCodigo());

                ok = b.ExecutaComando(true) == 1;

            }

            b.FinalizaTransacao(ok);
            return ok;
        }
        internal object BuscarInventarioAtivo(int CodigoAtivo)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().CommandText = @"select iv_codigo, iv_data from inventario where ati_codigo = @ativo;";
            b.getComandoSQL().Parameters.AddWithValue("@ativo", CodigoAtivo);
            DataTable dt = b.ExecutaSelect(true);

            if (dt.Rows.Count > 0)
            {
                return new
                {
                    Codigo = Convert.ToInt32(dt.Rows[0]["iv_codigo"]),
                    Data = Convert.ToDateTime(dt.Rows[0]["iv_data"])
                };
            }
            else
                return null;
        }
    }
}
