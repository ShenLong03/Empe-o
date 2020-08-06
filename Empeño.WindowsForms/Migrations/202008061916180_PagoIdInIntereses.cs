namespace Empeño.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PagoIdInIntereses : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Intereses", "PagoId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Intereses", "PagoId");
        }
    }
}
