using Empeño.CommonEF.Models;
using Empeño.WindowsForms.Data;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Empeño.WindowsForms.Dashboard
{
    // Datos de la bitácora (auditoría/errores) para la versión nueva. SOLO LECTURA: la tabla Bitacora
    // se escribe únicamente desde Funciones.SaveBitacora; esta clase solo la lee y proyecta para el
    // visor de diagnóstico del dashboard. No crea ni modifica nada.
    public static class BitacoraData
    {
        // Listado filtrable por rango de fecha, "solo errores" y texto libre. Ventana reciente por
        // defecto (últimos 7 días): la bitácora crece con cada revisión automática de intereses, así
        // que sin acotar por fecha una consulta "todo" sería enorme.
        public static object Lista(DataContext ctx, DateTime? desde = null, DateTime? hasta = null, bool soloErrores = false, string texto = null)
        {
            var d = (desde ?? DateTime.Today.AddDays(-7)).Date;
            var h = (hasta ?? DateTime.Today).Date;
            var topeH = h.AddDays(1);   // incluir todo el día 'hasta'

            var q = ctx.Bitacoras.Where(b => b.Fecha >= d && b.Fecha < topeH);

            if (soloErrores)
                q = q.Where(b => b.Error != 0);

            texto = (texto ?? "").Trim();
            if (texto.Length > 0)
                // Busca en Mensaje (el mensaje de la excepción) Y en Valor (el JSON de ValorBitacora,
                // que trae su propio campo "Valor" con el detalle, p. ej. "Error al revisar el empeño 3943").
                q = q.Where(b => (b.Mensaje != null && b.Mensaje.Contains(texto)) || (b.Valor != null && b.Valor.Contains(texto)));

            int total = q.Count();

            // Más recientes primero; tope de 300 filas (igual que EmpenosData.Lista/Buscar) para que
            // una bitácora enorme nunca cuelgue la UI.
            var crudo = q.OrderByDescending(b => b.Fecha).Take(300)
                .Select(b => new { b.BitacoraId, b.Fecha, b.Error, b.Mensaje, b.Usuario, b.Valor })
                .ToList();

            var lista = crudo.Select(b =>
            {
                // b.Valor es el JSON de ValorBitacora (Modulo/Accion/Valor). Puede venir null o mal
                // formado (fila antigua, corte a mitad de escritura): nunca debe tumbar el listado completo.
                string modulo = null, accion = null;
                if (!string.IsNullOrEmpty(b.Valor))
                {
                    try
                    {
                        var v = JsonConvert.DeserializeObject<ValorBitacora>(b.Valor);
                        if (v != null) { modulo = v.Modulo; accion = v.Accion; }
                    }
                    catch
                    {
                        // Fila con Valor no parseable: se muestra igual, sin módulo/acción.
                    }
                }
                return new
                {
                    id = b.BitacoraId,
                    fecha = b.Fecha.ToString("dd/MM/yyyy HH:mm"),
                    error = b.Error != 0,
                    mensaje = b.Mensaje,
                    usuario = b.Usuario,
                    modulo,
                    accion,
                    valor = b.Valor
                };
            }).ToList();

            return new
            {
                desde = d.ToString("dd/MM/yyyy"),
                hasta = h.ToString("dd/MM/yyyy"),
                soloErrores,
                texto,
                total,
                lista
            };
        }
    }
}
