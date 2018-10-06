namespace MiFincaVirtual.Backend.Models
{
    using MiFincaVirtual.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class FincasView: Fincas
    {
        public HttpPostedFileBase ImageFile { get; set; }
    }
}