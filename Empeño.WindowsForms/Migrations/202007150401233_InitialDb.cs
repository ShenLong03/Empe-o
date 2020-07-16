namespace Empeño.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bitacoras",
                c => new
                    {
                        BitacoraId = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Error = c.Int(nullable: false),
                        Valor = c.String(),
                        Mensaje = c.String(),
                    })
                .PrimaryKey(t => t.BitacoraId);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        Identificacion = c.String(),
                        Nombre = c.String(),
                        Telefono = c.String(),
                        Correo = c.String(),
                        Direccion = c.String(),
                        Comentario = c.String(),
                        Fecha = c.DateTime(nullable: false),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ClienteId);
            
            CreateTable(
                "dbo.Empenoes",
                c => new
                    {
                        EmpenoId = c.Int(nullable: false, identity: true),
                        ClienteId = c.Int(nullable: false),
                        EmpleadoId = c.Int(nullable: false),
                        EditorId = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        EsOro = c.Boolean(nullable: false),
                        Monto = c.Double(nullable: false),
                        MontoPendiente = c.Double(nullable: false),
                        InteresId = c.Int(nullable: false),
                        Descripcion = c.String(),
                        Comentario = c.String(),
                        Estado = c.Int(nullable: false),
                        FechaVencimiento = c.DateTime(nullable: false),
                        FechaRetiro = c.DateTime(),
                        FechaRetiroAdministrador = c.DateTime(),
                        Retirado = c.Boolean(nullable: false),
                        RetiradoAdministrador = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmpenoId)
                .ForeignKey("dbo.Clientes", t => t.ClienteId)
                .ForeignKey("dbo.Empleadoes", t => t.EditorId)
                .ForeignKey("dbo.Empleadoes", t => t.EmpleadoId)
                .ForeignKey("dbo.Interes", t => t.InteresId)
                .Index(t => t.ClienteId)
                .Index(t => t.EmpleadoId)
                .Index(t => t.EditorId)
                .Index(t => t.InteresId);
            
            CreateTable(
                "dbo.Empleadoes",
                c => new
                    {
                        EmpleadoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Telefono = c.String(),
                        Correo = c.String(),
                        Usuario = c.String(),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmpleadoId);
            
            CreateTable(
                "dbo.Interes",
                c => new
                    {
                        InteresId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Porcentaje = c.Double(nullable: false),
                        Mayor = c.Int(nullable: false),
                        Menor = c.Int(nullable: false),
                        Igual = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InteresId);
            
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
            
            CreateTable(
                "dbo.Pagoes",
                c => new
                    {
                        PagoId = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        EmpenoId = c.Int(nullable: false),
                        TipoPago = c.Int(nullable: false),
                        Monto = c.Double(nullable: false),
                        EmpleadoId = c.Int(nullable: false),
                        Comentario = c.String(),
                    })
                .PrimaryKey(t => t.PagoId)
                .ForeignKey("dbo.Empenoes", t => t.EmpenoId)
                .ForeignKey("dbo.Empleadoes", t => t.EmpleadoId)
                .Index(t => t.EmpenoId)
                .Index(t => t.EmpleadoId);
            
            CreateTable(
                "dbo.Configuracions",
                c => new
                    {
                        ConfiguracionId = c.Int(nullable: false, identity: true),
                        Identificacion = c.String(),
                        Nombre = c.String(),
                        Telefono = c.String(),
                        Meses = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ConfiguracionId);
            
            CreateTable(
                "dbo.Perfils",
                c => new
                    {
                        PerfilId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.PerfilId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        Usuario = c.String(),
                        Password = c.String(),
                        Codigo = c.String(),
                        Activo = c.Boolean(nullable: false),
                        PerfilId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UsuarioId)
                .ForeignKey("dbo.Perfils", t => t.PerfilId)
                .Index(t => t.PerfilId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "PerfilId", "dbo.Perfils");
            DropForeignKey("dbo.Pagoes", "EmpleadoId", "dbo.Empleadoes");
            DropForeignKey("dbo.Pagoes", "EmpenoId", "dbo.Empenoes");
            DropForeignKey("dbo.Intereses", "EmpenoId", "dbo.Empenoes");
            DropForeignKey("dbo.Empenoes", "InteresId", "dbo.Interes");
            DropForeignKey("dbo.Empenoes", "EmpleadoId", "dbo.Empleadoes");
            DropForeignKey("dbo.Empenoes", "EditorId", "dbo.Empleadoes");
            DropForeignKey("dbo.Empenoes", "ClienteId", "dbo.Clientes");
            DropIndex("dbo.Users", new[] { "PerfilId" });
            DropIndex("dbo.Pagoes", new[] { "EmpleadoId" });
            DropIndex("dbo.Pagoes", new[] { "EmpenoId" });
            DropIndex("dbo.Intereses", new[] { "EmpenoId" });
            DropIndex("dbo.Empenoes", new[] { "InteresId" });
            DropIndex("dbo.Empenoes", new[] { "EditorId" });
            DropIndex("dbo.Empenoes", new[] { "EmpleadoId" });
            DropIndex("dbo.Empenoes", new[] { "ClienteId" });
            DropTable("dbo.Users");
            DropTable("dbo.Perfils");
            DropTable("dbo.Configuracions");
            DropTable("dbo.Pagoes");
            DropTable("dbo.Intereses");
            DropTable("dbo.Interes");
            DropTable("dbo.Empleadoes");
            DropTable("dbo.Empenoes");
            DropTable("dbo.Clientes");
            DropTable("dbo.Bitacoras");
        }
    }
}
