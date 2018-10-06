namespace MiFincaVirtual.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionarFinca : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fincas",
                c => new
                    {
                        FincaId = c.Int(nullable: false, identity: true),
                        NombreFinca = c.String(nullable: false, maxLength: 50),
                        PaisFinca = c.String(),
                        EstadoFinca = c.String(),
                        CiudadFinca = c.String(),
                        IngresoFinca = c.DateTime(nullable: false),
                        HabilitadaFinca = c.Boolean(nullable: false),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.FincaId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Fincas");
        }
    }
}
