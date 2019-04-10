namespace EntidadesDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class cepbr_bairro
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cepbr_bairro()
        {
            logradouro = new HashSet<logradouro>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_bairro { get; set; }

        public string bairro { get; set; }

        public int? id_cidade { get; set; }

        public virtual cepbr_cidade cepbr_cidade { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<logradouro> logradouro { get; set; }
    }
}
