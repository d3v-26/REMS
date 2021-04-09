using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace REMS.Models
{
    public enum PropertyType
    {
        Residential, Garage, Apartment, Room, Commercial, Other
    }
    public enum PropertyMode
    {
        Rent, Sell
    }
    public enum PropertyStatus
    {
        Verified, Pending, Unfit
    }
    public enum PropertyDetail
    {
        Sold, Unsold
    }
    public class PropertyModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        [Display(Name = "Property Name")]
        public string name { get; set; }

        [Required]
        public string image { get; set; }

        [Required]
        [Display(Name = "Property Type")]
        public PropertyType propertyType { get; set; }

        [Required]
        [Display(Name = "Property Mode")]
        public PropertyMode propertyMode { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string address { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string description { get; set; }

        [Required]
        [Display(Name = "Property Status")]
        public PropertyStatus propertyStatus { get; set; }

        [Required]
        [Display(Name = "Property Details")]
        public PropertyDetail propertyDetail { get; set; }
        
        [Required]
        [Display(Name = "Price")]
        public int price { get; set; }

        [Required]
        [Display(Name = "Owner")]
        public string owner { get; set; }
    }
}
