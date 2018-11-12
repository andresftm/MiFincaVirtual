namespace MiFincaVirtual.Backend.Models
{
    using MiFincaVirtual.Common.Models;
    using System.Runtime.Serialization;
    using System.Web;

    public class InventariosView: Inventarios
    {
        [DataMember]

        public HttpPostedFileBase ImageFile { get; set; }
    }
}