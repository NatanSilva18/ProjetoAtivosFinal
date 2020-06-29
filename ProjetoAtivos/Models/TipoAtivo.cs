using ProjetoAtivos.DAO;
using System;
using System.Collections.Generic;

namespace ProjetoAtivos.Models
{
    public class TipoAtivo
    {
        private int Codigo;
        private string Descricao;
        private Boolean StAtivo;

        public TipoAtivo()
        {
            this.Codigo = 0;
            this.Descricao = "";
            this.StAtivo = true;
        }
        public TipoAtivo(int Codigo, string Descricao, Boolean StAtivo)
        {
            this.Codigo = Codigo;
            this.Descricao = Descricao;
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
                return new TipoAtivoDAO().Gravar(this);
            else
                return false;
        }

        public Boolean ExcluirLogico(int Codigo)
        {
            if (Codigo > 0)
                return new TipoAtivoDAO().ExcluirLogico(Codigo);
            else
                return false;
        }
        public Boolean Ativar(int Codigo)
        {
            if (Codigo > 0)
                return new TipoAtivoDAO().Ativar(Codigo);
            else
                return false;
        }
        public TipoAtivo BuscarTipoAtivo(int Codigo)
        {
            if (Codigo > 0)
                return new TipoAtivoDAO().BuscarTipoAtivo(Codigo);
            else
                return null;
        }

        public TipoAtivo BuscarTipoAtivo(string Descricao)
        {
            if (Descricao != "")
                return new TipoAtivoDAO().BuscarTipoAtivo(Descricao);
            else
                return null;
        }

        public List<TipoAtivo> ObterTiposAtivos(string Chave, string Filtro, int Ativo)
        {
            return new TipoAtivoDAO().ObterTiposAtivos(Chave, Filtro, Ativo);

        }
    }
}
