namespace MiFincaVirtual.Common.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Ordenos
    {
        [Key]
        public int OrdenoId { get; set; }

        /// <summary> Código del animal.</summary>
        [Required]
        [Display(Name = "Bovino")]
        public string CodigoAnimal { get; set; }

        /// <summary> Nímero del ordeño. </summary>
        [Required]
        [Display(Name = "Ordeño")]
        [Range(1, 3, ErrorMessage = "Los ordeños son 1, 2 o 3")]
        public int NumeroOrdeno { get; set; }

        /// <summary> Listros del animal en el ordeño. </summary>
        [Required]
        [Display(Name = "Litros")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public Decimal LitrosOrdeno { get; set; }

        /// <summary> Peso de la leche en el ordeño </summary>
        [Required]
        [Display(Name = "Peso Leche")]
        public Decimal PesoOrdeno { get; set; }

        /// <summary> Fecha en la que se registra el ordeño. </summary>
        [Required]
        [DataType(DataType.Date)]
        [Display (Name = "Fecha")]
        public DateTime FechaOrdeno { get; set; }
    }
}
