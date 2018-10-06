namespace MiFincaVirtual.Common.Models
{

    using System;
    using System.ComponentModel.DataAnnotations;

    public class Pesos
    {
        [Key]
        public int PesoId { get; set; }

        /// <summary> Código del animal al que se le esta registrando el peso. </summary>
        [Required]
        [Display(Name = "Animal")]
        public string CodigoAnimal { get; set; }

        /// <summary> Peso del animal. </summary>
        [Display(Name = "Peso")]
        [Required]
        public Decimal PesoPeso { get; set; }

        /// <summary> Fecha del pesaje. </summary>
        [Display(Name = "Fecha Pesaje")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime FechaPeso { get; set; }
    }
}
