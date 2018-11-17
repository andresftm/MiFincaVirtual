using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiFincaVirtual.Backend.Models
{
    public class LotesW : MiFincaVirtual.Common.Models.Lotes
    {
        public String TipoAnimal { get; set; }

        public String Cuido { get; set; }
    }
}