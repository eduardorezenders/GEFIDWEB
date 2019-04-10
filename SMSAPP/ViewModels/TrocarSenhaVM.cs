using System.ComponentModel.DataAnnotations;

namespace SMSAPP.ViewModels
{
    public class TrocarSenhaVM
    {
        [Key]
        public int idLogin { get; set; }
        [Required]
        [DataType(DataType.Password)]
        //[StringLength(15, MinimumLength = 6)]
        public string senhaantiga { get; set; }
        [Required]
        [DataType(DataType.Password)]
        //[StringLength(15, MinimumLength = 6)]
        public string senha { get; set; }
        [DataType(DataType.Password)]
        //[StringLength(15, MinimumLength = 6)]
        [Compare("senha", ErrorMessage = "A nove senha e a confirmação da senha são diferentes")]
        public string confirmasenha { get; set; }
    }
}