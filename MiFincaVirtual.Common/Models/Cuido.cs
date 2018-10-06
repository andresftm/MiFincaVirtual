namespace MiFincaVirtual.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Producto
    {
        [Key]
        public int ProductoId { get; set; }

        /// Nombre del Producto
        [Required]
        [Display(Name ="Código")]
        public String CodigoProducto { get; set; }

        ///Descripción del Producto
        [Display(Name ="Descripción")]
        public String DescripcionProducto { get; set; }

        /// Unidad de medida del producto.
        [Required]
        [Display(Name ="Unidad de medida")]
        public String UnidadMedidaProducto { get; set; }
    }
}
