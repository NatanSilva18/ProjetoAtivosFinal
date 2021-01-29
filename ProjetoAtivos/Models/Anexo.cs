using System;
using System.Collections.Generic;
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
        public Ativo Ativo{ get; set; }

        public Anexo()
        {
            Codigo = 0;
            Nome = Local = Type = "";
            Ativo = new Ativo();
        }
    }
}
