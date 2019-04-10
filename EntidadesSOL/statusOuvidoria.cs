namespace EntidadesSOL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("statusOuvidoria")]
    public partial class statusOuvidoria
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public statusOuvidoria()
        {
            solicitacao = new HashSet<solicitacao>();
        }

        [Key]
        public int idStatusOuvidoria { get; set; }

        [Required]
        public string descricao { get; set; }

        public bool ativo { get; set; }

        public int? ordem { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<solicitacao> solicitacao { get; set; }
    }
}
