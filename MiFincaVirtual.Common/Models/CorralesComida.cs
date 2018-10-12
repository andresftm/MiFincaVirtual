namespace MiFincaVirtual.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class CorralesComida
    {
        [Key]
        public int CorralComidaId { get; set; }

        [Required]
        [Display(Name = "Cantidad")]
        /// <summary>La cantidad en gramos de lo que se esta registrando.</summary>
        public int CantidadCorralComida { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        public DateTime FechaCorralComida { get; set; }

        #region Corrales
        public int CorralId { get; set; }

        public virtual Corrales Corrales { get; set; }
        #endregion

        #region Opcion
        public int OpcionId { get; set; }

        [Required]
        /// <summary> Tipo de cuido, por ejemplo, Pre iniciador, Iniciador, Chanchito.</summary>
        public virtual Opciones Opciones { get; set; }
        #endregion
    }
}
