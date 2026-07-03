using Empeño.CommonEF.Enum;
using Empeño.WindowsForms.Data;
using System;
using System.Linq;

namespace Empeño.WindowsForms.Dashboard
{
    // Resumen de caja del día, DESGLOSADO como el clásico (intereses / abonos / prestado).
    // SOLO LECTURA: el cierre formal y el arqueo se hacen en los forms clásicos.
    public static class CajaData
    {
        public static object ResumenHoy(DataContext ctx)
        {
            var hoy = DateTime.Today;
            var man = hoy.AddDays(1);

            // Pago.Monto es solo el interés base; el total cobrado incluye bodegaje + avalúo (MontoTotal).
            double intereses = ctx.Pago.Where(p => p.Fecha >= hoy && p.Fecha < man && p.TipoPago == TipoPago.Interes).Select(p => (double?)(p.Monto + (p.MontoBodega ?? 0) + (p.MontoAvaluo ?? 0))).Sum() ?? 0;
            double abonos = ctx.Pago.Where(p => p.Fecha >= hoy && p.Fecha < man && p.TipoPago == TipoPago.Principal).Select(p => (double?)p.Monto).Sum() ?? 0;
            double cobrado = ctx.Pago.Where(p => p.Fecha >= hoy && p.Fecha < man).Select(p => (double?)(p.Monto + (p.MontoBodega ?? 0) + (p.MontoAvaluo ?? 0))).Sum() ?? 0;
            double prestado = ctx.Empenos.Where(e => !e.IsDelete && e.Fecha >= hoy && e.Fecha < man).Select(e => (double?)e.Monto).Sum() ?? 0;
            int nPagos = ctx.Pago.Count(p => p.Fecha >= hoy && p.Fecha < man);
            int nEmp = ctx.Empenos.Count(e => !e.IsDelete && e.Fecha >= hoy && e.Fecha < man);

            return new
            {
                cobrado,
                intereses,
                abonos,
                prestado,
                flujo = cobrado - prestado,
                nPagos,
                nEmp,
                fecha = hoy.ToString("dd/MM/yyyy")
            };
        }
    }
}
