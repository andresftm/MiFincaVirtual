namespace MiFincaVirtual.Backend.Models
{
    using MiFincaVirtual.Common.Models;
    using System.Collections.Generic;
    using System.Web.Routing;

    public class ordenosPaginados: BaseModelo
    {
        public List<Ordenos> OrdenosO { get; set; }
    }
}