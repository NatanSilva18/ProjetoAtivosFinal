using ProjetoAtivos.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAtivos.Models
{
    public class NotaFiscal
    {
        private int Codigo;
        private string CodigoNota;
        private double ValorNota;
        private DateTime DataEmissao;
        private string Fornecedor;

        public NotaFiscal()
        {
            this.Codigo = 0;
            this.CodigoNota = "";
            this.ValorNota = 0;
            this.DataEmissao = DateTime.Now;
            this.Fornecedor = "";
        }
        public NotaFiscal(int Codigo)
        {
            this.Codigo = Codigo;
            this.CodigoNota = "";
            this.ValorNota = 0;
            this.DataEmissao = DateTime.Now;
            this.Fornecedor = "";
        }
        public NotaFiscal(int Codigo, string CodigoNota, double ValorNota, DateTime DataEmissao, string Fornecedor)
        {
            this.Codigo = Codigo;
            this.CodigoNota = CodigoNota;
            this.ValorNota = ValorNota;
            this.DataEmissao = DataEmissao;
            this.Fornecedor = Fornecedor;
        }
        public int GetCodigo()
        {
            return this.Codigo;
        }
        public void SetCodigo(int Codigo)
        {
            this.Codigo = Codigo;
        }
        public string GetCodigoNota()
        {
            return CodigoNota;
        }
        public void SetCodigoNota(string CodigoNota)
        {
            this.CodigoNota = CodigoNota;
        }
        public double GetValorNota()
        {
            return this.ValorNota;
        }
        public void SetValorNota(double ValorNota)
        {
            this.ValorNota = ValorNota;
        }
        public DateTime GetDataEmissao()
        {
            return this.DataEmissao;
        }
        public void SetDataEmissao(DateTime DataEmissao)
        {
            this.DataEmissao = DataEmissao;
        }
        public string GetFornecedor()
        {
            return this.Fornecedor;
        }
        public void SetFornecedor(string Fornecedor)
        {
            this.Fornecedor = Fornecedor;
        }
        public int Gravar()
        {
            if (this.CodigoNota != "" && this.Fornecedor != "" && this.ValorNota > 0)
                return new NotaFiscalDAO().Gravar(this);
            else
                return 0;
        }

    }
}
