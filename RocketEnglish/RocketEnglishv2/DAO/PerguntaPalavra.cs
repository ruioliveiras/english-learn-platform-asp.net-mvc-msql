namespace RocketEnglishv2.DAO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PerguntaPalavra")]
    public partial class PerguntaPalavra
    {
        public PerguntaPalavra()
        {
            UtilizadorPerguntaPalavra = new HashSet<UtilizadorPerguntaPalavra>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(150)]
        public string pergunta { get; set; }

        [Required]
        [StringLength(75)]
        public string resposta { get; set; }

        public int idLicao { get; set; }

        [StringLength(75)]
        public string dica { get; set; }

        [StringLength(150)]
        public string imagem { get; set; }

        [StringLength(150)]
        public string video { get; set; }

        public virtual Licao Licao { get; set; }

        public virtual ICollection<UtilizadorPerguntaPalavra> UtilizadorPerguntaPalavra { get; set; }
    }
}
