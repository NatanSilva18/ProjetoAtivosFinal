using ProjetoAtivos.Models;
using Survey.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAtivos.DAO
{
    public class VeiculoDAO
    {
        private Banco b;

        internal VeiculoDAO()       //pegando instancia do objeto banco
        {
            b = Banco.GetInstance();
        }
        internal List<Veiculo> TableToList(DataTable dt)
        {
            List<Veiculo> dados = null;
            if (dt != null && dt.Rows.Count > 0)
                dados = (from DataRow row in dt.Rows
                         select new Veiculo()
                         {
                             Codigo = Convert.ToInt32(row["ve_codigo"]),
                             Placa = row["ve_placa"].ToString(),
                             Cor = row["ve_cor"].ToString(),
                             Filial = new FilialDAO().BuscarFilial(Convert.ToInt32(row["fil_codigo"])),
                             CRLV = row["ve_crlv"].ToString(),
                             DUT = row["ve_dut"].ToString(),
                             Fipe = new Fipe()
                             {
                                 Codigo = row["ve_fipe"].ToString()
                             }
                         }
                         ).ToList();
           
            return dados;
        }

        internal Boolean Gravar(Veiculo Veiculo)
        {
            
                b.getComandoSQL().Parameters.Clear();

            if (Veiculo.Codigo == 0)
            {

                b.getComandoSQL().CommandText = @"INSERT INTO `veiculos`
                                                (                                                
                                                `ve_placa`,                                                
                                                `ve_cor`,                                                
                                                `ve_fipe`,
                                                `ve_dut`,
                                                `ve_crlv`,
                                                `fil_codigo`)
                                                VALUES
                                                (                                                
                                                @ve_placa,                                               
                                                @ve_cor,                                              
                                                @ve_fipe,
                                                @ve_dut,
                                                @ve_crlv,
                                                @fil_codigo);
                                            SELECT LAST_INSERT_ID();";

            }
            else
            {
                b.getComandoSQL().CommandText = @"UPDATE `veiculos`
                                                SET
                                                
                                                `ve_placa` = @ve_placa,                                                
                                                `ve_cor` = @ve_cor,                                                
                                                `ve_fipe` = @ve_fipe,
                                                `ve_dut` = @ve_dut,
                                                `ve_crlv` = @ve_crlv,
                                                `fil_codigo` = @fil_codigo
                                                WHERE `ve_codigo` = @ve_codigo;
                                                ";

                b.getComandoSQL().Parameters.AddWithValue("@ve_codigo", Veiculo.Codigo);
            }

                /*b.getComandoSQL().Parameters.AddWithValue("@ve_tipo", Veiculo.Fipe.Tipo);
                b.getComandoSQL().Parameters.AddWithValue("@ve_marca", Veiculo.Fipe.Marca);
                b.getComandoSQL().Parameters.AddWithValue("@ve_modelo", Veiculo.Fipe.Modelo);*/
                b.getComandoSQL().Parameters.AddWithValue("@ve_placa", Veiculo.Placa);
                //b.getComandoSQL().Parameters.AddWithValue("@ve_ano", Veiculo.Fipe.Ano);
                b.getComandoSQL().Parameters.AddWithValue("@ve_cor", Veiculo.Cor);
                /*b.getComandoSQL().Parameters.AddWithValue("@ve_combustivel", Veiculo.Fipe.Combustivel);
                b.getComandoSQL().Parameters.AddWithValue("@ve_anoRef", Veiculo.Fipe.AnoRef);*/
                b.getComandoSQL().Parameters.AddWithValue("@ve_fipe", Veiculo.Fipe.Codigo);
                b.getComandoSQL().Parameters.AddWithValue("@ve_dut", Veiculo.DUT);
                b.getComandoSQL().Parameters.AddWithValue("@ve_crlv", Veiculo.CRLV);
                b.getComandoSQL().Parameters.AddWithValue("@fil_codigo", Veiculo.Filial.GetCodigo());

            int cod = 0;

            if (Veiculo.Codigo == 0)
            {
                if (b.ExecutaComando(true, out cod) == 1)
                {
                    Veiculo.Codigo = cod;
                    return true;
                }
            }
            else
                return b.ExecutaComando(true) == 1;

            return false;
        }

        internal Veiculo Buscar(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select * from veiculos where ve_codigo = @codigo;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            DataTable dt = b.ExecutaSelect();

            if (dt.Rows.Count > 0)
            {
                Veiculo a = TableToList(dt).FirstOrDefault();
                
                return a;
            }
            else
                return null;
        }

        internal Veiculo BuscarVeiculoAtivo(int Codigo)
        {
            b.getComandoSQL().Parameters.Clear();

            b.getComandoSQL().CommandText = @"select * from veiculos v inner join ativos a on a.ve_codigo = v.ve_codigo 
                                where ati_codigo = @codigo;";
            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            DataTable dt = b.ExecutaSelect(true);

            if (dt.Rows.Count > 0)
            {
                Veiculo a = TableToList(dt).FirstOrDefault();

                return a;
            }
            else
                return new Veiculo(); ;
        }

    }
}
