namespace Empeño.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NuevoCierre : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DetalleCierreCajas", "Cantidad");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DetalleCierreCajas", "Cantidad", c => c.Double(nullable: false));
        }
    }
}
