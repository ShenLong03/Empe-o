namespace Empeño.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_1_3_7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vencimientos",
                c => new
                    {
                        VencimientosId = c.Int(nullable: false, identity: true),
                        EmpleadoId = c.Int(nullable: false),
                        EmpenoId = c.Int(nullable: false),
                        Consecutivo = c.Double(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.VencimientosId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Vencimientos");
        }
    }
}
