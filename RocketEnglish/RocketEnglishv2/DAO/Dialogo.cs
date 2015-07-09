namespace RocketEnglishv2.DAO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Dialogo")]
    public partial class Dialogo
    {
        public int id { get; set; }

        [Column("dialogo")]
        [Required]
        [StringLength(75)]
        public string dialogo1 { get; set; }

        public int idDesbloqueavel { get; set; }

        public virtual Desbloqueavel Desbloqueavel { get; set; }
    }
}
