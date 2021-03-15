
using ProjetoAtivos.DAO;
using System;
using System.Collections.Generic;

namespace ProjetoAtivos.Models
{
    public class Usuario
    {
        private int Codigo;
        private string Login;
        private string Senha;
        private Boolean Logado;
        private Pessoa Pessoa;
        private TipoUsuario TipoUsuario;

        public Usuario(int Codigo, string Login, string Senha, Boolean Logado, int Pessoa, int TipoUsuario)
        {
            this.Codigo = Codigo;
            this.Login = Login;
            this.Senha = Senha;
            this.Logado = Logado;
            this.Pessoa = new Pessoa(Pessoa);
            this.TipoUsuario = new TipoUsuario(TipoUsuario);
        }
        public Usuario(int Codigo, string Login, string Senha, Boolean Logado, int Pessoa, string Nome, int TipoUsuario)
        {
            this.Codigo = Codigo;
            this.Login = Login;
            this.Senha = Senha;
            this.Logado = Logado;
            this.Pessoa = new Pessoa(Pessoa, Nome);
            this.TipoUsuario = new TipoUsuario(TipoUsuario);
        }
        public Usuario(int Codigo)
        {
            this.Codigo = Codigo;
            this.Login = "";
            this.Senha = "";
            this.Logado = false;
            this.Pessoa = new Pessoa();
            this.TipoUsuario = new TipoUsuario();
        }
        public Usuario()
        {
            this.Codigo = 0;
            this.Login = "";
            this.Senha = "";
            this.Logado = false;
            this.Pessoa = new Pessoa();
            this.TipoUsuario = new TipoUsuario();
        }
        public int GetCodigo()
        {
            return this.Codigo;
        }
        public void SetCodigo(int Codigo)
        {
            this.Codigo = Codigo;
        }
        public string GetLogin()
        {
            return this.Login;
        }
        public void SetLogin(string Login)
        {
            this.Login = Login;
        }
        public string GetSenha()
        {
            return this.Senha;
        }
        public void SetSenha(string Senha)
        {
            this.Senha = Senha;
        }
        public Boolean GetLogado()
        {
            return this.Logado;
        }
        public void SetLogado(Boolean Logado)
        {
            this.Logado = Logado;
        }
        public Pessoa GetPessoa()
        {
            return this.Pessoa;
        }
        public void SetPessoa(Pessoa Pessoa)
        {
            this.Pessoa = Pessoa;
        }
        public TipoUsuario GetTipoUsuario()
        {
            return this.TipoUsuario;
        }
        public void SetTipoUsuario(TipoUsuario TipoUsuario)
        {
            this.TipoUsuario = TipoUsuario;
        }
        public Usuario BuscarUsuario(string Login, string Senha)
        {
            return new UsuarioDAO().BuscarUsuario(Login, Senha);
        }
        public Boolean Gravar()
        {
            return new UsuarioDAO().Gravar(this);
        }
        public Boolean Excluir()
        {
            return new UsuarioDAO().Excluir(this);
        }
        public Usuario BuscarUsuario(int Codigo)
        {
            return new UsuarioDAO().BuscarUsuario(Codigo);
        }
        public Usuario BuscarUsuario(string Login)
        {
            return new UsuarioDAO().BuscarUsuario(Login);
        }
        public Usuario BuscaUserPessoa(int Codigo)
        {
            return new UsuarioDAO().BuscaUserPessoa(Codigo);

        }

        public List<Usuario> ObterUsuarios(string Chave, string Filtro)
        {
            return new UsuarioDAO().ObterUsuarios(Chave, Filtro);
        }
        public Boolean AlterarStatusLogin(int Codigo, Boolean Status)
        {
            return new UsuarioDAO().AlterarStatusLogin(Codigo, Status);
        }
    }
}
