using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace REMS.Models
{
    public class RentPropertyModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Image")]
        public string image { get; set; }

        [Required]
        [Display(Name = "Property Type")]
        public PropertyType propertyType { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string address { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string description { get; set; }

        [Required]
        [Display(Name = "Price")]
        public int price { get; set; }

        [Required]
        [Display(Name = "Owner")]
        public string owner { get; set; }
        
    }
}
