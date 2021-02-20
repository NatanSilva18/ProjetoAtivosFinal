using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Survey.DAL
{
    /// <summary>
    /// Classe responsável pela conexão com o banco de dados e execução de comandos de SQL
    /// </summary>
    internal class Banco
    {
        private static Banco b;
        /// <summary>
        /// Campo responsável pela definição da string de conexão
        /// </summary>
        private string _strConexao;
        /// <summary>
        /// Campo responsável pelo comando de SQL a ser executado
        /// </summary>
        private MySqlCommand _ComandoSQL;
        /// <summary>
        /// Propriedade que expõe o campo para definição do comando de SQL a ser executado
        /// </summary>

        public MySqlCommand getComandoSQL() { return _ComandoSQL; }

        public void setComandoSQL(MySqlCommand _ComandoSQL) { this._ComandoSQL = _ComandoSQL; }
        /// <summary>
        /// Campo que define o objeto de conexão
        /// </summary>
        private MySqlConnection _conn;
        /// <summary>
        /// Campo que define o objeto de transação
        /// </summary>
        private MySqlTransaction _transacao;


        /// <summary>
        /// Construtor que define uma string de conexão fixa e cria os objetos de conexão e 
        /// comando
        /// </summary>
        /// 





        private Banco()
        {
            _strConexao = @"Server=localhost;Port=3306;Database=16440_parebemativos;Uid=root;Pwd=";
            //_strConexao = @"Server=bd.asp.hostazul.com.br;Port=4406;Database=16440_parebemativos;Uid=16440_prod;Pwd=pundvlbk2imgjth;";
            //_strConexao = @"Data Source=bd.asp.hostazul.com.br; Initial Catalog=16440_parebemativos;User Id=16440_prod;Password=pundvlbk2imgjth";
            _conn = new MySqlConnection(_strConexao);
            _ComandoSQL = new MySqlCommand();
            _ComandoSQL.Connection = _conn;
        }
        public static Banco GetInstance()
        {
            if (b == null)
                b = new Banco();

            return b;
        }

        /// <summary>
        /// Construtor que recebe por parametro a string de conexão a ser utilizada e cria
        /// os objetos de comando e conexão
        /// </summary>
        /// <param name="stringConexao">String de conexão a ser utilizada</param>

        /// <summary>
        /// Método para abrir a conexão com o banco de dados
        /// </summary>
        /// <param name="transacao">true -> Com transação | false -> Sem transação</param>
        /// <returns></returns>
        internal bool AbreConexao(bool transacao)
        {
            try
            {                
                    _conn.Open();
                    if (transacao)
                    {
                        _transacao = _conn.BeginTransaction();
                        _ComandoSQL.Transaction = _transacao;
                    }
                
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Métodos para fechar a conexão com o banco de dados
        /// </summary>
        /// <returns>Retorna um booleano para indicar o resultado da operação</returns>
        internal bool FechaConexao()
        {
            try
            {
                if (_conn.State == ConnectionState.Open)
                    _conn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Finaliza uma transação
        /// </summary>
        /// <param name="commit">true -> Executa o commit | false -. Executa o rollback</param>
        internal void FinalizaTransacao(bool commit)
        {
            if (commit)
                _transacao.Commit();
            else
                _transacao.Rollback();
            FechaConexao();
        }
        /// <summary>
        /// Destrutor que fecha a conexão com o banco de dados
        /// </summary>
        ~Banco()
        {
            FechaConexao();
        }
        /// <summary>
        /// Método responsável pela execução dos comandos de Insert, Update e Delete
        /// </summary>
        /// <returns>Retorna um número inteiro que indica a quantidade de linhas afetadas</returns>
        internal int ExecutaComando(bool transacao = false)
        {
            if (_ComandoSQL.CommandText.Trim() == string.Empty)
                throw new Exception("Não há instrução SQL a ser executada.");

            int retorno;
            if (_conn.State != ConnectionState.Open)
                AbreConexao(transacao);
            try
            {
                retorno = _ComandoSQL.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                retorno = -1;
                throw new Exception("Erro ao executar o comando SQL:", ex);
            }
            finally
            {
                if (!transacao)
                    FechaConexao();
            }
            return retorno;
        }
        /// <summary>
        /// Método responsável pela execução dos comandos de Insert com retorno do último código cadastrado
        /// </summary>
        /// <returns>Retorna um número inteiro que indica a quantidade de linhas afetadas</returns>
        internal int ExecutaComando(bool transacao, out int ultimoCodigo)
        {
            if (_ComandoSQL.CommandText.Trim() == string.Empty)
                throw new Exception("Não há instrução SQL a ser executada.");

            int retorno;
            ultimoCodigo = 0;

            if (_conn.State != ConnectionState.Open)
                AbreConexao(transacao);
            try
            {
                //Executa o comando de insert e já retorna o @@IDENTITY
                ultimoCodigo = Convert.ToInt32(_ComandoSQL.ExecuteScalar());
                retorno = 1;
            }
            catch (Exception ex)
            {
                retorno = -1;
                throw new Exception("Erro ao executar o comando SQL: ", ex);
            }
            finally
            {
                if (!transacao)
                    FechaConexao();
            }
            return retorno;
        }
        /// <summary>
        /// Método responsável pela execução dos comandos de Select
        /// </summary>
        /// <returns>Retorna um DataTable com o resultado da operação</returns>
        internal DataTable ExecutaSelect(bool transacao = false)
        {
            if (_ComandoSQL.CommandText.Trim() == string.Empty)
                throw new Exception("Não há instrução SQL a ser executada.");

            if (_conn.State != ConnectionState.Open)
                AbreConexao(transacao);
            DataTable dt = new DataTable();
            try
            {
                dt.Load(_ComandoSQL.ExecuteReader());
            }
            catch (Exception ex)
            {
                dt = null;
                throw new Exception("Erro ao executar o comando SQL: ", ex);
            }
            finally
            {
                if (!transacao)
                    FechaConexao();
            }

            return dt;
        }
        /// <summary>
        /// Método que executa comandos de Select para retornos escalares, ou seja,
        /// retorna a primeira linha e primeira coluna do resultado do comando de Select.
        /// Para nosso exemplo, sempre convertemos esse valor para Double
        /// </summary>
        /// <returns>Retorna a primeira linha e primeira coluna do resultado comando de Select</returns>
        internal double ExecutaScalar()
        {
            if (_ComandoSQL.CommandText.Trim() == string.Empty)
                throw new Exception("Não há instrução SQL a ser executada.");

            AbreConexao(false);
            double retorno;
            try
            {
                retorno = Convert.ToDouble(_ComandoSQL.ExecuteScalar());
            }
            catch (Exception ex)
            {
                retorno = -9999;
                throw new Exception("Erro ao executar o comando SQL: ", ex);
            }
            finally
            {
                FechaConexao();
            }
            return retorno;
        }
    }
}