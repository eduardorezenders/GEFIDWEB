namespace EntidadesDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("acessoModulo")]
    public partial class acessoModulo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public acessoModulo()
        {
            acessoPerfil = new HashSet<acessoPerfil>();
        }

        [Key]
        public int idModulo { get; set; }

        [Required]
        [StringLength(25)]
        public string descricao { get; set; }

        public bool ativo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<acessoPerfil> acessoPerfil { get; set; }
    }
}
