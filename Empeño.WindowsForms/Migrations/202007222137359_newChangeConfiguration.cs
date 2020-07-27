namespace Empeño.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newChangeConfiguration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Configuracions", "Compañia", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Configuracions", "Compañia");
        }
    }
}
