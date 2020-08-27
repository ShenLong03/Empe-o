namespace Empeños.Importador
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Consecutivo")]
    public partial class Consecutivo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Numero_Consecutivo { get; set; }
    }
}
