namespace MiFincaVirtual.Common.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Opciones
    {
        [Key]
        public int OpcionId { get; set; }

        /// Nombre del Producto
        [Required]
        [Display(Name ="Código")]
        public String Codigopcion { get; set; }

        ///Descripción del Producto
        [Display(Name ="Descripción")]
        public String DescripcionOpcion { get; set; }

        ///Descripción del Producto
        [Display(Name = "Tipo")]
        public String TipoOpcion { get; set; }

        [JsonIgnore]
        public virtual ICollection<Inventarios> Inventarios { get; set; }

        [JsonIgnore]
        public virtual ICollection<LotesOpciones> LotesOpciones { get; set; }

    }
}
