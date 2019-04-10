namespace EntidadesDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class cepbr_endereco
    {
        [Key]
        [StringLength(10)]
        public string cep { get; set; }

        public string logradouro { get; set; }

        [StringLength(255)]
        public string tipo_logradouro { get; set; }

        [StringLength(255)]
        public string complemento { get; set; }

        [StringLength(255)]
        public string local { get; set; }

        public int? id_cidade { get; set; }

        public int? id_bairro { get; set; }

        public virtual cepbr_cidade cepbr_cidade { get; set; }
    }
}
