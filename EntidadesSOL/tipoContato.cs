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
        [Display(Name ="Descri��o")]
        public string nome { get; set; }

        [Display(Name ="Mostrar Solicita��o")]
        public bool mostrarSolicitacao { get; set; }

        [Display(Name ="�rg�o P�blico")]
        public bool orgaoPublico { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<contato> contato { get; set; }
    }
}
