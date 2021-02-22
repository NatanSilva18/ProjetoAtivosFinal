using ProjetoAtivos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAtivos.Control
{
    public class InventarioControl
    {
        public List<object> Buscar(string DtIni, string DtFim, int Regiao, int Filial)
        {
            return new Inventario().Buscar(DtIni, DtFim, Regiao, Filial);
        }
    }
}
