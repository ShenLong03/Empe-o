namespace Empeño.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNewArqueo2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empenoes", "Prorroga", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Empenoes", "Prorroga");
        }
    }
}
