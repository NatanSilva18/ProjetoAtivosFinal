using ProjetoAtivos.Models;
using System;
using System.Collections.Generic;

namespace ProjetoAtivos.Control
{
    public class TipoAtivoControl
    {

        public Boolean Gravar(int Codigo, string Descricao, double Valor, Boolean StAtivo)
        {
            return new TipoAtivo(Codigo, Descricao, Valor, StAtivo).Gravar();
        }

        public List<TipoAtivo> ObterTiposAtivos(string Chave, string Filtro, int Ativo)
        {
            return new TipoAtivo().ObterTiposAtivos(Chave, Filtro, Ativo);
        }

        public Boolean ExcluirLogico(int Codigo)
        {
            return new TipoAtivo().ExcluirLogico(Codigo);
        }
        public Boolean Ativar(int Codigo)
        {
            return new TipoAtivo().Ativar(Codigo);
        }
        public TipoAtivo BuscarTipoAtivo(int Codigo)
        {
            return new TipoAtivo().BuscarTipoAtivo(Codigo);
        }
        public TipoAtivo BuscarTipoAtivo(string Descricao)
        {
            return new TipoAtivo().BuscarTipoAtivo(Descricao);
        }
    }
}
