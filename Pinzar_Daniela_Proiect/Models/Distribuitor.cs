using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pinzar_Daniela_Proiect.Models
{
    public class Distribuitor
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Nume Distribuitor")]
        [StringLength(50)]
        public string NumeDistribuitor { get; set; }

        [StringLength(70)]
        public string Adresa { get; set; }
        public ICollection<DistribuitorProdus> DistribuitorProduse { get; set; }
    }
}
