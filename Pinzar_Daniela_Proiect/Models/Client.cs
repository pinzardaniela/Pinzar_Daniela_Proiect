using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pinzar_Daniela_Proiect.Models
{
    public class Client
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ClientID { get; set; }
        public string Nume { get; set; }
        public string Adresa { get; set; }
        public DateTime DataNasterii { get; set; }
        public ICollection<Comanda> Comenzi { get; set; }

    }
}

