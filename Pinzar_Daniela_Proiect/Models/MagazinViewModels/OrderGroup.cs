using System;
using System.ComponentModel.DataAnnotations;

namespace Pinzar_Daniela_Proiect.Models.MagazinViewModels
{
    public class OrderGroup
    {
        [DataType(DataType.Date)]
        public DateTime? DataComenzii { get; set; }
        public int ProduseCount { get; set; }

    }
}
