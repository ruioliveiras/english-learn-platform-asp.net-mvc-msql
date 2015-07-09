namespace RocketEnglishv2.DAO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Categoria")]
    public partial class Categoria
    {
        public Categoria()
        {
            NivelCategoria = new HashSet<NivelCategoria>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(75)]
        public string nome { get; set; }

        public virtual ICollection<NivelCategoria> NivelCategoria { get; set; }
    }
}
