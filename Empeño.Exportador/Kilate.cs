namespace Empeño.Exportador
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Kilate
    {
        [Key]
        public int Id_Kilate { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion_Kilate { get; set; }

        public bool Activo { get; set; }
    }
}
