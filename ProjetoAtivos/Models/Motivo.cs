using ProjetoAtivos.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAtivos.Models
{
    public class Motivo
    {
        private int Codigo;
        private string Descricao;
        private Boolean StAtivo;

        public Motivo()
        {
            this.Codigo = 0;
            this.Descricao = "";
            this.StAtivo = true;
        }
        public Motivo(int Codigo)
        {
            this.Codigo = Codigo;
            this.Descricao = "";
            this.StAtivo = true;
        }
        public Motivo(int Codigo, string Descricao, Boolean StAtivo)
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
                return new MotivoDAO().Gravar(this);
            else
                return false;
        }
        public Boolean ExcluirLogico(int Codigo)
        {
            if (Codigo > 0)
                return new MotivoDAO().ExcluirLogico(Codigo);
            else
                return false;
        }
        public Boolean Ativar(int Codigo)
        {
            if (Codigo > 0)
                return new MotivoDAO().Ativar(Codigo);
            else
                return false;
        }
        public Motivo BuscarMotivo(int Codigo)
        {
            if (Codigo > 0)
                return new MotivoDAO().BuscarMotivo(Codigo);
            else
                return null;
        }

        public Motivo BuscarMotivo(string Descricao)
        {
            if (Descricao != "")
                return new MotivoDAO().BuscarMotivo(Descricao);
            else
                return null;
        }
        public List<Motivo> ObterMotivos(string Chave, string Filtro, int Ativo)
        {
            return new MotivoDAO().ObterMotivos(Chave, Filtro, Ativo);
        }
    }
}
