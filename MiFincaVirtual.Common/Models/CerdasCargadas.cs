namespace MiFincaVirtual.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
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
        public DateTime FechaMontaCerdaCargada { get; set; }

        [Required]
        [Display(Name = "Recordación")]
        public DateTime FechaRecordacionCerdaCargada { get; set; }

        [Required]
        [Display(Name = "Inyectar")]
        public DateTime FechaInyectarCerdaCargada { get; set; }

        [Required]
        [Display(Name = "Posible parto")]
        public DateTime FechaPosiblePartoCerdaCargada { get; set; }

        [Display(Name = "Fecha parto")]
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
