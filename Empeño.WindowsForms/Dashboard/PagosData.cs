using Empeño.WindowsForms.Data;
using System;
using System.Data.Entity;
using System.Linq;

namespace Empeño.WindowsForms.Dashboard
{
    // Contexto para el modal Cobrar/Abonar de la versión nueva (SOLO LECTURA). El cobro real lo
    // hace frmPagar.CobrarHeadless reusando la lógica EXACTA del clásico (Guardar/PagaInteres/SetPagaInteres).
    public static class PagosData
    {
        public static object CobrarInfo(DataContext ctx, int empenoId)
        {
            var e = ctx.Empenos.Where(x => x.EmpenoId == empenoId)
                .Include(x => x.Cliente).Include(x => x.Intereses).FirstOrDefault();
            if (e == null) return new { ok = false, error = "Empeño no encontrado." };

            // Cuotas pendientes (MontoTotal > Pagado), de la más antigua a la más nueva — se pagan en ese orden.
            var cuotas = e.Intereses.Where(i => i.MontoTotal > i.Pagado).OrderBy(i => i.FechaVencimiento)
                .Select(i => new
                {
                    id = i.InteresesId,
                    mes = i.FechaVencimiento.ToString("MMM yyyy"),
                    vence = i.FechaVencimiento.ToString("dd/MM/yyyy"),
                    total = i.MontoTotal,
                    pagado = i.Pagado,
                    pend = i.MontoTotal - i.Pagado
                }).ToList();

            // montoMinimo = TODO el interés pendiente. Es el mínimo a pagar para poder abonar a capital.
            double montoMinimo = e.Intereses.Where(i => i.MontoTotal > i.Pagado).Sum(i => i.MontoTotal - i.Pagado);
            var ultima = e.Intereses.OrderByDescending(o => o.InteresesId).FirstOrDefault();

            return new
            {
                ok = true,
                id = e.EmpenoId,
                cli = e.Cliente != null ? e.Cliente.Nombre : "",
                prenda = e.Descripcion,
                capital = e.MontoPendiente,
                montoMinimo,
                proxima = ultima != null ? ultima.FechaVencimiento.AddMonths(1).ToString("dd/MM/yyyy") : "",
                cuotas
            };
        }
    }
}
