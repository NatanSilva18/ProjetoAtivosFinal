
using ProjetoAtivos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAtivos.Control
{
    public class TransferenciaControl
    {
        public Boolean Gravar(int Codigo, string Observacao, string Status, DateTime dtAbertura, DateTime dtFechamento,int Motivo, int FilialOrigem, int FilialDestino, int Ativo)
        {         
            return new Transferencia(Codigo, Observacao, Status, dtAbertura, dtFechamento, Motivo, FilialOrigem, FilialDestino, Ativo).Gravar();
        }

        public bool Gravar(int Origem, int Destino, int Motivo, string Descricao, string[] Docs, string[] Nome, string[] Content, int[] Ativos, string[] Imgs, string Caminho)
        {
            Transferencia transf = new Transferencia()
            {
                AprovacaoDestino=null,
                AprovacaoGerente=null
            };

            transf.SetdtAbertura(DateTime.Now);
            transf.SetFilialDestino(new Filial(Destino));
            transf.SetFilialOrigem(new Filial(Origem));
            transf.SetMotivo(new Motivo(Motivo));
            transf.SetObservacao(Descricao);

            List<Ativo> Atvs = new List<Ativo>();

            for (int i = 0; i < Ativos.Length; i++)
            {
                Atvs.Add(new Ativo(Ativos[i]));
                Atvs[i].Imagens.Add(new Imagem(0, Imgs[i], Ativos[i]));
            }
            transf.SetAtivos(Atvs);

            List<Documento> Dc = new List<Documento>();
            
            for (int i = 0; i < Docs.Length; i++)
            {
                Dc.Add(new Documento() { 
                Caminho=Caminho+ "\\",
                ContentType=Content[i],
                Doc = Docs[i],
                Nome = Nome[i]                });
            }

            transf.Documentos = Dc;

            return transf.Gravar();

            ;
        }

        public List<object> ObterTransferencias(int Origem, int Destino, int Ativo, int Regiao, int Filial)
        {
            return new Transferencia().ObterTransferencias(Origem, Destino, Ativo, Regiao, Filial);
        }

        public Transferencia BuscarTransferencia(int Codigo)
        {
            return new Transferencia().BuscarTransferencia(Codigo);
        }

        public bool Aprovar(int Transf, string Obs, int Pessoa)
        {
            return new Transferencia().Aprovar(Transf, Obs, Pessoa);
        }

        public bool Recusar(int Transf, string Obs, int Pessoa)
        {
            return new Transferencia().Recusar(Transf, Obs, Pessoa);
        }

        public List<object> BuscaDocs(int Codigo)
        {
            return new Documento().Busca(new Transferencia(Codigo));
        }

        public Documento BuscaDoc(int Codigo)
        {
            return new Documento().Busca(Codigo);
        }

        public bool Receber(int Codigo, string Obs, int[] Ativos, string[] Imgs, int pessoa, string Latitude, string Longitude, int[] Salas)
        {
            Localizacao loc = new Localizacao(Latitude, Longitude, 0);
            Transferencia transf = new Transferencia()
            {
                AprovacaoDestino = new Aprovacao() { Observacao = Obs, Responsável = new Pessoa(pessoa)},
                AprovacaoGerente = null
            };

            transf.SetCodigo(Codigo);
            transf.AprovacaoDestino.Status = true;
            transf.AprovacaoDestino.DataInsercao=DateTime.Now;
            

            List<Ativo> Atvs = new List<Ativo>();

            for (int i = 0; i < Ativos.Length; i++)
            {
                Atvs.Add(new Ativo(Ativos[i]));
                Atvs[i].Imagens.Add(new Imagem(0, Imgs[i], Ativos[i]));
                Atvs[i].SetSala(new Sala(Salas[i]));
            }
            transf.AprovacaoDestino.Ativos=Atvs;

            return transf.Receber(loc);
        }
        public object StatusTransferencias()
        {
            return new Transferencia().StatusTransferencias();
        }

    }
}
