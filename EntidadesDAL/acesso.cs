namespace EntidadesDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("acesso")]
    public partial class acesso
    {
        [Key]
        public int idLogin { get; set; }

        public int idGrupo { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string senha { get; set; }

        [Required]
        [StringLength(11)]
        public string cpf { get; set; }

        public bool ativo { get; set; }

        [Required]
        [StringLength(30)]
        public string nome { get; set; }

        [StringLength(30)]
        public string sobrenome { get; set; }

        public int idCliente { get; set; }

        public virtual acessoGrupo acessoGrupo { get; set; }

        public virtual cliente cliente { get; set; }
    }
}
