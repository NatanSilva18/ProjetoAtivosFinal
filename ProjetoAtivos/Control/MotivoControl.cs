using ProjetoAtivos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAtivos.Control
{
    public class MotivoControl
    {
        public Boolean Gravar(int Codigo, string Descricao, Boolean StAtivo)
        {
            return new Motivo(Codigo, Descricao, StAtivo).Gravar();
        }

        public List<Motivo> ObterMotivos(string Chave, string Filtro, int Ativo)
        {
            return new Motivo().ObterMotivos(Chave, Filtro, Ativo);
        }

        public Boolean ExcluirLogico(int Codigo)
        {
            return new Motivo().ExcluirLogico(Codigo);
        }
        public Boolean Ativar(int Codigo)
        {
            return new Motivo().Ativar(Codigo);
        }
        public Motivo BuscarMotivo(int Codigo)
        {
            return new Motivo().BuscarMotivo(Codigo);
        }
        public Motivo BuscarMotivo(string Descricao)
        {
            return new Motivo().BuscarMotivo(Descricao);
        }
    }
}
