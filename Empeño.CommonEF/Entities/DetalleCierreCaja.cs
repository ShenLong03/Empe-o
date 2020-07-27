using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Empeño.CommonEF.Entities
{
    public class DetalleCierreCaja
    {
        [Key]
        public int DetalleCierreCajaId { get; set; }

        public string Concepto { get; set; }

        public double Valor { get; set; }

        public double Cantidad { get; set; }

        public double SubTotal { get { return Valor * Cantidad; } }

        public int CierreCajaId { get; set; }

        public virtual CierreCaja CierreCaja { get; set; }
    }
}
