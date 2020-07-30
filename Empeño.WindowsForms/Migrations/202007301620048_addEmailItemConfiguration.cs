namespace Empeño.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEmailItemConfiguration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Configuracions", "EmailNotification", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Configuracions", "EmailNotification");
        }
    }
}
