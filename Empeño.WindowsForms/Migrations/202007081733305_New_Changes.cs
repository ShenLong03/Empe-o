namespace Empeño.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New_Changes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Intereses",
                c => new
                    {
                        InteresesId = c.Int(nullable: false, identity: true),
                        EmpenoId = c.Int(nullable: false),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaVencimiento = c.DateTime(nullable: false),
                        Monto = c.Double(nullable: false),
                        Pagado = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.InteresesId)
                .ForeignKey("dbo.Empenoes", t => t.EmpenoId)
                .Index(t => t.EmpenoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Intereses", "EmpenoId", "dbo.Empenoes");
            DropIndex("dbo.Intereses", new[] { "EmpenoId" });
            DropTable("dbo.Intereses");
        }
    }
}
