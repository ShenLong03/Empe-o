﻿namespace Empeño.CommonEF.Entities
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Interes
    {
        [Key]
        public int InteresId { get; set; }

        [Display(Name = "Interes")]
        public string Nombre { get; set; }

        public double Porcentaje { get; set; } = 1;

        public double Bodega { get; set; } = 1;

        public int Mayor { get; set; }

        public int Menor { get; set; }

        public int Igual { get; set; }

        public bool Activo { get; set; } = true;

        [JsonIgnore]
        public virtual ICollection<Intereses> Intereses { get; set; }

        public Interes()
        {
            Porcentaje = 1;
            Bodega = 1;
        }
    }
}
