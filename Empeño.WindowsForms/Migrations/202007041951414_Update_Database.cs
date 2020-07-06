namespace Empeño.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Database : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Interes", "Mayor", c => c.Int(nullable: false));
            AddColumn("dbo.Interes", "Menor", c => c.Int(nullable: false));
            AddColumn("dbo.Interes", "Igual", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Interes", "Igual");
            DropColumn("dbo.Interes", "Menor");
            DropColumn("dbo.Interes", "Mayor");
        }
    }
}
