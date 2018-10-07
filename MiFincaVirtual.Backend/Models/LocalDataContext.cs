
namespace MiFincaVirtual.Backend.Models
{
    using Domain.Models;
    using MiFincaVirtual.Common.Models;
    using System.Data.Entity;

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
        }

        public DbSet<Razas> Razas { get; set; }

        public DbSet<Animales> Animales { get; set; }

        public DbSet<AnimalesTipos> AnimalesTipos { get; set; }

        public DbSet<Fincas> Fincas { get; set; }

        public System.Data.Entity.DbSet<MiFincaVirtual.Common.Models.CerdasCargadas> CerdasCargadas { get; set; }
    }
}