namespace Empeño.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewChanges_CierreCaja : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CierreCajas",
                c => new
                    {
                        CierreCajaId = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        EmpleadoId = c.Int(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CierreCajaId);
            
            CreateTable(
                "dbo.DetalleCierreCajas",
                c => new
                    {
                        DetalleCierreCajaId = c.Int(nullable: false, identity: true),
                        Concepto = c.String(),
                        Valor = c.Double(nullable: false),
                        Cantidad = c.Double(nullable: false),
                        CierreCajaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DetalleCierreCajaId)
                .ForeignKey("dbo.CierreCajas", t => t.CierreCajaId)
                .Index(t => t.CierreCajaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DetalleCierreCajas", "CierreCajaId", "dbo.CierreCajas");
            DropIndex("dbo.DetalleCierreCajas", new[] { "CierreCajaId" });
            DropTable("dbo.DetalleCierreCajas");
            DropTable("dbo.CierreCajas");
        }
    }
}
