using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Empeño.CommonEF.Entities
{
    public class Prorroga
    {
        [Key]
        public int ProrrogaId { get; set; }

        public DateTime Fecha { get; set; }

        [Display(Name ="Días de Prorroga")]
        public int DiasProrroga { get; set; }

        public int EmpenoId { get; set; }

        public int EmpleadoId { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comentario { get; set; }

        public virtual Empeno Empeno { get; set; }

        public virtual Empleado Empleado { get; set; }
    }
}
