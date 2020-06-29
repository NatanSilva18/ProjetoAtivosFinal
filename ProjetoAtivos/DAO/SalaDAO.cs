using ProjetoAtivos.Models;
using Survey.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProjetoAtivos.DAO
{
    public class SalaDAO
    {
        private Banco b;

        internal SalaDAO()       //pegando instancia do objeto banco
        {
            b = Banco.GetInstance();
        }
        internal List<Sala> TableToList(DataTable dt)
        {
            List<Sala> dados = null;
            if (dt != null && dt.Rows.Count > 0)
                dados = (from DataRow row in dt.Rows
                         select new Sala(
                              Convert.ToInt32(row["sal_codigo"]),
                                              row["sal_descricao"].ToString(),
                                              Convert.ToBoolean(row["sal_stativo"]),
                                              Convert.ToInt32(row["fil_codigo"]),
                                              row["fil_razao"].ToString(),
                                              Convert.ToBoolean(row["fil_stativo"])
                         )).ToList();

            return dados;
        }
        internal List<Sala> TableToListBusca(DataTable dt)
        {
            List<Sala> dados = null;
            if (dt != null && dt.Rows.Count > 0)
                dados = (from DataRow row in dt.Rows
                         select new Sala(
                              Convert.ToInt32(row["sal_codigo"]),
                                              row["sal_descricao"].ToString(),
                                              Convert.ToBoolean(row["sal_stativo"]),
                                              Convert.ToInt32(row["fil_codigo"])
                         )).ToList();

            return dados;
        }
        internal Boolean Gravar(Sala Sala)
        {
            b.getComandoSQL().Parameters.Clear();

            if (Sala.GetCodigo() == 0)
            {
                b.getComandoSQL().CommandText = @"insert into Sala (sal_descricao, sal_stativo, fil_codigo) values(@descricao, @ativo, @filial);";

            }
            else
            {
                b.getComandoSQL().CommandText = @"update Sala set sal_descricao = @descricao, sal_stativo = @ativo, fil_codigo = @filial where sal_codigo = @codigo;";
                b.getComandoSQL().Parameters.AddWithValue("@codigo", Sala.GetCodigo());
            }

            b.getComandoSQL().Parameters.AddWithValue("@descricao", Sala.GetDescricao());
            b.getComandoSQL().Parameters.AddWithValue("@ativo", Sala.GetStAtivo());
            b.getComandoSQL().Parameters.AddWithValue("@filial", Sala.GetFilial().GetCodigo());




            return b.ExecutaComando() == 1;
        }
        internal Boolean ExcluirLogico(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"update Sala set sal_stativo = 0 where sal_codigo = @codigo;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            return b.ExecutaComando() == 1;
        }
        internal Boolean Ativar(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"update Sala set sal_stativo = 1 where sal_codigo = @codigo;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            return b.ExecutaComando() == 1;
        }

        internal Sala BuscarSala(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select sal_codigo, sal_descricao, sal_stativo, fil_codigo from Sala where sal_codigo = @codigo and sal_stativo = 1";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToListBusca(dt).FirstOrDefault();
            else
                return null;
        }
        internal List<Sala> BuscarSalas(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select sal_codigo, sal_descricao, sal_stativo, fil_codigo from Sala where fil_codigo = @codigo and sal_stativo = 1";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToListBusca(dt);
            else
                return null;
        }
        internal Sala BuscarSala(string Descricao)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select sal_codigo, sal_descricao, sal_stativo, fil_codigo from Sala where sal_descricao =  @descricao and sal_stativo = 1";
            b.getComandoSQL().Parameters.AddWithValue("@descricao", Descricao);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToListBusca(dt).FirstOrDefault();
            else
                return null;
        }
        internal List<Sala> ObterSalas(string Chave, string Filtro, int Ativo)
        {
            b.getComandoSQL().Parameters.Clear();


            if (Filtro == "Filial")
            {
                if (Chave == null)
                {
                    b.getComandoSQL().CommandText = @"select s.sal_codigo, s.sal_descricao, s.sal_stativo, s.fil_codigo, f.fil_razao, f.fil_stativo from Sala s
                                                      inner join Filial f on s.fil_codigo = f.fil_codigo
                                                      where s.sal_stativo = @ativo order by s.sal_descricao;";

                }
                else
                {
                    b.getComandoSQL().CommandText = @"select s.sal_codigo, s.sal_descricao, s.sal_stativo, s.fil_codigo, f.fil_razao, f.fil_stativo from Sala s
                                                      inner join Filial f on s.fil_codigo = f.fil_codigo
                                                      where s.sal_descricao like @descricao and s.sal_stativo = @ativo order by s.sal_descricao;";
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
