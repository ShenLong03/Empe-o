namespace Empeño.Common.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [Display(Name ="Identificación")]
        public string Identificacion { get; set; }

        public string Nombre { get; set; }

        public string Telefono { get; set; }

        public string Correo { get; set; }

        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        public string Comentario { get; set; }

        public bool Activo { get; set; } = true;

        public virtual ICollection<Empeno> Empenos { get; set; }

        public Cliente()
        {
            Activo = true;
        }
    }
}
