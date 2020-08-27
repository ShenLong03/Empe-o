namespace Empeños.Importe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AbonoPrincipal")]
    public partial class AbonoPrincipal
    {
        [Key]
        public int Id_Abono { get; set; }

        public long Numero_Empeño { get; set; }

        public int Monto_Abono { get; set; }

        public DateTime Fecha_Abono { get; set; }

        [Required]
        [StringLength(50)]
        public string Cedula_Empleado { get; set; }

        public long Numero_Consecutivo { get; set; }
    }
}
