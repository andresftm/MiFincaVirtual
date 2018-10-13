namespace MiFincaVirtual.Backend.Models
{
    using MiFincaVirtual.Common.Models;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Web.Routing;

    public class ordenosPaginados: BaseModelo
    {
        public ObservableCollection<Ordenos> OrdenosO { get; set; }
    }
}