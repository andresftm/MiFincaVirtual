namespace MiFincaVirtual.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inventario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inventarios", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inventarios", "ImagePath");
        }
    }
}
