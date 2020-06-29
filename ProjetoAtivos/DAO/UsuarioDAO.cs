
using ProjetoAtivos.Models;
using Survey.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProjetoAtivos.DAO
{
    public class UsuarioDAO
    {
        private Banco b;
        private static UsuarioDAO u;

        internal UsuarioDAO()       //pegando instancia do objeto banco
        {
            b = Banco.GetInstance();
        }
        public static UsuarioDAO GetInstance() //padrao de projeto singleton
        {
            if (u == null)
                u = new UsuarioDAO();

            return u;
        }
        internal List<Usuario> TableToList(DataTable dt)
        {
            List<Usuario> dados = null;
            if (dt != null && dt.Rows.Count > 0)
                dados = (from DataRow row in dt.Rows
                         select new Usuario(
                              Convert.ToInt32(row["user_codigo"]),
                                              row["user_login"].ToString(),
                                              row["user_senha"].ToString(),
                                              Convert.ToInt32(row["pes_codigo"]),
                                              row["pes_nome"].ToString(),
                                              Convert.ToInt32(row["tpu_codigo"])
                         )).ToList();

            return dados;
        }
        internal Usuario BuscarUsuario(string Login, string Senha)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().CommandText = @"select u.user_codigo, u.user_login, u.user_senha, u.pes_codigo, u.tpu_codigo, p.pes_nome
                                              from usuario u inner join Pessoa p on p.pes_codigo = u.pes_codigo
                                              where u.user_login = @login and u.user_senha = @senha ;";
            b.getComandoSQL().Parameters.AddWithValue("@login", Login);
            b.getComandoSQL().Parameters.AddWithValue("@senha", Senha);

            DataTable dt = b.ExecutaSelect();

            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt).FirstOrDefault();
            else
                return null;
        }

        internal List<Usuario> ObterUsuarios(string Chave, string Filtro)
        {

            b.getComandoSQL().Parameters.Clear();

            if (Chave == "" || Chave == null)
            {
                b.getComandoSQL().CommandText = @"select u.user_codigo, u.user_login, u.user_senha, u.pes_codigo, tpu_codigo, p.pes_nome
                                                      from usuario u
                                                      inner join Pessoa p on u.pes_codigo = p.pes_codigo
                                                      order by user_login";
            }
            else
            {
                if (Filtro == "Email")
                {
                    b.getComandoSQL().CommandText = @"select u.user_codigo, u.user_login, u.user_senha, u.pes_codigo, u.tpu_codigo, p.pes_nome
                                                      from usuario u
                                                      inner join Pessoa p on u.pes_codigo = p.pes_codigo
                                                      where u.user_login like @login
                                                      order by p.pes_nome;";
                    b.getComandoSQL().Parameters.AddWithValue("@login", "%" + Chave + "%");
                }
                if (Filtro == "Pessoa")
                {
                    b.getComandoSQL().CommandText = @"select u.user_codigo, u.user_login, u.user_senha, u.pes_codigo, u.tpu_codigo,  p.pes_nome
                                                      from usuario u
                                                      inner join Pessoa p on u.pes_codigo = p.pes_codigo
                                                      where p.pes_nome like @nome
                                                      order by p.pes_nome;";
                    b.getComandoSQL().Parameters.AddWithValue("@nome", "%" + Chave + "%");
                }
            }


            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }
        /// <summary>
        /// //////////////////////arrumado ate aquiiii .... 
        /// </summary>
        /// <param name="Usuario"></param>
        /// <param name="Transacao"></param>
        /// <returns></returns>

        internal Boolean Gravar(Usuario Usuario)
        {
            Boolean Ok = false;

            b.getComandoSQL().Parameters.Clear();
            if (Usuario.GetCodigo() == 0)        //insert
            {
                b.getComandoSQL().CommandText = @"insert into usuario (user_login, user_senha, pes_codigo, tpu_codigo) values
                                        (@login, @senha, @pessoa, @tipousuario);";
            }
            else                              //update
            {
                b.getComandoSQL().CommandText = @"update usuario Set user_login = @login, user_senha = @senha, pes_codigo = @pessoa, tpu_codigo = @tipousuario
                                                    where user_codigo = @codigo;";
                b.getComandoSQL().Parameters.AddWithValue("@codigo", Usuario.GetCodigo());
            }

            b.getComandoSQL().Parameters.AddWithValue("@login", Usuario.GetLogin());
            b.getComandoSQL().Parameters.AddWithValue("@senha", Usuario.GetSenha());
            b.getComandoSQL().Parameters.AddWithValue("@pessoa", Usuario.GetPessoa().GetCodigo());
            b.getComandoSQL().Parameters.AddWithValue("@tipousuario", Usuario.GetTipoUsuario().GetCodigo());

            Ok = b.ExecutaComando() == 1;

            return Ok;

        }
        internal Boolean Excluir(Usuario Usuario)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().CommandText = @"delete from usuario where user_codigo = @codigo;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Usuario.GetCodigo());

            return b.ExecutaComando() == 1;
        }
        internal Usuario BuscarUsuario(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().CommandText = @"select u.user_codigo, u.user_login, u.user_senha, u.pes_codigo,u.tpus_codigo, p.pes_nome 
                                              from usuario u inner join pessoa p on p.pes_codigo = u.pes_codigo 
                                              where u.user_codigo = @codigo;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            DataTable dt = b.ExecutaSelect();
            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt).FirstOrDefault();
            else
                return null;
        }
        internal Usuario BuscarUsuario(string Login)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().CommandText = @"select u.user_codigo, u.user_login, u.user_senha, u.pes_codigo, u.tpu_codigo, p.pes_nome
                                              from usuario u inner join Pessoa p on p.pes_codigo = u.pes_codigo 
                                              where u.user_login = @login;";
            b.getComandoSQL().Parameters.AddWithValue("@login", Login);

            DataTable dt = b.ExecutaSelect();

            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt).FirstOrDefault();
            else
                return null;

        }
        internal Usuario BuscaUserPessoa(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().CommandText = @"select * from usuario where pes_codigo = @codigo;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            DataTable dt = b.ExecutaSelect();

            if (dt != null && dt.Rows.Count > 0)
                return TableToList(dt).FirstOrDefault();
            else
                return null;
        }
     

      
    }
}
