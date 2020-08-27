namespace Empeño.Exportador
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cliente")]
    public partial class Cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cliente()
        {
            Empeño = new HashSet<Empeño>();
        }

        [Key]
        [StringLength(50)]
        public string Cedula_Cliente { get; set; }

        [Required]
        [StringLength(350)]
        public string Nombre_Completo { get; set; }

        [StringLength(50)]
        public string Telefono { get; set; }

        [StringLength(250)]
        public string Correo { get; set; }

        [StringLength(250)]
        public string Nota_Cliente { get; set; }

        public bool Activo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Empeño> Empeño { get; set; }
    }
}
