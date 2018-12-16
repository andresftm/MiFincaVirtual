namespace MiFincaVirtual.Backend.Models
{
    using MiFincaVirtual.Common.Models;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    public class Animal: Animales
    {
        #region Tipo de animal
        [Display(Name = "Tipo")]
        [Remote("SeleccionarCombo", "Home", ErrorMessage = "Debe de seleccionar un tipo de animal.")]
        public int OpcionId { get; set; }

        public virtual Opciones Opciones { get; set; }
        #endregion

    }
}