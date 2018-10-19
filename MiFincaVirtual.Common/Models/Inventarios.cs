using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiFincaVirtual.Common.Models
{
    public class Inventarios
    {
        [Key]
        public int InventarioId { get; set; }

        #region Tipo de Cuido
        [Range(1, 32767, ErrorMessage = "Debe seleccionar un tipo de cuido")]
        public int OpcionId { get; set; }

        public virtual Opciones Opciones { get; set; }
        #endregion

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de ingreso")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaIngreso { get; set; }

        /// Cantidad ingresada
        [Required]
        [Display(Name = "Cantidad Entrada")]
        public int CantidadInventario { get; set; }

        /// Precio de lo comprado.
        [Required]
        [Display(Name = "Precio")]
        public Decimal PrecioInventario { get; set; }

        /// Valor del transporte.
        [Required]
        [Display(Name = "Flete")]
        public Decimal FleteInventario { get; set; }

        [Display(Name = "Valor Unitario")]
        public Decimal ValorUnitarioInventario { get; set; }

        ///Valor total de lo comprado
        [Display(Name = "Valor Total")]
        public Decimal ValorTotalInventario { get; set; }

        /// Queryable se ha gastado.
        [Display(Name = "Repartido")]
        public int RepartidoInventario { get; set; }

        /// <summary> Imagen de la factura. </summary>
        [Display(Name = "Image")]
        public string ImagePath { get; set; }

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
        public byte[] ImageArray { get; set; }
    }
}
