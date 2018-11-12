namespace MiFincaVirtual.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;

    public class Inventarios
    {
        [Key]
        [DataMember]
        public int InventarioId { get; set; }

        #region Tipo de Cuido
        [Range(1, 32767, ErrorMessage = "Debe seleccionar un tipo de cuido")]
        [DataMember]
        public int OpcionId { get; set; }

        [DataMember]
        public virtual Opciones Opciones { get; set; }
        #endregion

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de ingreso")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataMember]
        public DateTime FechaIngreso { get; set; }

        /// Cantidad ingresada
        [Required]
        [Display(Name = "Cantidad Entrada")]
        [DataMember]
        public int CantidadInventario { get; set; }

        /// Precio de lo comprado.
        [Required]
        [Display(Name = "Precio")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataMember]
        public Decimal PrecioInventario { get; set; }

        /// Valor del transporte.
        [Required]
        [Display(Name = "Flete")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataMember]
        public Decimal FleteInventario { get; set; }

        [Display(Name = "Valor Unitario")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataMember]
        public Decimal ValorUnitarioInventario { get; set; }

        ///Valor total de lo comprado
        [Display(Name = "Valor Total")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataMember]
        public Decimal ValorTotalInventario { get; set; }

        /// Queryable se ha gastado.
        [Display(Name = "Repartido")]
        [DataMember]
        public int RepartidoInventario { get; set; }

        [NotMapped]
        [DataMember]
        public Decimal SaldoInventario
        {
            get
            {
                return CantidadInventario - RepartidoInventario;
            }
        }

        [NotMapped]
        [DataMember]
        public String CuidoSaldoInventario
        {
            get
            {
                return Opciones.Codigopcion + " :: " + SaldoInventario;
            }
        }

        /// <summary> Imagen de la factura. </summary>
        [Display(Name = "Image")]
        [DataMember]
        public string ImagePath { get; set; }

        [DataMember]
        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImagePath))
                {
                    return "farm";
                }

                return $"http://mifincavirtual-001-site2.dtempurl.com/{this.ImagePath.Substring(1)}";
            }
        }

        [NotMapped]
        [DataMember]
        public byte[] ImageArray { get; set; }

        [NotMapped]
        [DataMember]
        public String FechaIngresoS { get; set; }
    }
}
