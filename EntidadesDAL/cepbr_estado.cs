namespace EntidadesDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class cepbr_estado
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cepbr_estado()
        {
            logradouro = new HashSet<logradouro>();
        }

        [Key]
        [StringLength(2)]
        public string uf { get; set; }

        [Required]
        public string estado { get; set; }

        public double? cod_ibge { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<logradouro> logradouro { get; set; }
    }
}
