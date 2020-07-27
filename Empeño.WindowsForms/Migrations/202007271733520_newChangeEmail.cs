namespace Empeño.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newChangeEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Configuracions", "Email", c => c.String());
            AddColumn("dbo.Configuracions", "Password", c => c.String());
            AddColumn("dbo.Configuracions", "SMTP", c => c.String());
            AddColumn("dbo.Configuracions", "Puerto", c => c.Int(nullable: false));
            AddColumn("dbo.Configuracions", "SSL", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Configuracions", "SSL");
            DropColumn("dbo.Configuracions", "Puerto");
            DropColumn("dbo.Configuracions", "SMTP");
            DropColumn("dbo.Configuracions", "Password");
            DropColumn("dbo.Configuracions", "Email");
        }
    }
}
