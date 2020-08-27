namespace Empeños.Importador
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

        public virtual DbSet<AbonoPrincipal> AbonoPrincipal { get; set; }
        public virtual DbSet<Bitacora_Empeño> Bitacora_Empeño { get; set; }
        public virtual DbSet<Bitacora_Sistema> Bitacora_Sistema { get; set; }
        public virtual DbSet<Cancelacion> Cancelacion { get; set; }
        public virtual DbSet<Clave_Administrador> Clave_Administrador { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Consecutivo> Consecutivo { get; set; }
        public virtual DbSet<Empeño> Empeño { get; set; }
        public virtual DbSet<Empleado> Empleado { get; set; }
        public virtual DbSet<EstadoEmpeño> EstadoEmpeño { get; set; }
        public virtual DbSet<Form> Form { get; set; }
        public virtual DbSet<Interes_Empeño> Interes_Empeño { get; set; }
        public virtual DbSet<Intereses_Anteriores> Intereses_Anteriores { get; set; }
        public virtual DbSet<Kilates> Kilates { get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<Porcentaje> Porcentaje { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

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
                .HasMany(e => e.Cancelacion)
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
                .HasMany(e => e.Role)
                .WithMany(e => e.Empleado)
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
                .HasMany(e => e.Role)
                .WithMany(e => e.Form)
                .Map(m => m.ToTable("RoleForm").MapLeftKey("IdForm").MapRightKey("Id_Role"));

            modelBuilder.Entity<Interes_Empeño>()
                .Property(e => e.Nombre_Completo)
                .IsUnicode(false);

            modelBuilder.Entity<Kilates>()
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
