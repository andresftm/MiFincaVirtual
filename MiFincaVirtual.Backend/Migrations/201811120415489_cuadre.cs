namespace MiFincaVirtual.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cuadre : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.LotesComidas", "ProcesadoLoteComida");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LotesComidas", "ProcesadoLoteComida", c => c.Boolean(nullable: false));
        }
    }
}
