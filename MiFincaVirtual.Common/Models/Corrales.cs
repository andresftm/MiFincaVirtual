namespace MiFincaVirtual.Common.Models
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Corrales
    {
        [Key]
        public int CorralId { get; set; }

        /// <summary> Nombre con el que se identifica el corral. </summary>
        [Display(Name = "Corral")]
        [Required]
        public String CodigoCorral { get; set; }

        /// <summary>. Cantidad de animales que caben en el corral.</summary>
        [Display(Name = "Cantidad Animales")]
        [Required]
        public String CantidadAnimalesCorral { get; set; }

        /// <summary>Las medidas que tiene el corral. </summary>
        [Display(Name = "Medidas")]
        [Required]
        public String MedidasCorral { get; set; }

        [Display(Name = "Activo")]
        /// <summary>Indica si el corral se esta utilizando o no.</summary>
        public Boolean ActivoCorral { get; set; }

        #region Opcion
        [Range(1, 32767, ErrorMessage = "Debe seleccionar un tipo de animal")]
        public int OpcionId { get; set; }

        /// <summary> Tipo del corral, por ejemplo, Cerdos, Cabras, Equinos.</summary>
        public virtual Opciones Opciones { get; set; }
        #endregion

        [JsonIgnore]
        public virtual ICollection<CorralesComida> CorralesComida { get; set; }
    }
}
