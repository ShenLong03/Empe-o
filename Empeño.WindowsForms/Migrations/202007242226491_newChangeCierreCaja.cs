namespace Empeño.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newChangeCierreCaja : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CierreCajas", "SaldoInicial", c => c.Double(nullable: false));
            CreateIndex("dbo.CierreCajas", "EmpleadoId");
            AddForeignKey("dbo.CierreCajas", "EmpleadoId", "dbo.Empleadoes", "EmpleadoId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CierreCajas", "EmpleadoId", "dbo.Empleadoes");
            DropIndex("dbo.CierreCajas", new[] { "EmpleadoId" });
            DropColumn("dbo.CierreCajas", "SaldoInicial");
        }
    }
}
