namespace RocketEnglishv2.DAO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UtilizadorPerguntaEscolhaMultipla")]
    public partial class UtilizadorPerguntaEscolhaMultipla
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idPerguntaEscolhaMultipla { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idUtilizador { get; set; }

        public bool correta { get; set; }

        public virtual PerguntaEscolhaMultipla PerguntaEscolhaMultipla { get; set; }

        public virtual Utilizador Utilizador { get; set; }
    }
}
