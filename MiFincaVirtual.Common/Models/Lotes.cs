namespace MiFincaVirtual.Common.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    public class Lotes
    {
        [Key]
        [DataMember]
        public int LoteId { get; set; }

        [Required]
        [Display(Name = "Lote")]
        [DataMember]
        public String NombreLote { get; set; }

        [Required]
        [Display(Name = "Descripción")]
        [DataMember]
        public String DescripcionLote { get; set; }

        [Required]
        [Display(Name = "Animales")]
        [DataMember]
        public int AnimalesLote { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataMember]
        public DateTime FechaAperturaLote { get; set; }

        [DataMember]
        [NotMapped]
        public String FechaApertura { get; set; }

        [Required]
        [Display(Name ="Kilos inicial")]
        [DataMember]
        public Decimal KilosCarneInicialLote { get; set; }

        [Required]
        [Display(Name ="Valor inicial")]
        [DataMember]
        public Decimal ValorInicialLote { get; set; }

        [Required]
        [Display(Name = "Madre")]
        [DataMember]
        public String MadreLote { get; set; }

        [Required]
        [Display(Name = "Padre")]
        [DataMember]
        public String PadreLote { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataMember]
        public DateTime FechaFinalLote { get; set; }

        [DataMember]
        [NotMapped]
        public String FechaFinal { get; set; }

        [Display(Name = "Kilos Final")]
        [DataMember]
        public Decimal KilosCarneFinalLote { get; set; }

        [Required]
        [Display(Name = "Valor final")]
        [DataMember]
        public Decimal ValorFinalLote { get; set; }

        [Display(Name = "Cerrar lote")]
        [DataMember]
        public Boolean CerradoLote { get; set; }

        [Display(Name = "Gramos x día")]
        [DataMember]
        public Decimal GramosConsumoDiaLote { get; set; }

        [JsonIgnore]
        public virtual ICollection<LotesOpciones> LotesOpciones { get; set; }

        [DataMember]
        public int OpcionId { get; set; }

        [DataMember]
        public int CuidoId { get; set; }
    }
}
