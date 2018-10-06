namespace MiFincaVirtual.Common.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AnimalesTipos
    {
        [Key]
        public int AnimalTipoId { get; set; }

        /// <summary> Nombre de la raza. </summary>
        [Display(Name = "Tipo")]
        [Required]
        public string TipoAnimalTipo { get; set; }

        [JsonIgnore]
        public virtual ICollection<Animales> Animales { get; set; }
    }
}
