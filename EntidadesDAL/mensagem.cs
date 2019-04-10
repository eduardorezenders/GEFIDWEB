namespace EntidadesDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("mensagem")]
    public partial class mensagem
    {
        [Key]
        public int idMensagem { get; set; }

        [Required]
        [Display(Name ="Texto da Mensagem")]
        public string texto { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Data em formato inv�lido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Column(TypeName = "date")]
        [Display(Name ="Data de Cria��o")]
        public DateTime dtCriacao { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Data em formato inv�lido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Column(TypeName = "date")]
        [Display(Name ="Data de Altera��o")]
        public DateTime? dtAltercao { get; set; }

        [Display(Name ="Ativo")]
        public bool ativo { get; set; }

        public int idCliente { get; set; }

        public virtual cliente cliente { get; set; }
    }
}
