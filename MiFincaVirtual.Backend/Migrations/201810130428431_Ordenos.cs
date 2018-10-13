namespace MiFincaVirtual.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ordenos : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ordenos", "CodigoAnimal", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ordenos", "CodigoAnimal", c => c.String(nullable: false));
        }
    }
}
