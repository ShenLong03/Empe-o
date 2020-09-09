using System;
using System.Collections.Generic;
using System.Text;

namespace Empeño.CommonEF.Entities
{
    public class Vencimientos
    {
        public int VencimientosId { get; set; }

        public int EmpleadoId { get; set; }

        public int EmpenoId { get; set; }

        public double Consecutivo { get; set; }

        public DateTime Fecha { get; set; }
    }
}
