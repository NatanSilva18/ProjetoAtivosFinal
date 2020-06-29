using ProjetoAtivos.DAO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAtivos.Models
{
    public class Documento
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Caminho { get; set; }
        public string ContentType { get; set; }
        public string Doc { get; set; } //File

        public bool GravarArquivo()
        {
            bool ok = true;

            try
            {
                
                byte[] dataArray = Convert.FromBase64String(Doc);
                // Create random data to write to the file.
                //new Random().NextBytes(dataArray);

                File.WriteAllBytes(Caminho, dataArray);
                /*
                using (FileStream
                    fileStream = new FileStream(Caminho, FileMode.Create))
                {
                    // Write the data to the file, byte by byte.
                    for (int i = 0; i < dataArray.Length; i++)
                    {
                        fileStream.WriteByte(dataArray[i]);
                    }

                    // Set the stream position to the beginning of the file.
                    fileStream.Seek(0, SeekOrigin.Begin);
                }*/
            }
            catch(Exception e)
            {
                ok = false;
            }

            return ok;
        }

        public bool Gravar(int transf)
        {
            return new DocumentoDAO().Gravar(this, transf);
        }

        public static void ExcluirArquivos(List<Documento> l)
        {
            foreach (Documento d in l)
            {
                File.Delete(d.Caminho);
            }
        }

        public List<object> Busca(Transferencia t)
        {
            return new DocumentoDAO().Buscar(t);
        }

        public Documento Busca(int cod)
        {
            return new DocumentoDAO().Buscar(cod);
        }
    }
}
