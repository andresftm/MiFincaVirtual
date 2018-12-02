namespace MiFincaVirtual.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AcasCargadas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VacasCargadas",
                c => new
                    {
                        VacaCargadaId = c.Int(nullable: false, identity: true),
                        AnimalId = c.Int(nullable: false),
                        FechaMontaVacaCargada = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        FechaRecordacionVacaCargada = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        FechaPosiblePartoVacaCargada = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        FechaRealPartoVacaCargada = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        FechaDesteteVacaCargada = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        ActivoVacaCargada = c.Boolean(nullable: false),
                        NacidosVacaCargada = c.Int(nullable: false),
                        SexoCriaVacaCargada = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.VacaCargadaId)
                .ForeignKey("dbo.Animales", t => t.AnimalId)
                .Index(t => t.AnimalId);
            
            AddColumn("dbo.CerdasCargadas", "FechaDestetePartoCerdaCargada", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VacasCargadas", "AnimalId", "dbo.Animales");
            DropIndex("dbo.VacasCargadas", new[] { "AnimalId" });
            DropColumn("dbo.CerdasCargadas", "FechaDestetePartoCerdaCargada");
            DropTable("dbo.VacasCargadas");
        }
    }
}
