namespace Empeño.CommonEF.Entities
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Perfil
    {
        [Key]
        public int PerfilId { get; set; }

        public string Nombre { get; set; }

        [JsonIgnore]
        public virtual ICollection<Perfil> Perfiles { get; set; }
    }
}
