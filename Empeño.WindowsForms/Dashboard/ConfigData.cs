using Empeño.WindowsForms.Data;
using System.Linq;

namespace Empeño.WindowsForms.Dashboard
{
    // Config del negocio (solo lectura para mostrar). Editar se hace en el form clásico.
    // No se toca IVA (por decisión del usuario).
    public static class ConfigData
    {
        public static object Get(DataContext ctx)
        {
            var c = ctx.Configuraciones.FirstOrDefault();
            if (c == null) return new { existe = false };
            return new
            {
                existe = true,
                compania = c.Compañia,
                nombre = c.Nombre,
                identificacion = c.Identificacion,
                telefono = c.Telefono,
                direccion = c.Direccion,
                meses = c.Meses,
                email = c.EmailNotification
            };
        }
    }
}
