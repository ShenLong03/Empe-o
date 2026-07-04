using Empeño.CommonEF.Enum;
using Empeño.WindowsForms.Data;
using System;
using System.Data.Entity;
using System.Linq;

namespace Empeño.WindowsForms.Dashboard
{
    // Datos del arqueo de cartera para la versión nueva. SOLO LECTURA: replica EXACTAMENTE la
    // matemática de frmArqueo.LoadDetalle() (totales + cantidades + lista por empeño). Las acciones
    // (retiro admin, prórroga, imprimir, correo) se hacen reusando la lógica clásica headless.
    public static class ArqueoData
    {
        public static object Resumen(DataContext ctx)
        {
            // Mismo filtro que frmArqueo_Load: activos (Vigente/Pendiente/Vencido), no retirados.
            var empenos = ctx.Empenos.Where(x => !x.IsDelete && (x.Estado == Estado.Vigente
                     || x.Estado == Estado.Pendiente
                     || x.Estado == Estado.Vencido)
                     && (!x.Retirado || x.FechaRetiro == null)
                     && (!x.RetiradoAdministrador || x.FechaRetiroAdministrador == null))
                .Include(x => x.Cliente).Include(x => x.Empleado).Include(x => x.Intereses).Include(x => x.Prorrogas)
                .ToList();

            double totalPrincipal = empenos.Sum(l => l.Monto);
            double totalIntereses = empenos.SelectMany(l => l.Intereses).Sum(l => l.Monto);
            double totalGeneral = totalPrincipal + totalIntereses;

            var prorrogaSet = empenos.Where(m => m.Prorrogas.Count() > 0);
            double totalProrroga = prorrogaSet.Sum(m => m.Monto + m.Intereses.Sum(i => i.Monto));

            var alDiaSet = empenos.Where(m => m.Estado == Estado.Vigente || m.Estado == Estado.Pendiente);
            double totalActivos = alDiaSet.Sum(m => m.Monto + m.Intereses.Sum(i => i.Monto));

            var vencidoSet = empenos.Where(l => l.Estado == Estado.Vencido && l.Prorrogas.Count() == 0 && !l.RetiradoAdministrador);
            double totalVencido = vencidoSet.Sum(l => l.Monto + l.Intereses.Sum(i => i.Monto));

            var retSet = empenos.Where(l => l.RetiradoAdministrador || l.FechaRetiroAdministrador != null);
            double totalRetirados = retSet.Sum(l => l.Monto + l.Intereses.Sum(i => i.Monto));

            var lista = empenos.Select(x => new
            {
                id = x.EmpenoId,
                prenda = x.Descripcion,
                ced = x.Cliente != null ? x.Cliente.Identificacion : "",
                cli = x.Cliente != null ? x.Cliente.Nombre : "",
                estado = x.Estado.ToString(),
                vence = x.FechaVencimiento.ToString("dd/MM/yyyy"),
                dias = (int)(x.FechaVencimiento.Date - DateTime.Today).TotalDays,
                empleado = x.Empleado != null ? x.Empleado.Nombre : "",
                prorroga = x.Prorroga,
                monto = x.Monto,
                pend = x.MontoPendiente + x.Intereses.Sum(i => i.MontoTotal - i.Pagado)
            }).OrderBy(x => x.vence).ToList();

            return new
            {
                totales = new
                {
                    principal = totalPrincipal,
                    principalN = empenos.Count,
                    intereses = totalIntereses,
                    general = totalGeneral,
                    alDia = totalActivos,
                    alDiaN = alDiaSet.Count(),
                    vencido = totalVencido,
                    vencidoN = vencidoSet.Count(),
                    retirado = totalRetirados,
                    retiradoN = retSet.Count(),
                    prorroga = totalProrroga,
                    prorrogaN = prorrogaSet.Count()
                },
                empenos = lista
            };
        }
    }
}
