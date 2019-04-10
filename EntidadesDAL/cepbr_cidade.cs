namespace EntidadesDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class cepbr_cidade
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cepbr_cidade()
        {
            cepbr_bairro = new HashSet<cepbr_bairro>();
            cepbr_endereco = new HashSet<cepbr_endereco>();
            logradouro = new HashSet<logradouro>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id_cidade { get; set; }

        public string cidade { get; set; }

        [StringLength(2)]
        public string uf { get; set; }

        public double? cod_ibge { get; set; }

        [StringLength(255)]
        public string area { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cepbr_bairro> cepbr_bairro { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cepbr_endereco> cepbr_endereco { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<logradouro> logradouro { get; set; }
    }
}
