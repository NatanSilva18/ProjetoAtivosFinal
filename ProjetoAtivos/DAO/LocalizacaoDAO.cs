using ProjetoAtivos.Models;
using Survey.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProjetoAtivos.DAO
{
    public class LocalizacaoDAO
    {
        private Banco b;
        internal LocalizacaoDAO()
        {
            b = Banco.GetInstance();
        }

        internal List<Localizacao> TableToList(DataTable dt)
        {
            List<Localizacao> dados = null;
            if (dt != null && dt.Rows.Count > 0)
                dados = (from DataRow row in dt.Rows
                         select new Localizacao(
                                              row["loca_latitude"].ToString(),
                                              row["loca_longitude"].ToString(),
                                              Convert.ToInt32(row["img_codigo"])

                         )).ToList();
            return dados;
        }

        internal Boolean Gravar(Localizacao Localizacao)
        {
            Boolean Ok = false;
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().CommandText = @"insert into localizacao (loca_latitude, loca_longitude, img_codigo) values (@latitude, @longitude, @imagem);";
            b.getComandoSQL().Parameters.AddWithValue("@latitude", Localizacao.GetLatitude());
            b.getComandoSQL().Parameters.AddWithValue("@longitude", Localizacao.GetLongitude());
            b.getComandoSQL().Parameters.AddWithValue("@imagem", Localizacao.GetImagem().GetCodigo());

            return Ok = b.ExecutaComando(true) == 1;
        }
        internal Localizacao BuscarLocalizacao(int Ordem)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select l.loca_latitude, l.loca_longitude, l.img_codigo from localizacao l
                                              inner join imagem i on l.img_codigo = i.img_codigo
                                              inner join Ativos a on i.ati_codigo = a.ati_codigo
                                              where a.ati_codigo = @codigo;";

            b.getComandoSQL().Parameters.AddWithValue("@codigo", Ordem);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0 && dt != null)
                return TableToList(dt).FirstOrDefault();
            else
                return null;
        }

        internal Boolean Excluir(int CodigoImagem)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"delete from localizacao where img_codigo = @codigo;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", CodigoImagem);

            return b.ExecutaComando(true) == 1;
        }
    }
}
