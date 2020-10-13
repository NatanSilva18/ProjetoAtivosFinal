using ProjetoAtivos.Models;
using System;
using System.Collections.Generic;

namespace ProjetoAtivos.Control
{
    public class AtivoControl
    {
        public int Gravar(int Codigo, int Regional, int Filial, int Sala, int Placa, string Tag, string Estado, string Observacao, string Descricao, int TipoAtivo, string Marca, string NumeroSerie, string Modelo, double Valor, string Img, string Latitude, string Longitude, int CodigoNota, string NumeroNota, double ValorNota, DateTime DataEmissao, string Fornecedor)
        {           
                Localizacao Localiza = new Localizacao(Latitude, Longitude, 0);

                List<Imagem> imagens = new List<Imagem>();

                string[] bases64 = Img.Split("**Separdor Imagem**");

                foreach (string i in bases64)
                {
                    imagens.Add(new Imagem(0, i, 0));
                }

                Ativo Ativo = new Ativo(Codigo, Placa, Descricao, Estado, Observacao, Tag, Marca, Modelo, NumeroSerie, true, Valor, TipoAtivo, "",0, Sala, "", CodigoNota);
            NotaFiscal Nota = new NotaFiscal(CodigoNota, NumeroNota, ValorNota, DataEmissao, Fornecedor);
            Ativo.SetNota(Nota);


                return Ativo.Gravar(imagens, Localiza);
        }

        public List<object> ObterAtivos(string Chave, string Filtro, int Ativo, int Regiao, int Filial)
        {
            return new Ativo().ObterAtivos(Chave, Filtro, Ativo,  Regiao,  Filial);
        }
        public List<object> ObterAtivosPlaca(int Placa)
        {
            return new Ativo().ObterAtivosPlaca(Placa);
        }
        public List<object> ObterAtivosTransf(int Transf)
        {
            return new Ativo().ObterAtivos(new Transferencia(Transf));
        }

        public Boolean ExcluirLogico(int Codigo)
        {
            return new Ativo().ExcluirLogico(Codigo);
        }

        public Boolean Ativar(int Codigo)
        {
            return new Ativo().Ativar(Codigo);
        }

        public Ativo BuscarAtivo(int Codigo)
        {
            return new Ativo().BuscarAtivo(Codigo);
        }
        public List<Ativo> BuscarAtivos(int Codigo)
        {
            return new Ativo().BuscarAtivos(Codigo);
        }
        public Localizacao BuscarLocalizacao(int Ordem)
        {
            return new Localizacao().BuscarLocalizacao(Ordem);
        }
        public object AtivosImagem()
        {
            return new Ativo().AtivosImagem();
        }
        public object SomaAtivos()
        {
            return new Ativo().SomaAtivos();
        }
        public List<object> ObterValores(int Regional, int Filial)
        {
            return new Ativo().ObterValores(Regional, Filial);
        }
        public List<object> ObterRelatorioImagem(int Regional, int Filial)
        {
            return new Ativo().ObterRelatorioImagem(Regional, Filial);
        }

        
    }
}
