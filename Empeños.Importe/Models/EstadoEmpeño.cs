namespace Empeños.Importe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EstadoEmpeño
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EstadoEmpeño()
        {
            Empeño = new HashSet<Empeño>();
        }

        [Key]
        public int Id_Estado { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion_Estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Empeño> Empeño { get; set; }
    }
}
