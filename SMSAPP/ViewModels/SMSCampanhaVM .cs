using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SMSAPP.ViewModels
{
    public class SMSCampanhaVM
    {
        [Required]
        public string Sender { get; set; }
        public string Mensagem { get; set; }
        public String Content { get; set; }
        [Required]
        public int idCampanha { get; set; }
        public int idCliente { get; set; }

    }
}