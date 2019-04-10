namespace EntidadesSOL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SOLModelos : DbContext
    {
        public SOLModelos()
            : base("name=SOLModelos")
        {
        }

        public virtual DbSet<contato> contato { get; set; }
        public virtual DbSet<nivelSatisfacaoResposta> nivelSatisfacaoResposta { get; set; }
        public virtual DbSet<providencia> providencia { get; set; }
        public virtual DbSet<servico> servico { get; set; }
        public virtual DbSet<solicitacao> solicitacao { get; set; }
        public virtual DbSet<statusOuvidoria> statusOuvidoria { get; set; }
        public virtual DbSet<tipoAssunto> tipoAssunto { get; set; }
        public virtual DbSet<tipoContato> tipoContato { get; set; }
        public virtual DbSet<tipoPrioridade> tipoPrioridade { get; set; }
        public virtual DbSet<tratamentoPessoa> tratamentoPessoa { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<contato>()
                .Property(e => e.abreviacao)
                .IsFixedLength();

            modelBuilder.Entity<contato>()
                .HasMany(e => e.servico)
                .WithOptional(e => e.contato)
                .HasForeignKey(e => e.idContatoServico);

            modelBuilder.Entity<solicitacao>()
                .Property(e => e.cep)
                .IsFixedLength();

            modelBuilder.Entity<solicitacao>()
                .HasMany(e => e.providencia)
                .WithRequired(e => e.solicitacao)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<statusOuvidoria>()
                .HasMany(e => e.solicitacao)
                .WithRequired(e => e.statusOuvidoria)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tipoAssunto>()
                .HasMany(e => e.solicitacao)
                .WithRequired(e => e.tipoAssunto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tipoPrioridade>()
                .HasMany(e => e.solicitacao)
                .WithRequired(e => e.tipoPrioridade)
                .HasForeignKey(e => e.idTipoPrioridade)
                .WillCascadeOnDelete(false);
        }
    }
}
