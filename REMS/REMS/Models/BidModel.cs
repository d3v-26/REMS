using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace REMS.Models
{
    public class BidModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        [Display(Name = "Property Name")]
        public string propname { get; set; }

        [Required]
        [Display(Name = "Customer Name")]
        public string customername { get; set; }

        [Required]
        [Display(Name = "Bid")]
        public int bid { get; set; }
    }
}
