
namespace MiFincaVirtual.Backend.Models
{
    using Domain.Models;
    using MiFincaVirtual.Common.Models;
    using System;
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
            modelBuilder.Properties<DateTime>().Configure(x => x.HasColumnType("datetime2"));
            Database.SetInitializer<LocalDataContext>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<Razas> Razas { get; set; }

        public DbSet<Animales> Animales { get; set; }

        public DbSet<Fincas> Fincas { get; set; }

        public DbSet<CerdasCargadas> CerdasCargadas { get; set; }

        public DbSet<Quesos> Quesos { get; set; }

        public DbSet<Opciones> Opciones { get; set; }

        public DbSet<Corrales> Corrales { get; set; }

        public DbSet<Inventarios> Inventarios { get; set; }

        public DbSet<Lotes> Lotes { get; set; }

        public DbSet<LotesComida> LotesComidas { get; set; }

        public DbSet<LotesOpciones> LotesOpciones { get; set; }
    }
}