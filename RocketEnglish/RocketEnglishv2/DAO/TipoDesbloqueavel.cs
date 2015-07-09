namespace RocketEnglishv2.DAO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TipoDesbloqueavel")]
    public partial class TipoDesbloqueavel
    {
        public TipoDesbloqueavel()
        {
            Desbloqueavel = new HashSet<Desbloqueavel>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(75)]
        public string nome { get; set; }

        public virtual ICollection<Desbloqueavel> Desbloqueavel { get; set; }
    }
}
