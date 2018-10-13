namespace MiFincaVirtual.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Animal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ordenos", "AnimalId", c => c.Int());
            CreateIndex("dbo.Ordenos", "AnimalId");
            AddForeignKey("dbo.Ordenos", "AnimalId", "dbo.Animales", "AnimalId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ordenos", "AnimalId", "dbo.Animales");
            DropIndex("dbo.Ordenos", new[] { "AnimalId" });
            DropColumn("dbo.Ordenos", "AnimalId");
        }
    }
}
