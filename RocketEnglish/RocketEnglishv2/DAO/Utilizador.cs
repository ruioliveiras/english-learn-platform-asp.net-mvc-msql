namespace RocketEnglishv2.DAO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Utilizador")]
    public partial class Utilizador
    {
        public Utilizador()
        {
            UtilizadorPerguntaEscolhaMultipla = new HashSet<UtilizadorPerguntaEscolhaMultipla>();
            UtilizadorPerguntaPalavra = new HashSet<UtilizadorPerguntaPalavra>();
            UtilizadorNivelCategoria = new HashSet<UtilizadorNivelCategoria>();
            Desbloqueavel = new HashSet<Desbloqueavel>();
            Licao = new HashSet<Licao>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(75)]
        public string nome { get; set; }

        public bool tutorialFeito { get; set; }

        public int nivelGlobal { get; set; }

        public int loginSeguidosAtual { get; set; }

        public int loginSeguidosMaximo { get; set; }

        public DateTime dataUltimoLogin { get; set; }

        [Required]
        [StringLength(75)]
        public string passsword { get; set; }

        [Required]
        [StringLength(75)]
        public string email { get; set; }

        [Required]
        [StringLength(75)]
        public string username { get; set; }

        public virtual ICollection<UtilizadorPerguntaEscolhaMultipla> UtilizadorPerguntaEscolhaMultipla { get; set; }

        public virtual ICollection<UtilizadorPerguntaPalavra> UtilizadorPerguntaPalavra { get; set; }

        public virtual ICollection<UtilizadorNivelCategoria> UtilizadorNivelCategoria { get; set; }

        public virtual ICollection<Desbloqueavel> Desbloqueavel { get; set; }

        public virtual ICollection<Licao> Licao { get; set; }
    }
}
