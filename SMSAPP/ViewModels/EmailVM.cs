using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SMSAPP.ViewModels
{
    public class EmailVM
    {
        [Key]
        public int idEmail { get; set; }

        public int? idCliente { get; set; }

        public int? idPessoa { get; set; }

        public string conteudo { get; set; }

        public bool redeSocial { get; set; }
    }
}