//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace gestionNavette.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class demande
    {
        public int Id { get; set; }


        [Display(Name = "Heure de deppart")]
        [Required(ErrorMessage = "ce champs est obligatoire")]
        public System.TimeSpan heureD { get; set; }


        [Display(Name = "Ville de depart")]
        [Required(ErrorMessage = "ce champs est obligatoire")]
        public string villeD { get; set; }


        [Display(Name = "Ville d'arrivé")]
        [Required(ErrorMessage = "ce champs est obligatoire")]
        public string villeA { get; set; }
        public int id_client { get; set; }
    
        public virtual client client { get; set; }
    }
}
