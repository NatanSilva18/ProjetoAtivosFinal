using ProjetoAtivos.Models;
using Survey.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProjetoAtivos.DAO
{
    public class RegionalDAO
    {
        private Banco b;

        internal RegionalDAO()       //pegando instancia do objeto banco
        {
            b = Banco.GetInstance();
        }
        internal List<Regional> TableToList(DataTable dt)
        {
            List<Regional> dados = null;
            if (dt != null && dt.Rows.Count > 0)
                dados = (from DataRow row in dt.Rows
                         select new Regional(
                              Convert.ToInt32(row["reg_codigo"]),
                                              row["reg_descricao"].ToString(),
                                              Convert.ToBoolean(row["reg_stativo"]),
                                              Convert.ToInt32(row["pes_codigo"]),
                                              row["pes_nome"].ToString()
                         )).ToList();

            return dados;
        }
        internal Boolean Gravar(Regional Regional)
        {
            b.getComandoSQL().Parameters.Clear();

            if (Regional.GetCodigo() == 0)
            {
                b.getComandoSQL().CommandText = @"insert into Regional (reg_descricao, reg_stativo, pes_codigo) values(@descricao, @ativo, @pessoa);";

            }
            else
            {
                b.getComandoSQL().CommandText = @"update Regional set reg_descricao = @descricao, pes_codigo = @pessoa, reg_stativo = @ativo where reg_codigo = @codigo;";
                b.getComandoSQL().Parameters.AddWithValue("@codigo", Regional.GetCodigo());
            }

            b.getComandoSQL().Parameters.AddWithValue("@descricao", Regional.GetDescricao());
            b.getComandoSQL().Parameters.AddWithValue("@ativo", Regional.GetStAtivo());
            b.getComandoSQL().Parameters.AddWithValue("@pessoa", Regional.GetPessoa().GetCodigo());




            return b.ExecutaComando() == 1;
        }

        internal Boolean ExcluirLogico(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"update Regional set reg_stativo = 0 where reg_codigo = @codigo;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            return b.ExecutaComando() == 1;
        }
        internal Boolean Ativar(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"update Regional set reg_stativo = 1 where reg_codigo = @codigo;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            return b.ExecutaComando() == 1;
        }

        internal Regional BuscarRegionalPessoa(int Pessoa)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select r.reg_codigo, r.reg_descricao, r.reg_stativo, p.pes_codigo, p.pes_nome from Regional r
                                              inner join Pessoa p on p.pes_codigo = r.pes_codigo
                                              where r.reg_stativo = 1 and r.pes_codigo = @pes;";
            b.getComandoSQL().Parameters.AddWithValue("@pes", Pessoa);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToList(dt).FirstOrDefault();
            else
                return null;
        }

        internal Regional BuscarRegional(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select r.reg_codigo, r.reg_descricao, r.reg_stativo, p.pes_codigo, p.pes_nome from Regional r
                                              inner join Pessoa p on p.pes_codigo = r.pes_codigo
                                              where r.reg_codigo = @codigo and r.reg_stativo = 1;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToList(dt).FirstOrDefault();
            else
                return null;
        }
        internal Regional BuscarRegional(string Descricao)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select r.reg_codigo, r.reg_descricao, r.reg_stativo, p.pes_codigo, p.pes_nome from Regional r
                                              inner join Pessoa p on p.pes_codigo = r.pes_codigo 
                                              where r.reg_descricao = @descricao and r.reg_stativo = 1;";
            b.getComandoSQL().Parameters.AddWithValue("@descricao", Descricao);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToList(dt).FirstOrDefault();
            else
                return null;
        }
        internal List<Regional> ObterRegionais(string Chave, string Filtro, int Ativo)
        {
            b.getComandoSQL().Parameters.Clear();


            if (Filtro == "Descricao")
            {
                if (Chave == null)
                {
                    b.getComandoSQL().CommandText = @"select r.reg_codigo, r.reg_descricao, r.reg_stativo, p.pes_codigo, p.pes_nome from Regional r
                                                      inner join Pessoa p on p.pes_codigo = r.pes_codigo 
                                                      where r.reg_stativo = @ativo order by r.reg_descricao;";

                }
                else
                {
                    b.getComandoSQL().CommandText = @"select r.reg_codigo, r.reg_descricao, r.reg_stativo, p.pes_codigo, p.pes_nome from Regional r
                                                      inner join Pessoa p on p.pes_codigo = r.pes_codigo
                                                      where reg_descricao like @descricao and reg_stativo = @ativo order by reg_descricao;";
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
        internal List<object> AtivosRegional()
        {
            List<object> Dados = new List<object>();
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select r.reg_codigo, r.reg_descricao, count(*) as Cont, sum(a.ati_valor) as Valor
                                              from Regional r
                                              inner join Filial f on f.reg_codigo = r.reg_codigo
                                              inner join Sala s on f.fil_codigo = s.fil_codigo
                                              inner join Ativos a on a.sal_codigo = s.sal_codigo
                                              where a.ati_stativo = 1
                                              group by r.reg_descricao;";

            DataTable dt = b.ExecutaSelect();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Dados.Add(new
                {
                    Codigo = Convert.ToInt32(dt.Rows[i]["reg_codigo"]),
                    Quantidade = Convert.ToInt32(dt.Rows[i]["Cont"]),
                    Regional = dt.Rows[i]["reg_descricao"].ToString(),
                    Soma = Convert.ToDouble(dt.Rows[i]["Valor"])
                });
            }

            return Dados == null ? null : Dados;

        }
    }
}
