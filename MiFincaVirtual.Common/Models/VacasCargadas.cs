namespace MiFincaVirtual.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class VacasCargadas
    {
        [Key]
        public int VacaCargadaId { get; set; }

        #region Animal
        public int AnimalId { get; set; }

        public virtual Animales Animales { get; set; }
        #endregion

        [Required]
        [Display(Name = "Cargada")]
        [Column(TypeName = "datetime2")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaMontaVacaCargada { get; set; }

        [Required]
        [Display(Name = "Recordación")]
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaRecordacionVacaCargada { get; set; }

        [Required]
        [Display(Name = "Posible parto")]
        [Column(TypeName = "datetime2")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaPosiblePartoVacaCargada { get; set; }

        [Display(Name = "Fecha parto")]
        [Column(TypeName = "datetime2")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaRealPartoVacaCargada { get; set; }

        [Display(Name = "Fecha destete")]
        [Column(TypeName = "datetime2")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaDesteteVacaCargada { get; set; }

        [Required]
        [Display(Name = "Activo")]
        public Boolean ActivoVacaCargada { get; set; }

        [Display(Name = "Nacidos")]
        public int NacidosVacaCargada { get; set; }

        [Display(Name = "Sexo cría")]
        public Boolean SexoCriaVacaCargada { get; set; }

        [NotMapped]
        public String FechaMontaVacaCargadaS { get; set; }

        [NotMapped]
        public String FechaRecordacionVacaCargadaS { get; set; }

        [NotMapped]
        public String FechaPosiblePartoVacaCargadaS { get; set; }

        [NotMapped]
        public String FechaDesteteVacaCargadaS { get; set; }

        [NotMapped]
        public String FechaRealPartoVacaCargadaS { get; set; }

    }
}
