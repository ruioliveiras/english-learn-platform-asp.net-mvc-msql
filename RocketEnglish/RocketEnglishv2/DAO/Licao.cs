namespace RocketEnglishv2.DAO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Licao")]
    public partial class Licao
    {
        public Licao()
        {
            PerguntaEscolhaMultipla = new HashSet<PerguntaEscolhaMultipla>();
            PerguntaPalavra = new HashSet<PerguntaPalavra>();
            Utilizador = new HashSet<Utilizador>();
        }

        public int id { get; set; }

        public int nivel { get; set; }

        [Required]
        [StringLength(75)]
        public string nome { get; set; }

        [StringLength(300)]
        public string descricao { get; set; }

        public int idCategoria { get; set; }

        public virtual NivelCategoria NivelCategoria { get; set; }

        public virtual ICollection<PerguntaEscolhaMultipla> PerguntaEscolhaMultipla { get; set; }

        public virtual ICollection<PerguntaPalavra> PerguntaPalavra { get; set; }

        public virtual ICollection<Utilizador> Utilizador { get; set; }
    }
}
