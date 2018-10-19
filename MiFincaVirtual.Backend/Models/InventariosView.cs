namespace MiFincaVirtual.Backend.Models
{
    using MiFincaVirtual.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    public class InventariosView: Inventarios
    {
        public HttpPostedFileBase ImageFile { get; set; }
    }
}