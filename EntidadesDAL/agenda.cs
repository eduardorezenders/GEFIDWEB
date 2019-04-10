namespace EntidadesDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class agenda
    {
        [Key]
        public int idAgenda { get; set; }

        public int idPessoa { get; set; }

        [Column(TypeName = "date")]
        public DateTime dtAgenda { get; set; }

        public TimeSpan hrAgenda { get; set; }

        public bool ativo { get; set; }

        public virtual pessoa pessoa { get; set; }
    }
}
