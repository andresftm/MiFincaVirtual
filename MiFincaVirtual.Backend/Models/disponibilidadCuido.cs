using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiFincaVirtual.Backend.Models
{
    public class disponibilidadCuido
    {
        public int OpcionId { get; set; }

        public String Codigopcion { get; set; }

        public Decimal Existencia { get; set; }

        public String ExistenciaS
        {
            get
            {
                return Existencia.ToString("#,#00.00");
            }
        }

        public Decimal ConsumoDiario { get; set; }

        public String ConsumoDiarioS
        {
            get
            {
                return ConsumoDiario.ToString("#,#00.00");
            }
        }

        public Decimal DiasDisponibles { get; set; }

        public String DiasDisponiblesS
        {
            get
            {
                return DiasDisponibles.ToString("#,#00.00");
            }
        }

    }
}