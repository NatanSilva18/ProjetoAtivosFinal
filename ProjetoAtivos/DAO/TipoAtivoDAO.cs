using ProjetoAtivos.Models;
using Survey.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProjetoAtivos.DAO
{
    public class TipoAtivoDAO
    {
        private Banco b;

        internal TipoAtivoDAO()
        {
            b = Banco.GetInstance();
        }
        internal List<TipoAtivo> TableToList(DataTable dt)
        {
            List<TipoAtivo> dados = null;
            if (dt != null && dt.Rows.Count > 0)
                dados = (from DataRow row in dt.Rows
                         select new TipoAtivo(
                              Convert.ToInt32(row["tpa_codigo"]),
                                              row["tpa_descricao"].ToString(),
                                              Convert.ToDouble(row["tpa_valor"]),
                                              Convert.ToBoolean(row["tpa_stativo"])
                         )).ToList();
            return dados;
        }

        internal List<TipoAtivo> TableToListBusca(DataTable dt)
        {
            List<TipoAtivo> dados = null;
            if (dt != null && dt.Rows.Count > 0)
                dados = (from DataRow row in dt.Rows
                         select new TipoAtivo(
                              Convert.ToInt32(row["tpa_codigo"]),
                                              row["tpa_descricao"].ToString(),
                                              Convert.ToDouble(row["tpa_valor"]),
                                              Convert.ToBoolean(row["tpa_stativo"])
                         )).ToList();

            return dados;
        }

        internal Boolean Gravar(TipoAtivo tipoAtivo)
        {
            b.getComandoSQL().Parameters.Clear();

            if (tipoAtivo.GetCodigo() == 0)
            {
                b.getComandoSQL().CommandText = @"insert into Tipo_Ativo (tpa_descricao,tpa_valor, tpa_stativo) values(@descricao,@valor, @ativo);";

            }
            else
            {
                b.getComandoSQL().CommandText = @"update Tipo_Ativo set tpa_descricao = @descricao, tpa_valor = @valor, tpa_stativo = @ativo where tpa_codigo = @codigo;";
                b.getComandoSQL().Parameters.AddWithValue("@codigo", tipoAtivo.GetCodigo());
            }

            b.getComandoSQL().Parameters.AddWithValue("@descricao", tipoAtivo.GetDescricao());
            b.getComandoSQL().Parameters.AddWithValue("@ativo", tipoAtivo.GetStAtivo());
            b.getComandoSQL().Parameters.AddWithValue("@valor", tipoAtivo.GetValor());


            return b.ExecutaComando() == 1;
        }

        internal Boolean ExcluirLogico(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"update Tipo_Ativo set tpa_stativo = 0 where tpa_codigo = @codigo;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            return b.ExecutaComando() == 1;
        }
        internal Boolean Ativar(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"update Tipo_Ativo set tpa_stativo = 1 where tpa_codigo = @codigo;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            return b.ExecutaComando() == 1;
        }

        internal TipoAtivo BuscarTipoAtivo(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select tpa_codigo, tpa_descricao, tpa_valor, tpa_stativo from Tipo_Ativo where tpa_codigo = @codigo and tpa_stativo = 1;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToListBusca(dt).FirstOrDefault();
            else
                return null;
        }

        internal TipoAtivo BuscarTipoAtivo(string Descricao)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select tpa_codigo, tpa_descricao, tpa_valor, tpa_stativo from Tipo_Ativo where tpa_descricao =  @descricao and tpa_stativo = 1;";
            b.getComandoSQL().Parameters.AddWithValue("@descricao", Descricao);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToListBusca(dt).FirstOrDefault();
            else
                return null;
        }

        internal List<TipoAtivo> ObterTiposAtivos(string Chave, string Filtro, int Ativo)
        {
            b.getComandoSQL().Parameters.Clear();


            if (Filtro == "Descricao")
            {
                if (Chave == null)
                {
                    b.getComandoSQL().CommandText = @"select tpa_codigo, tpa_descricao, tpa_valor, tpa_stativo from Tipo_Ativo where tpa_stativo = @ativo order by tpa_descricao;";

                }
                else
                {
                    b.getComandoSQL().CommandText = @"select tpa_codigo, tpa_descricao, tpa_valor, tpa_stativo from Tipo_Ativo where tpa_stativo = @ativo and tpa_descricao like @descricao order by tpa_descricao;";
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
