using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace gestionNavette.Models
{
    public class Login
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "vous devez remplire ce champ")]
        public string email { get; set; }

        [Display(Name = "Mot de passe")]
        [Required(ErrorMessage = "vous devez remplire ce champ")]
        public string password { get; set; }
    }
}