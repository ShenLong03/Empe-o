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
        // Conteos REALES por chip (server-side), con la misma lógica de estado que Proyectar
        // (RetiradoAdministrador se muestra como "Perdido").
        static object Counts(IQueryable<Empeno> b)
        {
            return new
            {
                activos = b.Count(x => !x.RetiradoAdministrador && (x.Estado == Estado.Vigente || x.Estado == Estado.Pendiente || x.Estado == Estado.Vencido)),
                vencidos = b.Count(x => !x.RetiradoAdministrador && x.Estado == Estado.Vencido),
                retirados = b.Count(x => !x.RetiradoAdministrador && (x.Estado == Estado.Cancelado || x.Estado == Estado.Anulado)),
                perdidos = b.Count(x => x.RetiradoAdministrador),
                todo = b.Count()
            };
        }

        // Lista filtrada por chip, con conteos y total REALES (no capados). La lista trae los 300 más
        // recientes del filtro; `total` dice cuántos hay de verdad para que el front no muestre "300" falso.
        public static object Lista(DataContext ctx, string filtro, int skip)
        {
            var b = ctx.Empenos.Where(x => !x.IsDelete);
            var counts = Counts(b);
            IQueryable<Empeno> q;
            switch (filtro)
            {
                case "vencidos": q = b.Where(x => !x.RetiradoAdministrador && x.Estado == Estado.Vencido); break;
                case "cancelados": q = b.Where(x => !x.RetiradoAdministrador && (x.Estado == Estado.Cancelado || x.Estado == Estado.Anulado)); break;
                case "perdidos": q = b.Where(x => x.RetiradoAdministrador); break;
                case "todo": q = b; break;
                default: q = b.Where(x => !x.RetiradoAdministrador && (x.Estado == Estado.Vigente || x.Estado == Estado.Pendiente || x.Estado == Estado.Vencido)); break; // activos
            }
            int total = q.Count();
            // Página de 300 desde `skip` (scroll infinito): al llegar al final el front pide skip=E.length.
            var empenos = Proyectar(q.OrderByDescending(x => x.EmpenoId).Skip(skip).Take(300));
            return new { empenos, counts, total, skip };
        }

        // Búsqueda contra la BD (server-side): encuentra CUALQUIER empeño, no solo los cargados.
        // Por número exacto ("3000" -> #3000, "01" -> #1) o por nombre / cédula / descripción.
        public static object Buscar(DataContext ctx, string texto, string filtro, int skip)
        {
            texto = (texto ?? "").Trim();
            if (texto.Length == 0) return Lista(ctx, filtro ?? "activos", skip);

            int num;
            bool esNum = int.TryParse(texto, out num);

            var b = ctx.Empenos.Where(x => !x.IsDelete);
            // Coincidencias por texto (id exacto / nombre / cédula / descripción), SIN filtrar aún por estado.
            var matched = b.Where(x =>
                    (esNum && x.EmpenoId == num)
                    || x.Cliente.Nombre.Contains(texto)
                    || x.Cliente.Identificacion.Contains(texto)
                    || x.Descripcion.Contains(texto));
            // Conteos ACOTADOS a la búsqueda: los chips muestran cuántos activos/vencidos/etc. tiene ESA persona.
            var counts = Counts(matched);

            // Sobre las coincidencias, aplicar el MISMO filtro de estado que los chips (Lista). Así búsqueda + chip combinan.
            IQueryable<Empeno> q;
            switch (filtro)
            {
                case "vencidos": q = matched.Where(x => !x.RetiradoAdministrador && x.Estado == Estado.Vencido); break;
                case "cancelados": q = matched.Where(x => !x.RetiradoAdministrador && (x.Estado == Estado.Cancelado || x.Estado == Estado.Anulado)); break;
                case "perdidos": q = matched.Where(x => x.RetiradoAdministrador); break;
                case "todo": q = matched; break;
                default: q = matched.Where(x => !x.RetiradoAdministrador && (x.Estado == Estado.Vigente || x.Estado == Estado.Pendiente || x.Estado == Estado.Vencido)); break; // activos
            }
            int total = q.Count();
            // Si busca un número, el empeño con ESE id exacto va PRIMERO (no enterrado entre coincidencias de cédula/descripción).
            IQueryable<Empeno> ordered = esNum
                ? q.OrderByDescending(x => x.EmpenoId == num).ThenByDescending(x => x.EmpenoId)
                : q.OrderByDescending(x => x.EmpenoId);
            var empenos = Proyectar(ordered.Skip(skip).Take(300));
            return new { empenos, counts, total, skip };
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

        // Planes de interés activos (para el dropdown del editor del dashboard).
        public static object Planes(DataContext ctx)
        {
            var planes = ctx.Interes.Where(i => i.Activo).OrderBy(i => i.Nombre).Select(i => i.Nombre).ToList();
            return new { planes };
        }

        // Planes de interés activos CON detalle (para el formulario de alta del dashboard):
        // porcentaje, meses, y las fracciones de avalúo/bodegaje ya calculadas (Avaluo/100, Bodegaje/100)
        // igual que Interes.PorcentajeAvaluo / PorcentajeBodegaje, más el umbral Mayor para el auto-select por monto.
        public static object PlanesDetalle(DataContext ctx)
        {
            var planes = ctx.Interes.Where(i => i.Activo).OrderBy(i => i.Nombre)
                .Select(i => new { i.InteresId, i.Nombre, i.Porcentaje, i.Meses, i.Avaluo, i.Bodegaje, i.Mayor }).ToList()
                .Select(i => new
                {
                    id = i.InteresId,
                    nombre = i.Nombre,
                    pct = i.Porcentaje,
                    meses = i.Meses,
                    avaluoPct = (i.Avaluo ?? 0) / 100.0,
                    bodegajePct = (i.Bodegaje ?? 0) / 100.0,
                    tieneAvaluo = (i.Avaluo ?? 0) > 0,
                    tieneBodegaje = (i.Bodegaje ?? 0) > 0,
                    mayor = i.Mayor
                }).ToList();
            return new { planes };
        }

        public static object Detalle(DataContext ctx, int id)
        {
            // La cuota real = interés + avalúo + bodegaje (igual que el clásico), no solo el interés base.
            var cuotas = ctx.Intereses.Where(i => i.EmpenoId == id).OrderBy(i => i.FechaVencimiento)
                .Select(i => new { i.FechaVencimiento, i.Monto, i.MontoAvaluo, i.MontoBodega, i.Pagado, i.PagoId }).ToList()
                .Select(i => new
                {
                    mes = i.FechaVencimiento.ToString("MMM yyyy"),   // con AÑO: con muchos meses de atraso se repetían sin distinguir
                    intr = i.Monto,                                 // interés de la cuota
                    bod = (double?)i.MontoBodega ?? 0,              // bodegaje
                    av = (double?)i.MontoAvaluo ?? 0,               // avalúo (una vez, primera cuota)
                    tot = i.Monto + ((double?)i.MontoAvaluo ?? 0) + ((double?)i.MontoBodega ?? 0),
                    pag = i.Pagado,
                    pid = i.PagoId
                }).ToList();

            var pagos = ctx.Pago.Where(p => p.EmpenoId == id).OrderByDescending(p => p.Fecha)
                .Select(p => new { p.PagoId, p.Fecha, p.TipoPago, p.Monto, p.MontoBodega, p.MontoAvaluo }).ToList()
                .Select(p => new { id = p.PagoId, f = p.Fecha.ToString("dd/MM/yyyy"), tipo = p.TipoPago.ToString(), m = p.Monto + (p.MontoBodega ?? 0) + (p.MontoAvaluo ?? 0) }).ToList();

            return new { id, cuotas, pagos };
        }
    }
}
