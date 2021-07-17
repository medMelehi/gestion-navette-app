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

    public partial class transport
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public transport()
        {
            this.trajets = new HashSet<trajet>();
        }
    
        public int Id { get; set; }

        [Display(Name = "Type")]
        [Required(ErrorMessage = "ce champs est obligatoire")]
        public string type { get; set; }

        [Display(Name = "Marque")]
        [Required(ErrorMessage = "ce champs est obligatoire")]
        public string marque { get; set; }

        [Display(Name = "Nembre de places")]
        [Required(ErrorMessage = "ce champs est obligatoire")]
        public int capacite { get; set; }


        [Display(Name = "Climatiseur")]
        public Nullable<bool> clim { get; set; }

        [Display(Name = "Couleur")]
     
        public string couleur { get; set; }

        [Display(Name = "Nombre de portes")]
       
        public Nullable<int> nbrPortes { get; set; }
        public int id_societe { get; set; }
    
        public virtual societe societe { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<trajet> trajets { get; set; }
    }
}
