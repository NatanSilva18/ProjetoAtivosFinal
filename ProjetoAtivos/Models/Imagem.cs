using ProjetoAtivos.DAO;
using System;
using System.Collections.Generic;

namespace ProjetoAtivos.Models
{
    public class Imagem
    {
        private int Codigo;
        private string Foto;    //base64
        private DateTime DataInsercao;
        private Ativo Ativo;

        public Imagem()
        {
            this.Codigo = 0;
            this.Foto = "";
            this.DataInsercao = DateTime.Now;
            this.Ativo = new Ativo();
        }
        public Imagem(int Codigo, string Foto, int CodigoAtivo)
        {
            this.Codigo = Codigo;
            this.Foto = Foto;
            this.DataInsercao = DateTime.Now;
            this.Ativo = new Ativo(CodigoAtivo);
        }
        public Imagem(int Codigo)
        {
            this.Codigo = Codigo;
            this.Foto = "";
            this.DataInsercao = DateTime.Now;
            this.Ativo = new Ativo();
        }
        public int GetCodigo()
        {
            return this.Codigo;
        }
        public void SetCodigo(int Codigo)
        {
            this.Codigo = Codigo;
        }
        public string GetFoto()
        {
            return this.Foto;
        }
        public void SetImagem(string Foto)
        {
            this.Foto = Foto;
        }
        public DateTime GetDataInsercao()
        {
            return this.DataInsercao;
        }
        public void SetDataInsercao(DateTime DataInsercao)
        {
            this.DataInsercao = DataInsercao;
        }
        public Ativo GetAtivo()
        {
            return this.Ativo;
        }
        public void SetAtivo(Ativo Ativo)
        {
            this.Ativo = Ativo;
        }

        public List<Imagem> BuscarImagens(int Codigo)
        {
            if (Codigo > 0)
                return new ImagemDAO().BuscarImagens(Codigo);
            else
                return null;
        }

        public Boolean Excluir()
        {
            return new ImagemDAO().Excluir(Codigo);
        }
        public bool Alterar()
        {
            return new ImagemDAO().Alterar(this);
        }

        public bool Gravar(int transf = 0)
        {
            return new ImagemDAO().Gravar(this, transf);
        }

        internal bool Gravar(Localizacao Localizacao, int transf = 0)
        {
            return new ImagemDAO().Gravar(this, Localizacao, transf);
        }
    }
}
