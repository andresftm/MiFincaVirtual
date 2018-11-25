namespace MiFincaVirtual.Common.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Ordenos
    {
        [Key]
        public int OrdenoId { get; set; }

        /// <summary> Nímero del ordeño. </summary>
        [Required]
        [Display(Name = "Ordeño")]
        [Range(1, 3, ErrorMessage = "Los ordeños son 1, 2 o 3")]
        public int NumeroOrdeno { get; set; }

        /// <summary> Listros del animal en el ordeño. </summary>
        [Required]
        [Display(Name = "Litros")]
        public Decimal LitrosOrdeno { get; set; }

        /// <summary> Peso de la leche en el ordeño </summary>
        [Required]
        [Display(Name = "Peso Leche")]
        public Decimal PesoOrdeno { get; set; }


        /// <summary>Los gramos de cuido consumidos por el animal en el ordeño.</summary>
        [Required]
        [Display(Name = "Gramos de cuido")]
        public int GramosCuidoOrdeno { get; set; }

        /// <summary> Fecha en la que se registra el ordeño. </summary>
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaOrdeno { get; set; }

        #region Animales
        [Range(1, 32767, ErrorMessage = "Debe seleccionar un bovino")]
        public int? AnimalId { get; set; }

        public virtual Animales Animales { get; set; }

        [NotMapped]
        ///Ete muestra el nombre del animal en la web
        public String Animal { get; set; }
        #endregion

    }
}
