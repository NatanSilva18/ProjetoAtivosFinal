﻿using ProjetoAtivos.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAtivos.Models
{
    public class Veiculo 
    {
        public int Codigo { get; set; }              
        public string Placa { get; set; }
        public string Cor { get; set; }
        public string DUT { get; set; }
        public string CRLV { get; set; }
        public Fipe Fipe { get; set; }
        public Filial Filial { get; set; }

        public Veiculo()
        {
            Codigo = 0;
        }

        public bool Gravar()
        {
           
            return new VeiculoDAO().Gravar(this);
            
        }
    }
}
