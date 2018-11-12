namespace MiFincaVirtual.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class InventariosExistencias
    {
        [Key]
        public int InventarioExistenciaId { get; set; }

        #region Tipo de animal
        [Display(Name = "Tipo")]
        [Range(1, 32767, ErrorMessage = "Debe seleccionar un tipo de cuido")]
        public int OpcionId { get; set; }

        public virtual Opciones Opciones { get; set; }
        #endregion

        [Display(Name = "Gramos")]
        public Decimal CantidadExistenteInventariosExistencias { get; set; }

        [Display(Name = "Valor Unitario")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public Decimal ValorUnitarioInventariosExistencias { get; set; }
    }
}
