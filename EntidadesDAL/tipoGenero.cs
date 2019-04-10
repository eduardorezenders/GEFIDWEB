namespace EntidadesDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tipoGenero")]
    public partial class tipoGenero
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tipoGenero()
        {
            pessoa = new HashSet<pessoa>();
        }

        [Key]
        [StringLength(1)]
        [Display(Name = "Id.Genêro")]
        public string idGenero { get; set; }

        [Required]
        [Display(Name = "Genêro")]
        public string descricao { get; set; }

        [Display(Name = "Ativo")]
        public bool ativo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<pessoa> pessoa { get; set; }
    }
}
