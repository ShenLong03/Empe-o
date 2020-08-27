namespace Empeños.Importe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bitacora_Empeño
    {
        [Key]
        public long Id_Consecutivo { get; set; }

        public long Numero_Empeño { get; set; }

        public DateTime Fecha_Bitacora { get; set; }

        public DateTime Fecha_Realizacion_Anterior { get; set; }

        [Required]
        [StringLength(50)]
        public string Cedula_Cliente { get; set; }

        [Required]
        [StringLength(450)]
        public string Nombre_Completo { get; set; }

        public decimal? Gramos_Anterior { get; set; }

        [Required]
        public string Articulo_Descripcion_Anterior { get; set; }

        public bool Es_Oro { get; set; }

        public int Monto_Empeño_Anterior { get; set; }

        public int Saldo_Pendiente_Anterior { get; set; }

        public DateTime Fecha_Vencimiento_Anterior { get; set; }

        public int Id_Porcentaje { get; set; }

        [Required]
        [StringLength(50)]
        public string Cedula_Empleado_Anterior { get; set; }

        [Required]
        [StringLength(50)]
        public string Cedula_Empleado_Edito_Anterior { get; set; }

        public int Id_Estado_Anterior { get; set; }

        public bool Retirado { get; set; }

        public int? Numero_Consecutivo { get; set; }

        public virtual Empeño Empeño { get; set; }
    }
}
