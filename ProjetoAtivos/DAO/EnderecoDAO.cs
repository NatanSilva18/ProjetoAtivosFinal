
using ProjetoAtivos.Models;
using Survey.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProjetoAtivos.DAO
{
    public class EnderecoDAO
    {
        private Banco b;

        internal EnderecoDAO()
        {
            b = Banco.GetInstance();
        }
        internal List<Endereco> TableToList(DataTable dt)
        {
            List<Endereco> dados = null;
            if (dt != null && dt.Rows.Count > 0)
                dados = (from DataRow row in dt.Rows
                         select new Endereco(
                              Convert.ToInt32(row["end_codigo"]),
                                              row["end_logradouro"].ToString(),
                                              Convert.ToInt32(row["end_numero"]),
                                              row["end_referencia"].ToString(),
                                              row["end_bairro"].ToString(),
                                              row["end_cep"].ToString(),
                                              row["end_cidade"].ToString(),
                                              row["end_estado"].ToString()
                         )).ToList();
            return dados;
        }
        internal int Gravar(Endereco Endereco)
        {
            int Codigo;
            b.getComandoSQL().Parameters.Clear();

            if (Endereco.GetCodigo() == 0)
            {
                b.getComandoSQL().CommandText = @"insert into Endereco (end_referencia, end_logradouro, end_numero, end_bairro,
                                                  end_cep, end_cidade, end_estado, end_stativo) values(
                                                  @referencia, @logradouro, @numero, @bairro, @cep, @cidade, @estado, 1);
                                                  SELECT LAST_INSERT_ID();";
            }
            else
            {
                b.getComandoSQL().CommandText = @"update Endereco set end_referencia = @referencia, end_logradouro = @logradouro, end_numero = @numero,
                                                  end_bairro = @bairro, end_cep = @cep, end_cidade = @cidade, end_estado = @estado
                                                  where end_codigo = @codigo;";
                b.getComandoSQL().Parameters.AddWithValue("@codigo", Endereco.GetCodigo());
            }

            b.getComandoSQL().Parameters.AddWithValue("@referencia", Endereco.GetReferencia());
            b.getComandoSQL().Parameters.AddWithValue("@logradouro", Endereco.GetLogradouro());
            b.getComandoSQL().Parameters.AddWithValue("@numero", Endereco.GetNumero());
            b.getComandoSQL().Parameters.AddWithValue("@bairro", Endereco.GetBairro());
            b.getComandoSQL().Parameters.AddWithValue("@cep", Endereco.GetCep());
            b.getComandoSQL().Parameters.AddWithValue("@cidade", Endereco.GetCidade());
            b.getComandoSQL().Parameters.AddWithValue("@estado", Endereco.GetEstado());
            if (Endereco.GetCodigo() == 0)
            {
                if (b.ExecutaComando(true, out Codigo) == 1)
                    return Codigo;
                else
                    return -10;
            }
            else
            {

                if (b.ExecutaComando(true) == 1)
                    return 10;
                else
                    return -10;
            }
        }
        internal List<Endereco> ObterEnderecos()
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().CommandText = @"select * from Endereco e 
                                                  where not exists (select * from Regional r where r.end_codigo = e.end_codigo)";


            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }
    }
}
