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
        [DataType(DataType.Date)]
        public DateTime FechaIngresoAnimal { get; set; }

        ///Fecha de nacimiento del animal.
        [Display(Name = "Fecha Nacimiento")]
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

        public int RazaId { get; set; }

        public virtual Razas Razas { get; set; }

        public int AnimalTipoId { get; set; }

        public virtual AnimalesTipos AnimalesTipos { get; set; }
    }
}
