using ProjetoAtivos.Models;
using Survey.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ProjetoAtivos.DAO
{
    public class ImagemDAO
    {
        private Banco b;

        internal ImagemDAO()
        {
            b = Banco.GetInstance();
        }
        internal List<Imagem> TableToList(DataTable dt)
        {
            List<Imagem> dados = null;
            if (dt != null && dt.Rows.Count > 0)
                dados = (from DataRow row in dt.Rows
                         select new Imagem(
                                            Convert.ToInt32(row["img_codigo"]),
                                            Encoding.UTF8.GetString((byte[])row["img_imagem"]),
                                            Convert.ToInt32(row["ati_codigo"])
                         )).ToList();
            return dados;
        }

        internal Boolean Gravar(Imagem Imagem, Localizacao Localizacao, int transf = 0)
        {
            int Codigo = 0;
            Boolean OK = false;
            byte[] Img = Encoding.UTF8.GetBytes(Imagem.GetFoto());

            b.getComandoSQL().Parameters.Clear();

            if (Imagem.GetCodigo() == 0)
            {
                b.getComandoSQL().CommandText = @"insert into imagem (img_imagem, img_dtinsercao, ati_codigo, transf_codigo) values(@imagem, @data, @ativo, @transf);
                                                    SELECT LAST_INSERT_ID();";
            }
            b.getComandoSQL().Parameters.AddWithValue("@imagem", Img);
            b.getComandoSQL().Parameters.AddWithValue("@data", Imagem.GetDataInsercao());
            b.getComandoSQL().Parameters.AddWithValue("@ativo", Imagem.GetAtivo().GetCodigo());

            if(transf != 0 )
                b.getComandoSQL().Parameters.AddWithValue("@transf", transf);
            else
                b.getComandoSQL().Parameters.AddWithValue("@transf", DBNull.Value);

            OK = b.ExecutaComando(true, out Codigo) == 1;

            if (OK)
            {
                Imagem.SetCodigo(Codigo);
                Localizacao.SetCodigoImagem(Codigo);
                LocalizacaoDAO LocalizacaoDAO = new LocalizacaoDAO();

                if (LocalizacaoDAO.Gravar(Localizacao))
                    OK = true;
                else
                    OK = false;
            }


            return OK;
        }
        internal List<Imagem> BuscarImagens(int Codigo, bool transf = false)
        {
            b.getComandoSQL().Parameters.Clear();

            if(transf)
                b.getComandoSQL().CommandText = @"select img_codigo, img_imagem, img_dtinsercao, ati_codigo,
                                                    (select count(*) from itens_ativos where img_codigo = i.img_codigo)
                                                    from imagem i where ati_codigo = @codigo;";
            else
                b.getComandoSQL().CommandText = @"select img_codigo, img_imagem, img_dtinsercao, ati_codigo,
                                                    (select count(*) from itens_ativos where img_codigo = i.img_codigo)
                                                    from imagem i where ati_codigo = @codigo and transf_codigo is null;";

            b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

            
            DataTable dt = b.ExecutaSelect(true);

            if (dt.Rows.Count > 0)
                return TableToList(dt);
            else
                return null;
        }

        internal Boolean Excluir(int Codigo)
        {
            

            if (new LocalizacaoDAO().Excluir(Codigo))
            {
                b.getComandoSQL().Parameters.Clear();
                b.getComandoSQL().CommandText = @"delete from imagem where img_codigo = @codigo;";
                b.getComandoSQL().Parameters.AddWithValue("@codigo", Codigo);

                return b.ExecutaComando(true) == 1;
            }

            return false;
        }

        internal bool Gravar(Imagem Imagem, int transf = 0)
        {
            int Codigo = 0;
            Boolean OK = false;
            byte[] Img = Encoding.UTF8.GetBytes(Imagem.GetFoto());

            b.getComandoSQL().Parameters.Clear();

            if (Imagem.GetCodigo() == 0)
            {
                b.getComandoSQL().CommandText = @"insert into imagem (img_imagem, img_dtinsercao, ati_codigo, transf_codigo) values(@imagem, @data, @ativo, @transf);
                                                    SELECT LAST_INSERT_ID();";
            }
            b.getComandoSQL().Parameters.AddWithValue("@imagem", Img);
            b.getComandoSQL().Parameters.AddWithValue("@data", Imagem.GetDataInsercao());
            b.getComandoSQL().Parameters.AddWithValue("@ativo", Imagem.GetAtivo().GetCodigo());
            if (transf != 0)
                b.getComandoSQL().Parameters.AddWithValue("@transf", transf);
            else
                b.getComandoSQL().Parameters.AddWithValue("@transf", DBNull.Value);

            OK = b.ExecutaComando(true, out Codigo) == 1;

            Imagem.SetCodigo(Codigo);
           
            return OK;
        }
    }
}
