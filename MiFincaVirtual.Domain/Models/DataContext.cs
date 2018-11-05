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

        public DbSet<Ordenos> Ordenos { get; set; }

        public DbSet<Fincas> Fincas { get; set; }

        public DbSet<Animales> Animales { get; set; }

        public DbSet<Opciones> Opciones { get; set; }

        public DbSet<Corrales> Corrales { get; set; }
    }
}
