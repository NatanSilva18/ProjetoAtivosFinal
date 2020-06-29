
using System;

namespace ProjetoAtivos.Models
{
    public class TipoUsuario
    {
        private int Codigo;
        private string Descricao;
        private int Nivel;
        private Boolean StAtivo;

        public TipoUsuario(int Codigo, string Descricao, int Nivel, Boolean StAtivo)
        {
            this.Codigo = Codigo;
            this.Descricao = Descricao;
            this.Nivel = Nivel;
            this.StAtivo = StAtivo;
        }
        public TipoUsuario()
        {
            this.Codigo = 0;
            this.Descricao = "";
            this.Nivel = 0;
            this.StAtivo = true;
        }
        public TipoUsuario(int Codigo)
        {
            this.Codigo = Codigo;
            this.Descricao = "";
            this.Nivel = 0;
            this.StAtivo = true;
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
        public int GetNivel()
        {
            return this.Nivel;
        }
        public void SetNivel(int Nivel)
        {
            this.Nivel = Nivel;
        }
        public Boolean GetStAtivo()
        {
            return this.StAtivo;
        }
        public void SetStAtivo(Boolean StAtivo)
        {
            this.StAtivo = StAtivo;
        }
        /*public int Gravar(Boolean Transacao)
        {
            return new TipoUsuarioDAO().Gravar(this, Transacao);
        }*/

    }
}
