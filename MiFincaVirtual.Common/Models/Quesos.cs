namespace MiFincaVirtual.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Quesos
    {
        [Key]
        public int QuedoId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha")]
        public DateTime FechaQueso { get; set; }

        [Required]
        [Display(Name = "Cantidad/Minilitros")]
        public int LecheUtilizadaQueso { get; set; }

        [Required]
        [Display(Name = "Peso/Gramos")]
        public int PesoQueso { get; set; }

        [Required]
        [Display(Name = "Precio * 1000 Gramos")]
        public int PrecioQueso { get; set; }
    }
}
