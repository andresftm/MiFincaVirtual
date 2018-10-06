namespace MiFincaVirtual.Common.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Razas
    {
        [Key]
        public int RazaId { get; set; }

        /// <summary> Nombre de la raza. </summary>
        [Display(Name = "Raza")]
        [Required]
        public string NombreRaza { get; set; }

        /// <summary> Descripción de la raza, por ejemplo, 3/8 braman 5/8 Holstein.</summary>
        [Display(Name = "Descripción")]
        public string DescripcionRaza { get; set; }

        [JsonIgnore]
        public virtual ICollection<Animales> Animales { get; set; }
    }
}
