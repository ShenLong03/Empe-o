namespace Empeños.Importador
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Marca")]
    public partial class Marca
    {
        [Key]
        public int Id_Marca { get; set; }

        [Required]
        [StringLength(150)]
        public string Descripcion_Marca { get; set; }

        public bool Activo { get; set; }
    }
}
