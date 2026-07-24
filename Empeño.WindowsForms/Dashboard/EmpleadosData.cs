using Empeño.WindowsForms.Data;
using System.Linq;

namespace Empeño.WindowsForms.Dashboard
{
    // Empleados y sus accesos (User/Perfil). SOLO LECTURA para la vista nueva; el alta/edición/baja
    // los hace frmEmpleados.GuardarHeadless / EliminarHeadless (misma lógica del clásico).
    public static class EmpleadosData
    {
        public static object Lista(DataContext ctx)
        {
            // Igual que el clásico: se listan los ACTIVOS y se excluye la cuenta "Admin".
            var emps = ctx.Empleados.Where(e => e.Usuario != "Admin" && e.Activo)
                .OrderBy(e => e.Nombre).ToList();

            // Perfil de cada empleado vía su usuario (User keyed por Usuario, igual que el clásico).
            var users = ctx.User.Select(u => new { u.Usuario, Perfil = u.Perfil.Nombre }).ToList();

            var empleados = emps.Select(e => new
            {
                id = e.EmpleadoId,
                nombre = e.Nombre,
                correo = e.Correo,
                telefono = e.Telefono,
                usuario = e.Usuario,
                activo = e.Activo,
                perfil = users.Where(u => u.Usuario == e.Usuario).Select(u => u.Perfil).FirstOrDefault() ?? ""
            }).ToList();

            // Mismos perfiles que ofrecía el combo clásico (Empleado / Supervisor / Administrador).
            var perfiles = ctx.Perfil
                .Where(p => p.Nombre == "Empleado" || p.Nombre == "Supervisor" || p.Nombre == "Administrador")
                .Select(p => p.Nombre).ToList();

            return new { empleados, perfiles, total = empleados.Count };
        }
    }
}
