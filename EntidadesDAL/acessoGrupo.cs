namespace EntidadesDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("acessoGrupo")]
    public partial class acessoGrupo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public acessoGrupo()
        {
            acesso = new HashSet<acesso>();
            acessoPerfil = new HashSet<acessoPerfil>();
        }

        [Key]
        public int IdGrupo { get; set; }

        [Required]
        public string nome { get; set; }

        public int idCliente { get; set; }

        public bool deletar { get; set; }

        public bool ativo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<acesso> acesso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<acessoPerfil> acessoPerfil { get; set; }
    }
}
