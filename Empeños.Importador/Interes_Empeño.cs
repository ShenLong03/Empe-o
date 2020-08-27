namespace Empeños.Importador
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Interes_Empeño
    {
        [Key]
        public int Id_Interes { get; set; }

        public DateTime? Fecha_Registro { get; set; }

        [Required]
        [StringLength(50)]
        public string Cedula_Cliente { get; set; }

        [Required]
        [StringLength(450)]
        public string Nombre_Completo { get; set; }

        public long Numero_Empeño { get; set; }

        public DateTime Fecha_Cobro_Interes { get; set; }

        [Required]
        [StringLength(50)]
        public string Mes_Pago { get; set; }

        public int Monto_Interes { get; set; }

        public int? Monto_Pagado_Interes { get; set; }

        [StringLength(50)]
        public string Cedula_Empleado { get; set; }

        public bool Esta_Pago { get; set; }

        public long? Numero_Consecutivo { get; set; }

        public virtual Empeño Empeño { get; set; }
    }
}
