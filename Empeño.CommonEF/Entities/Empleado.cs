﻿namespace Empeño.CommonEF.Entities
{
    using Newtonsoft.Json;
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

        [JsonIgnore]
        public virtual ICollection<Empeno> Empenos { get; set; }

        [JsonIgnore]
        public virtual ICollection<Empeno> Ediciones { get; set; }

        [JsonIgnore]
        public virtual ICollection<Prorroga> Prorrogas { get; set; }

        [JsonIgnore]
        public virtual ICollection<CierreCaja> CierreCajas { get; set; }

        [JsonIgnore]
        public virtual ICollection<Bitacora> Bitacoras { get; set; }

        public Empleado()
        {
            Activo = true;
        }
    }
}
