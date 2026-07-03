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
                    act = c.activos
                }).ToList();

            return new { clientes };
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
