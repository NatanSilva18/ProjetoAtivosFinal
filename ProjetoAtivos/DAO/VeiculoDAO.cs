using ProjetoAtivos.Models;
using Survey.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAtivos.DAO
{
    public class VeiculoDAO
    {
        private Banco b;

        internal VeiculoDAO()       //pegando instancia do objeto banco
        {
            b = Banco.GetInstance();
        }
        internal List<Veiculo> TableToList(DataTable dt)
        {
            List<Veiculo> dados = null;
           /* if (dt != null && dt.Rows.Count > 0)
                dados = (from DataRow row in dt.Rows
                         select new Anexo()
                         {
                             Codigo = Convert.ToInt32(row["aa_codigo"]),
                             Local = row["aa_local"].ToString(),
                             Nome = row["aa_nmArquivo"].ToString(),
                             Type = row["aa_type"].ToString(),
                             Ativo = new Ativo(Convert.ToInt32(row["ati_codigo"]))
                         }
                         ).ToList();
           */
            return dados;
        }

        internal Boolean Gravar(Anexo Anexo)
        {
            
                b.getComandoSQL().Parameters.Clear();


                b.getComandoSQL().CommandText = @"insert into anexos_ativos (aa_local, aa_nmArquivo, aa_type, ati_codigo) values(@local, @nmArquivo, @tipo, @ativo);";



                b.getComandoSQL().Parameters.AddWithValue("@local", Anexo.Local);
                b.getComandoSQL().Parameters.AddWithValue("@nmArquivo", Anexo.Nome);
                b.getComandoSQL().Parameters.AddWithValue("@tipo", Anexo.Type);
                b.getComandoSQL().Parameters.AddWithValue("@ativo", Anexo.Ativo.GetCodigo());


                return b.ExecutaComando(true) == 1;
            
        }

    }
}
