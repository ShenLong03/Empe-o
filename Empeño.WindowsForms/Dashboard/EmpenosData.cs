using Empeño.CommonEF.Entities;
using Empeño.CommonEF.Enum;
using Empeño.WindowsForms.Data;
using System;
using System.Linq;

namespace Empeño.WindowsForms.Dashboard
{
    // Datos de la lista/detalle de empeños para la versión nueva. SOLO LECTURA:
    // no crea ni modifica nada; las escrituras las hace el formulario clásico.
    public static class EmpenosData
    {
        public static object Lista(DataContext ctx)
        {
            var q = ctx.Empenos.Where(x => !x.IsDelete).OrderByDescending(x => x.EmpenoId).Take(300);
            return new { empenos = Proyectar(q) };
        }

        // Búsqueda contra la BD (server-side): encuentra CUALQUIER empeño, no solo los cargados.
        // Por número exacto ("3000" -> #3000, "01" -> #1) o por nombre / cédula / descripción.
        public static object Buscar(DataContext ctx, string texto)
        {
            texto = (texto ?? "").Trim();
            if (texto.Length == 0) return Lista(ctx);

            int num;
            bool esNum = int.TryParse(texto, out num);

            var q = ctx.Empenos
                .Where(x => !x.IsDelete && (
                    (esNum && x.EmpenoId == num)
                    || x.Cliente.Nombre.Contains(texto)
                    || x.Cliente.Identificacion.Contains(texto)
                    || x.Descripcion.Contains(texto)))
                .OrderByDescending(x => x.EmpenoId)
                .Take(300);
            return new { empenos = Proyectar(q) };
        }

        static object Proyectar(IQueryable<Empeno> query)
        {
            return query
                .Select(x => new
                {
                    x.EmpenoId,
                    cli = x.Cliente.Nombre,
                    ced = x.Cliente.Identificacion,
                    tel = x.Cliente.Telefono,
                    cor = x.Cliente.Correo,
                    x.Descripcion,
                    x.Comentario,
                    x.EsOro,
                    plan = x.Interes.Nombre,
                    pct = x.Interes.Porcentaje,
                    x.Monto,
                    x.MontoAvaluo,
                    x.MontoPendiente,
                    emp = x.Empleado.Nombre,
                    x.Fecha,
                    x.FechaVencimiento,
                    x.Estado,
                    x.RetiradoAdministrador
                })
                .ToList()
                .Select(x => new
                {
                    id = x.EmpenoId,
                    cli = x.cli,
                    ced = x.ced,
                    tel = x.tel,
                    correo = x.cor,
                    prenda = x.Descripcion,
                    comentario = x.Comentario,
                    oro = x.EsOro,
                    plan = x.plan,
                    pct = Convert.ToDouble(x.pct),
                    monto = x.Monto,
                    avaluo = (double?)x.MontoAvaluo ?? 0,
                    pend = x.MontoPendiente,
                    empleado = x.emp,
                    fecha = x.Fecha.ToString("dd/MM/yyyy"),
                    vence = x.FechaVencimiento.ToString("dd/MM/yyyy"),
                    estado = x.RetiradoAdministrador ? "Perdido" : x.Estado.ToString()
                }).ToList();
        }

        public static object Detalle(DataContext ctx, int id)
        {
            // La cuota real = interés + avalúo + bodegaje (igual que el clásico), no solo el interés base.
            var cuotas = ctx.Intereses.Where(i => i.EmpenoId == id).OrderBy(i => i.FechaVencimiento)
                .Select(i => new { i.FechaVencimiento, i.Monto, i.MontoAvaluo, i.MontoBodega, i.Pagado }).ToList()
                .Select(i => new
                {
                    mes = i.FechaVencimiento.ToString("MMM"),
                    tot = i.Monto + ((double?)i.MontoAvaluo ?? 0) + ((double?)i.MontoBodega ?? 0),
                    pag = i.Pagado
                }).ToList();

            var pagos = ctx.Pago.Where(p => p.EmpenoId == id).OrderByDescending(p => p.Fecha)
                .Select(p => new { p.PagoId, p.Fecha, p.TipoPago, p.Monto, p.MontoBodega, p.MontoAvaluo }).ToList()
                .Select(p => new { id = p.PagoId, f = p.Fecha.ToString("dd/MM/yyyy"), tipo = p.TipoPago.ToString(), m = p.Monto + (p.MontoBodega ?? 0) + (p.MontoAvaluo ?? 0) }).ToList();

            return new { id, cuotas, pagos };
        }
    }
}
