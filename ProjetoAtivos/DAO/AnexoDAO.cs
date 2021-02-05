using ProjetoAtivos.Models;
using Survey.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAtivos.DAO
{
    public class AnexoDAO
    {

        private Banco b;

        internal AnexoDAO()       //pegando instancia do objeto banco
        {
            b = Banco.GetInstance();
        }
        internal List<Anexo> TableToList(DataTable dt)
        {
            List<Anexo> dados = null;
            if (dt != null && dt.Rows.Count > 0)
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

            return dados;
        }

        internal Boolean Gravar(Anexo Anexo)
        {
            if (this.Excluir(Anexo.Ativo.GetCodigo()))
            {

                b.getComandoSQL().Parameters.Clear();


                b.getComandoSQL().CommandText = @"insert into anexos_ativos (aa_local, aa_nmArquivo, aa_type, ati_codigo) values(@local, @nmArquivo, @tipo, @ativo);";



                b.getComandoSQL().Parameters.AddWithValue("@local", Anexo.Local);
                b.getComandoSQL().Parameters.AddWithValue("@nmArquivo", Anexo.Nome);
                b.getComandoSQL().Parameters.AddWithValue("@tipo", Anexo.Type);
                b.getComandoSQL().Parameters.AddWithValue("@ativo", Anexo.Ativo.GetCodigo());


                return b.ExecutaComando(true) == 1;
            }
            else
                return false;
        }

        internal Anexo Buscar(int CodigoAtivo, bool transction = false)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select *
                                              from anexos_ativos  
                                              where ati_codigo = @codigo;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", CodigoAtivo);

            DataTable dt = b.ExecutaSelect(transction);

            if (dt.Rows.Count > 0)
                return TableToList(dt).FirstOrDefault();
            else
                return null;
        }

        internal Boolean Excluir(int CodigoAtivo)
        {
            Anexo a = Buscar(CodigoAtivo);

            if (a != null)
            {
                try
                {

                    File.Delete(a.Local);


                    b.getComandoSQL().Parameters.Clear();
                    b.getComandoSQL().CommandText = @"delete from anexos_ativos where ati_codigo = @codigo;";
                    b.getComandoSQL().Parameters.AddWithValue("@codigo", CodigoAtivo);

                    return b.ExecutaComando(true) == 1;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            else
                return true;
        }

    }
}
