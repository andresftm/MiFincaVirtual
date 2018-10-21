namespace MiFincaVirtual.Common.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Lotes
    {
        [Key]
        public int LoteId { get; set; }

        [Required]
        [Display(Name = "Lote")]
        public String NombreLote { get; set; }

        [Required]
        [Display(Name = "Descripción")]
        public String DescripcionLote { get; set; }

        #region Opcion
        [Range(1, 32767, ErrorMessage = "Debe seleccionar un tipo de animales del lote")]
        public int OpcionId { get; set; }

        /// <summary> Tipo de cuido, por ejemplo, Pre iniciador, Iniciador, Chanchito.</summary>
        public virtual Opciones Opciones { get; set; }
        #endregion

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaAperturaLote { get; set; }

        [Required]
        [Display(Name ="Kilos inicial")]
        public Decimal KilosCarneInicialLote { get; set; }

        [Required]
        [Display(Name ="Valor inicial")]
        public Decimal ValorInicialLote { get; set; }

        [Required]
        [Display(Name = "Madre")]
        public String MadreLote { get; set; }

        [Required]
        [Display(Name = "Padre")]
        public String PadreLote { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinalLote { get; set; }

        [Display(Name = "Kilos Final")]
        public Decimal KilosCarneFinalLote { get; set; }

        [Required]
        [Display(Name = "Valor final")]
        public Decimal ValorFinalLote { get; set; }

        [Display(Name = "Cerrar lote")]
        public Boolean CerradoLote { get; set; }

        [JsonIgnore]
        public virtual ICollection<LotesComida> LotesComida { get; set; }
    }
}
