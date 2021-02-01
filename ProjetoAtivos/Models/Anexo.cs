using ProjetoAtivos.DAO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAtivos.Models
{
    public class Anexo
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Local { get; set; }
        public string Type { get; set; }
        public string  Base64 { get; set; }
        public Ativo Ativo{ get; set; }

        public Anexo()
        {
            Codigo = 0;
            Nome = Type = Base64 = "";
            Local = Path.GetFullPath("Anexos/");
        }

        public Anexo Buscar(int Codigo)
        {
            return new AnexoDAO().Buscar(Codigo);
        }

        public bool Gravar()
        {
            if (Base64 != "" && Base64 != null)
            {
                Type = Nome.Split('.')[1];
                string n = Nome.Split('.')[0];
                string l = Local;

                while (File.Exists(l + n + "."+ Type))
                {
                    n += "(1)";
                }

                Local += n + "." + Type;

                try
                {
                    
                    File.WriteAllBytes(Local, Convert.FromBase64String(Base64));

                    return new AnexoDAO().Gravar(this);
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            

            return false;
        }
    }
}
