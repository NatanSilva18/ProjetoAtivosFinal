using ProjetoAtivos.Models;
using Survey.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAtivos.DAO
{
    public class MotivoDAO
    {
        private Banco b;

        internal MotivoDAO()
        {
            b = Banco.GetInstance();
        }
        internal List<Motivo> TableToList(DataTable dt)
        {
            List<Motivo> dados = null;
            if (dt != null && dt.Rows.Count > 0)
                dados = (from DataRow row in dt.Rows
                         select new Motivo(
                              Convert.ToInt32(row["mot_codigo"]),
                                              row["mot_descricao"].ToString(),
                                              Convert.ToBoolean(row["mot_stativo"])
                         )).ToList();
            return dados;
        }

        internal List<Motivo> TableToListBusca(DataTable dt)
        {
            List<Motivo> dados = null;
            if (dt != null && dt.Rows.Count > 0)
                dados = (from DataRow row in dt.Rows
                         select new Motivo(
                              Convert.ToInt32(row["mot_codigo"]),
                                              row["mot_descricao"].ToString(),
                                              Convert.ToBoolean(row["mot_stativo"])
                         )).ToList();

            return dados;
        }

        internal Boolean Gravar(Motivo Motivo)
        {
            b.getComandoSQL().Parameters.Clear();

            if (Motivo.GetCodigo() == 0)
            {
                b.getComandoSQL().CommandText = @"insert into Motivo (mot_descricao, mot_stativo) values(@descricao, @ativo);";

            }
            else
            {
                b.getComandoSQL().CommandText = @"update Motivo set mot_descricao = @descricao, mot_stativo = @ativo where mot_codigo = @codigo;";
                b.getComandoSQL().Parameters.AddWithValue("@codigo", Motivo.GetCodigo());
            }

            b.getComandoSQL().Parameters.AddWithValue("@descricao", Motivo.GetDescricao());
            b.getComandoSQL().Parameters.AddWithValue("@ativo", Motivo.GetStAtivo());




            return b.ExecutaComando() == 1;
        }

        internal Boolean ExcluirLogico(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"update Motivo set mot_stativo = 0 where mot_codigo = @codigo;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            return b.ExecutaComando() == 1;
        }
        internal Boolean Ativar(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"update Motivo set mot_stativo = 1 where mot_codigo = @codigo;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            return b.ExecutaComando() == 1;
        }

        internal Motivo BuscarMotivo(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select mot_codigo, mot_descricao, mot_stativo from Motivo where mot_codigo = @codigo and mot_stativo = 1;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToListBusca(dt).FirstOrDefault();
            else
                return null;
        }

        internal Motivo BuscarMotivo(string Descricao)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select mot_codigo, mot_descricao, mot_stativo from Motivo where mot_descricao =  @descricao and mot_stativo = 1;";
            b.getComandoSQL().Parameters.AddWithValue("@descricao", Descricao);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToListBusca(dt).FirstOrDefault();
            else
                return null;
        }

        internal List<Motivo> ObterMotivos(string Chave, string Filtro, int Ativo)
        {
            b.getComandoSQL().Parameters.Clear();


            if (Filtro == "Descricao")
            {
                if (Chave == null)
                {
                    b.getComandoSQL().CommandText = @"select mot_codigo, mot_descricao, mot_stativo from Motivo where mot_stativo = @ativo order by mot_descricao;";

                }
                else
                {
                    b.getComandoSQL().CommandText = @"select mot_codigo, mot_descricao, mot_stativo from Motivo where mot_stativo = @ativo and mot_descricao like @descricao order by mot_descricao;";
                    b.getComandoSQL().Parameters.AddWithValue("@descricao", "%" + Chave + "%");
                }
            }

            b.getComandoSQL().Parameters.AddWithValue("@ativo", Ativo);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }
    }
}
