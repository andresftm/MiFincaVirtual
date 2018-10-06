namespace MiFincaVirtual.Domain.Models
{
    using MiFincaVirtual.Common.Models;
    using System.Data.Entity;

    public class DataContext: DbContext
    {
        public DataContext():  base("DefaultConnection")
        {
                
        }

        public DbSet<Ordenos> Ordenos { get; set; }
        public DbSet<Razas> Razas { get; set; }

        public System.Data.Entity.DbSet<MiFincaVirtual.Common.Models.Fincas> Fincas { get; set; }
    }
}
