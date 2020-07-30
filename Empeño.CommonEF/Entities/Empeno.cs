namespace Empeño.CommonEF.Entities
{
    using Enum;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Empeno
    {
        [Key]
        public int EmpenoId { get; set; }

        public int ClienteId { get; set; }

        public int EmpleadoId { get; set; }

        public int? EditorId { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        [Display(Name = "Es oro?")]
        public bool EsOro { get; set; } = false;

        public double Monto { get; set; } = 0;

        public double MontoPendiente { get; set; } = 0;

        public int InteresId { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        public string Comentario { get; set; }

        public Estado Estado { get; set; } = Estado.Activo;

        public DateTime FechaVencimiento { get; set; } = DateTime.Now.AddMonths(3).Date;

        public DateTime? FechaRetiro { get; set; } = null;

        public DateTime? FechaRetiroAdministrador { get; set; } = null;

        public bool Retirado { get; set; } = false;

        public bool RetiradoAdministrador { get; set; } = false;

        public bool Prorroga { get; set; } = false;

        public bool IsDelete { get; set; } = false;

        [JsonIgnore]
        public virtual Cliente Cliente { get; set; }
        [JsonIgnore]
        public virtual Empleado Empleado { get; set; }
        [JsonIgnore]
        public virtual Empleado Editor { get; set; }
        [JsonIgnore]
        public virtual Interes Interes { get; set; }
        [JsonIgnore]
        public virtual ICollection<Pago> Pagos { get; set; }
        [JsonIgnore]
        public virtual ICollection<Intereses> Intereses { get; set; }
        [JsonIgnore]
        public virtual ICollection<Prorroga> Prorrogas { get; set; }

        public Empeno()
        {
            Fecha = DateTime.Now;
            Estado = Estado.Activo;
            FechaVencimiento = DateTime.Now.AddMonths(3).Date;
            Retirado = false;
            Prorroga = false;
            RetiradoAdministrador = false;
            IsDelete = false;
        }
    }
}
