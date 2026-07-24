using Empeño.WindowsForms.Data;
using System.Linq;

namespace Empeño.WindowsForms.Dashboard
{
    // Planes de interés (tasas). SOLO LECTURA para la vista nueva; el alta/edición/baja
    // los hace frmIntereses.GuardarHeadless / EliminarHeadless (misma lógica del clásico).
    public static class InteresesData
    {
        public static object Lista(DataContext ctx)
        {
            var planes = ctx.Interes.OrderBy(x => x.InteresId).ToList()
                .Select(x => new
                {
                    id = x.InteresId,
                    nombre = x.Nombre,
                    porcentaje = x.Porcentaje,
                    mayor = x.Mayor,
                    menor = x.Menor,
                    igual = x.Igual,
                    meses = x.Meses,
                    avaluo = x.Avaluo ?? 0,
                    bodegaje = x.Bodegaje ?? 0,
                    activo = x.Activo
                }).ToList();
            return new { planes, total = planes.Count };
        }
    }
}
