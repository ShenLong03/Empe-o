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

        // Movimientos del día (pagos + empeños nuevos), para llenar la vista de Caja con el detalle
        // de lo que pasó hoy. SOLO LECTURA. Ordenado del más reciente al más antiguo.
        public static object MovimientosHoy(DataContext ctx)
        {
            var hoy = DateTime.Today;
            var man = hoy.AddDays(1);

            var pagos = ctx.Pago.Where(p => p.Fecha >= hoy && p.Fecha < man && !p.Empeno.IsDelete)
                .Select(p => new
                {
                    fecha = p.Fecha,
                    tipo = p.TipoPago == TipoPago.Interes ? "Interés" : "Abono",
                    empenoId = p.EmpenoId,
                    cli = p.Empeno.Cliente != null ? p.Empeno.Cliente.Nombre : "",
                    prenda = p.Empeno.Descripcion,
                    empleado = p.Empleado != null ? p.Empleado.Nombre : "",
                    monto = p.TipoPago == TipoPago.Interes ? (p.Monto + (p.MontoBodega ?? 0) + (p.MontoAvaluo ?? 0)) : p.Monto,
                    entra = true
                }).ToList();

            var empenos = ctx.Empenos.Where(e => !e.IsDelete && e.Fecha >= hoy && e.Fecha < man)
                .Select(e => new
                {
                    fecha = e.Fecha,
                    tipo = "Empeño",
                    empenoId = e.EmpenoId,
                    cli = e.Cliente != null ? e.Cliente.Nombre : "",
                    prenda = e.Descripcion,
                    empleado = e.Empleado != null ? e.Empleado.Nombre : "",
                    monto = e.Monto,
                    entra = false
                }).ToList();

            var movimientos = pagos.Concat(empenos)
                .OrderByDescending(x => x.fecha)
                .Select(x => new
                {
                    hora = x.fecha.ToString("HH:mm"),
                    x.tipo,
                    id = x.empenoId,
                    cli = x.cli,
                    prenda = x.prenda,
                    empleado = x.empleado,
                    monto = x.monto,
                    entra = x.entra
                }).ToList();

            return new { movimientos, total = movimientos.Count };
        }

        // Saldo inicial SUGERIDO para un cierre = el "Acumulado" con el que cerró el cierre ANTERIOR
        // (el más reciente con fecha previa). Así el saldo de apertura se calcula solo, sin pedir nada.
        public static double SaldoInicialSugerido(DataContext ctx, DateTime fecha)
        {
            var f = fecha.Date;
            var prev = ctx.CierreCajas.Where(c => !c.IsDelete && c.Fecha < f)
                .OrderByDescending(c => c.Fecha).FirstOrDefault();
            if (prev == null) return 0;
            var acum = ctx.DetalleCierreCajas
                .Where(d => d.CierreCajaId == prev.CierreCajaId && d.Concepto == "Acumulado")
                .Select(d => (double?)d.Valor).FirstOrDefault();
            return acum ?? 0;
        }

        // Preview del cierre para una fecha (solo lectura). Replica EXACTAMENTE la matemática de
        // frmCierreCaja.ProcessClose() para que lo que se muestra sea idéntico a lo que se guarda.
        public static object CierrePreview(DataContext ctx, DateTime fecha)
        {
            var f = fecha.Date;
            var tomorrow = f.AddDays(1);

            var empeñosActivos = ctx.Empenos.Where(x => !x.IsDelete && (x.Estado == Estado.Vigente
                       || x.Estado == Estado.Pendiente || x.Estado == Estado.Vencido));

            double vencidos = ctx.Empenos.Where(x => !x.IsDelete && x.FechaRetiroAdministrador >= f && x.FechaRetiroAdministrador < tomorrow).ToList().Sum(x => x.MontoPendiente);

            double c1 = empeñosActivos.Where(x => x.Fecha < f
                     && (!x.Retirado || (x.FechaRetiroAdministrador >= f && x.FechaRetiroAdministrador < tomorrow))).Sum(x => x.MontoPendiente);

            double c2c3 = ctx.Pago.Where(p => p.Fecha >= f && p.TipoPago == TipoPago.Principal).Any()
                ? ctx.Pago.Where(p => p.Fecha >= f && p.TipoPago == TipoPago.Principal).Sum(x => x.Monto)
                : 0;

            double acumuladoInicial = c1 + vencidos + c2c3;

            double montoEmpeñoDia = empeñosActivos.Where(x => !x.IsDelete && x.Fecha >= f && x.Fecha < tomorrow).ToList().Sum(x => x.Monto);

            double montoInteresDia = ctx.Empenos.Where(x => !x.IsDelete && (x.Estado == Estado.Vigente
                       || x.Estado == Estado.Pendiente || x.Estado == Estado.Vencido || x.Estado == Estado.Cancelado))
                  .SelectMany(x => x.Pagos).Where(x => x.TipoPago == TipoPago.Interes && x.Fecha >= f && x.Fecha < tomorrow).ToList().Sum(x => x.Monto);

            double abonoDia = empeñosActivos
                .SelectMany(x => x.Pagos).Where(x => x.TipoPago == TipoPago.Principal && x.Fecha >= f && x.Fecha < tomorrow).ToList().Sum(x => x.Monto);

            double cancelados = ctx.Empenos.Where(x => !x.IsDelete && (x.Estado == Estado.Cancelado
                     || x.Retirado || x.FechaRetiro != null))
              .SelectMany(x => x.Pagos).Where(x => x.TipoPago == TipoPago.Principal && x.Fecha >= f && x.Fecha < tomorrow).ToList().Sum(x => x.Monto);

            double acumulado = (acumuladoInicial + montoEmpeñoDia) - (abonoDia + vencidos + cancelados);

            double montoAvaluoDia = ctx.Empenos.Where(x => !x.IsDelete && (x.Estado == Estado.Vigente
                      || x.Estado == Estado.Pendiente || x.Estado == Estado.Vencido || x.Estado == Estado.Cancelado))
                 .SelectMany(x => x.Pagos).Where(x => x.TipoPago == TipoPago.Interes && x.Fecha >= f && x.Fecha < tomorrow).ToList().Sum(x => (x.MontoAvaluo ?? 0));

            double montoBodegajeDia = ctx.Empenos.Where(x => !x.IsDelete && (x.Estado == Estado.Vigente
                     || x.Estado == Estado.Pendiente || x.Estado == Estado.Vencido || x.Estado == Estado.Cancelado))
                .SelectMany(x => x.Pagos).Where(x => x.TipoPago == TipoPago.Interes && x.Fecha >= f && x.Fecha < tomorrow).ToList().Sum(x => (x.MontoBodega ?? 0));

            var configuracion = ctx.Configuraciones.FirstOrDefault();
            double iva = (montoAvaluoDia + montoBodegajeDia) * ((configuracion != null ? (configuracion.IVA ?? 0) : 0) / 100.0);

            var lineas = new[]
            {
                new { concepto = "Empeños", valor = montoEmpeñoDia },
                new { concepto = "Monto de Abonos", valor = abonoDia },
                new { concepto = "Intereses", valor = montoInteresDia },
                new { concepto = "Avalúos", valor = montoAvaluoDia },
                new { concepto = "Bodegajes", valor = montoBodegajeDia },
                new { concepto = "Retiros", valor = cancelados },
                new { concepto = "Vencidos", valor = vencidos },
                new { concepto = "Acumulado", valor = acumulado },
                new { concepto = "IVA", valor = iva },
            };

            return new
            {
                fecha = f.ToString("dd/MM/yyyy"),
                prestado = montoEmpeñoDia,
                abonos = abonoDia,
                intereses = montoInteresDia,
                avaluos = montoAvaluoDia,
                bodegajes = montoBodegajeDia,
                retiros = cancelados,
                vencidos,
                acumuladoInicial,
                acumulado,
                iva,
                lineas,
                // Saldo inicial calculado (acumulado del cierre anterior). El front lo muestra y lo guarda.
                saldoInicial = SaldoInicialSugerido(ctx, fecha)
            };
        }

        // Historial de cierres guardados (funcionalidad NUEVA, el clásico no la tenía).
        public static object Historial(DataContext ctx)
        {
            var cierres = ctx.CierreCajas.Where(c => !c.IsDelete).OrderByDescending(c => c.Fecha)
                .Select(c => new
                {
                    c.CierreCajaId,
                    c.Fecha,
                    c.SaldoInicial,
                    emp = c.Empleado.Nombre,
                    acumulado = c.Detalles.Where(d => d.Concepto == "Acumulado").Select(d => (double?)d.Valor).FirstOrDefault()
                })
                .Take(200).ToList()
                .Select(c => new
                {
                    id = c.CierreCajaId,
                    fecha = c.Fecha.ToString("dd/MM/yyyy HH:mm"),
                    empleado = c.emp,
                    saldoInicial = c.SaldoInicial,
                    acumulado = c.acumulado ?? 0
                }).ToList();
            return new { cierres };
        }

        // Detalle de un cierre guardado (para verlo/reimprimirlo desde el historial).
        public static object CierreGuardado(DataContext ctx, int id)
        {
            var c = ctx.CierreCajas.Where(x => x.CierreCajaId == id)
                .Select(x => new { x.CierreCajaId, x.Fecha, x.SaldoInicial, emp = x.Empleado.Nombre }).FirstOrDefault();
            if (c == null) return new { ok = false };
            var lineas = ctx.DetalleCierreCajas.Where(d => d.CierreCajaId == id)
                .Select(d => new { d.Concepto, d.Valor }).ToList()
                .Select(d => new { concepto = d.Concepto, valor = d.Valor }).ToList();
            return new
            {
                ok = true,
                id = c.CierreCajaId,
                fecha = c.Fecha.ToString("dd/MM/yyyy HH:mm"),
                empleado = c.emp,
                saldoInicial = c.SaldoInicial,
                lineas
            };
        }
    }
}
