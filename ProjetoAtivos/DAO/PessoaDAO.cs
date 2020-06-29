using ProjetoAtivos.Models;
using Survey.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProjetoAtivos.DAO
{
    public class PessoaDAO
    {
        private Banco b;
        internal PessoaDAO()
        {
            b = Banco.GetInstance();
        }

        internal List<Pessoa> TableToListCompleta(DataTable dt)
        {
            List<Pessoa> dados = null;
            if (dt != null && dt.Rows.Count > 0)
                dados = (from DataRow row in dt.Rows
                         select new Pessoa(
                              Convert.ToInt32(row["pes_codigo"]),
                                              row["pes_matricula"].ToString(),
                                              row["pes_nome"].ToString(),
                                              row["pes_email"].ToString(),
                                              row["pes_cargo"].ToString(),
                                              row["pes_telefone"].ToString(),
                                              row["pes_telefone2"].ToString(),
                                              Convert.ToBoolean(row["pes_stativo"]),
                                              row["end_logradouro"].ToString(),
                                              Convert.ToInt32(row["end_numero"]),
                                              row["end_referencia"].ToString(),
                                              row["end_bairro"].ToString(),
                                              row["end_cep"].ToString(),
                                              row["end_cidade"].ToString(),
                                              row["end_estado"].ToString()
                         )).ToList();

            return dados;
        }

        internal List<Pessoa> TableToList(DataTable dt)
        {
            List<Pessoa> dados = null;
            if (dt != null && dt.Rows.Count > 0)
                dados = (from DataRow row in dt.Rows
                         select new Pessoa(
                              Convert.ToInt32(row["pes_codigo"]),
                                              row["pes_matricula"].ToString(),
                                              row["pes_nome"].ToString(),
                                              row["pes_email"].ToString(),
                                              row["pes_cargo"].ToString(),
                                              row["pes_telefone"].ToString(),
                                              row["pes_telefone2"].ToString(),
                                              Convert.ToBoolean(row["pes_stativo"]),
                                              Convert.ToInt32(row["end_codigo"])
                         )).ToList();
            return dados;
        }
        internal Boolean Gravar(Pessoa Pessoa)
        {
            Boolean OK = false;
            if (Pessoa.GetCodigo() == 0)
            {
                Endereco end = Pessoa.GetEndereco();
                EnderecoDAO ctlEndereco = new EnderecoDAO();
                int Codigo = ctlEndereco.Gravar(end);

                if (Codigo > 0)
                {
                    b.getComandoSQL().Parameters.Clear();

                    b.getComandoSQL().CommandText = @"insert into Pessoa (pes_matricula, pes_nome, pes_email, pes_cargo, pes_telefone,
                                                  pes_telefone2, pes_stativo, end_codigo) values(
                                                 @matricula, @nome, @email, @cargo, @telefone, @telefone2, @ativo, @endereco);";

                    b.getComandoSQL().Parameters.AddWithValue("@matricula", Pessoa.GetMatricula());
                    b.getComandoSQL().Parameters.AddWithValue("@nome", Pessoa.GetNome());
                    b.getComandoSQL().Parameters.AddWithValue("@email", Pessoa.GetEmail());
                    b.getComandoSQL().Parameters.AddWithValue("@cargo", Pessoa.GetCargo());
                    b.getComandoSQL().Parameters.AddWithValue("@telefone", Pessoa.GetTelefone1());
                    b.getComandoSQL().Parameters.AddWithValue("@telefone2", Pessoa.GetTelefone2());
                    b.getComandoSQL().Parameters.AddWithValue("@ativo", true);
                    b.getComandoSQL().Parameters.AddWithValue("@endereco", Codigo);

                    OK = b.ExecutaComando(true) == 1;

                    if (OK)
                        b.FinalizaTransacao(true);
                    else
                        b.FinalizaTransacao(false);

                }
                else
                    b.FinalizaTransacao(false);
            }
            else
            {
                Endereco end = Pessoa.GetEndereco();
                EnderecoDAO ctlEndereco = new EnderecoDAO();
                int Codigo = ctlEndereco.Gravar(end);

                if (Codigo > 0)
                {
                    b.getComandoSQL().Parameters.Clear();

                    b.getComandoSQL().CommandText = @"update Pessoa set pes_matricula = @matricula, pes_nome = @nome, pes_email = @email, pes_cargo = @cargo, pes_telefone = @telefone,
                                                  pes_telefone2 = @telefone2, pes_stativo = @ativo
                                                  where pes_codigo = @codigo and pes_stativo = 1;";

                    b.getComandoSQL().Parameters.AddWithValue("@matricula", Pessoa.GetMatricula());
                    b.getComandoSQL().Parameters.AddWithValue("@nome", Pessoa.GetNome());
                    b.getComandoSQL().Parameters.AddWithValue("@email", Pessoa.GetEmail());
                    b.getComandoSQL().Parameters.AddWithValue("@cargo", Pessoa.GetCargo());
                    b.getComandoSQL().Parameters.AddWithValue("@telefone", Pessoa.GetTelefone1());
                    b.getComandoSQL().Parameters.AddWithValue("@telefone2", Pessoa.GetTelefone2());
                    b.getComandoSQL().Parameters.AddWithValue("@ativo", Pessoa.GetStAtivo());
                    b.getComandoSQL().Parameters.AddWithValue("@endereco", Codigo);

                    OK = b.ExecutaComando(true) == 1;

                    if (OK)
                        b.FinalizaTransacao(true);
                    else
                        b.FinalizaTransacao(false);
                }
            }


            return OK;
        }

        internal Boolean ExcluirLogico(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().CommandText = @"update Pessoa set pes_stativo = 0 where pes_codigo = @codigo;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);


            return b.ExecutaComando() == 1;
        }
        internal Boolean Ativar(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();
            b.getComandoSQL().CommandText = @"update Pessoa set pes_stativo = 1 where pes_codigo = @codigo;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);


            return b.ExecutaComando() == 1;
        }

        internal Pessoa BuscarPessoa(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select p.pes_codigo, p.pes_matricula, p.pes_nome, p.pes_email, p.pes_cargo, p.pes_telefone, p.pes_telefone2, p.pes_stativo, e.end_logradouro, e.end_numero, e.end_referencia, e.end_bairro,  e.end_cep, e.end_cidade, e.end_estado 
                from Pessoa p
                inner join Endereco e on p.end_codigo = e.end_codigo
                where pes_codigo = @codigo and pes_stativo = 1;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToListCompleta(dt).FirstOrDefault();
            else
                return null;
        }
        internal Pessoa BuscarPessoa(string Descricao)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select pes_codigo, pes_matricula, pes_nome, pes_email, pes_cargo, pes_telefone, pes_telefone2, pes_stativo, end_codigo from Pessoa where pes_matricula = @descricao and pes_stativo = 1;";
            b.getComandoSQL().Parameters.AddWithValue("@descricao", Descricao);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToList(dt).FirstOrDefault();
            else
                return null;
        }
        internal List<Pessoa> ObterPessoas(string Chave, string Filtro, int Ativo)
        {
            b.getComandoSQL().Parameters.Clear();


            if (Filtro == "Nome")
            {
                if (Chave == null)
                {
                    b.getComandoSQL().CommandText = @"select pes_codigo, pes_matricula, pes_nome, pes_email, pes_cargo, pes_telefone, pes_telefone2, pes_stativo, end_codigo from Pessoa where pes_stativo = @ativo order by pes_nome;";

                }
                else
                {
                    b.getComandoSQL().CommandText = @"select pes_codigo, pes_matricula, pes_nome, pes_email, pes_cargo, pes_telefone, pes_telefone2, pes_stativo, end_codigo from Pessoa where pes_nome like @nome and pes_stativo = @ativo order by pes_nome;";
                    b.getComandoSQL().Parameters.AddWithValue("@nome", "%" + Chave + "%");
                }
            }


            b.getComandoSQL().Parameters.AddWithValue("@ativo", Ativo);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }
    }
}
