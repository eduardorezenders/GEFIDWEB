namespace EntidadesDAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("controle")]
    public partial class controle
    {
        [Key]
        public int idControle { get; set; }

        public int idCliente { get; set; }

        [Display(Name ="1° SMS")]
        [MaxLength(5,ErrorMessage ="Formato inválido, (correto: HH:mm)")]
        [HoraBrasil(ErrorMessage = "Hora inválida", HoraRequerida = false, Hora24 = true)]
        public string priSms { get; set; }

        [Display(Name = "2° SMS")]
        [MaxLength(5, ErrorMessage = "Formato inválido, (correto: HH:mm)")]
        [HoraBrasil(ErrorMessage = "Hora inválida", HoraRequerida = false, Hora24 = true)]
        public string segSms { get; set; }

        [Display(Name = "3° SMS")]
        [MaxLength(5, ErrorMessage = "Formato inválido, (correto: HH:mm)")]
        [HoraBrasil(ErrorMessage = "Hora inválida", HoraRequerida = false, Hora24 = true)]
        public string terSms { get; set; }

        [Display(Name = "4° SMS")]
        [MaxLength(5, ErrorMessage = "Formato inválido, (correto: HH:mm)")]
        [HoraBrasil(ErrorMessage = "Hora inválida", HoraRequerida = false, Hora24 = true)]
        public string quaSms { get; set; }

        [Display(Name = "5° SMS")]
        [MaxLength(5, ErrorMessage = "Formato inválido, (correto: HH:mm)")]
        [HoraBrasil(ErrorMessage = "Hora inválida", HoraRequerida = false, Hora24 = true)]
        public string quiSms { get; set; }

        [Display(Name = "A Cada")]
        [MaxLength(5, ErrorMessage = "Formato inválido, (correto: HH:mm)")]
        [HoraBrasil(ErrorMessage = "Hora inválida", HoraRequerida = false, Hora24 = true)]
        public string aCada { get; set; }

        [Display(Name = "Automático")]
        public bool envioAuto { get; set; }

        [Display(Name = "Facebook")]
        public string facebook { get; set; }

        public string sfacebook { get; set; }

        [Display(Name = "Whatsapp")]
        public string whatsapp { get; set; }

        public string swhatsapp { get; set; }

        public virtual cliente cliente { get; set; }
    }
}
