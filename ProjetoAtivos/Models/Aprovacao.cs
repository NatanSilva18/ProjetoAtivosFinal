using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAtivos.Models
{
    public class Aprovacao
    {
        public int Codigo { get; set; }
        public bool Status { get; set; }
        public string Observacao { get; set; }
        public DateTime DataInsercao { get; set; }
        public Pessoa Responsável { get; set; }
        public List<Ativo> Ativos{ get; set; }
    }
}
