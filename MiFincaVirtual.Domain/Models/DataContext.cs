namespace MiFincaVirtual.Domain.Models
{
    using MiFincaVirtual.Common.Models;
    using System.Data.Entity;

    public class DataContext: DbContext
    {
        public DataContext():  base("DefaultConnection")
        {
                
        }

        public DbSet<Razas> Razas { get; set; }

        public System.Data.Entity.DbSet<MiFincaVirtual.Common.Models.Ordenos> Ordenos { get; set; }

        public System.Data.Entity.DbSet<MiFincaVirtual.Common.Models.Fincas> Fincas { get; set; }

        public System.Data.Entity.DbSet<MiFincaVirtual.Common.Models.Animales> Animales { get; set; }

        public System.Data.Entity.DbSet<MiFincaVirtual.Common.Models.Opciones> Opciones { get; set; }

        public System.Data.Entity.DbSet<MiFincaVirtual.Common.Models.LotesComida> CorralesComidas { get; set; }

        public System.Data.Entity.DbSet<MiFincaVirtual.Common.Models.Corrales> Corrales { get; set; }
    }
}
