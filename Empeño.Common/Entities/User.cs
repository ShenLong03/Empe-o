using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace Empeño.Common.Entities
{
    public class User
    {
        [Key]
        public int UsuarioId { get; set; }

        public string Usuario { get; set; }

        public string Password { get; set; }

        public string Codigo { get; set; }

        public bool Activo { get; set; } = true;

        public int PerfilId { get; set; }

        public Perfil Perfil { get; set; }

        public User()
        {
            Activo = true;
        }
    }
}
