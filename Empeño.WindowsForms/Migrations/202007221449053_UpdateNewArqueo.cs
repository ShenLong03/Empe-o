namespace Empeño.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNewArqueo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Prorrogas",
                c => new
                    {
                        ProrrogaId = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        DiasProrroga = c.Int(nullable: false),
                        EmpenoId = c.Int(nullable: false),
                        EmpleadoId = c.Int(nullable: false),
                        Comentario = c.String(),
                    })
                .PrimaryKey(t => t.ProrrogaId)
                .ForeignKey("dbo.Empenoes", t => t.EmpenoId)
                .ForeignKey("dbo.Empleadoes", t => t.EmpleadoId)
                .Index(t => t.EmpenoId)
                .Index(t => t.EmpleadoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prorrogas", "EmpleadoId", "dbo.Empleadoes");
            DropForeignKey("dbo.Prorrogas", "EmpenoId", "dbo.Empenoes");
            DropIndex("dbo.Prorrogas", new[] { "EmpleadoId" });
            DropIndex("dbo.Prorrogas", new[] { "EmpenoId" });
            DropTable("dbo.Prorrogas");
        }
    }
}
