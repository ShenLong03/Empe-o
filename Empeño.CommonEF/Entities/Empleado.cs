namespace Empeño.CommonEF.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Empleado
    {
        [Key]
        [Display(Name = "Id")]
        public int EmpleadoId { get; set; }

        public string Nombre { get; set; }

        public string Telefono { get; set; }

        public string Correo { get; set; }

        public string Usuario { get; set; }

        public bool Activo { get; set; } = true;

        public bool IsDelete { get; set; } = false;

        public virtual ICollection<Empeno> Empenos { get; set; }

        public virtual ICollection<Empeno> Ediciones { get; set; }

        public virtual ICollection<Prorroga> Prorrogas { get; set; }

        public virtual ICollection<CierreCaja> CierreCajas { get; set; }

        public Empleado()
        {
            Activo = true;
        }
    }
}
