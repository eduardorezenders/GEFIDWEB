namespace EntidadesDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("campanha")]
    public partial class campanha
    {
        [Key]
        public int idCampanha { get; set; }

        public int idCliente { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string nome { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Column(TypeName = "date")]
        [Display(Name = "Data de Criação")]
        public DateTime? dtCriacao { get; set; }

        [Required]
        [Display(Name = "Ativo")]
        public bool ativo { get; set; }
    }
}