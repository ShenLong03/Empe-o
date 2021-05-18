using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Empeño.CommonEF.Entities
{
    public class Intereses
    {
        [Key]
        public int InteresesId { get; set; }

        public int EmpenoId { get; set; }

        public int? InteresId { get; set; } = null;

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaVencimiento { get; set; }

        public DateTime? FechaPago { get; set; } = null;

        public double Monto { get; set; }

        public double MontoInteres { get; set; } = 0;

        public double MontoBodega { get; set; } = 0;

        public double Pagado { get; set; }

        public int? PagoId { get; set; }

        [JsonIgnore]
        public virtual Empeno Empeno { get; set; }

        public virtual Interes Interes { get; set; }
    }
}
