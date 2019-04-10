namespace EntidadesDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("acessoPerfil")]
    public partial class acessoPerfil
    {
        [Key]
        public int idPerfil { get; set; }

        public int idGrupo { get; set; }

        public int idModulo { get; set; }

        public bool al { get; set; }

        public bool ap { get; set; }

        public bool cr { get; set; }

        public bool ex { get; set; }

        public bool le { get; set; }

        public int idCliente { get; set; }

        public virtual acessoGrupo acessoGrupo { get; set; }

        public virtual acessoModulo acessoModulo { get; set; }
    }
}
