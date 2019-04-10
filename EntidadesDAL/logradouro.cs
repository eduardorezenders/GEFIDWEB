namespace EntidadesDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("logradouro")]
    public partial class logradouro
    {
        [Key]
        public int idLogradouro { get; set; }

        public int? idCliente { get; set; }

        public int? idPessoa { get; set; }

        public int? idTipoEndereco { get; set; }

        [StringLength(3)]
        public string idTipoLogradouro { get; set; }

        [StringLength(10)]
        public string cep { get; set; }

        public string endereco { get; set; }

        public string numero { get; set; }

        public string complemento { get; set; }

        [StringLength(2)]
        public string uf { get; set; }

        public int? idBairro { get; set; }

        public int? idCidade { get; set; }

        public bool correspondencia { get; set; }

        public virtual cepbr_bairro cepbr_bairro { get; set; }

        public virtual cepbr_cidade cepbr_cidade { get; set; }

        public virtual cepbr_estado cepbr_estado { get; set; }

        public virtual cliente cliente { get; set; }

        public virtual pessoa pessoa { get; set; }

        public virtual tipoEndereco tipoEndereco { get; set; }

        public virtual tipoLogradouro tipoLogradouro { get; set; }
    }
}
