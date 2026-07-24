using Empeño.WindowsForms.Data;
using System.Linq;

namespace Empeño.WindowsForms.Dashboard
{
    // Config del negocio. Lectura para mostrar Y para prellenar el editor de la versión nueva.
    // El guardado real lo hace frmConfiguracionGeneral.GuardarHeadless (misma lógica del clásico).
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
                iva = c.IVA ?? 0,
                avisoEmail = c.EmailNotification,
                smtpEmail = c.Email,
                smtpPass = c.Password,
                smtp = c.SMTP,
                puerto = c.Puerto,
                ssl = c.SSL,
                email = c.EmailNotification   // compat con la vista de lectura actual
            };
        }
    }
}
