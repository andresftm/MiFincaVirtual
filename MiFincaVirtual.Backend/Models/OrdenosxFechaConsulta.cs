namespace MiFincaVirtual.Backend.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class OrdenosxFechaConsulta
    {
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Inicial")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicial { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha Final")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinal { get; set; }

        public String CodigoAnimal { get; set; }

        public Decimal LitrosOrdeno { get; set; }

        public String LitrosOrdenoS
        {
            get
            {
                return LitrosOrdeno.ToString("#,#00.00");
            }
        }

        public int GramosCuidoOrdeno { get; set; }

        public String GramosCuidoOrdenoS
        {
            get
            {
                return GramosCuidoOrdeno.ToString("#,#00.00");
            }
        }

        public Decimal PesoOrdeno { get; set; }

        public String PesoOrdenoS
        {
            get
            {
                return PesoOrdeno.ToString("#,#00.00");
            }
        }
    }
}