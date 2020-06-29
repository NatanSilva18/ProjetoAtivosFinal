using ProjetoAtivos.Models;
using System;
using System.Collections.Generic;

namespace ProjetoAtivos.Control
{
    public class RegionalControl
    {
        public int Gravar(int Codigo, string Descricao, Boolean StAtivo, int Operacao, int PesCodigo)
        {
            Regional Val = new Regional();

            if (Codigo == 0)       //inserção
            {
                if (Val.BuscarRegional(Descricao) == null)  //tem que tratar operacao (Alterar)
                {
                    Regional Regional = new Regional(Codigo, Descricao, StAtivo, PesCodigo, "");
                    if (Regional.Gravar())
                        return 10;          //gravado
                    else
                        return -10;         //erro gravacao
                }
                else
                    return -20;             //regional ja existe
            }
            else                   //alteração
            {
                Regional Regional = new Regional(Codigo, Descricao, StAtivo, PesCodigo, "");
                if (Regional.Gravar())
                    return 10;          //gravado
                else
                    return -10;
            }

        }
        public Boolean ExcluirLogico(int Codigo)
        {
            return new Regional().ExcluirLogico(Codigo);
        }
        public Boolean Ativar(int Codigo)
        {
            return new Regional().Ativar(Codigo);
        }
        public Regional BuscarRegional(int Codigo)
        {
            return new Regional().BuscarRegional(Codigo);
        }
        public Regional BuscarRegional(string Descricao)
        {
            return new Regional().BuscarRegional(Descricao);
        }
        public List<Regional> ObterRegionais(string Chave, string Filtro, int Ativo)
        {
            return new Regional().ObterRegionais(Chave, Filtro, Ativo);
        }
        public Regional BuscarRegionalPessoa(int Pessoa)
        {
            return new Regional().BuscarRegionalPessoa(Pessoa);
        }
        public List<object> AtivosRegional()
        {
            return new Regional().AtivosRegional();
        }
    }
}
