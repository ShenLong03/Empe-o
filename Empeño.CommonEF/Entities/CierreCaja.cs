using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Empeño.CommonEF.Entities
{
    public class CierreCaja
    {
        [Key]
        public int CierreCajaId { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        public int EmpleadoId { get; set; }

        public double SaldoInicial { get; set; }

        public bool IsDelete { get; set; } = false;

        public virtual ICollection<DetalleCierreCaja> Detalles { get; set; }

        public virtual Empleado Empleado { get; set; }
    }
}
