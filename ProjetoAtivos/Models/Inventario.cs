using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAtivos.Models
{
    public class Inventario
    {
        public int Codigo { get; set; }
        public DateTime Data { get; set; }
        public string Obs { get; set; }
        public Ativo Ativo{ get; set; }        
        public Filial Filial { get; set; }


    }
}
