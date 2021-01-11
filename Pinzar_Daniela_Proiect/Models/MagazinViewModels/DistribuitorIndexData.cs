using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pinzar_Daniela_Proiect.Models.MagazinViewModels
{
    public class DistribuitorIndexData
    {
        public IEnumerable<Distribuitor> Distribuitori { get; set; }
        public IEnumerable<Produs> Produse { get; set; }
        public IEnumerable<Comanda> Comenzi { get; set; }
    }
}
