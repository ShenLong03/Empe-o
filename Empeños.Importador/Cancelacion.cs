namespace Empeños.Importador
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cancelacion")]
    public partial class Cancelacion
    {
        [Key]
        public int Id_Cancelacion { get; set; }

        public DateTime Fecha_Cancelacion { get; set; }

        public long Numero_Empeño { get; set; }

        public int Monto_Intereses_Acumulados { get; set; }

        public int Monto_Total { get; set; }

        [Required]
        [StringLength(50)]
        public string Cedula_Empleado { get; set; }

        public long Numero_Consecutivo { get; set; }

        public bool Activo { get; set; }

        public int Total_Intereses { get; set; }

        public virtual Empeño Empeño { get; set; }
    }
}
