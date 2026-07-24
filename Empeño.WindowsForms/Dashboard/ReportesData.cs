using Empeño.CommonEF.Enum;
using Empeño.WindowsForms.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Empeño.WindowsForms.Dashboard
{
    // Datos de los reportes para la versión nueva. SOLO LECTURA: replica las consultas de los
    // forms clásicos (frmReporteIngresos, frmReporteEmpeños). Estos reportes NO tenían impresión
    // ni correo en el clásico, así que la versión nueva tampoco los pone.
    public static class ReportesData
    {
        private static string EstadoNombre(Estado e)
        {
            switch (e)
            {
                case Estado.Vigente: return "Activo";
                case Estado.Pendiente: return "Pendiente";
                case Estado.Vencido: return "Vencido";
                case Estado.Anulado: return "Cancelada";
                case Estado.Retirado: return "Retirada";
                default: return e.ToString();
            }
        }

        // Ingresos (pagos) vs Egresos (empeños nuevos) en un rango de fechas. Mismo criterio que
        // frmReporteIngresos.Buscar: egresos = empeños por Fecha, ingresos = pagos por Fecha.
        public static object IngresosEgresos(DataContext ctx, DateTime desde, DateTime hasta)
        {
            var d = desde.Date;
            var h = hasta.Date.AddHours(23).AddMinutes(59);

            var egresos = ctx.Empenos.Where(x => !x.IsDelete && x.Fecha >= d && x.Fecha <= h)
                .Include(x => x.Cliente).Include(x => x.Empleado).ToList();
            var ingresos = ctx.Pago.Where(x => x.Fecha >= d && x.Fecha <= h && !x.Empeno.IsDelete)
                .Include(x => x.Empeno.Cliente).Include(x => x.Empleado).ToList();

            var filas = new List<object>();
            double totIng = 0, totEg = 0;

            foreach (var it in ingresos)
            {
                filas.Add(new
                {
                    id = it.EmpenoId,
                    cli = it.Empeno != null && it.Empeno.Cliente != null ? it.Empeno.Cliente.Nombre : "",
                    ced = it.Empeno != null && it.Empeno.Cliente != null ? it.Empeno.Cliente.Identificacion : "",
                    tipo = it.TipoPago == TipoPago.Interes ? "Interés" : it.TipoPago == TipoPago.Principal ? "Principal" : "",
                    empleado = it.Empleado != null ? it.Empleado.Nombre : "",
                    fecha = it.Fecha.ToString("dd/MM/yyyy"),
                    ingreso = (double?)it.Monto,
                    egreso = (double?)null
                });
                totIng += it.Monto;
            }
            foreach (var it in egresos)
            {
                filas.Add(new
                {
                    id = it.EmpenoId,
                    cli = it.Cliente != null ? it.Cliente.Nombre : "",
                    ced = it.Cliente != null ? it.Cliente.Identificacion : "",
                    tipo = "Empeño",
                    empleado = it.Empleado != null ? it.Empleado.Nombre : "",
                    fecha = it.Fecha.ToString("dd/MM/yyyy"),
                    ingreso = (double?)null,
                    egreso = (double?)it.Monto
                });
                totEg += it.Monto;
            }

            // Serie para el gráfico: rango corto (<=92 días) por DÍA; rango largo por MES.
            // Así siempre hay gráfico, sin generar cientos de barras cuando el rango es de meses/años.
            var serie = new List<object>();
            if ((h.Date - d).TotalDays <= 92)
            {
                for (var day = d; day <= hasta.Date; day = day.AddDays(1))
                {
                    var dayEnd = day.AddHours(23).AddMinutes(59);
                    double ing = ingresos.Where(x => x.Fecha >= day && x.Fecha <= dayEnd).Sum(x => x.Monto);
                    double eg = egresos.Where(x => x.Fecha >= day && x.Fecha <= dayEnd).Sum(x => x.Monto);
                    serie.Add(new { dia = day.ToString("dd/MM"), ing, eg });
                }
            }
            else
            {
                for (var mes = new DateTime(d.Year, d.Month, 1); mes <= hasta.Date; mes = mes.AddMonths(1))
                {
                    var mesEnd = mes.AddMonths(1);
                    double ing = ingresos.Where(x => x.Fecha >= mes && x.Fecha < mesEnd).Sum(x => x.Monto);
                    double eg = egresos.Where(x => x.Fecha >= mes && x.Fecha < mesEnd).Sum(x => x.Monto);
                    serie.Add(new { dia = mes.ToString("MMM yy"), ing, eg });
                }
            }

            return new
            {
                filas,
                totalIngresos = totIng,
                totalEgresos = totEg,
                serie,
                desde = d.ToString("dd/MM/yyyy"),
                hasta = hasta.Date.ToString("dd/MM/yyyy")
            };
        }

        // Reporte de empeños por rango de fecha (de alta). El filtro por estado y los totales los
        // calcula el front sobre las filas devueltas (mismo comportamiento que los checkboxes del clásico).
        public static object Empenos(DataContext ctx, DateTime desde, DateTime hasta, bool incluirBorrados)
        {
            var d = desde.Date;
            var h = hasta.Date.AddHours(23).AddMinutes(59);

            var q = ctx.Empenos.Where(e => e.Fecha >= d && e.Fecha <= h);
            if (!incluirBorrados) q = q.Where(e => !e.IsDelete);

            var list = q.Include(x => x.Cliente).Include(x => x.Empleado).Include(x => x.Interes).ToList();

            var filas = list.Select(x => new
            {
                id = x.EmpenoId,
                ced = x.Cliente != null ? x.Cliente.Identificacion : "",
                cli = x.Cliente != null ? x.Cliente.Nombre : "",
                prenda = x.Descripcion,
                oro = x.EsOro,
                empleado = x.Empleado != null ? x.Empleado.Nombre : "",
                estado = EstadoNombre(x.Estado),
                fecha = x.Fecha.ToString("dd/MM/yyyy"),
                vence = x.FechaVencimiento.ToString("dd/MM/yyyy"),
                pct = x.Interes != null ? Convert.ToDouble(x.Interes.Porcentaje) : 0,
                monto = x.Monto,
                interes = x.Interes != null ? x.Monto * (Convert.ToDouble(x.Interes.Porcentaje) / 100.0) : 0,
                pend = x.MontoPendiente,
                borrado = x.IsDelete
            }).OrderByDescending(x => x.id).ToList();

            return new { filas };
        }

        // Cartera vencida (frmVencidos): empeños en estado Vencido, no retirados. Totales de vencido y prórroga.
        public static object Vencidos(DataContext ctx, DateTime? corte = null)
        {
            // "Vencido al corte": empeños cuyo vencimiento cae EN/ANTES de la fecha de corte y que NO
            // estaban retirados/sacados a esa fecha. Si no se pasa corte, se toma el día de hoy.
            var f = (corte ?? DateTime.Today).Date;
            var tope = f.AddDays(1);

            var empenos = ctx.Empenos.Where(x => !x.IsDelete
                     && x.FechaVencimiento < tope
                     && (x.FechaRetiro == null || x.FechaRetiro >= tope)
                     && (x.FechaRetiroAdministrador == null || x.FechaRetiroAdministrador >= tope))
                .Include(x => x.Cliente).Include(x => x.Empleado).Include(x => x.Intereses).Include(x => x.Prorrogas)
                .ToList();

            var prorrogaSet = empenos.Where(m => m.Prorrogas.Count() > 0);
            var vencidoSet = empenos.Where(l => l.Prorrogas.Count() == 0);
            double totalVencido = vencidoSet.Sum(l => l.Monto + l.Intereses.Sum(i => i.Monto));
            double totalProrroga = prorrogaSet.Sum(m => m.Monto + m.Intereses.Sum(i => i.Monto));

            var lista = empenos.Select(x => new
            {
                id = x.EmpenoId,
                prenda = x.Descripcion,
                ced = x.Cliente != null ? x.Cliente.Identificacion : "",
                cli = x.Cliente != null ? x.Cliente.Nombre : "",
                vence = x.FechaVencimiento.ToString("dd/MM/yyyy"),
                dias = (int)(x.FechaVencimiento.Date - f).TotalDays,
                empleado = x.Empleado != null ? x.Empleado.Nombre : "",
                prorroga = x.Prorroga,
                monto = x.Monto,
                pend = x.MontoPendiente + x.Intereses.Sum(i => i.MontoTotal - i.Pagado)
            }).OrderBy(x => x.dias).ToList();   // del más viejo al más nuevo (por vencimiento real, no por el texto dd/MM/yyyy)

            return new
            {
                fecha = f.ToString("dd/MM/yyyy"),
                totales = new
                {
                    vencido = totalVencido,
                    vencidoN = vencidoSet.Count(),
                    prorroga = totalProrroga,
                    prorrogaN = prorrogaSet.Count()
                },
                empenos = lista
            };
        }
    }
}
