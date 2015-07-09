namespace RocketEnglishv2.DAO
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class RocketEnglish : DbContext
    {
        public RocketEnglish()
            : base("name=RocketEnglish")
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Desbloqueavel> Desbloqueavel { get; set; }
        public virtual DbSet<Dialogo> Dialogo { get; set; }
        public virtual DbSet<Licao> Licao { get; set; }
        public virtual DbSet<NivelCategoria> NivelCategoria { get; set; }
        public virtual DbSet<PerguntaEscolhaMultipla> PerguntaEscolhaMultipla { get; set; }
        public virtual DbSet<PerguntaPalavra> PerguntaPalavra { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TipoDesbloqueavel> TipoDesbloqueavel { get; set; }
        public virtual DbSet<Utilizador> Utilizador { get; set; }
        public virtual DbSet<UtilizadorNivelCategoria> UtilizadorNivelCategoria { get; set; }
        public virtual DbSet<UtilizadorPerguntaEscolhaMultipla> UtilizadorPerguntaEscolhaMultipla { get; set; }
        public virtual DbSet<UtilizadorPerguntaPalavra> UtilizadorPerguntaPalavra { get; set; }
        public virtual DbSet<UtilizadorCategoriaDesbloqueavel> UtilizadorCategoriaDesbloqueavel { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<Categoria>()
                .HasMany(e => e.NivelCategoria)
                .WithRequired(e => e.Categoria)
                .HasForeignKey(e => e.idCategoria)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Desbloqueavel>()
                .Property(e => e.imagem)
                .IsUnicode(false);

            modelBuilder.Entity<Desbloqueavel>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<Desbloqueavel>()
                .HasMany(e => e.NivelCategoria)
                .WithRequired(e => e.Desbloqueavel)
                .HasForeignKey(e => e.idDesbloqueavel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Desbloqueavel>()
                .HasMany(e => e.Dialogo)
                .WithRequired(e => e.Desbloqueavel)
                .HasForeignKey(e => e.idDesbloqueavel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Desbloqueavel>()
                .HasMany(e => e.Utilizador)
                .WithMany(e => e.Desbloqueavel)
                .Map(m => m.ToTable("UtilizadorDesbloqueavel").MapLeftKey("idDesbloqueavel").MapRightKey("idUtilizador"));

            modelBuilder.Entity<Dialogo>()
                .Property(e => e.dialogo1)
                .IsUnicode(false);

            modelBuilder.Entity<Licao>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<Licao>()
                .Property(e => e.descricao)
                .IsUnicode(false);

            modelBuilder.Entity<Licao>()
                .HasMany(e => e.PerguntaEscolhaMultipla)
                .WithRequired(e => e.Licao)
                .HasForeignKey(e => e.idLicao)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Licao>()
                .HasMany(e => e.PerguntaPalavra)
                .WithRequired(e => e.Licao)
                .HasForeignKey(e => e.idLicao)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Licao>()
                .HasMany(e => e.Utilizador)
                .WithMany(e => e.Licao)
                .Map(m => m.ToTable("UtilizadorLicao").MapLeftKey("idLicao").MapRightKey("idUtilizador"));

            modelBuilder.Entity<NivelCategoria>()
                .HasMany(e => e.Licao)
                .WithRequired(e => e.NivelCategoria)
                .HasForeignKey(e => new { e.nivel, e.idCategoria })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NivelCategoria>()
                .HasMany(e => e.UtilizadorNivelCategoria)
                .WithRequired(e => e.NivelCategoria)
                .HasForeignKey(e => new { e.nivel, e.idCategoria })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PerguntaEscolhaMultipla>()
                .Property(e => e.resposta)
                .IsUnicode(false);

            modelBuilder.Entity<PerguntaEscolhaMultipla>()
                .Property(e => e.errada1)
                .IsUnicode(false);

            modelBuilder.Entity<PerguntaEscolhaMultipla>()
                .Property(e => e.errada2)
                .IsUnicode(false);

            modelBuilder.Entity<PerguntaEscolhaMultipla>()
                .Property(e => e.errada3)
                .IsUnicode(false);

            modelBuilder.Entity<PerguntaEscolhaMultipla>()
                .Property(e => e.pergunta)
                .IsUnicode(false);

            modelBuilder.Entity<PerguntaEscolhaMultipla>()
                .Property(e => e.dica)
                .IsUnicode(false);

            modelBuilder.Entity<PerguntaEscolhaMultipla>()
                .Property(e => e.imagem)
                .IsUnicode(false);

            modelBuilder.Entity<PerguntaEscolhaMultipla>()
                .Property(e => e.video)
                .IsUnicode(false);

            modelBuilder.Entity<PerguntaEscolhaMultipla>()
                .HasMany(e => e.UtilizadorPerguntaEscolhaMultipla)
                .WithRequired(e => e.PerguntaEscolhaMultipla)
                .HasForeignKey(e => e.idPerguntaEscolhaMultipla)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PerguntaPalavra>()
                .Property(e => e.pergunta)
                .IsUnicode(false);

            modelBuilder.Entity<PerguntaPalavra>()
                .Property(e => e.resposta)
                .IsUnicode(false);

            modelBuilder.Entity<PerguntaPalavra>()
                .Property(e => e.dica)
                .IsUnicode(false);

            modelBuilder.Entity<PerguntaPalavra>()
                .Property(e => e.imagem)
                .IsUnicode(false);

            modelBuilder.Entity<PerguntaPalavra>()
                .Property(e => e.video)
                .IsUnicode(false);

            modelBuilder.Entity<PerguntaPalavra>()
                .HasMany(e => e.UtilizadorPerguntaPalavra)
                .WithRequired(e => e.PerguntaPalavra)
                .HasForeignKey(e => e.idPerguntaPalavra)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoDesbloqueavel>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<TipoDesbloqueavel>()
                .HasMany(e => e.Desbloqueavel)
                .WithRequired(e => e.TipoDesbloqueavel)
                .HasForeignKey(e => e.tipo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utilizador>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<Utilizador>()
                .Property(e => e.passsword)
                .IsUnicode(false);

            modelBuilder.Entity<Utilizador>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Utilizador>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<Utilizador>()
                .HasMany(e => e.UtilizadorPerguntaEscolhaMultipla)
                .WithRequired(e => e.Utilizador)
                .HasForeignKey(e => e.idUtilizador)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utilizador>()
                .HasMany(e => e.UtilizadorPerguntaPalavra)
                .WithRequired(e => e.Utilizador)
                .HasForeignKey(e => e.idUtilizador)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Utilizador>()
                .HasMany(e => e.UtilizadorNivelCategoria)
                .WithRequired(e => e.Utilizador)
                .HasForeignKey(e => e.idUtilizador)
                .WillCascadeOnDelete(false);
        }
    }
}
