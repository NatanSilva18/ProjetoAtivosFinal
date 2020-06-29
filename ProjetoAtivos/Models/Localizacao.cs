
using ProjetoAtivos.DAO;
using System;


namespace ProjetoAtivos.Models
{
    public class Localizacao
    {
        private string Latitude;
        private string Longitude;
        private Imagem Img;

        public Localizacao(string Latitude, string Longitude, int Codigo)
        {
            this.Latitude = Latitude;
            this.Longitude = Longitude;
            this.Img = new Imagem(Codigo);
        }
        public Localizacao()
        {
            this.Latitude = "";
            this.Longitude = "";
            this.Img = new Imagem(1);
        }
        public string GetLatitude()
        {
            return this.Latitude;
        }
        public void SetLatitude(string Latitude)
        {
            this.Latitude = Latitude;
        }
        public string GetLongitude()
        {
            return this.Longitude;
        }
        public void SetLongitude(string Longitude)
        {
            this.Longitude = Longitude;
        }
        public Imagem GetImagem()
        {
            return this.Img;
        }
        public void SetImagem(Imagem Img)
        {
            this.Img = Img;
        }
        public void SetCodigoImagem(int Codigo)
        {
            this.Img.SetCodigo(Codigo);
        }
        public Boolean Gravar()
        {
            return new LocalizacaoDAO().Gravar(this);
        }
        public Localizacao BuscarLocalizacao(int Ordem)
        {
            if (Ordem > 0)
                return new LocalizacaoDAO().BuscarLocalizacao(Ordem);
            else
                return null;
        }
    }
}
