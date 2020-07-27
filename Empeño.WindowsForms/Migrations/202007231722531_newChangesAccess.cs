namespace Empeño.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newChangesAccess : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empenoes", "IsDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Empleadoes", "IsDelete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Empleadoes", "IsDelete");
            DropColumn("dbo.Empenoes", "IsDelete");
        }
    }
}
