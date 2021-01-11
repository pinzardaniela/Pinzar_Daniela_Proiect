using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pinzar_Daniela_Proiect.Models
{
    
        public class Produs
        {
            public int ID { get; set; }
            public string Denumire { get; set; }
            public string Furnizor { get; set; }

            //[Column(TypeName = "decimal(6, 2)")]
            public decimal Pret { get; set; }
            public ICollection<Comanda> Comenzi { get; set; }

            public ICollection<DistribuitorProdus> DistribuitorProduse { get; set; }

    }
    
}
