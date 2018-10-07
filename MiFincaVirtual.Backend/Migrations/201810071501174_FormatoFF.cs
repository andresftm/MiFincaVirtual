namespace MiFincaVirtual.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FormatoFF : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CerdasCargadas", "FechaMontaCerdaCargada", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CerdasCargadas", "FechaMontaCerdaCargada", c => c.DateTime(nullable: false));
        }
    }
}
