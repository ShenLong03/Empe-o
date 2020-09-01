namespace Empeño.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsDeleteCustomers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clientes", "IsDelete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clientes", "IsDelete");
        }
    }
}
