using Empeño.CommonEF.Entities;
using Empeño.CommonEF.Enum;
using Empeño.WindowsForms.Data;
using System;
using System.Linq;

namespace Empeño.WindowsForms.Dashboard
{
    // Directorio de clientes para la versión nueva. SOLO LECTURA: crear/editar lo hace el form clásico.
    public static class ClientesData
    {
        public static object Lista(DataContext ctx)
        {
            // Solo se excluyen los borrados (IsDelete), igual que el clásico: los INACTIVOS también se listan
            // (para poder consultarlos/seleccionarlos), marcados con su bandera Activo.
            var clientes = ctx.Clientes
                .Where(c => !c.IsDelete)
                .OrderBy(c => c.Nombre)
                .Select(c => new
                {
                    c.ClienteId,
                    c.Nombre,
                    c.Identificacion,
                    c.Telefono,
                    c.Correo,
                    c.Direccion,
                    c.Comentario,
                    c.Activo,
                    c.Fecha,
                    activos = c.Empenos.Count(e => !e.IsDelete && (e.Estado == Estado.Vigente || e.Estado == Estado.Pendiente || e.Estado == Estado.Vencido))
                })
                .Take(1000).ToList()
                .Select(c => new
                {
                    id = c.ClienteId,
                    nom = c.Nombre,
                    ced = c.Identificacion,
                    tel = c.Telefono,
                    cor = c.Correo,
                    dir = c.Direccion,
                    com = c.Comentario,
                    activo = c.Activo,
                    fecha = c.Fecha.ToString("dd/MM/yyyy"),
                    act = c.activos
                }).ToList();

            return new { clientes };
        }

        // Alta de cliente desde la versión nueva. Misma validación que el clásico:
        // nombre e identificación obligatorios, cédula única entre los no borrados.
        public static object Crear(DataContext ctx, string identificacion, string nombre, string telefono, string correo, string direccion, string comentario, bool activo, DateTime fecha)
        {
            identificacion = (identificacion ?? "").Trim();
            nombre = (nombre ?? "").Trim();
            if (nombre.Length == 0) return new { ok = false, error = "El nombre es obligatorio." };
            if (identificacion.Length == 0) return new { ok = false, error = "La identificación es obligatoria." };
            if (ctx.Clientes.Any(c => !c.IsDelete && c.Identificacion == identificacion))
                return new { ok = false, error = "Ya existe un cliente con esa identificación." };

            var cliente = new Cliente
            {
                Identificacion = identificacion,
                Nombre = nombre,
                Telefono = (telefono ?? "").Trim(),
                Correo = (correo ?? "").Trim(),
                Direccion = (direccion ?? "").Trim(),
                Comentario = (comentario ?? "").Trim(),
                Activo = activo,
                IsDelete = false,
                Fecha = fecha
            };
            ctx.Clientes.Add(cliente);
            ctx.SaveChanges();
            return new { ok = true, id = cliente.ClienteId };
        }

        // Edición de cliente desde la versión nueva. Cédula única excluyendo al propio cliente.
        public static object Editar(DataContext ctx, int id, string identificacion, string nombre, string telefono, string correo, string direccion, string comentario, bool activo, DateTime fecha)
        {
            identificacion = (identificacion ?? "").Trim();
            nombre = (nombre ?? "").Trim();
            if (nombre.Length == 0) return new { ok = false, error = "El nombre es obligatorio." };
            if (identificacion.Length == 0) return new { ok = false, error = "La identificación es obligatoria." };
            var cliente = ctx.Clientes.Find(id);
            if (cliente == null) return new { ok = false, error = "Cliente no encontrado." };

            // Mismos campos que el clásico (incluye Activo y Fecha); el clásico no re-valida cédula al editar.
            cliente.Identificacion = identificacion;
            cliente.Nombre = nombre;
            cliente.Telefono = (telefono ?? "").Trim();
            cliente.Correo = (correo ?? "").Trim();
            cliente.Direccion = (direccion ?? "").Trim();
            cliente.Comentario = (comentario ?? "").Trim();
            cliente.Activo = activo;
            cliente.Fecha = fecha;
            ctx.SaveChanges();
            return new { ok = true, id = cliente.ClienteId };
        }

        public static object Detalle(DataContext ctx, int id)
        {
            var empenos = ctx.Empenos
                .Where(e => e.ClienteId == id && !e.IsDelete)
                .OrderByDescending(e => e.EmpenoId)
                .Select(e => new { e.EmpenoId, e.Descripcion, e.EsOro, e.MontoPendiente, e.Estado, e.RetiradoAdministrador })
                .Take(50).ToList()
                .Select(e => new
                {
                    id = e.EmpenoId,
                    pr = e.Descripcion,
                    oro = e.EsOro,
                    pend = e.MontoPendiente,
                    est = e.RetiradoAdministrador ? "Perdido" : e.Estado.ToString()
                }).ToList();

            // Métricas del cliente que el clásico muestra al editar: total de empeños y ganancias (suma de pagos).
            int totalEmp = ctx.Empenos.Count(e => e.ClienteId == id && !e.IsDelete);
            double ganancias = ctx.Pago.Where(p => p.Empeno.ClienteId == id).Select(p => (double?)p.Monto).Sum() ?? 0;

            return new { id, empenos, totalEmp, ganancias };
        }
    }
}
