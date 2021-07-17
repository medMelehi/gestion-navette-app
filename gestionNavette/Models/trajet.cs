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

    public partial class trajet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public trajet()
        {
            this.abonnements = new HashSet<abonnement>();
        }
    
        public int Id { get; set; }

        [Display(Name = "Heure de depart")]
        [Required(ErrorMessage = "ce champs est obligatoire")]
        public System.TimeSpan heureD { get; set; }


        [Display(Name = "Heure d'arrivé")]
        [Required(ErrorMessage = "ce champs est obligatoire")]
        public System.TimeSpan heureA { get; set; }


        [Display(Name = "Ville de depart")]
        [Required(ErrorMessage = "ce champs est obligatoire")]
        public string villeD { get; set; }

        [Display(Name = "Prix")]
        [Required(ErrorMessage = "ce champs est obligatoire")]
        public double prix { get; set; }

        [Display(Name = "Ville d'arrivé")]
        [Required(ErrorMessage = "ce champs est obligatoire")]
        public string villeA { get; set; }

        [Display(Name = "Transport")]
        public int id_transport { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<abonnement> abonnements { get; set; }
        public virtual transport transport { get; set; }
    }
}
