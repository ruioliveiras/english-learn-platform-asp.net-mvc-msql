namespace RocketEnglishv2.DAO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UtilizadorNivelCategoria")]
    public partial class UtilizadorNivelCategoria
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idUtilizador { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int nivel { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idCategoria { get; set; }

        public bool testePerfeito { get; set; }

        public virtual NivelCategoria NivelCategoria { get; set; }

        public virtual Utilizador Utilizador { get; set; }
    }
}
