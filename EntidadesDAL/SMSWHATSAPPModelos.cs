namespace EntidadesDAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SMSWHATSAPPModelos : DbContext
    {
        public SMSWHATSAPPModelos()
            : base("name=SMSWHATSAPPModelos")
        {
        }
        public virtual DbSet<acesso> acesso { get; set; }
        public virtual DbSet<acessoGrupo> acessoGrupo { get; set; }
        public virtual DbSet<acessoModulo> acessoModulo { get; set; }
        public virtual DbSet<acessoPerfil> acessoPerfil { get; set; }
        public virtual DbSet<agenda> agenda { get; set; }
        public virtual DbSet<cepbr_bairro> cepbr_bairro { get; set; }
        public virtual DbSet<cepbr_cidade> cepbr_cidade { get; set; }
        public virtual DbSet<cepbr_endereco> cepbr_endereco { get; set; }
        public virtual DbSet<cepbr_estado> cepbr_estado { get; set; }
        public virtual DbSet<cliente> cliente { get; set; }
        public virtual DbSet<controle> controle { get; set; }
        public virtual DbSet<emailRedesocial> emailRedesocial { get; set; }
        public virtual DbSet<licenca> licenca { get; set; }
        public virtual DbSet<logradouro> logradouro { get; set; }
        public virtual DbSet<mensagem> mensagem { get; set; }
        public virtual DbSet<pessoa> pessoa { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<telefone> telefone { get; set; }
        public virtual DbSet<tipoEndereco> tipoEndereco { get; set; }
        public virtual DbSet<tipoGenero> tipoGenero { get; set; }
        public virtual DbSet<tipoLogradouro> tipoLogradouro { get; set; }
        public virtual DbSet<tipoTelefone> tipoTelefone { get; set; }
        public virtual DbSet<actionsLog> actionsLog { get; set; }
        public virtual DbSet<campanha> campanha { get; set; }
        public virtual DbSet<controleCampanha> controleCampanha { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<acesso>()
                .Property(e => e.cpf)
                .IsUnicode(false);

            modelBuilder.Entity<acesso>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<acesso>()
                .Property(e => e.sobrenome)
                .IsUnicode(false);

            modelBuilder.Entity<acessoGrupo>()
                .Property(e => e.nome)
                .IsUnicode(false);

            modelBuilder.Entity<acessoGrupo>()
                .HasMany(e => e.acesso)
                .WithRequired(e => e.acessoGrupo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<acessoGrupo>()
                .HasMany(e => e.acessoPerfil)
                .WithRequired(e => e.acessoGrupo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<acessoModulo>()
                .Property(e => e.descricao)
                .IsFixedLength();

            modelBuilder.Entity<acessoModulo>()
                .HasMany(e => e.acessoPerfil)
                .WithRequired(e => e.acessoModulo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cepbr_bairro>()
                .HasMany(e => e.logradouro)
                .WithOptional(e => e.cepbr_bairro)
                .HasForeignKey(e => e.idBairro);

            modelBuilder.Entity<cepbr_cidade>()
                .Property(e => e.uf)
                .IsFixedLength();

            modelBuilder.Entity<cepbr_cidade>()
                .HasMany(e => e.logradouro)
                .WithOptional(e => e.cepbr_cidade)
                .HasForeignKey(e => e.idCidade);

            modelBuilder.Entity<cepbr_estado>()
                .Property(e => e.uf)
                .IsFixedLength();

            modelBuilder.Entity<cliente>()
                .HasMany(e => e.acesso)
                .WithRequired(e => e.cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cliente>()
                .HasMany(e => e.controle)
                .WithRequired(e => e.cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cliente>()
                .HasMany(e => e.mensagem)
                .WithRequired(e => e.cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cliente>()
                .HasMany(e => e.pessoa)
                .WithRequired(e => e.cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cliente>()
                .HasMany(e => e.telefone)
                .WithRequired(e => e.cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cliente>()
                .HasMany(e => e.licenca)
                .WithRequired(e => e.cliente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<controle>()
                .Property(e => e.priSms)
                .IsFixedLength();

            modelBuilder.Entity<controle>()
                .Property(e => e.segSms)
                .IsFixedLength();

            modelBuilder.Entity<controle>()
                .Property(e => e.terSms)
                .IsFixedLength();

            modelBuilder.Entity<controle>()
                .Property(e => e.quaSms)
                .IsFixedLength();

            modelBuilder.Entity<controle>()
                .Property(e => e.quiSms)
                .IsFixedLength();

            modelBuilder.Entity<controle>()
                .Property(e => e.aCada)
                .IsFixedLength();

            modelBuilder.Entity<logradouro>()
                .Property(e => e.uf)
                .IsFixedLength();

            modelBuilder.Entity<pessoa>()
                .Property(e => e.idGenero)
                .IsFixedLength();

            modelBuilder.Entity<pessoa>()
                .HasMany(e => e.agenda)
                .WithRequired(e => e.pessoa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<pessoa>()
                .HasMany(e => e.telefone)
                .WithRequired(e => e.pessoa)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<tipoGenero>()
                .Property(e => e.idGenero)
                .IsFixedLength();
        }
    }
}
