namespace MiFincaVirtual.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Vaca : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.VacasCargadas", "UtilidadVacasLactancias");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VacasCargadas", "UtilidadVacasLactancias", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
