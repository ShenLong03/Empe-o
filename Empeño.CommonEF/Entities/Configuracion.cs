using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Empeño.CommonEF.Entities
{
    public class Configuracion
    {
        [Key]
        public int ConfiguracionId { get; set; }


        [Display(Name = "Compañía")]
        public string Compañia { get; set; }

        [Display(Name = "Identificación")]
        public string Identificacion { get; set; }

        [Display(Name = "Empresa")]
        public string Nombre { get; set; }

        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        public string Direccion { get; set; }

        public int Meses { get; set; } = 3;

        public int? IVA { get; set; } = 0;

        public double PorcentajeIVA 
        {
            get 
            {
               return ((double)IVA / (double)100);
            }
        }
        //---------------------

        public string EmailNotification { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string SMTP { get; set; }

        public int Puerto { get; set; }

        public bool SSL { get; set; }

        public Configuracion()
        {
            Meses = 3;
            SMTP = "smtp.gmail.com";
            Puerto = 587;
            SSL = true;
        }
    }
}
