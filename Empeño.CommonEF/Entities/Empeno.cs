using System;
using System.Collections.Generic;
using System.Text;

namespace Empeño.CommonEF.Entities
{
    using Enum;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class Empeno
    {
        [Key]
        public int EmpenoId { get; set; }

        public int ClienteId { get; set; }

        public int EmpleadoId { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        [Display(Name = "Es oro?")]
        public bool EsOro { get; set; } = false;

        public double Monto { get; set; } = 0;

        public double MontoPendiente { get; set; } = 0;

        public int InteresId { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        public Estado Estado { get; set; } = Estado.Activo;

        public DateTime FechaVencimiento { get; set; } = DateTime.Now.AddMonths(3).Date;

        public virtual Cliente Cliente { get; set; }

        public virtual Empleado Empleado { get; set; }

        public virtual Interes Interes { get; set; }

        public virtual ICollection<Intereses> Intereses { get; set; }

        public Empeno()
        {
            Fecha = DateTime.Now;
            Estado = Estado.Activo;
            FechaVencimiento = DateTime.Now.AddMonths(3).Date;
        }
    }
}
