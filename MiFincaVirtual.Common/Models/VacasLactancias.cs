namespace MiFincaVirtual.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class VacasLactancias
    {
        [Key]
        public int VacaLactanciaId { get; set; }

        #region Vacas cargadas
        [Range(1, 32767, ErrorMessage = "Debe seleccionar un bovino")]
        public int? VacaCargadaId { get; set; }

        public virtual VacasCargadas VacasCargadas { get; set; }
        #endregion

        #region Animales
        [Range(1, 32767, ErrorMessage = "Debe seleccionar un bovino")]
        public int? AnimalId { get; set; }

        public virtual Animales Animales { get; set; }
        #endregion

        [Display(Name = "Terneros")]
        public int TernerosVacasLactancias { get; set; }

        [Display(Name = "Activo")]
        public Boolean ActivoVacasLactancias { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Inicial")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicialVacasLactancias { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Final")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinalVacasLactancias { get; set; }

        [Display(Name = "Leche")]
        public Decimal? LecheVacasLactancias { get; set; }

        [Display(Name = "Leche Valor")]
        public Decimal? LecheValorVacasLactancias { get; set; }

        [Display(Name = "Cuido")]
        public Decimal? CuidoVacasLactancias { get; set; }

        [Display(Name = "Cuido Valor")]
        public Decimal? CuidoValorVacasLactancias { get; set; }

        [Display(Name = "Utilidad")]
        public Decimal? UtilidadVacasLactancias { get; set; }
    }
}
