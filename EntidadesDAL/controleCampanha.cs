namespace EntidadesDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("controleCampanha")]

    public partial class controleCampanha
    {
        [Key]
        public int idControleCampanha { get; set; }
        public int idCliente { get; set; }
        public int idContato { get; set; }
        public int idCampanha { get; set; }
        public DateTime? dtEnvio { get; set; }
        public DateTime? dtCadastro { get; set; }
        public bool? status { get; set; }
        public string telefone { get; set; }
    }
}