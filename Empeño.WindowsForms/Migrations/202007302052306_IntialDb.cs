namespace Empeño.WindowsForms.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntialDb : DbMigration
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
                        EmpleadoId = c.Int(),
                        Usuario = c.String(),
                    })
                .PrimaryKey(t => t.BitacoraId)
                .ForeignKey("dbo.Empleadoes", t => t.EmpleadoId)
                .Index(t => t.EmpleadoId);
            
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
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmpleadoId);
            
            CreateTable(
                "dbo.CierreCajas",
                c => new
                    {
                        CierreCajaId = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        EmpleadoId = c.Int(nullable: false),
                        SaldoInicial = c.Double(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CierreCajaId)
                .ForeignKey("dbo.Empleadoes", t => t.EmpleadoId)
                .Index(t => t.EmpleadoId);
            
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
                        Prorroga = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
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
            
            CreateTable(
                "dbo.Configuracions",
                c => new
                    {
                        ConfiguracionId = c.Int(nullable: false, identity: true),
                        Compañia = c.String(),
                        Identificacion = c.String(),
                        Nombre = c.String(),
                        Telefono = c.String(),
                        Direccion = c.String(),
                        Meses = c.Int(nullable: false),
                        EmailNotification = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        SMTP = c.String(),
                        Puerto = c.Int(nullable: false),
                        SSL = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ConfiguracionId);
            
            CreateTable(
                "dbo.Perfils",
                c => new
                    {
                        PerfilId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Perfil_PerfilId = c.Int(),
                    })
                .PrimaryKey(t => t.PerfilId)
                .ForeignKey("dbo.Perfils", t => t.Perfil_PerfilId)
                .Index(t => t.Perfil_PerfilId);
            
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
            DropForeignKey("dbo.Perfils", "Perfil_PerfilId", "dbo.Perfils");
            DropForeignKey("dbo.Prorrogas", "EmpleadoId", "dbo.Empleadoes");
            DropForeignKey("dbo.Prorrogas", "EmpenoId", "dbo.Empenoes");
            DropForeignKey("dbo.Pagoes", "EmpleadoId", "dbo.Empleadoes");
            DropForeignKey("dbo.Pagoes", "EmpenoId", "dbo.Empenoes");
            DropForeignKey("dbo.Intereses", "EmpenoId", "dbo.Empenoes");
            DropForeignKey("dbo.Empenoes", "InteresId", "dbo.Interes");
            DropForeignKey("dbo.Empenoes", "EmpleadoId", "dbo.Empleadoes");
            DropForeignKey("dbo.Empenoes", "EditorId", "dbo.Empleadoes");
            DropForeignKey("dbo.Empenoes", "ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.CierreCajas", "EmpleadoId", "dbo.Empleadoes");
            DropForeignKey("dbo.DetalleCierreCajas", "CierreCajaId", "dbo.CierreCajas");
            DropForeignKey("dbo.Bitacoras", "EmpleadoId", "dbo.Empleadoes");
            DropIndex("dbo.Users", new[] { "PerfilId" });
            DropIndex("dbo.Perfils", new[] { "Perfil_PerfilId" });
            DropIndex("dbo.Prorrogas", new[] { "EmpleadoId" });
            DropIndex("dbo.Prorrogas", new[] { "EmpenoId" });
            DropIndex("dbo.Pagoes", new[] { "EmpleadoId" });
            DropIndex("dbo.Pagoes", new[] { "EmpenoId" });
            DropIndex("dbo.Intereses", new[] { "EmpenoId" });
            DropIndex("dbo.Empenoes", new[] { "InteresId" });
            DropIndex("dbo.Empenoes", new[] { "EditorId" });
            DropIndex("dbo.Empenoes", new[] { "EmpleadoId" });
            DropIndex("dbo.Empenoes", new[] { "ClienteId" });
            DropIndex("dbo.DetalleCierreCajas", new[] { "CierreCajaId" });
            DropIndex("dbo.CierreCajas", new[] { "EmpleadoId" });
            DropIndex("dbo.Bitacoras", new[] { "EmpleadoId" });
            DropTable("dbo.Users");
            DropTable("dbo.Perfils");
            DropTable("dbo.Configuracions");
            DropTable("dbo.Prorrogas");
            DropTable("dbo.Pagoes");
            DropTable("dbo.Intereses");
            DropTable("dbo.Interes");
            DropTable("dbo.Clientes");
            DropTable("dbo.Empenoes");
            DropTable("dbo.DetalleCierreCajas");
            DropTable("dbo.CierreCajas");
            DropTable("dbo.Empleadoes");
            DropTable("dbo.Bitacoras");
        }
    }
}
