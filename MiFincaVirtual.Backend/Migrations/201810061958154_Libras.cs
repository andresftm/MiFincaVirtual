namespace MiFincaVirtual.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Libras : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ordenos", "GramosCuidoOrdeno", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ordenos", "GramosCuidoOrdeno");
        }
    }
}
