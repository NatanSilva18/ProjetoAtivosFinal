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
                             Imagem = new ImagemDAO().Buscar(Convert.ToInt32(row["fil_codigo"])),
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
                        Data = Convert.ToDateTime(dt.Rows[i]["iv_data"]),
                        Filial = new FilialDAO().BuscarFilial(Convert.ToInt32(dt.Rows[i]["fil_codigo"])),
                        Ativo = new AtivoDAO().BuscarAtivo(Convert.ToInt32(dt.Rows[i]["ati_codigo"])),
                        Obs = dt.Rows[i]["iv_obs"].ToString(),
                        Imagem = new ImagemDAO().Buscar(Convert.ToInt32(dt.Rows[i]["img_codigo"]))                        
                    });
                }
            }

            return Dados == null ? null : Dados;
            
        }

        internal List<object> Buscar(string dtIni, string dtFim, int regiao, int filial)
        {
            b.getComandoSQL().Parameters.Clear();

            string where = "f.reg_codigo = @reg";

            if (filial != 0)
            {
                where += " and i.fil_codigo = @fil";
                b.getComandoSQL().Parameters.AddWithValue("@fil", filial);
            }

            if(dtIni != "")
            {
                if(dtFim != "")
                {
                    where += " and iv_date between @dtIni and @dtFim";
                    b.getComandoSQL().Parameters.AddWithValue("@dtFim", dtFim);
                }
                else
                {
                    where += " and iv_date >= @dtIni";
                }
                b.getComandoSQL().Parameters.AddWithValue("@dtIni", dtIni);
            }
            else
            {
                where += " and iv_date <= @dtFim";
                b.getComandoSQL().Parameters.AddWithValue("@dtFim", dtFim);
            }

            b.getComandoSQL().CommandText = @"select * from inventario i
                                               inner join filial f on f.fil_codigo = i.fil_codigo
                                               where "+ where + " order by fil_codigo, iv_date";

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

            return ok;
        }
    }
}
