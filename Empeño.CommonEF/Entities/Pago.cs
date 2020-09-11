namespace Empeño.CommonEF.Entities
{
    using Enum;
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Pago
    {
        [Key]
        public int PagoId { get; set; }

        public double? Consecutivo { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        public int EmpenoId { get; set; }

        public TipoPago TipoPago { get; set; }

        public double Monto { get; set; } = 0;

        public int EmpleadoId { get; set; }

        public string Comentario { get; set; }

        [JsonIgnore]
        public virtual Empeno Empeno { get; set; }

        [JsonIgnore]
        public virtual Empleado Empleado { get; set; }

        public Pago()
        {
            Fecha = DateTime.Now;
            Monto = 0;
        }
    }
}
