namespace Empeño.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial_Pruebas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Interes", "Activo", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Interes", "Activo");
        }
    }
}
