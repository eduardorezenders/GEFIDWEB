namespace EntidadesDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("tipoLogradouro")]
    public partial class tipoLogradouro
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tipoLogradouro()
        {
            logradouro = new HashSet<logradouro>();
        }

        [Key]
        [StringLength(3)]
        public string idTipoLogradouro { get; set; }

        [Required]
        public string Descricao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<logradouro> logradouro { get; set; }
    }
}
