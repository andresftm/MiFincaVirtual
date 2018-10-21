
namespace MiFincaVirtual.Backend.Models
{
    using Domain.Models;
    using MiFincaVirtual.Common.Models;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class LocalDataContext: DataContext
    {
        public LocalDataContext()
        {
            Database.SetInitializer<LocalDataContext>(new DropCreateDatabaseAlways<LocalDataContext>());
        }

        public DbSet<Ordenos> Ordenos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<LocalDataContext>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<Razas> Razas { get; set; }

        public DbSet<Animales> Animales { get; set; }

        public DbSet<Fincas> Fincas { get; set; }

        public System.Data.Entity.DbSet<MiFincaVirtual.Common.Models.CerdasCargadas> CerdasCargadas { get; set; }

        public System.Data.Entity.DbSet<MiFincaVirtual.Common.Models.Quesos> Quesos { get; set; }

        public System.Data.Entity.DbSet<MiFincaVirtual.Common.Models.Opciones> Opciones { get; set; }

        public System.Data.Entity.DbSet<MiFincaVirtual.Common.Models.Corrales> Corrales { get; set; }

        public System.Data.Entity.DbSet<MiFincaVirtual.Common.Models.Inventarios> Inventarios { get; set; }

        public System.Data.Entity.DbSet<MiFincaVirtual.Common.Models.Lotes> Lotes { get; set; }

        public System.Data.Entity.DbSet<MiFincaVirtual.Common.Models.LotesComida> LotesComidas { get; set; }
    }
}