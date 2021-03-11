using ProjetoAtivos.Models;
using System;
using System.Collections.Generic;

namespace ProjetoAtivos.Control
{
    public class AtivoControl
    {        
        public int Gravar(int Codigo, int Regional, int Filial,  int Placa, string Tag, string Estado, string Observacao, string Descricao, int TipoAtivo, string Marca, string NumeroSerie, string Modelo, double Valor, string Img, string Latitude, string Longitude, int CodigoNota, string NumeroNota, double ValorNota, DateTime DataEmissao, string Fornecedor, string Cnpj, string NomeAnexo, string Anexo, string Cor, string PlacaVeiculo, string CRLV, string DUT, string FIPE, string ModeloV)
        {
            Localizacao Localiza = new Localizacao(Latitude, Longitude, 0);

            List<Imagem> imagens = new List<Imagem>();

            string[] bases64 = Img.Split("**Separdor Imagem**");

            foreach (string i in bases64)
            {
                imagens.Add(new Imagem(0, i, 0));
            }

            Ativo Ativo = new Ativo(Codigo, Placa, Descricao, Estado, Observacao, Tag, Marca, Modelo, NumeroSerie, true, Valor, TipoAtivo, "", 0, 0, "", CodigoNota);
            NotaFiscal Nota = new NotaFiscal(CodigoNota, NumeroNota, ValorNota, DataEmissao, Fornecedor, Cnpj);
            Ativo.SetNota(Nota);

            Veiculo v = new Veiculo()
            {
                Cor = Cor,
                CRLV = CRLV,
                DUT = DUT,
                Filial = new Filial(Filial),
                Placa = PlacaVeiculo,
                Fipe = new Fipe()
                {
                    Codigo = FIPE,
                    Modelo = ModeloV
                }
            };

            Ativo.Veiculo = v;

            if (NomeAnexo != "" && NomeAnexo != null)
            {
                Ativo.SetAnexo(new Anexo()
                {
                    Nome = NomeAnexo,
                    Base64 = Anexo
                });
            }

            return Ativo.Gravar(imagens, Localiza);
        }
        public int Gravar(int Codigo, int Regional, int Filial, int Sala, int Placa, string Tag, string Estado, string Observacao, string Descricao, int TipoAtivo, string Marca, string NumeroSerie, string Modelo, double Valor, string Img, string Latitude, string Longitude, int CodigoNota, string NumeroNota, double ValorNota, DateTime DataEmissao, string Fornecedor, string Cnpj, string NomeAnexo, string Anexo)
        {           
                Localizacao Localiza = new Localizacao(Latitude, Longitude, 0);

                List<Imagem> imagens = new List<Imagem>();

                string[] bases64 = Img.Split("**Separdor Imagem**");

                foreach (string i in bases64)
                {
                    imagens.Add(new Imagem(0, i, 0));
                }

                Ativo Ativo = new Ativo(Codigo, Placa, Descricao, Estado, Observacao, Tag, Marca, Modelo, NumeroSerie, true, Valor, TipoAtivo, "",0, Sala, "", CodigoNota);
            NotaFiscal Nota = new NotaFiscal(CodigoNota, NumeroNota, ValorNota, DataEmissao, Fornecedor, Cnpj);
            Ativo.SetNota(Nota);

                if(NomeAnexo != "" && NomeAnexo != null)
            {
                Ativo.SetAnexo(new Anexo()
                {
                    Nome = NomeAnexo,
                    Base64 = Anexo
                });
            }

                return Ativo.Gravar(imagens, Localiza);
        }

        public bool Inventariar(int Codigo, string Observacao, string Imagem, string Latitude, string Longitude)
        {
            List<Imagem> imagens = new List<Imagem>();

            string[] bases64 = Imagem.Split("**Separdor Imagem**");

            foreach (string i in bases64)
            {
                imagens.Add(new Imagem(0, i, 0));
            }

            Inventario iv = new Inventario()
            {
                Ativo = new Ativo(Codigo),
                Data = DateTime.Now,
                Obs = Observacao,
                Imagem = imagens[0],
                Localizacao = new Localizacao(Latitude, Longitude, 0)                
            };

            return iv.Gravar();
        }

        public List<object> ObterAtivos(string Chave, string Filtro, int Ativo, int Regiao, int Filial, bool Veiculo = false, bool Todos = false, bool Fotos = true)
        {
            return new Ativo().ObterAtivos(Chave, Filtro, Ativo,  Regiao,  Filial, Veiculo, Todos, Fotos);
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
        public List<object> ObterImagens(int Codigo)
        {
            return new Ativo().ObterImagens(Codigo);
        }

        public Anexo BuscaAnexo(int codigo)
        {
            return new Anexo().Buscar(codigo);
        }


    }
}
