namespace Empeño.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lastChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bitacoras", "EmpleadoId", c => c.Int());
            AddColumn("dbo.Bitacoras", "Usuario", c => c.String());
            CreateIndex("dbo.Bitacoras", "EmpleadoId");
            AddForeignKey("dbo.Bitacoras", "EmpleadoId", "dbo.Empleadoes", "EmpleadoId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bitacoras", "EmpleadoId", "dbo.Empleadoes");
            DropIndex("dbo.Bitacoras", new[] { "EmpleadoId" });
            DropColumn("dbo.Bitacoras", "Usuario");
            DropColumn("dbo.Bitacoras", "EmpleadoId");
        }
    }
}
