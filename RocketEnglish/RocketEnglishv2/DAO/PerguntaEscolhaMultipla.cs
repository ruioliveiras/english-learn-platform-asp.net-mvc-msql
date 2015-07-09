namespace RocketEnglishv2.DAO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PerguntaEscolhaMultipla")]
    public partial class PerguntaEscolhaMultipla
    {
        public PerguntaEscolhaMultipla()
        {
            UtilizadorPerguntaEscolhaMultipla = new HashSet<UtilizadorPerguntaEscolhaMultipla>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(75)]
        public string resposta { get; set; }

        [Required]
        [StringLength(75)]
        public string errada1 { get; set; }

        [Required]
        [StringLength(75)]
        public string errada2 { get; set; }

        [Required]
        [StringLength(75)]
        public string errada3 { get; set; }

        [Required]
        [StringLength(150)]
        public string pergunta { get; set; }

        public int idLicao { get; set; }

        [StringLength(75)]
        public string dica { get; set; }

        [StringLength(150)]
        public string imagem { get; set; }

        [StringLength(150)]
        public string video { get; set; }

        public virtual Licao Licao { get; set; }

        public virtual ICollection<UtilizadorPerguntaEscolhaMultipla> UtilizadorPerguntaEscolhaMultipla { get; set; }
    }
}
