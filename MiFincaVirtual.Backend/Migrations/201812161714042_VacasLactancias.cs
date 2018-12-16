namespace MiFincaVirtual.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VacasLactancias : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VacasLactancias", "AnimalId", "dbo.Animales");
            DropForeignKey("dbo.VacasLactancias", "VacaCargadaId", "dbo.VacasCargadas");
            DropIndex("dbo.VacasLactancias", new[] { "VacaCargadaId" });
            DropIndex("dbo.VacasLactancias", new[] { "AnimalId" });
            DropColumn("dbo.Animales", "EsHembraLactanteAnimal");
            DropTable("dbo.VacasLactancias");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VacasLactancias",
                c => new
                    {
                        VacaLactanciaId = c.Int(nullable: false, identity: true),
                        VacaCargadaId = c.Int(nullable: false),
                        AnimalId = c.Int(nullable: false),
                        TernerosVacasLactancias = c.Int(nullable: false),
                        ActivoVacasLactancias = c.Boolean(nullable: false),
                        FechaInicialVacasLactancias = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        FechaFinalVacasLactancias = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.VacaLactanciaId);
            
            AddColumn("dbo.Animales", "EsHembraLactanteAnimal", c => c.Boolean(nullable: false));
            CreateIndex("dbo.VacasLactancias", "AnimalId");
            CreateIndex("dbo.VacasLactancias", "VacaCargadaId");
            AddForeignKey("dbo.VacasLactancias", "VacaCargadaId", "dbo.VacasCargadas", "VacaCargadaId");
            AddForeignKey("dbo.VacasLactancias", "AnimalId", "dbo.Animales", "AnimalId");
        }
    }
}
