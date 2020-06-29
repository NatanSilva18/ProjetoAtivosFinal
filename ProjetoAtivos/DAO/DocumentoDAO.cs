using ProjetoAtivos.Models;
using Survey.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAtivos.DAO
{
    public class DocumentoDAO
    {
        private Banco b;

        internal DocumentoDAO()       //pegando instancia do objeto banco
        {
            b = Banco.GetInstance();
        }

        internal List<object> TableToListCompleta(DataTable dt)
        {
            List<object> Dados = new List<object>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Dados.Add(new
                    {
                        Codigo = Convert.ToInt32(dt.Rows[i]["doc_codigo"]),
                        Nome = dt.Rows[i]["doc_nome"].ToString(),
                        Caminho = dt.Rows[i]["doc_local"].ToString()
                    });
                }
            }

            return Dados == null ? null : Dados;
        }

        internal Documento TableToObj(DataTable dt)
        {
            Documento Dados=null;

            if (dt.Rows.Count > 0)
            {
                    Dados=new Documento()
                    {
                        Codigo = Convert.ToInt32(dt.Rows[0]["doc_codigo"]),
                        Nome = dt.Rows[0]["doc_nome"].ToString(),
                        Caminho = dt.Rows[0]["doc_local"].ToString()
                    };
                
            }

            return Dados;
        }


        public List<object> Buscar(Transferencia t)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select * from documentos where transf_codigo = @transf;";
            b.getComandoSQL().Parameters.AddWithValue("@transf", t.GetCodigo());

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToListCompleta(dt);
            else
                return null;
        }

        public Documento Buscar(int Cod)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select * from documentos where doc_codigo = @doc_codigo;";
            b.getComandoSQL().Parameters.AddWithValue("@doc_codigo", Cod);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToObj(dt);
            else
                return null;
        }

        internal bool Gravar(Documento Doc, int transferencia)
        {

            Boolean OK = false;

            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"
INSERT INTO documentos
           (doc_nome, doc_local
           ,transf_codigo)
     VALUES
           (@nome, @local, @transf);
SELECT LAST_INSERT_ID();

";

            b.getComandoSQL().Parameters.AddWithValue("@nome", Doc.Nome);
            b.getComandoSQL().Parameters.AddWithValue("@local", Doc.Caminho);
            b.getComandoSQL().Parameters.AddWithValue("@transf", transferencia);

            int cod = 0;
            OK = b.ExecutaComando(true, out cod) == 1;

            Doc.Codigo = cod;

            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"
                update documentos
                          set doc_local = @local
                          where doc_codigo = @cod;
                ";

            Doc.Caminho+= cod + "&$&" + Doc.Nome;
            b.getComandoSQL().Parameters.AddWithValue("@local", Doc.Caminho);
            b.getComandoSQL().Parameters.AddWithValue("@cod", cod);

            OK = b.ExecutaComando(true) == 1;
            return OK;
        }
    }
}
