using Empeño.CommonEF.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empeño.WindowsForms.Data
{
    //Se usa para relacionar una tabla mas de una vez o configurar nombres o propiedades que no existen en su relacion
    internal class EmpenoMap : EntityTypeConfiguration<Empeno>
    {
        public EmpenoMap()
        {
            //Relaciones multiples a la tabla de empleados
            HasRequired(o => o.Empleado)
                               .WithMany(m => m.Empenos)
                               .HasForeignKey(m => m.EmpleadoId);

            //Relacion circular con la misma tabla
            HasRequired(o => o.Editor)
                              .WithMany(m => m.Ediciones)
                              .HasForeignKey(m => m.EditorId);

        }
    }
}
