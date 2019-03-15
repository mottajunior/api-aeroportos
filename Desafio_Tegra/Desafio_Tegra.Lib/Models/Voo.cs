using System;
using System.Collections.Generic;

namespace Desafio_Tegra.Lib.Models
{
    public class Voo
    {
        public string voo { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public DateTime DataSaida { get; set; }
        public TimeSpan Saida { get; set; }
        public TimeSpan Chegada { get; set; }
        public decimal Preco { get; set; }
        public int idOperadora {get; set;}
        public List<Voo> trechos {get; set;}
         public string Operadora { get; set; }
     
        
    }
}
