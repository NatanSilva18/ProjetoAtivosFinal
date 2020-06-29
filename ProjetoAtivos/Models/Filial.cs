using ProjetoAtivos.DAO;
using System;
using System.Collections.Generic;

namespace ProjetoAtivos.Models
{
    public class Filial
    {
        private int Codigo;
        private string Razao;
        private string Cnpj;
        private Boolean StAtivo;
        private Endereco Endereco;
        private Pessoa Responsavel;
        private Regional Regional;

        public Filial()
        {
            this.Codigo = 0;
            this.Razao = "";
            this.Cnpj = "";
            this.StAtivo = true;
            this.Endereco = new Endereco();
            this.Responsavel = new Pessoa();
            this.Regional = new Regional();
        }
        public Filial(int Codigo)
        {
            this.Codigo = Codigo;
            this.Razao = "";
            this.Cnpj = "";
            this.StAtivo = true;
            this.Endereco = new Endereco();
            this.Responsavel = new Pessoa();
            this.Regional = new Regional();
        }
        public Filial(int Codigo, string Razao, string Cnpj, Boolean StAtivo, int Endereco, int Responsavel, int Regional)
        {
            this.Codigo = Codigo;
            this.Razao = Razao;
            this.Cnpj = Cnpj;
            this.StAtivo = StAtivo;
            this.Endereco = new Endereco(Endereco);
            this.Responsavel = new Pessoa(Responsavel);
            this.Regional = new Regional(Regional);
        }
        public Filial(int Codigo, string Razao, string Cnpj, Boolean StAtivo, string EndLogradouro, int EndNumero, string EndReferencia, string EndBairro, string EndCep, string EndCidade, string EndEstado, int Responsavel, int Regional)
        {
            this.Codigo = Codigo;
            this.Razao = Razao;
            this.Cnpj = Cnpj;
            this.StAtivo = StAtivo;
            this.Endereco = new Endereco(0, EndLogradouro, EndNumero, EndReferencia, EndBairro, EndCep, EndCidade, EndEstado);
            this.Responsavel = new Pessoa(Responsavel);
            this.Regional = new Regional(Regional);
        }
        public Filial(int Codigo, string Razao, string Cnpj, Boolean StAtivo, string EndLogradouro, int EndNumero, string EndReferencia, string EndBairro, string EndCep, string EndCidade, string EndEstado, int Responsavel, string RespNome, int Regional, string RegDescicao, Boolean RegAtivo)
        {
            this.Codigo = Codigo;
            this.Razao = Razao;
            this.Cnpj = Cnpj;
            this.StAtivo = StAtivo;
            this.Endereco = new Endereco(0, EndLogradouro, EndNumero, "", EndBairro, EndCep, EndCidade, EndEstado);
            this.Responsavel = new Pessoa(Responsavel, RespNome);
            this.Regional = new Regional(Regional, RegDescicao, RegAtivo, 0);
        }
        public int GetCodigo()
        {
            return this.Codigo;
        }
        public void SetCodigo(int Codigo)
        {
            this.Codigo = Codigo;
        }
        public string GetRazao()
        {
            return this.Razao;
        }
        public void SetRazao(string Razao)
        {
            this.Razao = Razao;
        }
        public string GetCnpj()
        {
            return this.Cnpj;
        }
        public void SetCnpj(string Cnpj)
        {
            this.Cnpj = Cnpj;
        }
        public Boolean GetStativo()
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
        public Pessoa GetResponsavel()
        {
            return this.Responsavel;
        }
        public void SetResponsavel(Pessoa Responsavel)
        {
            this.Responsavel = Responsavel;
        }
        public Regional GetRegional()
        {
            return this.Regional;
        }
        public void SetRegional(Regional Regional)
        {
            this.Regional = Regional;
        }

        public Boolean Gravar()
        {
            if (this.Razao != "" && this.Cnpj != "" && this.Regional != null)
                return new FilialDAO().Gravar(this);
            else
                return false;
        }
        public Boolean ExcluirLogico(int Codigo)
        {
            if (Codigo > 0)
                return new FilialDAO().ExcluirLogico(Codigo);
            else
                return false;
        }
        public Boolean Ativar(int Codigo)
        {
            if (Codigo > 0)
                return new FilialDAO().Ativar(Codigo);
            else
                return false;
        }

        public Filial BuscarFilialPessoa(int Pessoa)
        {
           
                return new FilialDAO().BuscarFilialPessoa(Pessoa);
            
        }
        public Filial BuscarFilial(int Codigo)
        {
            if (Codigo > 0)
                return new FilialDAO().BuscarFilial(Codigo);
            else
                return null;
        }
        public List<Filial> BuscarFiliais(int Codigo)
        {
            if (Codigo > 0)
                return new FilialDAO().BuscarFiliais(Codigo);
            else
                return null;
        }
        public Filial BuscarFilial(string Descricao)
        {
            if (Descricao != "")
                return new FilialDAO().BuscarFilial(Descricao);
            else
                return null;
        }
        public List<Filial> ObterFiliais(string Chave, string Filtro, int Ativo)
        {
            return new FilialDAO().ObterFiliais(Chave, Filtro, Ativo);
        }
    }
}
