namespace Empeños.Importador
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Intereses_Anteriores
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Numero_Empeño { get; set; }

        public int Cantidad_Meses { get; set; }

        public int Monto_Recibido { get; set; }
    }
}
