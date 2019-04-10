namespace EntidadesSOL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tipoPrioridade")]
    public partial class tipoPrioridade
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tipoPrioridade()
        {
            solicitacao = new HashSet<solicitacao>();
        }

        [Key]
        public int idtTipoPrioridade { get; set; }

        [Required]
        [Display(Name ="Descrição")]
        public string descricao { get; set; }

        [Display(Name ="Ativo")]
        public bool ativo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<solicitacao> solicitacao { get; set; }
    }
}
