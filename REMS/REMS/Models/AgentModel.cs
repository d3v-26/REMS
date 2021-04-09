using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace REMS.Models
{
    public class AgentModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        [Display(Name = "Agent Name")]
        public string name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email")]
        public string email { get; set; }
    }
}
