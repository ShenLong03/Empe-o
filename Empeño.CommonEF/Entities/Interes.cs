namespace Empeño.CommonEF.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Interes
    {
        [Key]
        public int InteresId { get; set; }

        [Display(Name = "Interes")]
        public string Nombre { get; set; }

        public double Porcentaje { get; set; } = 1;

        public int Mayor { get; set; }

        public int Menor { get; set; }

        public int Igual { get; set; }

        public int Meses { get; set; }

        public double? Avalauo { get; set; } = 0;

        public double? Bodegaje { get; set; } = 0;

        public bool Activo { get; set; } = true;

        public Interes()
        {
            Porcentaje = 1;
        }
    }
}
