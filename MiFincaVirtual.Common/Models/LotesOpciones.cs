namespace MiFincaVirtual.Common.Models
{
    using System.ComponentModel.DataAnnotations;
    public class LotesOpciones
    {
        [Key]
        public int LoteOpcionId { get; set; }

        #region Lotes
        [Range(1, 32767, ErrorMessage = "")]
        public int LoteId { get; set; }

        /// <summary> Tipo de cuido, por ejemplo, Pre iniciador, Iniciador, Chanchito.</summary>
        public virtual Lotes Lotes { get; set; }
        #endregion

        #region Opcion
        [Range(1, 32767, ErrorMessage = "")]
        public int OpcionId { get; set; }

        /// <summary> Tipo de cuido, por ejemplo, Pre iniciador, Iniciador, Chanchito.</summary>
        public virtual Opciones Opciones { get; set; }
        #endregion
    }
}
