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

        public Interes()
        {
            Porcentaje = 1;
        }
    }
}
