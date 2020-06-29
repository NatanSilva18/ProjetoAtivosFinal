using ProjetoAtivos.DAO;
using System;
using System.Collections.Generic;

namespace ProjetoAtivos.Models
{
    public class Sala
    {
        private int Codigo;
        private string Descricao;
        private Boolean StAtivo;
        private Filial Filial;

        public Sala()
        {
            this.Codigo = 0;
            this.Descricao = "";
            this.StAtivo = true;
            this.Filial = new Filial();
        }
        public Sala(int Codigo)
        {
            this.Codigo = Codigo;
            this.Descricao = "";
            this.StAtivo = true;
            this.Filial = new Filial();
        }
        public Sala(int Codigo, string Descricao, Boolean StAtivo, int Filial)
        {
            this.Codigo = Codigo;
            this.Descricao = Descricao;
            this.StAtivo = StAtivo;
            this.Filial = new Filial(Filial);
        }
        public Sala(int Codigo, string Descricao, Boolean StAtivo, int Filial, string Razao, Boolean FilAtivo)
        {
            this.Codigo = Codigo;
            this.Descricao = Descricao;
            this.StAtivo = StAtivo;
            this.Filial = new Filial(Filial); this.Filial.SetRazao(Razao); this.Filial.SetStAtivo(FilAtivo);
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
        public Boolean GetStAtivo()
        {
            return this.StAtivo;
        }
        public void SetStAtivo(Boolean StAtivo)
        {
            this.StAtivo = StAtivo;
        }
        public Filial GetFilial()
        {
            return this.Filial;
        }
        public void SetFilial(Filial Filial)
        {
            this.Filial = Filial;
        }

        public Boolean Gravar()
        {
            if (this.Descricao != "")
                return new SalaDAO().Gravar(this);
            else
                return false;
        }
        public Boolean ExcluirLogico(int Codigo)
        {
            if (Codigo > 0)
                return new SalaDAO().ExcluirLogico(Codigo);
            else
                return false;
        }
        public Boolean Ativar(int Codigo)
        {
            if (Codigo > 0)
                return new SalaDAO().Ativar(Codigo);
            else
                return false;
        }
        public Sala BuscarSala(int Codigo)
        {
            if (Codigo > 0)
                return new SalaDAO().BuscarSala(Codigo);
            else
                return null;
        }
        public List<Sala> BuscarSalas(int Codigo)
        {
            if (Codigo > 0)
                return new SalaDAO().BuscarSalas(Codigo);
            else
                return null;
        }
        public Sala BuscarSala(string Descricao)
        {
            if (Descricao != "")
                return new SalaDAO().BuscarSala(Descricao);
            else
                return null;
        }
        public List<Sala> ObterSalas(string Chave, string Filtro, int Ativo)
        {
            return new SalaDAO().ObterSalas(Chave, Filtro, Ativo);
        }
    }
}
