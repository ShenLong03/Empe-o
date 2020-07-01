using System;
using System.ComponentModel.DataAnnotations;

namespace Empeño.Common
{
    public class Configuracion
    {
        [Key]
        public int ConfiguracionId { get; set; }

        [Display(Name ="Identificación")]
        public string Identificacion { get; set; }

        [Display(Name = "Empresa")]
        public string Nombre { get; set; }

        [Display(Name ="Teléfono")]
        public string Telefono { get; set; }

        public int Meses { get; set; } = 3;

        public Configuracion()
        {
            Meses = 3;
        }
    }
}
