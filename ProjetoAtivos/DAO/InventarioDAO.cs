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
                             Imagem = null//new ImagemDAO().Buscar(Convert.ToInt32(row["fil_codigo"])),
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
                        Filial = new FilialDAO().BuscarFilial(Convert.ToInt32(dt.Rows[i]["fil_codigo"])),
                        Ativo = new AtivoDAO().BuscarObject(Convert.ToInt32(dt.Rows[i]["ati_codigo"])),
                        Obs = dt.Rows[i]["iv_obs"].ToString(),
                        Imagem = ""//new ImagemDAO().Buscar(Convert.ToInt32(dt.Rows[i]["img_codigo"])).GetFoto()                        
                    });
                }
            }

            return Dados == null ? null : Dados;
            
        }

        internal List<object> Buscar(string dtIni, string dtFim, int regiao, int filial)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().CommandTimeout = 0;

            string where = "f.reg_codigo = @reg";

            if (filial != 0)
            {
                where += " and i.fil_codigo = @fil";
                b.getComandoSQL().Parameters.AddWithValue("@fil", filial);
            }

            if(dtIni != null)
            {
                if(dtFim != null)
                {
                    if(dtIni == dtFim)
                    {
                        string [] Datas = dtFim.Split("-");
                        int Dia = Convert.ToInt32(Datas[2]); Dia++;
                        string DataFormatada = Datas[0] + "-" + Datas[1] + "-" + Dia;
                        dtFim = DataFormatada;
                    }
                    where += " and iv_data >= @dtIni and  iv_data <= @dtFim";

                    b.getComandoSQL().Parameters.AddWithValue("@dtFim", dtFim);
                    b.getComandoSQL().Parameters.AddWithValue("@dtIni", dtIni);
                }
               
            }
           
            b.getComandoSQL().CommandText = @"select * from inventario i
                                               inner join filial f on f.fil_codigo = i.fil_codigo
                                               where "+ where + " order by i.fil_codigo, iv_data";

            b.getComandoSQL().Parameters.AddWithValue("@reg", regiao);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToListO(dt);
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
            DataTable dt = b.ExecutaSelect();

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
