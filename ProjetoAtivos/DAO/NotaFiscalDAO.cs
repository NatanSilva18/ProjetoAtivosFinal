using ProjetoAtivos.Models;
using Survey.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAtivos.DAO
{
    public class NotaFiscalDAO
    {
        private Banco b;

        internal NotaFiscalDAO()       //pegando instancia do objeto banco
        {
            b = Banco.GetInstance();
        }

        internal List<NotaFiscal> TableToList(DataTable dt)
        {
            List<NotaFiscal> dados = null;
            if (dt != null && dt.Rows.Count > 0)
                dados = (from DataRow row in dt.Rows
                         select new NotaFiscal(
                                         Convert.ToInt32(row["ati_codigo"]),
                                         row["nt_numero"].ToString(),
                                         Convert.ToDouble(row["nt_valor"]),
                                         Convert.ToDateTime(row["nt_dataemissao"]),
                                         row["nt_fornecedor"].ToString()



                         )).ToList();

            return dados;
        }
        internal int Gravar(NotaFiscal Nota)
        {
            Boolean OK = false;
            int Codigo = 0;
            b.getComandoSQL().Parameters.Clear();

            if(Nota.GetCodigo() == 0)   //insert
            {
                b.getComandoSQL().CommandText = @"insert into nota_fiscal (nt_numero, nt_valor, nt_dataemissao, nt_fornecedor) 
                   values(@codigonota, @valor, @dataemissao, @fornecedor);
                SELECT LAST_INSERT_ID();";
            }
            else
            {
                b.getComandoSQL().CommandText = @"update nota_fiscal set nt_numero = @codigonota, nt_valor = @valor, nt_dataemissao = @dataemissao, nt_fornecedor = @fornecedor where nt_codigo = @codigo";

                b.getComandoSQL().Parameters.AddWithValue("@codigo", Nota.GetCodigo());

            }
            b.getComandoSQL().Parameters.AddWithValue("@codigonota", Nota.GetCodigoNota());
            b.getComandoSQL().Parameters.AddWithValue("@valor", Nota.GetValorNota());
            b.getComandoSQL().Parameters.AddWithValue("@dataemissao", Nota.GetDataEmissao());
            b.getComandoSQL().Parameters.AddWithValue("@fornecedor", Nota.GetFornecedor());


            if (Nota.GetCodigo() == 0)
            {
                OK = b.ExecutaComando(true, out Codigo) == 1;

                if (OK)
                    return Codigo;
                else
                    return -10;
            }
            else
            {
                OK = b.ExecutaComando(true) == 1;

                if (OK)
                    return Nota.GetCodigo();
                else
                    return -10;

            }
        }
    }
}
