namespace MiFincaVirtual.Common.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Animales
    {
        [Key]
        public int AnimalId { get; set; }

        /// <summary> Código del animal. </summary>
        [Required]
        [Display(Name = "Código")]
        public string CodigoAnimal { get; set; }

        /// <summary> Descripción del animal. </summary>
        [Display(Name = "Descripción")]
        public string DescripcionAnimal { get; set; }

        /// Fecha de ingreso del animal a la finca.
        [Display(Name = "Fecha Ingreso")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime FechaIngresoAnimal { get; set; }

        ///Fecha de nacimiento del animal.
        [Display(Name = "Fecha Nacimiento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime FechaNacimientoAnimal { get; set; }

        ///Indica si existe el animal.
        [Display(Name = "Existe")]
        public bool ActivoAnimal { get; set; }

        ///Indica si pertenece a la finca
        [Display(Name = "Pertenece a la finca")]
        public bool PerteneceAnimal { get; set; }

        /// Indica verdadero si es hembra, de lo contrario es macho.
        [Display(Name = "Hembra")]
        public bool EshembraAnimal { get; set; }

        /// <summary> Nombre del padre del animal. </summary>
        [Display(Name = "Padre")]
        public string PadreAnimal { get; set; }

        /// Nombre de la madre del animal.
        [Display(Name = "Madre")]
        public string MadreAnimal { get; set; }

        #region Raza
        [Range(1, 32767, ErrorMessage = "Debe seleccionar una raza")]
        public int RazaId { get; set; }

        public virtual Razas Razas { get; set; }
        #endregion

        #region Tipo de animal
        [Display(Name = "Tipo")]
        [Range (1, 32767, ErrorMessage="Debe seleccionar un tipo de animal")]
        public int OpcionId { get; set; }

        public virtual Opciones Opciones { get; set; }
        #endregion

        [JsonIgnore]
        public virtual ICollection<CerdasCargadas> CerdasCargadas { get; set; }

        [JsonIgnore]
        public virtual ICollection<Ordenos> Ordenos { get; set; }

    }
}
