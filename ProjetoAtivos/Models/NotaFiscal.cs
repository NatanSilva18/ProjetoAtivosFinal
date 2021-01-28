using ProjetoAtivos.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAtivos.Models
{
    public class NotaFiscal
    {
        public int Codigo { get; set; }
        public string CodigoNota { get; set; }
        public double ValorNota { get; set; }
        public DateTime DataEmissao { get; set; }
        public string Fornecedor { get; set; }
        public string Cnpj { get; set; }

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
            this.Cnpj = "";
        }
        public NotaFiscal(int Codigo, string CodigoNota, double ValorNota, DateTime DataEmissao, string Fornecedor, string Cnpj)
        {
            this.Codigo = Codigo;
            this.CodigoNota = CodigoNota;
            this.ValorNota = ValorNota;
            this.DataEmissao = DataEmissao;
            this.Fornecedor = Fornecedor;
            this.Cnpj = Cnpj;
        }
       
        public int Gravar()
        {
            if (this.CodigoNota != "" && this.Fornecedor != "" && this.ValorNota > 0 && this.Cnpj != "")
                return new NotaFiscalDAO().Gravar(this);
            else
                return 0;
        }
        public NotaFiscal BuscarNota(int Codigo)
        {
            if (Codigo > 0)
                return new NotaFiscalDAO().BuscarNota(Codigo);
            else
                return new NotaFiscal(0);
        }

    }
}
