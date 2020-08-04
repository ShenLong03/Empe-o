using Empeño.CommonEF.Entities;
using Empeño.CommonEF.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empeño.WindowsForms.ViewReports
{
    public class EmpeñoReporte
    {
        public int EmpeñoId { get; set; }

        public int ClienteId { get; set; }
        
        [Display(Name ="Identifiicación")]
        public string Identificacion { get; set; }

        public string Cliente { get; set; }

        public int EmpleadoId { get; set; }

        public string Empleado { get; set; }

        public string Descripcion { get; set; }

        public bool Es_Oro { get; set; }

        public DateTime? Fecha { get; set; }

        public DateTime? Vencimiento { get; set; }

        public DateTime? UltimoPago { get; set; }

        public string Estado  { get; set; }

        public double Interes { get; set; }

        public double MontoInteres { get; set; }

        public double Monto { get; set; }

        public double Pendiente { get; set; }
    }
}
