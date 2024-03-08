using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Admins
    {
        [Key]
        public int AdminId { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Min 3, max 20 caratteri")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Min 6, max 20 caratteri")]
        public string Password { get; set; }
    }
}