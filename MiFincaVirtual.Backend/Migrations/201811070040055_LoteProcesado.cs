namespace MiFincaVirtual.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LoteProcesado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LotesComidas", "ProcesadoLoteComida", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LotesComidas", "ProcesadoLoteComida");
        }
    }
}
