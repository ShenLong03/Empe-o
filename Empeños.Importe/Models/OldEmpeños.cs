namespace Empeños.Importe.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OldEmpeños : DbContext
    {
        public OldEmpeños()
            : base("name=OldEmpeños")
        {
        }

        public virtual DbSet<AbonoPrincipal> AbonoPrincipals { get; set; }
        public virtual DbSet<Bitacora_Empeño> Bitacora_Empeño { get; set; }
        public virtual DbSet<Bitacora_Sistema> Bitacora_Sistema { get; set; }
        public virtual DbSet<Cancelacion> Cancelacions { get; set; }
        public virtual DbSet<Clave_Administrador> Clave_Administrador { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Consecutivo> Consecutivoes { get; set; }
        public virtual DbSet<Empeño> Empeño { get; set; }
        public virtual DbSet<Empleado> Empleadoes { get; set; }
        public virtual DbSet<EstadoEmpeño> EstadoEmpeño { get; set; }
        public virtual DbSet<Form> Forms { get; set; }
        public virtual DbSet<Interes_Empeño> Interes_Empeño { get; set; }
        public virtual DbSet<Intereses_Anteriores> Intereses_Anteriores { get; set; }
        public virtual DbSet<Kilate> Kilates { get; set; }
        public virtual DbSet<Marca> Marcas { get; set; }
        public virtual DbSet<Porcentaje> Porcentajes { get; set; }
        public virtual DbSet<Producto> Productoes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bitacora_Empeño>()
                .Property(e => e.Nombre_Completo)
                .IsUnicode(false);

            modelBuilder.Entity<Bitacora_Empeño>()
                .Property(e => e.Gramos_Anterior)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Bitacora_Empeño>()
                .Property(e => e.Articulo_Descripcion_Anterior)
                .IsUnicode(false);

            modelBuilder.Entity<Bitacora_Sistema>()
                .Property(e => e.Cedula_Empleado)
                .IsUnicode(false);

            modelBuilder.Entity<Bitacora_Sistema>()
                .Property(e => e.Evento_Realizado)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Nombre_Completo)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .Property(e => e.Nota_Cliente)
                .IsUnicode(false);

            modelBuilder.Entity<Cliente>()
                .HasMany(e => e.Empeño)
                .WithRequired(e => e.Cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Empeño>()
                .Property(e => e.Nombre_Completo)
                .IsUnicode(false);

            modelBuilder.Entity<Empeño>()
                .Property(e => e.Gramos)
                .IsUnicode(false);

            modelBuilder.Entity<Empeño>()
                .Property(e => e.Descripcion_Producto)
                .IsUnicode(false);

            modelBuilder.Entity<Empeño>()
                .HasMany(e => e.Bitacora_Empeño)
                .WithRequired(e => e.Empeño)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Empeño>()
                .HasMany(e => e.Cancelacions)
                .WithRequired(e => e.Empeño)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Empeño>()
                .HasMany(e => e.Interes_Empeño)
                .WithRequired(e => e.Empeño)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Empleado>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Empleado>()
                .Property(e => e.Primer_Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Empleado>()
                .Property(e => e.Segundo_Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Empleado>()
                .HasMany(e => e.Empeño)
                .WithRequired(e => e.Empleado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Empleado>()
                .HasMany(e => e.Roles)
                .WithMany(e => e.Empleadoes)
                .Map(m => m.ToTable("EmpleadoRole").MapLeftKey("Cedula_Empleado").MapRightKey("Id_Role"));

            modelBuilder.Entity<EstadoEmpeño>()
                .Property(e => e.Descripcion_Estado)
                .IsUnicode(false);

            modelBuilder.Entity<EstadoEmpeño>()
                .HasMany(e => e.Empeño)
                .WithRequired(e => e.EstadoEmpeño)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Form>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Form>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Form>()
                .HasMany(e => e.Roles)
                .WithMany(e => e.Forms)
                .Map(m => m.ToTable("RoleForm").MapLeftKey("IdForm").MapRightKey("Id_Role"));

            modelBuilder.Entity<Interes_Empeño>()
                .Property(e => e.Nombre_Completo)
                .IsUnicode(false);

            modelBuilder.Entity<Kilate>()
                .Property(e => e.Descripcion_Kilate)
                .IsUnicode(false);

            modelBuilder.Entity<Marca>()
                .Property(e => e.Descripcion_Marca)
                .IsUnicode(false);

            modelBuilder.Entity<Porcentaje>()
                .HasMany(e => e.Empeño)
                .WithRequired(e => e.Porcentaje)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Producto>()
                .Property(e => e.Descripcion_Producto)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.Descripcion_Role)
                .IsUnicode(false);
        }
    }
}
