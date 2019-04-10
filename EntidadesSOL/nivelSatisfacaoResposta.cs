namespace EntidadesSOL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("nivelSatisfacaoResposta")]
    public partial class nivelSatisfacaoResposta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public nivelSatisfacaoResposta()
        {
            providencia = new HashSet<providencia>();
        }

        [Key]
        public int idNivelSatisfacaoResposta { get; set; }

        [Required]
        public string descricao { get; set; }

        public bool ativo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<providencia> providencia { get; set; }
    }
}
