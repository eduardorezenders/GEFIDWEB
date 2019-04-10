using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SMSAPP.ViewModels
{
    public class ImportacaoVM
    {
        [Display(Name ="Arquivo")]
        public string arquivo { get; set; }
    }
}