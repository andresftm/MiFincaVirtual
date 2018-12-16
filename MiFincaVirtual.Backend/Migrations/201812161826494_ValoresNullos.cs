namespace MiFincaVirtual.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValoresNullos : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VacasLactancias", "LecheVacasLactancias", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.VacasLactancias", "LecheValorVacasLactancias", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.VacasLactancias", "CuidoVacasLactancias", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.VacasLactancias", "CuidoValorVacasLactancias", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.VacasLactancias", "UtilidadVacasLactancias", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VacasLactancias", "UtilidadVacasLactancias", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.VacasLactancias", "CuidoValorVacasLactancias", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.VacasLactancias", "CuidoVacasLactancias", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.VacasLactancias", "LecheValorVacasLactancias", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.VacasLactancias", "LecheVacasLactancias", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
