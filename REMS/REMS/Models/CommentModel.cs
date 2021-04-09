using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace REMS.Models
{
    public class CommentModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Comment")]
        public string comment { get; set; }

        [Required]
        public string propname { get; set; }
    }
}
