namespace Empeños.Importe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Empeño
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Empeño()
        {
            Bitacora_Empeño = new HashSet<Bitacora_Empeño>();
            Cancelacions = new HashSet<Cancelacion>();
            Interes_Empeño = new HashSet<Interes_Empeño>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Numero_Empeño { get; set; }

        public DateTime Fecha_Realizacion { get; set; }

        [Required]
        [StringLength(50)]
        public string Cedula_Cliente { get; set; }

        [Required]
        [StringLength(450)]
        public string Nombre_Completo { get; set; }

        [StringLength(50)]
        public string Gramos { get; set; }

        [Required]
        [StringLength(500)]
        public string Descripcion_Producto { get; set; }

        public bool Es_Oro { get; set; }

        public int Monto_Empeño { get; set; }

        public int Saldo_Pendiente { get; set; }

        public DateTime Fecha_Vencimiento { get; set; }

        public int Id_Porcentaje { get; set; }

        [Required]
        [StringLength(50)]
        public string Cedula_Empleado { get; set; }

        [StringLength(50)]
        public string Cedula_Empleado_Edito { get; set; }

        public int Id_Estado { get; set; }

        public bool? Retirado { get; set; }

        public long? Numero_Consecutivo { get; set; }

        public DateTime? fretiro { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bitacora_Empeño> Bitacora_Empeño { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cancelacion> Cancelacions { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual Empleado Empleado { get; set; }

        public virtual EstadoEmpeño EstadoEmpeño { get; set; }

        public virtual Porcentaje Porcentaje { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Interes_Empeño> Interes_Empeño { get; set; }
    }
}
