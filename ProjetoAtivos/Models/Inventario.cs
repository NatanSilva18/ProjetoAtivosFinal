using ProjetoAtivos.DAO;
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
        public Imagem Imagem { get; set; }
        public Localizacao Localizacao { get; set; }

        public Inventario()
        {
            Codigo = 0;
        }

        public bool Gravar()
        {
            Filial = new FilialDAO().BuscarFilial(Ativo);
            Imagem.SetAtivo(Ativo);

            return new InventarioDAO().Gravar(this);

        }

        internal List<object> Buscar(string dtIni, string dtFim, int regiao, int filial)
        {
            return new InventarioDAO().Buscar(dtIni, dtFim, regiao, filial);
        }

    }
}
