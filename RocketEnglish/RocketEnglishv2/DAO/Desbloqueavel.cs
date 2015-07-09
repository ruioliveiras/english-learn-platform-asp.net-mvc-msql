namespace RocketEnglishv2.DAO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Desbloqueavel")]
    public partial class Desbloqueavel
    {
        public Desbloqueavel()
        {
            NivelCategoria = new HashSet<NivelCategoria>();
            Dialogo = new HashSet<Dialogo>();
            Utilizador = new HashSet<Utilizador>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(150)]
        public string imagem { get; set; }

        [Required]
        [StringLength(75)]
        public string nome { get; set; }

        public int tipo { get; set; }

        public virtual TipoDesbloqueavel TipoDesbloqueavel { get; set; }

        public virtual ICollection<NivelCategoria> NivelCategoria { get; set; }

        public virtual ICollection<Dialogo> Dialogo { get; set; }

        public virtual ICollection<Utilizador> Utilizador { get; set; }
    }
}
