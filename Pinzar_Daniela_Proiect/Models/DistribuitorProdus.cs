using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pinzar_Daniela_Proiect.Models
{
    public class DistribuitorProdus
    {
        public int DistribuitorID { get; set; }
        public int ProdusID { get; set; }
        public Distribuitor Distribuitor { get; set; }
        public Produs Produs { get; set; }
    }
}
