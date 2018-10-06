namespace MiFincaVirtual.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Fincas
    {
        [Key]
        public int FincaId { get; set; }

        /// <summary> Nombre de la finca. </summary>
        [Required]
        [Display(Name = "Finca")]
        [StringLength(50)]
        public string NombreFinca { get; set; }

        /// <summary> País donde esta ubicada la finca </summary>
        [Display(Name = "País")]
        public string PaisFinca { get; set; }

        /// <summary> Departamento donde esta la finca </summary>
        [Display(Name = "Estado/Provincia")]
        public string EstadoFinca { get; set; }

        /// <summary> Municipio donde esta ubicada la finca. </summary>
        [Display(Name = "Ciudad")]
        public string CiudadFinca { get; set; }

        /// <summary> Fecha en la que se registró la finca. </summary>
        [Display(Name = "Fecha Registro")]
        [DataType(DataType.Date)]
        public DateTime IngresoFinca { get; set; }

        /// <summary> Indica si la fecha esta activada o no. </summary>
        [Display(Name = "Activa")]
        public bool HabilitadaFinca { get; set; }

        /// <summary> Imagen de la finca. </summary>
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
