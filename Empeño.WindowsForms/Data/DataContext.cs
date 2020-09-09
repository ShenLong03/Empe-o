using Empeño.CommonEF;
using Empeño.CommonEF.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empeño.WindowsForms.Data
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
             Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Empeño.WindowsForms.Migrations.Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.Add(new EmpenoMap());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Bitacora> Bitacoras { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Configuracion> Configuraciones { get; set; }

        public DbSet<Empeno> Empenos { get; set; }

        public DbSet<Empleado> Empleados { get; set; }

        public DbSet<Interes> Interes { get; set; }

        public DbSet<Pago> Pago { get; set; }

        public DbSet<Perfil> Perfil { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Intereses> Intereses { get; set; }

        public DbSet<CierreCaja> CierreCajas { get; set; }

        public DbSet<DetalleCierreCaja> DetalleCierreCajas { get; set; }

        public DbSet<Prorroga> Prorrogas { get; set; }

        public DbSet<Vencimientos> Vencimientos { get; set; }
    }
}
