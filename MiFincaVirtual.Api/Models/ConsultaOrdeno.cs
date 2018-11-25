namespace MiFincaVirtual.Api.Models
{
    using System;

    public class ConsultaOrdeno
    {
        public int OrdenoId { get; set; }

        public int NumeroOrdeno { get; set; }

        public Decimal LitrosOrdeno { get; set; }

        public Decimal PesoOrdeno { get; set; }

        public DateTime FechaOrdeno { get; set; }

        public int GramosCuidoOrdeno { get; set; }

        public int AnimalId { get; set; }

        public String Animal { get; set; }
    }
}