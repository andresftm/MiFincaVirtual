namespace MiFincaVirtual.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LibrasdeCuido : DbMigration
    {
        public override void Up()
        {
                        
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Animales", "RazaId", "dbo.Razas");
            DropForeignKey("dbo.Animales", "AnimalTipoId", "dbo.AnimalesTipos");
            DropIndex("dbo.Animales", new[] { "AnimalTipoId" });
            DropIndex("dbo.Animales", new[] { "RazaId" });
            DropTable("dbo.Ordenos");
            DropTable("dbo.Fincas");
            DropTable("dbo.Razas");
            DropTable("dbo.AnimalesTipos");
            DropTable("dbo.Animales");
        }
    }
}
