namespace MiFincaVirtual.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class CerdasCargadas
    {
        [Key]
        public int CerdaCargadaId { get; set; }

        #region Animal
        public int AnimalId { get; set; }

        public virtual Animales Animales { get; set; }
        #endregion

        [Required]
        [Display(Name = "Cargada")]
        [Column(TypeName = "datetime2")]
        [DataType(DataType.Date)]
        public DateTime FechaMontaCerdaCargada { get; set; }

        [Required]
        [Display(Name = "Recordación")]
        [Column(TypeName = "datetime2")]
        public DateTime FechaRecordacionCerdaCargada { get; set; }

        [Required]
        [Display(Name = "Inyectar")]
        [Column(TypeName = "datetime2")]
        public DateTime FechaInyectarCerdaCargada { get; set; }

        [Required]
        [Display(Name = "Posible parto")]
        [Column(TypeName = "datetime2")]
        public DateTime FechaPosiblePartoCerdaCargada { get; set; }

        [Display(Name = "Fecha parto")]
        [Column(TypeName = "datetime2")]
        [DataType(DataType.Date)]
        public DateTime FechaRealPartoCerdaCargada { get; set; }

        [Required]
        [Display(Name = "Activo")]
        public Boolean ActivoCerdaCargada { get; set; }

        [Display(Name = "Nacidos")]
        public int NacidosCerdaCargada { get; set; }

        [Display(Name = "Vivos")]
        public int NacidosVivosCerdaCargada { get; set; }

        [Display(Name = "Muertos")]
        public int NacidosMuertosCerdaCargada { get; set; }

        [Display(Name = "Momias")]
        public int NacidosMomiasCerdaCargada { get; set; }

        [Display(Name = "Destetos")]
        public int DestetosCerdaCargada { get; set; }
    }
}
