using ProjetoAtivos.DAO;
using System;
using System.Collections.Generic;

namespace ProjetoAtivos.Models
{
    public class Regional
    {
        private int Codigo;
        private string Descricao;
        private Pessoa Pessoa;
        private Boolean StAtivo;

        public Regional()
        {
            this.Codigo = 0;
            this.Descricao = "";
            this.Pessoa = new Pessoa();
            this.StAtivo = true;
        }
        public Regional(int Codigo)
        {
            this.Codigo = Codigo;
            this.Descricao = "";
            this.Pessoa = new Pessoa();
            this.StAtivo = true;
        }
        public Regional(int Codigo, string Descricao, Boolean StAtivo, int Pessoa)
        {
            this.Codigo = Codigo;
            this.Descricao = Descricao;
            this.Pessoa = new Pessoa(Pessoa);
            this.StAtivo = StAtivo;
        }
        public Regional(int Codigo, string Descricao, Boolean StAtivo, int PesCodigo, string PesNome)
        {
            this.Codigo = Codigo;
            this.Descricao = Descricao;
            this.Pessoa = new Pessoa(PesCodigo, PesNome);
            this.StAtivo = StAtivo;
        }

        public int GetCodigo()
        {
            return this.Codigo;
        }
        public void SetCodigo(int Codigo)
        {
            this.Codigo = Codigo;
        }
        public string GetDescricao()
        {
            return this.Descricao;
        }
        public void SetDescricao(string Descricao)
        {
            this.Descricao = Descricao;
        }
        public Pessoa GetPessoa()
        {
            return this.Pessoa;
        }
        public void SetPessoa(Pessoa Pessoa)
        {
            this.Pessoa = Pessoa;
        }
        public Boolean GetStAtivo()
        {
            return this.StAtivo;
        }
        public void SetStAtivo(Boolean StAtivo)
        {
            this.StAtivo = StAtivo;
        }
        public Boolean Gravar()
        {
            if (this.Descricao != "")
                return new RegionalDAO().Gravar(this);
            else
                return false;
        }
        public Boolean ExcluirLogico(int Codigo)
        {
            if (Codigo > 0)
                return new RegionalDAO().ExcluirLogico(Codigo);
            else
                return false;
        }
        public Boolean Ativar(int Codigo)
        {
            if (Codigo > 0)
                return new RegionalDAO().Ativar(Codigo);
            else
                return false;
        }
        public Regional BuscarRegional(int Codigo)
        {
            if (Codigo > 0)
                return new RegionalDAO().BuscarRegional(Codigo);
            else
                return null;
        }
        public Regional BuscarRegionalPessoa(int Pessoa)
        {
            
                return new RegionalDAO().BuscarRegionalPessoa(Pessoa);
            
        }

        public Regional BuscarRegional(string Descricao)
        {
            if (Descricao != "")
                return new RegionalDAO().BuscarRegional(Descricao);
            else
                return null;
        }
        public List<Regional> ObterRegionais(string Chave, string Filtro, int Ativo)
        {
            return new RegionalDAO().ObterRegionais(Chave, Filtro, Ativo);
        }
        public List<object> AtivosRegional()
        {
            return new RegionalDAO().AtivosRegional();
        }
    }
}
