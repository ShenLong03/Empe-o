﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Empeño.CommonEF.Entities
{
     public class Bitacora
    {
        [Key]
        public int BitacoraId { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        public int Error { get; set; } = 0;

        public string Valor { get; set; }

        public string Mensaje { get; set; }

        public int? EmpleadoId { get; set; }

        public string Usuario { get; set; }

        public virtual Empleado Empleado { get; set; }

        public Bitacora()
        {
            Fecha = DateTime.Now;
            Error = 0;
        }
    }
}
