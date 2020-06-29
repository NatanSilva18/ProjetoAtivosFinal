using ProjetoAtivos.Models;
using System;
using System.Collections.Generic;

namespace ProjetoAtivos.Control
{
    public class PessoaControl
    {

        public int Gravar(int Codigo, string Matricula, string Nome, string Email, string Cargo, string Telefone, string Telefone2, Boolean Ativo, string EndLogradouro, int EndNumero, string EndReferencia, string EndBairro, string EndCep, string EndCidade, string EndEstado)
        {
            Pessoa Pessoa = new Pessoa(Codigo, Matricula, Nome, Email, Cargo, Telefone, Telefone2, Ativo, EndLogradouro, EndNumero, EndReferencia, EndBairro, EndCep, EndCidade, EndEstado);
            Pessoa Valida = new Pessoa();
            if (Codigo == 0)
            {
                if (Valida.BuscarPessoa(Matricula) == null)
                {
                    if (Pessoa.Gravar())
                        return 10;
                    else
                        return -10;
                }
                else
                    return -20;
            }
            else
            {
                if (Pessoa.Gravar())
                    return 10;
                else
                    return -10;
            }

        }

        public Boolean ExcluirLogico(int Codigo)
        {
            return new Pessoa().ExcluirLogico(Codigo);
        }
        public Boolean Ativar(int Codigo)
        {
            return new Pessoa().Ativar(Codigo);
        }
        public Pessoa BuscarPessoa(int Codigo)
        {
            return new Pessoa().BuscarPessoa(Codigo);
        }
        public Pessoa BuscarPessoa(string Descricao)
        {
            return new Pessoa().BuscarPessoa(Descricao);
        }
        public List<Pessoa> ObterPessoas(string Chave, string Filtro, int Ativo)
        {
            return new Pessoa().ObterPessoas(Chave, Filtro, Ativo);
        }
    }
}
