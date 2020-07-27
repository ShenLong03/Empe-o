using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empeño.WindowsForms.Models
{
    public class Transaccion
    {
        public DateTime Hora { get; set; }

        public string TipoTransaccion { get; set; }

        public double Monto { get; set; }
    }
}
