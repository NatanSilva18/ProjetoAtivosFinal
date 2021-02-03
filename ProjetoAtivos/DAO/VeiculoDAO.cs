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
           /* if (dt != null && dt.Rows.Count > 0)
                dados = (from DataRow row in dt.Rows
                         select new Anexo()
                         {
                             Codigo = Convert.ToInt32(row["aa_codigo"]),
                             Local = row["aa_local"].ToString(),
                             Nome = row["aa_nmArquivo"].ToString(),
                             Type = row["aa_type"].ToString(),
                             Ativo = new Ativo(Convert.ToInt32(row["ati_codigo"]))
                         }
                         ).ToList();
           */
            return dados;
        }

        internal Boolean Gravar(Veiculo Veiculo)
        {
            
                b.getComandoSQL().Parameters.Clear();

            if (Veiculo.Codigo == 0)
            {

                b.getComandoSQL().CommandText = @"INSERT INTO `veiculos`
                                                (
                                                `ve_tipo`,
                                                `ve_marca`,
                                                `ve_modelo`,
                                                `ve_placa`,
                                                `ve_ano`,
                                                `ve_cor`,
                                                `ve_combustivel`,
                                                `ve_anoRef`,
                                                `ve_fipe`,
                                                `ve_dut`,
                                                `ve_crlv`,
                                                `fil_codigo`)
                                                VALUES
                                                (
                                                @ve_tipo,
                                                @ve_marca,
                                                @ve_modelo,
                                                @ve_placa,
                                                @ve_ano,
                                                @ve_cor,
                                                @ve_combustivel,
                                                @ve_anoRef,
                                                @ve_fipe,
                                                @ve_dut,
                                                @ve_crlv,
                                                @fil_codigo);";

            }
            else
            {
                b.getComandoSQL().CommandText = @"UPDATE `veiculos`
                                                SET
                                                `ve_tipo` = @ve_tipo,
                                                `ve_marca` = @ve_marca,
                                                `ve_modelo` = @ve_modelo,
                                                `ve_placa` = @ve_placa,
                                                `ve_ano` = @ve_ano,
                                                `ve_cor` = @ve_cor,
                                                `ve_combustivel` = @ve_combustivel,
                                                `ve_anoRef` = @ve_anoRef,
                                                `ve_fipe` = @ve_fipe,
                                                `ve_dut` = @ve_dut,
                                                `ve_crlv` = @ve_crlv,
                                                `fil_codigo` = @fil_codigo
                                                WHERE `ve_codigo` = @ve_codigo;
                                                ";

                b.getComandoSQL().Parameters.AddWithValue("@ve_codigo", Veiculo.Codigo);
            }

                b.getComandoSQL().Parameters.AddWithValue("@ve_tipo", Veiculo.Fipe.Tipo);
                b.getComandoSQL().Parameters.AddWithValue("@ve_marca", Veiculo.Fipe.Marca);
                b.getComandoSQL().Parameters.AddWithValue("@ve_modelo", Veiculo.Fipe.Modelo);
                b.getComandoSQL().Parameters.AddWithValue("@ve_placa", Veiculo.Placa);
                b.getComandoSQL().Parameters.AddWithValue("@ve_ano", Veiculo.Fipe.Ano);
                b.getComandoSQL().Parameters.AddWithValue("@ve_cor", Veiculo.Cor);
                b.getComandoSQL().Parameters.AddWithValue("@ve_combustivel", Veiculo.Fipe.Combustivel);
                b.getComandoSQL().Parameters.AddWithValue("@ve_anoRef", Veiculo.Fipe.AnoRef);
                b.getComandoSQL().Parameters.AddWithValue("@ve_fipe", Veiculo.Fipe.Codigo);
                b.getComandoSQL().Parameters.AddWithValue("@ve_dut", Veiculo.DUT);
                b.getComandoSQL().Parameters.AddWithValue("@ve_crlv", Veiculo.CRLV);
                b.getComandoSQL().Parameters.AddWithValue("@fil_codigo", Veiculo.Filial.GetCodigo());


            return b.ExecutaComando(true) == 1;
            
        }

    }
}
