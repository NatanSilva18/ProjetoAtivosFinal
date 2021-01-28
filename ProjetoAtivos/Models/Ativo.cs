using ProjetoAtivos.DAO;
using System;
using System.Collections.Generic;

namespace ProjetoAtivos.Models
{
    public class Ativo
    {
        public List<Imagem> Imagens { get; set; }
        private int Codigo;
        private int Placa;
        private string Descricao;
        private string Estado;
        private string Observacao;
        private string Tag;
        private string Marca;
        private string Modelo;
        private string NumeroSerie;
        private Boolean StAtivo;
        private double Valor;
        private TipoAtivo TipoAtivo;
        private Sala Sala;
        private NotaFiscal Nota;

        public Ativo()
        {
            this.Imagens = new List<Imagem>();
            this.Codigo = 0;
            this.Placa = 0;
            this.Descricao = "";
            this.Estado = "";
            this.Observacao = "";
            this.Tag = "";
            this.Marca = "";
            this.Modelo = "";
            this.NumeroSerie = "";
            this.StAtivo = true;
            this.Valor = 0;
            this.TipoAtivo = new TipoAtivo();
            this.Sala = new Sala();
            this.Nota = new NotaFiscal();
        }
        public Ativo(int Codigo)
        {
            this.Imagens = new List<Imagem>();
            this.Codigo = Codigo;
            this.Placa = 0;
            this.Descricao = "";
            this.Estado = "";
            this.Observacao = "";
            this.Tag = "";
            this.Marca = "";
            this.Modelo = "";
            this.NumeroSerie = "";
            this.StAtivo = true;
            this.Valor = 0;
            this.TipoAtivo = new TipoAtivo();
            this.Sala = new Sala();
            this.Nota = new NotaFiscal();
        }

        public Ativo(int Codigo, Int32 Placa, string Descricao, string Estado, string Observacao, string Tag, string Marca, string Modelo, string NumeroSerie, Boolean StAtivo, double Valor, int TipoAtivo, string DescTpAtivo, double ValorApr, int Sala, string DescSala, int Nota)
        {
            this.Imagens = new List<Imagem>();
            this.Codigo = Codigo;
            this.Placa = Placa;
            this.Descricao = Descricao;
            this.Estado = Estado;
            this.Observacao = Observacao;
            this.Tag = Tag;
            this.Marca = Marca;
            this.Modelo = Modelo;
            this.NumeroSerie = NumeroSerie;
            this.StAtivo = StAtivo;
            this.Valor = Valor;
            this.TipoAtivo = new TipoAtivo(TipoAtivo, DescTpAtivo, ValorApr, true);
            this.Sala = new Sala(Sala, DescSala, true, 0);
            this.Nota = new NotaFiscal(Nota);
        }


        public int GetCodigo()
        {
            return this.Codigo;
        }
        public void SetCodigo(int Codigo)
        {
            this.Codigo = Codigo;
        }
        public int GetPlaca()
        {
            return this.Placa;
        }
        public void SetPlaca(int Placa)
        {
            this.Placa = Placa;
        }
        public string GetDescricao()
        {
            return this.Descricao;
        }
        public void SetDescricao(string Descricao)
        {
            this.Descricao = Descricao;
        }
        public string GetEstado()
        {
            return this.Estado;
        }
        public void SetEstado(string Estado)
        {
            this.Estado = Estado;
        }
        public string GetObservacao()
        {
            return this.Observacao;
        }
        public void SetObservacao(string Observacao)
        {
            this.Observacao = Observacao;
        }
        public string GetTag()
        {
            return this.Tag;
        }
        public void SetTag(string Tag)
        {
            this.Tag = Tag;
        }
        public string GetMarca()
        {
            return this.Marca;
        }
        public void SetMarca(string Marca)
        {
            this.Marca = Marca;
        }
        public string GetModelo()
        {
            return this.Modelo;
        }
        public void SetModelo(string Modelo)
        {
            this.Modelo = Modelo;
        }
        public string GetNumeroSerie()
        {
            return this.NumeroSerie;
        }
        public void SetNumeroSerie(string NumeroSerie)
        {
            this.NumeroSerie = NumeroSerie;
        }
        public Boolean GetStAtivo()
        {
            return this.StAtivo;
        }
        public void SetStAtivo(Boolean StAtivo)
        {
            this.StAtivo = StAtivo;
        }
        public double GetValor()
        {
            return this.Valor;
        }
        public void SetValor(double Valor)
        {
            this.Valor = Valor;
        }
        public TipoAtivo GetTipoAtivo()
        {
            return this.TipoAtivo;
        }
        public void SetTipoAtivo(TipoAtivo TipoAtivo)
        {
            this.TipoAtivo = TipoAtivo;
        }
        public Sala GetSala()
        {
            return this.Sala;
        }
        public void SetSala(Sala Sala)
        {
            this.Sala = Sala;
        }
        public NotaFiscal GetNota()
        {
            return this.Nota;
        }
        public void SetNota(NotaFiscal Nota)
        {
            this.Nota = Nota;
        }
        public int Gravar(List<Imagem> Imagem, Localizacao Localizacao)
        {
            if (Imagem != null && Localizacao != null)
                return new AtivoDAO().Gravar(this, Imagem, Localizacao);
            else
                return -10;
        }
        public List<object> ObterAtivos(string Chave, string Filtro, int Ativo, int Regiao, int Filial)
        {
            return new AtivoDAO().ObterAtivos(Chave, Filtro, Ativo,  Regiao,  Filial);
        }
        public List<object> ObterAtivosPlaca(int Placa)
        {
            return new AtivoDAO().ObterAtivosPlaca(Placa);
        }
        public List<object> ObterAtivos(Transferencia t)
        {
            return new AtivoDAO().ObterAtivos(t);
        }
        public List<Ativo> BuscarAtivos(int Local)
        {
            return new AtivoDAO().BuscarAtivos(Local);
        }

        public Boolean ExcluirLogico(int Codigo)
        {
            if (Codigo > 0)
                return new AtivoDAO().ExcluirLogico(Codigo);
            else
                return false;
        }

        public Boolean Ativar(int Codigo)
        {
            if (Codigo > 0)
                return new AtivoDAO().Ativar(Codigo);
            else
                return false;
        }

        public bool AtualizarSala()
        {
            
           return new AtivoDAO().AtualizarSala(this);
            
        }

        public Ativo BuscarAtivo(int Codigo)
        {
            if (Codigo > 0)
                return new AtivoDAO().BuscarAtivo(Codigo);
            else
                return null;
        }

        public object AtivosImagem()
        {
            return new AtivoDAO().AtivosImagem();
        }
        public object SomaAtivos()
        {
            return new AtivoDAO().SomaAtivos();
        }
        public List<object> ObterValores(int Regional, int Filial)
        {
            return new AtivoDAO().ObterValores(Regional, Filial);
        }
        public List<object> ObterRelatorioImagem(int Regional, int Filial) {
            return new AtivoDAO().ObterRelatorioImagem(Regional, Filial);

        }
        public List<object> ObterImagens(int Codigo)
        {
            if (Codigo > 0)
                return new AtivoDAO().ObterImagens(Codigo);
            else
                return null;
        }

    }
}
