namespace EntidadesSOL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("providencia")]
    public partial class providencia
    {
        [Key]
        public int idProvidencia { get; set; }

        public DateTime dataProvidencia { get; set; }

        [Required]
        public string descricaoProvidencia { get; set; }

        public DateTime? dataResposta { get; set; }

        public string descricaoResposta { get; set; }

        public bool sucessoResposta { get; set; }

        public string numeroOficioInterno { get; set; }

        public string anoOficioInterno { get; set; }

        public string nomeEntidadeOficio { get; set; }

        public string numeroOficioReiteirado { get; set; }

        public string anoOficioReiteirado { get; set; }

        public DateTime? dataReiteracao { get; set; }

        public int? idOperadorProvidencia { get; set; }

        public int? idOperadorResposta { get; set; }

        public int idSolicitacao { get; set; }

        public int? idNivelSatisfacaoResposta { get; set; }

        public virtual nivelSatisfacaoResposta nivelSatisfacaoResposta { get; set; }

        public virtual solicitacao solicitacao { get; set; }
    }
}
