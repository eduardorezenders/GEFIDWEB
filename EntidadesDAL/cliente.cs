namespace EntidadesDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("cliente")]
    public partial class cliente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cliente()
        {
            acesso = new HashSet<acesso>();
            controle = new HashSet<controle>();
            emailRedesocial = new HashSet<emailRedesocial>();
            logradouro = new HashSet<logradouro>();
            mensagem = new HashSet<mensagem>();
            pessoa = new HashSet<pessoa>();
            telefone = new HashSet<telefone>();
            licenca = new HashSet<licenca>();
        }

        [Key]
        public int idCliente { get; set; }

        [Required]
        [Display(Name ="Razão Social")]
        public string razaoSocial { get; set; }

        [Required]
        [Display(Name = "Titular")]
        public string nomeTitular { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Login")]
        public string login { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Column(TypeName = "date")]
        [Display(Name = "Data de Criação")]
        public DateTime dtCadastro { get; set; }

        [StringLength(11)]
        [Display(Name = "CPF")]
        public string cpf { get; set; }

        [StringLength(13)]
        [Display(Name = "CNPJ")]
        public string cnpj { get; set; }

        [Display(Name = "Ativo")]
        public bool ativo { get; set; }

        [Display(Name = "Chave da API")]
        public string chaveapi { get; set; }

        [Display(Name = "Facebook")]
        public string facebook { get; set; }

        public string sfacebook { get; set; }

        [Display(Name = "Whatsapp")]
        public string whatsapp { get; set; }

        public string swhatsapp { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<acesso> acesso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<licenca> licenca { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<controle> controle { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<emailRedesocial> emailRedesocial { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<logradouro> logradouro { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mensagem> mensagem { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pessoa> pessoa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<telefone> telefone { get; set; }
    }
}
