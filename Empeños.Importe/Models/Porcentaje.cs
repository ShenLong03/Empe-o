namespace Empeños.Importe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Porcentaje")]
    public partial class Porcentaje
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Porcentaje()
        {
            Empeño = new HashSet<Empeño>();
        }

        [Key]
        public int Id_Porcentaje { get; set; }

        [Column("Porcentaje", TypeName = "numeric")]
        public decimal? Porcentaje1 { get; set; }

        public bool Activo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Empeño> Empeño { get; set; }
    }
}
