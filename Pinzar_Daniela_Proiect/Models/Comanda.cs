using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pinzar_Daniela_Proiect.Models
{
    public class Comanda
        {
        
            public int ComandaID { get; set; }
            public int ClientID { get; set; }
            public int ProdusID { get; set; }
            public DateTime DataComenzii { get; set; }
            public Client Client { get; set; }
            public Produs Produs { get; set; }
        }
    
}
