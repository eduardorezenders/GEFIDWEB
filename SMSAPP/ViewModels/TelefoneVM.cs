using EntidadesDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SMSAPP.ViewModels
{
    public class TelefoneVM
    {
        [Key]
        public int idTelefone { get; set; }

        public int? idCliente { get; set; }

        public int? idPessoa { get; set; }

        public int? pais { get; set; }

        public int ddd { get; set; }

        public int numero { get; set; }

        public bool ativo { get; set; }

        public int? idTipoTelefone { get; set; }

        public virtual tipoTelefone tipoTelefone { get; set; }
    }
}