namespace RocketEnglishv2.DAO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NivelCategoria")]
    public partial class NivelCategoria
    {
        public NivelCategoria()
        {
            Licao = new HashSet<Licao>();
            UtilizadorNivelCategoria = new HashSet<UtilizadorNivelCategoria>();
        }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int nivel { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idCategoria { get; set; }

        public int idDesbloqueavel { get; set; }

        public virtual Categoria Categoria { get; set; }

        public virtual Desbloqueavel Desbloqueavel { get; set; }

        public virtual ICollection<Licao> Licao { get; set; }

        public virtual ICollection<UtilizadorNivelCategoria> UtilizadorNivelCategoria { get; set; }
    }
}
