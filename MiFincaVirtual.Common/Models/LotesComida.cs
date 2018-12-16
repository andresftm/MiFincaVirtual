namespace MiFincaVirtual.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class LotesComida
    {
        [Key]
        public int LoteComidaId { get; set; }

        #region Lotes
        [Range(1, 32767, ErrorMessage = "Debe seleccionar un lote")]
        public int LoteId { get; set; }

        public virtual Lotes Lotes { get; set; }
        #endregion

        #region Opcion
        [Range(1, 32767, ErrorMessage = "Debe seleccionar un tipo de cuido")]
        public int OpcionId { get; set; }

        /// <summary> Tipo de cuido, por ejemplo, Pre iniciador, Iniciador, Chanchito.</summary>
        public virtual Opciones Opciones { get; set; }
        #endregion

        [Required]
        [Display(Name = "Cantidad")]
        /// <summary>La cantidad en gramos de lo que se esta registrando.</summary>
        public int CantidadLoteComida { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaLoteComida { get; set; }

        [NotMapped]
        public String FechaSLoteComida { get; set; }

    }
}