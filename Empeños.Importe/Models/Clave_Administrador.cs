namespace Empeños.Importe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Clave_Administrador
    {
        [Key]
        public int Id_Clave { get; set; }

        [Column("Clave_Administrador")]
        [Required]
        [StringLength(50)]
        public string Clave_Administrador1 { get; set; }
    }
}
