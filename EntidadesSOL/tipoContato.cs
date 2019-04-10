namespace EntidadesSOL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tipoContato")]
    public partial class tipoContato
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tipoContato()
        {
            contato = new HashSet<contato>();
        }

        [Key]
        public int idTipoContato { get; set; }

        [Required]
        [Display(Name ="Descrição")]
        public string nome { get; set; }

        [Display(Name ="Mostrar Solicitação")]
        public bool mostrarSolicitacao { get; set; }

        [Display(Name ="Órgão Público")]
        public bool orgaoPublico { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<contato> contato { get; set; }
    }
}
