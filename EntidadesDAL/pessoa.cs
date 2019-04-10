namespace EntidadesDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("pessoa")]
    public partial class pessoa
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pessoa()
        {
            agenda = new HashSet<agenda>();
            emailRedesocial = new HashSet<emailRedesocial>();
            logradouro = new HashSet<logradouro>();
            telefone = new HashSet<telefone>();
        }

        [Key]
        public int idPessoa { get; set; }

        public int idCliente { get; set; }

        [Required]
        [Display(Name ="Nome Completo")]
        public string nomeCompleto { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Column(TypeName = "date")]
        [Display(Name = "Data de Nascimento")]
        public DateTime? dataNascimento { get; set; }

        [Display(Name = "Ativo")]
        public bool ativo { get; set; }

        [StringLength(1)]
        [Display(Name = "Genêro")]
        public string idGenero { get; set; }

        [StringLength(11)]
        [Display(Name = "CPF")]
        public string cpf { get; set; }

        public string arquivo { get; set; }

        [Display(Name = "Facebook")]
        public string facebook { get; set; }

        [Display(Name = "Whatsapp")]
        public string whatsapp { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<agenda> agenda { get; set; }

        public virtual cliente cliente { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<emailRedesocial> emailRedesocial { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<logradouro> logradouro { get; set; }

        [Display(Name = "Genêro")]
        public virtual tipoGenero tipoGenero { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<telefone> telefone { get; set; }
    }
}
