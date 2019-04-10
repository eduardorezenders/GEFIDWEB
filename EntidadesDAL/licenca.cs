namespace EntidadesDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("licenca")]
    public partial class licenca
    {
        [Key]
        public int idLicenca { get; set; }

        [Display(Name ="Chaves de licença")]
        public string chave { get; set; }

        public string chave1 { get; set; }

        public string chave2 { get; set; }

        public int idCliente { get; set; }

        public virtual cliente cliente { get; set; }
    }
}
