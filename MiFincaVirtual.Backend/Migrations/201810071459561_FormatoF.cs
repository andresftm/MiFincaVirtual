namespace MiFincaVirtual.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FormatoF : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CerdasCargadas", "FechaRecordacionCerdaCargada", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CerdasCargadas", "FechaInyectarCerdaCargada", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CerdasCargadas", "FechaPosiblePartoCerdaCargada", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.CerdasCargadas", "FechaRealPartoCerdaCargada", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CerdasCargadas", "FechaRealPartoCerdaCargada", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CerdasCargadas", "FechaPosiblePartoCerdaCargada", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CerdasCargadas", "FechaInyectarCerdaCargada", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CerdasCargadas", "FechaRecordacionCerdaCargada", c => c.DateTime(nullable: false));
        }
    }
}
