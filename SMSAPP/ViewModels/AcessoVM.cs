using System.ComponentModel.DataAnnotations;

namespace SMSAPP.ViewModels
{
    public class AcessoVM
    {
        [Required]
        [DataType(DataType.Password)]
        //[StringLength(15, MinimumLength = 6)]
        public string senha { get; set; }

        [Required]
        [StringLength(11)]
        public string cpf { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Cliente")]
        public string login { get; set; }
    }
}