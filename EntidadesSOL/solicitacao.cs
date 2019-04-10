namespace EntidadesSOL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("solicitacao")]
    public partial class solicitacao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public solicitacao()
        {
            providencia = new HashSet<providencia>();
        }

        [Key]
        public int idSolicitacao { get; set; }

        public DateTime dataRegistro { get; set; }

        public string descricao { get; set; }

        [StringLength(8)]
        public string cep { get; set; }

        public int idTipoAssunto { get; set; }

        public int idTipoPrioridade { get; set; }

        public int? idUsuario { get; set; }

        public int idStatusOuvidoria { get; set; }

        public int idPessoa { get; set; }

        public int? idServico { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<providencia> providencia { get; set; }

        public virtual servico servico { get; set; }

        public virtual statusOuvidoria statusOuvidoria { get; set; }

        public virtual tipoAssunto tipoAssunto { get; set; }

        public virtual tipoPrioridade tipoPrioridade { get; set; }
    }
}
