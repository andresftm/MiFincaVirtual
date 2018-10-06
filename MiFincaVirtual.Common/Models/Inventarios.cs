using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiFincaVirtual.Common.Models
{
    public class Inventarios
    {
        [Key]
        public int InventarioId { get; set; }

        /// Código del cuido
        [Required]
        [Display(Name ="Producto")]
        public String CodigoCuido { get; set; }

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

        [Required]
        [Display(Name = "Valor Unitario")]
        public Decimal ValorUnitarioInventario { get; set; }

        ///Valor total de lo comprado
        [Required]
        [Display(Name = "Valor Total")]
        public Decimal ValorTotalInventario { get; set; }

        /// Queryable se ha gastado.
        [Display(Name = "Repartido")]
        public int RepartidoInventario { get; set; }
    }
}
