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

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaVencimiento { get; set; }

        public double Monto { get; set; }

        public double? MontoBodega { get; set; } = 0;

        public double? MontoAvaluo { get; set; } = 0;

        public double MontoTotal { get 
            {
                return Monto + (MontoBodega == null ? 0:MontoBodega.Value) + (MontoAvaluo==null ? 0:MontoAvaluo.Value);
            } 
        }

        public double Pagado { get; set; }

        public int? PagoId { get; set; }

        [JsonIgnore]
        public virtual Empeno Empeno { get; set; }
    }
}
