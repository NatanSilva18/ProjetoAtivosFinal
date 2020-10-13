using ProjetoAtivos.Models;
using Survey.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProjetoAtivos.DAO
{
    public class FilialDAO
    {

        private Banco b;

        internal FilialDAO()       //pegando instancia do objeto banco
        {
            b = Banco.GetInstance();
        }
        internal List<Filial> TableToListCompleta(DataTable dt)
        {
            List<Filial> dados = null;
            if (dt != null && dt.Rows.Count > 0)
                dados = (from DataRow row in dt.Rows
                         select new Filial(
                              Convert.ToInt32(row["fil_codigo"]),
                                              row["fil_razao"].ToString(),
                                              row["fil_cnpj"].ToString(),
                                              Convert.ToBoolean(row["fil_stativo"]),
                                               Convert.ToInt32(row["end_codigo"]),
                                              row["end_logradouro"].ToString(),
                                              Convert.ToInt32(row["end_numero"]),
                                              row["end_referencia"].ToString(),
                                              row["end_bairro"].ToString(),
                                              row["end_cep"].ToString(),
                                              row["end_cidade"].ToString(),
                                              row["end_estado"].ToString(),
                                              Convert.ToInt32(row["pes_codigo"]),
                                              row["pes_nome"].ToString(),
                                              Convert.ToInt32(row["reg_codigo"]),
                                              row["reg_descricao"].ToString(),
                                              Convert.ToBoolean(row["reg_stativo"])
                         )).ToList();

            return dados;
        }
        internal List<Filial> TableToList(DataTable dt)
        {
            List<Filial> dados = null;
            if (dt != null && dt.Rows.Count > 0)
                dados = (from DataRow row in dt.Rows
                         select new Filial(
                              Convert.ToInt32(row["fil_codigo"]),
                                              row["fil_razao"].ToString(),
                                              row["fil_cnpj"].ToString(),
                                              Convert.ToBoolean(row["fil_stativo"]),
                                              Convert.ToInt32(row["end_codigo"]),
                                              Convert.ToInt32(row["pes_codigo"]),
                                              Convert.ToInt32(row["reg_codigo"])
                         )).ToList();

            return dados;
        }
        internal Boolean Gravar(Filial Filial)
        {
            Boolean OK = false;
            if (Filial.GetCodigo() == 0)
            {
                Endereco end = Filial.GetEndereco();
                EnderecoDAO ctlEndereco = new EnderecoDAO();
                int Codigo = ctlEndereco.Gravar(end);

                if (Codigo > 0)
                {
                    b.getComandoSQL().Parameters.Clear();

                    b.getComandoSQL().CommandText = @"insert into Filial (fil_razao, fil_cnpj, fil_stativo, end_codigo, pes_codigo, reg_codigo) values (@razao, @cnpj, @ativo ,@endcodigo, @gerente, @regional);";

                    b.getComandoSQL().Parameters.AddWithValue("@razao", Filial.GetRazao());
                    b.getComandoSQL().Parameters.AddWithValue("@cnpj", Filial.GetCnpj());
                    b.getComandoSQL().Parameters.AddWithValue("@ativo", Filial.GetStativo());
                    b.getComandoSQL().Parameters.AddWithValue("@endcodigo", Codigo);
                    b.getComandoSQL().Parameters.AddWithValue("@gerente", Filial.GetResponsavel().GetCodigo());
                    b.getComandoSQL().Parameters.AddWithValue("@regional", Filial.GetRegional().GetCodigo());

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
                EnderecoDAO ctlEndereco = new EnderecoDAO();
                

                int Codigo = ctlEndereco.Gravar(Filial.GetEndereco());

                if (Codigo > 0)
                {
                    b.getComandoSQL().Parameters.Clear();

                    b.getComandoSQL().CommandText = @"update Filial set fil_razao = @razao, fil_cnpj = @cnpj, fil_stativo = @ativo, pes_codigo = @codigoPes, reg_codigo = @regional, end_codigo = @endereco where fil_codigo = @codigo;";

                    b.getComandoSQL().Parameters.AddWithValue("@razao", Filial.GetRazao());
                    b.getComandoSQL().Parameters.AddWithValue("@cnpj", Filial.GetCnpj());
                    b.getComandoSQL().Parameters.AddWithValue("@ativo", Filial.GetStativo());
                    b.getComandoSQL().Parameters.AddWithValue("@codigo", Filial.GetCodigo());
                    b.getComandoSQL().Parameters.AddWithValue("@codigoPes", Filial.GetResponsavel().GetCodigo());
                    b.getComandoSQL().Parameters.AddWithValue("@regional", Filial.GetRegional().GetCodigo());
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

            b.getComandoSQL().CommandText = @"update Filial set fil_stativo = 0 where fil_codigo = @codigo;";         //mexer
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            return b.ExecutaComando() == 1;
        }
        internal Boolean Ativar(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"update Filial set fil_stativo = 1 where fil_codigo = @codigo;";     //mexer
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            return b.ExecutaComando() == 1;
        }

        internal Filial BuscarFilialPessoa(int Pessoa)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select f.fil_codigo, f.fil_razao, f.fil_cnpj, f.fil_stativo,e.end_codigo, e.end_logradouro, e.end_numero, e.end_referencia, e.end_bairro,  e.end_cep,                                          e.end_cidade, e.end_estado, p.pes_codigo, p.pes_nome, r.reg_codigo, r.reg_descricao, r.reg_stativo
                                              from Filial f 
                                              inner join Endereco e on f.end_codigo = e.end_codigo
                                              inner join Pessoa p on f.pes_codigo = p.pes_codigo
                                              inner join Regional r on f.reg_codigo = r.reg_codigo
                                              where f.fil_stativo = 1 and f.pes_codigo = @pes;";
            b.getComandoSQL().Parameters.AddWithValue("pes", Pessoa);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToListCompleta(dt).FirstOrDefault();
            else
                return null;
        }

        internal Filial BuscarFilial(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select f.fil_codigo, f.fil_razao, f.fil_cnpj, f.fil_stativo,e.end_codigo, e.end_logradouro, e.end_numero, e.end_referencia, e.end_bairro,  e.end_cep,                                          e.end_cidade, e.end_estado, p.pes_codigo, p.pes_nome, r.reg_codigo, r.reg_descricao, r.reg_stativo
                                              from Filial f 
                                              inner join Endereco e on f.end_codigo = e.end_codigo
                                              inner join Pessoa p on f.pes_codigo = p.pes_codigo
                                              inner join Regional r on f.reg_codigo = r.reg_codigo
                                              where f.fil_stativo = 1 and f.fil_codigo = @codigo;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToListCompleta(dt).FirstOrDefault();
            else
                return null;
        }

        internal Filial BuscarFilialEmail(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select f.fil_codigo, f.fil_razao, f.fil_cnpj, f.fil_stativo, e.end_codigo, e.end_logradouro, e.end_numero, e.end_referencia, e.end_bairro,  e.end_cep,              
                                                e.end_cidade, e.end_estado, p.pes_codigo, p.pes_nome, r.reg_codigo, 
                                                r.reg_descricao, r.reg_stativo, p.pes_email, pr.pes_email as emailReg, pr.pes_nome as gerente
                                              from Filial f 
                                              inner join Endereco e on f.end_codigo = e.end_codigo
                                              inner join Pessoa p on f.pes_codigo = p.pes_codigo
                                              inner join Regional r on f.reg_codigo = r.reg_codigo
                                              inner join Pessoa pr on pr.pes_codigo = r.pes_codigo
                                              where f.fil_stativo = 1 and f.fil_codigo = @codigo;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            DataTable dt = b.ExecutaSelect();
            Filial f = null;

            if (dt.Rows.Count > 0)
            {
                f = TableToListCompleta(dt).FirstOrDefault();

                f.GetResponsavel().SetEmail(dt.Rows[0]["pes_email"].ToString());
                f.GetRegional().GetPessoa().SetEmail(dt.Rows[0]["emailReg"].ToString());
                f.GetRegional().GetPessoa().SetNome(dt.Rows[0]["gerente"].ToString());
            }

            return f;

        }

        internal List<Filial> BuscarFiliais(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select f.fil_codigo, f.fil_razao, f.fil_cnpj, f.fil_stativo, f.end_codigo, f.pes_codigo, f.reg_codigo
                                              from Filial f 
                                              where f.fil_stativo = 1 and f.reg_codigo = @codigo;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }
        internal Filial BuscarFilial(string Descricao)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select f.fil_codigo, f.fil_razao, f.fil_cnpj, f.fil_stativo,e.end_codigo, e.end_logradouro, e.end_numero, e.end_referencia,  e.end_bairro,  e.end_cep,                                          e.end_cidade, e.end_estado, p.pes_codigo, p.pes_nome, r.reg_codigo, r.reg_descricao
                                              from Filial f 
                                              inner join Endereco e on f.end_codigo = e.end_codigo
                                              inner join Pessoa p on f.pes_codigo = p.pes_codigo
                                              inner join Regional r on f.reg_codigo = r.reg_codigo
                                              where f.fil_stativo = 1 and f.fil_razao = @razao;";
            b.getComandoSQL().Parameters.AddWithValue("@razao", Descricao);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToListCompleta(dt).FirstOrDefault();
            else
                return null;
        }
        internal List<Filial> ObterFiliais(string Chave, string Filtro, int Ativo)
        {
            b.getComandoSQL().Parameters.Clear();


            if (Filtro == "Razao")
            {
                if (Chave == null)
                {
                    b.getComandoSQL().CommandText = @"select f.fil_codigo, f.fil_razao, f.fil_cnpj, f.fil_stativo, e.end_codigo, e.end_logradouro, e.end_numero, e.end_referencia, e.end_bairro,  e.end_cep,                                      e.end_cidade, e.end_estado, p.pes_codigo, p.pes_nome, r.reg_codigo, r.reg_descricao, r.reg_stativo
                                                      from Filial f 
                                                      inner join Endereco e on f.end_codigo = e.end_codigo
                                                      inner join Pessoa p on f.pes_codigo = p.pes_codigo
                                                      inner join Regional r on f.reg_codigo = r.reg_codigo
                                                      where f.fil_stativo = @ativo order by f.fil_razao;";

                }
                else
                {
                    b.getComandoSQL().CommandText = @"select f.fil_codigo, f.fil_razao, f.fil_cnpj, f.fil_stativo, e.end_codigo, e.end_logradouro, e.end_numero, e.end_referencia, e.end_bairro, e.end_cep,                                        e.end_cidade, e.end_estado, p.pes_codigo, p.pes_nome, r.reg_codigo, r.reg_descricao, r.reg_stativo
                                                      from Filial f 
                                                      inner join Endereco e on f.end_codigo = e.end_codigo
                                                      inner join Pessoa p on f.pes_codigo = p.pes_codigo
                                                      inner join Regional r on f.reg_codigo = r.reg_codigo
                                                      where fil_razao like @razao and f.fil_stativo = @ativo  order by f.fil_razao;";
                    b.getComandoSQL().Parameters.AddWithValue("@Razao", "%" + Chave + "%");
                }
            }


            b.getComandoSQL().Parameters.AddWithValue("@ativo", Ativo);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return TableToListCompleta(dt);
            else
                return null;
        }
        internal int ValidarEnderecoFilial(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select fil_codigo, fil_razao, fil_cnpj, fil_stativo, end_codigo
              from filial
              where end_codigo = @codigo;";


            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
                return dt.Rows.Count;
            else
                return 0;
        }
    }
}
