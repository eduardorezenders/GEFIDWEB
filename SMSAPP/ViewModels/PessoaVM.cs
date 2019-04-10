using EntidadesDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SMSAPP.ViewModels
{
    public class PessoaVM
    {
        [Key]
        public int idPessoa { get; set; }

        public int idCliente { get; set; }

        [Required]
        [Display(Name = "Nome Completo")]
        public string nomeCompleto { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Column(TypeName = "date")]
        [Display(Name = "Data de Nascimento")]
        public DateTime? dataNascimento { get; set; }

        [Display(Name = "Ativo")]
        public bool ativo { get; set; }

        [StringLength(1)]
        [Display(Name = "Genêro")]
        public string idGenero { get; set; }

        [StringLength(11)]
        [Display(Name = "CPF")]
        public string cpf { get; set; }

        public string arquivo { get; set; }

        [Display(Name = "Facebook")]
        public string facebook { get; set; }

        [Display(Name = "Whatsapp")]
        public string whatsapp { get; set; }

        public TelefoneVM telefone {get; set;}
        public EmailVM email { get; set; }
        public LogradouroVM logradouro { get; set; }
    }
}