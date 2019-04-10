namespace EntidadesDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("actionsLog")]
    public partial class actionsLog
    {
        [Key]
        public string ActionsLogId { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string Ip { get; set; }

        public DateTime DateAndTime { get; set; }

        public string Usuario { get; set; }

        [StringLength(11)]
        public string cpf { get; set; }

        public int idCliente { get; set; }
    }
}
