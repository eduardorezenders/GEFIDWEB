namespace EntidadesSOL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("servico")]
    public partial class servico
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public servico()
        {
            solicitacao = new HashSet<solicitacao>();
        }

        [Key]
        public int idServico { get; set; }

        [Required]
        public string descricao { get; set; }

        public bool ativo { get; set; }

        public int? idContatoServico { get; set; }

        public virtual contato contato { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<solicitacao> solicitacao { get; set; }
    }
}
