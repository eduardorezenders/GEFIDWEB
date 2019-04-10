namespace EntidadesSOL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("contato")]
    public partial class contato
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public contato()
        {
            servico = new HashSet<servico>();
        }

        [Display(Name ="Nome do Contato")]
        public string nomeContato { get; set; }

        [Display(Name ="Ocupação")]
        public string ocupacao { get; set; }

        [Display(Name ="Descrição")]
        public string descricao { get; set; }

        [StringLength(15)]
        [Display(Name ="Abreviação")]
        public string abreviacao { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idContato { get; set; }

        [Display(Name ="Tipo de Contato")]
        public int? idTipoContato { get; set; }

        public int? idOperador { get; set; }

        [Display(Name ="Órgão")]
        public int? idEstruturaOrgao { get; set; }

        [Display(Name ="Tratamento")]
        public int? idTratamentoPessoa { get; set; }

        public int idCliente { get; set; }

        public virtual tipoContato tipoContato { get; set; }

        public virtual tratamentoPessoa tratamentoPessoa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<servico> servico { get; set; }
    }
}
