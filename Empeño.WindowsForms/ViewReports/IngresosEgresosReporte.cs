using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empeño.WindowsForms.ViewReports
{
    public class IngresosEgresosReporte
    {
        public int EmpeñoId { get; set; }

        public string Identificacion { get; set; }

        public string Cliente { get; set; }

        public double? Egresos { get; set; }

        public double? Ingresos { get; set; }

        public string Tipo { get; set; }

        public DateTime Fecha { get; set; }

        public string DiaMes { get; set; }

        public string Empleado { get; set; }
    }
}
