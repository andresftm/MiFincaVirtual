namespace MiFincaVirtual.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrdenosFecha : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Ordenos", "CodigoAnimal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ordenos", "CodigoAnimal", c => c.String());
        }
    }
}
