using System;
using System.Collections.Generic;
using ProjetoAtivos.Models;


namespace ProjetoAtivos.Control
{
    public class UsuarioControl
    {
        public int Gravar(int Codigo, string Login, string Senha, int TipoUsuario, int Pessoa,  int Operacao)
        {
            List<Usuario> Admin = new List<Usuario>();
            Usuario User = new Usuario();
            Usuario Usuario = new Usuario(Codigo, Login, Senha, Pessoa, TipoUsuario);
            Boolean Ok = true;


            if (Operacao == 0)   //gravacao
            {
                User = new Usuario().BuscarUsuario(Login);
                if (User == null)
                {
                    User = new Usuario().BuscaUserPessoa(Pessoa);
                    if (User == null)
                    {
                        if (Usuario.Gravar())
                            return 1;
                    }
                    else
                        return -30;
                }
                else
                    return -10;
            }
            return -20;
        }
        public List<Usuario> ObterUsuarios(string Chave, string Filtro)
        {
            return new Usuario().ObterUsuarios(Chave, Filtro);
        }

        public Usuario BuscarUsuario(int Codigo)
        {
            return new Usuario().BuscarUsuario(Codigo);
        }
        public int Excluir(int Codigo)
        {
            List<Usuario> Admin = new List<Usuario>();


                if (new Usuario(Codigo).Excluir())
                    return 1;
                else
                    return -20; //erro na exclusao

        }
    }
}
