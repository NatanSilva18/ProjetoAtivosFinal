using ProjetoAtivos.Models;
using System;
using System.Collections.Generic;

namespace ProjetoAtivos.Control
{
    public class SalaControl
    {
        public Boolean Gravar(int Codigo, string Descricao, Boolean StAtivo, int Filial)
        {
            return new Sala(Codigo, Descricao, StAtivo, Filial).Gravar();       //n tem validacao de sala
        }
        public Boolean ExcluirLogico(int Codigo)
        {
            return new Sala().ExcluirLogico(Codigo);
        }
        public Boolean Ativar(int Codigo)
        {
            return new Sala().Ativar(Codigo);
        }
        public Sala BuscarSala(int Codigo)
        {
            return new Sala().BuscarSala(Codigo);
        }
        public List<Sala> BuscarSalas(int Codigo)
        {
            return new Sala().BuscarSalas(Codigo);
        }
        public Sala BuscarSala(string Descricao)
        {
            return new Sala().BuscarSala(Descricao);
        }
        public List<Sala> ObterSalas(string Chave, string Filtro, int Ativo)
        {
            return new Sala().ObterSalas(Chave, Filtro, Ativo);
        }
    }
}
