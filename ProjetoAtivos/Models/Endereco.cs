namespace ProjetoAtivos.Models
{
    public class Endereco
    {
        private int Codigo;
        private string Referencia;
        private string Logradouro;
        private int Numero;
        private string Bairro;
        private string Cep;
        private string Cidade;
        private string Estado;

        public Endereco()
        {
            this.Codigo = 0;
            this.Referencia = "";
            this.Logradouro = "";
            this.Numero = 0;
            this.Bairro = "";
            this.Cep = "";
            this.Cidade = "";
            this.Estado = "";
        }
        public Endereco(int Codigo, string Logradouro, int Numero, string Referencia,
                        string Bairro, string Cep, string Cidade, string Estado)
        {
            this.Codigo = Codigo;
            this.Logradouro = Logradouro;
            this.Numero = Numero;
            this.Referencia = Referencia;
            this.Bairro = Bairro;
            this.Cep = Cep;
            this.Cidade = Cidade;
            this.Estado = Estado;
        }
        public Endereco(int Codigo, string Logradouro, int Numero,
                       string Bairro, string Cidade)
        {
            this.Codigo = Codigo;
            this.Referencia = "";
            this.Logradouro = Logradouro;
            this.Numero = Numero;
            this.Bairro = Bairro;
            this.Cep = "";
            this.Cidade = Cidade;
            this.Estado = "";
        }
        public Endereco(int Codigo)
        {
            this.Codigo = Codigo;
            this.Referencia = "";
            this.Logradouro = "";
            this.Numero = 0;
            this.Bairro = "";
            this.Cep = "";
            this.Cidade = "";
            this.Estado = "";
        }
        public int GetCodigo()
        {
            return this.Codigo;
        }
        public void SetCodigo(int Codigo)
        {
            this.Codigo = Codigo;
        }
        public string GetReferencia()
        {
            return this.Referencia;
        }
        public void SetReferencia(string Referencia)
        {
            this.Referencia = Referencia;
        }
        public string GetLogradouro()
        {
            return this.Logradouro;
        }
        public void SetLogradouro(string Logradouro)
        {
            this.Logradouro = Logradouro;
        }
        public int GetNumero()
        {
            return this.Numero;
        }
        public void SetNumero(int Numero)
        {
            this.Numero = Numero;
        }
        public string GetBairro()
        {
            return this.Bairro;
        }
        public void SetBairro(string Bairro)
        {
            this.Bairro = Bairro;
        }
        public string GetCep()
        {
            return this.Cep;
        }
        public void SetCep(string Cep)
        {
            this.Cep = Cep;
        }
        public string GetCidade()
        {
            return this.Cidade;
        }
        public void SetCidade(string Cidade)
        {
            this.Cidade = Cidade;
        }
        public string GetEstado()
        {
            return this.Bairro;
        }
        public void SetEstado(string Estado)
        {
            this.Estado = Estado;
        }
    }
}
