using Empeño.CommonEF.Enum;
using Empeño.WindowsForms.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Empeño.WindowsForms.Dashboard
{
    // Arma los datos del tablero desde EF. SOLO LECTURA: no cambia lógica de negocio ni la BD.
    // Se comparte entre la versión nueva (frmShell) y el tablero embebido.
    public static class TableroData
    {
        public static object Build(DataContext ctx, string usuario)
        {
            var hoy = DateTime.Today;
            var sig7 = hoy.AddDays(7);
            var m0 = new DateTime(hoy.Year, hoy.Month, 1);

            var activosQ = ctx.Empenos.Where(x => !x.IsDelete && (x.Estado == Estado.Vigente || x.Estado == Estado.Pendiente || x.Estado == Estado.Vencido));
            double cartera = activosQ.Select(x => (double?)x.MontoPendiente).Sum() ?? 0;
            int activos = activosQ.Count();
            int nuevosMes = ctx.Empenos.Count(x => !x.IsDelete && x.Fecha >= m0);

            var vencidosQ = ctx.Empenos.Where(x => !x.IsDelete && x.Estado == Estado.Vencido && !x.RetiradoAdministrador);
            int vencidosCount = vencidosQ.Count();
            double montoRiesgo = vencidosQ.Select(x => (double?)x.MontoPendiente).Sum() ?? 0;

            int porVencerCount = ctx.Empenos.Count(x => !x.IsDelete && (x.Estado == Estado.Vigente || x.Estado == Estado.Pendiente) && x.FechaVencimiento >= hoy && x.FechaVencimiento < sig7);
            double recaudoMes = ctx.Pago.Where(p => p.Fecha >= m0 && p.TipoPago == TipoPago.Interes).Select(p => (double?)p.Monto).Sum() ?? 0;

            var estadoDefs = new[] { Estado.Vigente, Estado.Pendiente, Estado.Vencido, Estado.Cancelado, Estado.Retirado };
            var estados = estadoDefs.Select(s => new { n = s.ToString(), v = ctx.Empenos.Count(x => !x.IsDelete && x.Estado == s) }).ToList();

            var vlist = vencidosQ.Select(x => new { x.FechaVencimiento, x.MontoPendiente }).ToList();
            var b1 = vlist.Where(v => (hoy - v.FechaVencimiento.Date).Days >= 1 && (hoy - v.FechaVencimiento.Date).Days <= 30).ToList();
            var b2 = vlist.Where(v => (hoy - v.FechaVencimiento.Date).Days >= 31 && (hoy - v.FechaVencimiento.Date).Days <= 60).ToList();
            var b3 = vlist.Where(v => (hoy - v.FechaVencimiento.Date).Days >= 61).ToList();
            var aging = new object[]
            {
                new { t = "1–30 días", v = b1.Sum(v => v.MontoPendiente), ct = b1.Count },
                new { t = "31–60 días", v = b2.Sum(v => v.MontoPendiente), ct = b2.Count },
                new { t = "60+ días", v = b3.Sum(v => v.MontoPendiente), ct = b3.Count },
            };

            var porVencer = ctx.Empenos
                .Where(x => !x.IsDelete && (x.Estado == Estado.Vigente || x.Estado == Estado.Pendiente) && x.FechaVencimiento >= hoy && x.FechaVencimiento < sig7)
                .OrderBy(x => x.FechaVencimiento)
                .Select(x => new { x.EmpenoId, cli = x.Cliente.Nombre, tel = x.Cliente.Telefono, x.Descripcion, x.EsOro, x.MontoPendiente, pct = x.Interes.Porcentaje, bod = x.Interes.Bodegaje, x.FechaVencimiento })
                .Take(60).ToList()
                .Select(x => Seguimiento(x.EmpenoId, x.cli, x.tel, x.Descripcion, x.EsOro, x.MontoPendiente, x.pct, x.bod, x.FechaVencimiento, hoy)).ToList();

            var vencidos = vencidosQ
                .OrderBy(x => x.FechaVencimiento)
                .Select(x => new { x.EmpenoId, cli = x.Cliente.Nombre, tel = x.Cliente.Telefono, x.Descripcion, x.EsOro, x.MontoPendiente, pct = x.Interes.Porcentaje, bod = x.Interes.Bodegaje, x.FechaVencimiento })
                .Take(60).ToList()
                .Select(x => Seguimiento(x.EmpenoId, x.cli, x.tel, x.Descripcion, x.EsOro, x.MontoPendiente, x.pct, x.bod, x.FechaVencimiento, hoy)).ToList();

            string fecha;
            try { fecha = hoy.ToString("dddd, d 'de' MMMM", new CultureInfo("es-ES")); }
            catch { fecha = hoy.ToString("dd/MM/yyyy"); }

            return new
            {
                fecha,
                empleado = string.IsNullOrEmpty(usuario) ? "Empleado" : usuario,
                kpis = new { cartera, empenosActivos = activos, nuevosMes, porVencer = porVencerCount, vencidos = vencidosCount, montoRiesgo, recaudoMes },
                serie = Serie(ctx, 1),
                estados,
                aging,
                porVencer,
                vencidos,
                contactados = new object[0]
            };
        }

        static object Seguimiento(int id, string cli, string tel, string prenda, bool oro, double monto, object pctRaw, object bodRaw, DateTime venc, DateTime hoy)
        {
            double pct = Convert.ToDouble(pctRaw);
            // Pago mensual estimado = interés + bodegaje (ambos mensuales). El avalúo es cargo único, no recurre.
            double bod = bodRaw != null ? Convert.ToDouble(bodRaw) : 0;
            return new
            {
                id,
                cli,
                tel,
                prenda,
                oro,
                monto = Math.Truncate(monto * (pct + bod) / 100.0),
                dias = (venc.Date - hoy).Days
            };
        }

        public static object Serie(DataContext ctx, int idx)
        {
            var hoy = DateTime.Today;
            DateTime desde;
            string mode;
            switch (idx)
            {
                case 0: desde = hoy.AddDays(-7); mode = "d"; break;
                case 2: desde = hoy.AddDays(-29); mode = "d"; break;
                case 3: desde = hoy.AddDays(-84); mode = "w"; break;
                case 4: desde = hoy.AddMonths(-6); mode = "m"; break;
                case 5: desde = hoy.AddYears(-1); mode = "m"; break;
                default: desde = hoy.AddDays(-14); mode = "d"; break;
            }

            var ing = ctx.Pago.Where(p => p.Fecha >= desde).Select(p => new { p.Fecha, Monto = p.Monto + (p.MontoBodega ?? 0) + (p.MontoAvaluo ?? 0) }).ToList();
            var egr = ctx.Empenos.Where(x => !x.IsDelete && x.Fecha >= desde).Select(x => new { x.Fecha, x.Monto }).ToList();

            var labels = new List<string>();
            var ingresos = new List<double>();
            var egresos = new List<double>();

            Action<DateTime, DateTime, string> bucket = (d0, d1, lbl) =>
            {
                labels.Add(lbl);
                ingresos.Add(ing.Where(x => x.Fecha >= d0 && x.Fecha < d1).Sum(x => x.Monto));
                egresos.Add(egr.Where(x => x.Fecha >= d0 && x.Fecha < d1).Sum(x => x.Monto));
            };

            if (mode == "d")
                for (var d = desde.Date; d <= hoy; d = d.AddDays(1)) bucket(d, d.AddDays(1), d.ToString("dd/MM"));
            else if (mode == "w")
                for (var d = desde.Date; d <= hoy; d = d.AddDays(7)) bucket(d, d.AddDays(7), d.ToString("dd/MM"));
            else
                for (var d = new DateTime(desde.Year, desde.Month, 1); d <= hoy; d = d.AddMonths(1)) bucket(d, d.AddMonths(1), d.ToString("MMM"));

            return new { labels, ingresos, egresos };
        }
    }
}
