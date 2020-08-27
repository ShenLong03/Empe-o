namespace Empeños.Importador
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bitacora_Sistema
    {
        [Key]
        public int Id_Bitacora { get; set; }

        [Required]
        [StringLength(50)]
        public string Cedula_Empleado { get; set; }

        public DateTime Fecha_Registro { get; set; }

        [Required]
        [StringLength(550)]
        public string Evento_Realizado { get; set; }
    }
}
