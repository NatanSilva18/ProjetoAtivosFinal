using ProjetoAtivos.Models;
using System;
using System.Collections.Generic;

namespace ProjetoAtivos.Control
{
    public class FilialControl
    {
        public int Gravar(int Codigo, string Razao, string Cnpj, Boolean StAtivo, string EndLogradouro, int EndNumero, string EndReferencia, string EndBairro, string EndCep, string EndCidade, string EndEstado, int Responsavel, int Regional)
        {
            Filial Filial = new Filial(Codigo, Razao, Cnpj, StAtivo, EndLogradouro, EndNumero, EndReferencia, EndBairro, EndCep, EndCidade, EndEstado, Responsavel, Regional);
            Filial Valida = new Filial();
            if (Codigo == 0)
            {
                if (Valida.BuscarFilial(Razao) == null)
                {
                    if (Filial.Gravar())
                        return 10;
                    else
                        return -10;
                }
                else
                    return -20;
            }
            else
            {
                if (Filial.Gravar())
                    return 10;
                else
                    return -10;
            }

        }
        public Boolean ExcluirLogico(int Codigo)
        {
            return new Filial().ExcluirLogico(Codigo);
        }
        public Boolean Ativar(int Codigo)
        {
            return new Filial().Ativar(Codigo);
        }
        public Filial BuscarFilialPessoa(int Pessoa)
        {
            return new Filial().BuscarFilialPessoa(Pessoa);
        }
        public Filial BuscarFilial(int Codigo)
        {
            return new Filial().BuscarFilial(Codigo);
        }
        public List<Filial> BuscarFiliais(int Codigo)
        {
            return new Filial().BuscarFiliais(Codigo);
        }
        public Filial BuscarFilial(string Descricao)
        {
            return new Filial().BuscarFilial(Descricao);
        }
        public List<Filial> ObterFiliais(string Chave, string Filtro, int Ativo)
        {
            return new Filial().ObterFiliais(Chave, Filtro, Ativo);
        }
    }


}
