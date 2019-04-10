using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SMSAPP.ViewModels
{
    public class LicencaVM
    {
        [Required]
        public string chave { get; set; }
    }
}