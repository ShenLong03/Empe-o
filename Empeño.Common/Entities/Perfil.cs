namespace Empeño.Common.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Perfil
    {
        [Key]
        public int PerfilId { get; set; }

        public string Nombre { get; set; }
    }
}
