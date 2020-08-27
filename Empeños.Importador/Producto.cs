namespace Empeños.Importador
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Producto")]
    public partial class Producto
    {
        [Key]
        public int Id_Producto { get; set; }

        [Required]
        [StringLength(150)]
        public string Descripcion_Producto { get; set; }

        public bool Activo { get; set; }
    }
}
