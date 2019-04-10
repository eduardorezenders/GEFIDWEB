using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMSAPP.ViewModels
{
    public class PesquisaDTVM
    {
        [Key]
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Column(TypeName = "date")]
        public DateTime dtInicial { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Column(TypeName = "date")]
        [Required]
        public DateTime dtFinal { get; set; }

        public String tipo { get; set; }
        public String numero { get; set; }
        public String usuario { get; set; }
    }
}