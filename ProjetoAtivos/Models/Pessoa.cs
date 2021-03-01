using ProjetoAtivos.DAO;
using System;
using System.Collections.Generic;

namespace ProjetoAtivos.Models
{
    public class Pessoa
    {
        private int Codigo;
        private string Matricula;
        private string Nome;
        private string Email;
        private string Cargo;
        private string Telefone1;
        private string Telefone2;
        private Boolean StAtivo;
        private Endereco Endereco;

        public Pessoa()
        {
            this.Codigo = 0;
            this.Matricula = "";
            this.Nome = "";
            this.Email = "";
            this.Cargo = "";
            this.Telefone1 = "";
            this.Telefone2 = "";
            this.StAtivo = true;
            this.Endereco = new Endereco();
        }
        public Pessoa(int Codigo)
        {
            this.Codigo = Codigo;
            this.Matricula = "";
            this.Nome = "";
            this.Email = "";
            this.Cargo = "";
            this.Telefone1 = "";
            this.Telefone2 = "";
            this.StAtivo = true;
            this.Endereco = new Endereco();
        }
        public Pessoa(int Codigo, string Nome)
        {
            this.Codigo = Codigo;
            this.Nome = Nome;
            this.Matricula = "";
            this.Email = "";
            this.Cargo = "";
            this.Telefone1 = "";
            this.Telefone2 = "";
            this.StAtivo = true;
            this.Endereco = new Endereco();
        }
        public Pessoa(int Codigo, string Matricula, string Nome, string Email, string Cargo, string Telefone1, string Telefone2, Boolean StAtivo, int Endereco)
        {
            this.Codigo = Codigo;
            this.Matricula = Matricula;
            this.Nome = Nome;
            this.Email = Email;
            this.Cargo = Cargo;
            this.Telefone1 = Telefone1;
            this.Telefone2 = Telefone2;
            this.StAtivo = StAtivo;
            this.Endereco = new Endereco(Endereco);
        }

        public Pessoa(int Codigo, string Matricula, string Nome, string Email, string Cargo, string Telefone1, string Telefone2, Boolean StAtivo, string EndLogradouro, int EndNumero, string EndReferencia, string EndBairro, string EndCep, string EndCidade, string EndEstado)
        {
            this.Codigo = Codigo;
            this.Matricula = Matricula;
            this.Nome = Nome;
            this.Email = Email;
            this.Cargo = Cargo;
            this.Telefone1 = Telefone1;
            this.Telefone2 = Telefone2;
            this.StAtivo = StAtivo;
            this.Endereco = new Endereco(0, EndLogradouro, EndNumero, EndReferencia, EndBairro, EndCep, EndCidade, EndEstado);

        }

        public int GetCodigo()
        {
            return this.Codigo;
        }
        public void SetCodigo(int Codigo)
        {
            this.Codigo = Codigo;
        }
        public string GetMatricula()
        {
            return this.Matricula;
        }
        public void SetMatricula(string Matricula)
        {
            this.Matricula = Matricula;
        }
        public string GetNome()
        {
            return this.Nome;
        }
        public void SetNome(string Nome)
        {
            this.Nome = Nome;
        }
        public string GetEmail()
        {
            return this.Email;
        }
        public void SetEmail(string Email)
        {
            this.Email = Email;
        }
        public string GetCargo()
        {
            return this.Cargo;
        }
        public void SetCargo(string Cargo)
        {
            this.Cargo = Cargo;
        }
        public string GetTelefone1()
        {
            return this.Telefone1;
        }
        public void SetTelefone1(string Telefone1)
        {
            this.Telefone1 = Telefone1;
        }
        public string GetTelefone2()
        {
            return this.Telefone2;
        }
        public void SetTelefone2(string Telefone2)
        {
            this.Telefone2 = Telefone2;
        }

        public Boolean GetStAtivo()
        {
            return this.StAtivo;
        }
        public void SetStAtivo(Boolean StAtivo)
        {
            this.StAtivo = StAtivo;
        }
        public Endereco GetEndereco()
        {
            return this.Endereco;
        }
        public void SetEndereco(Endereco Endereco)
        {
            this.Endereco = Endereco;
        }
        public Boolean Gravar()
        {
            if (this.Nome != "" && this.Matricula != "")
                return new PessoaDAO().Gravar(this);
            else
                return false;
        }
        public Boolean ExcluirLogico(int Codigo)
        {
            if (Codigo > 0)
                return new PessoaDAO().ExcluirLogico(Codigo);
            else
                return false;
        }
        public Boolean Ativar(int Codigo)
        {
            if (Codigo > 0)
                return new PessoaDAO().Ativar(Codigo);
            else
                return false;
        }
        public Pessoa BuscarPessoa(int Codigo)
        {
            if (Codigo > 0)
                return new PessoaDAO().BuscarPessoa(Codigo);
            else
                return null;
        }
        public Pessoa BuscarPessoa(string Descricao)
        {
            if (Descricao != "")
                return new PessoaDAO().BuscarPessoa(Descricao);
            else
                return null;
        }
        public List<Pessoa> ObterPessoas(string Chave, string Filtro, int Ativo)
        {
            return new PessoaDAO().ObterPessoas(Chave, Filtro, Ativo);
        }
    }
}
