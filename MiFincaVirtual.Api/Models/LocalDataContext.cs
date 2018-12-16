namespace MiFincaVirtual.Api.Models
{
    using MiFincaVirtual.Common.Models;
    using MiFincaVirtual.Domain.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class LocalDataContext : DataContext
    {
        public LocalDataContext()
        {
            Database.SetInitializer<LocalDataContext>(new DropCreateDatabaseAlways<LocalDataContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(x => x.HasColumnType("datetime2"));
            Database.SetInitializer<LocalDataContext>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public DbSet<Ordenos> Ordenos { get; set; }

    }
}