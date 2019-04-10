namespace EntidadesDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("emailRedesocial")]
    public partial class emailRedesocial
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idEmail { get; set; }

        public int? idCliente { get; set; }

        public int? idPessoa { get; set; }

        [Required]
        public string conteudo { get; set; }

        public bool redeSocial { get; set; }

        public virtual cliente cliente { get; set; }

        public virtual pessoa pessoa { get; set; }
    }
}
