namespace EntidadesSOL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tipoAssunto")]
    public partial class tipoAssunto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tipoAssunto()
        {
            solicitacao = new HashSet<solicitacao>();
        }

        [Key]
        public int idTipoAssunto { get; set; }

        [Required]
        [Display(Name = "Descrição")]
        public string descricao { get; set; }

        [Display(Name ="Ativo")]
        public bool ativo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<solicitacao> solicitacao { get; set; }
    }
}
