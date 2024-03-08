using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Clienti
    {
        [Key]
        public int ClienteId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Nome { get; set; }

        [Display(Name = "Cognome")]
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Cognome { get; set; }

        [Display(Name = "Codice Fiscale")]
        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(16, MinimumLength = 16, ErrorMessage = "Il CF deve essere di 16 caratteri")]
        public string CodiceFiscale { get; set; }

        [Display(Name = "Citta")]
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Citta { get; set; }

        [Display(Name = "Provincia")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "La provincia deve essere di 2 caratteri")]
        [Required(ErrorMessage = "Campo obbligatorio")]
        public string Provincia { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Campo obbligatorio")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Telefono")]
        [Required(ErrorMessage = "Campo obbligatorio")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(10, MinimumLength = 9, ErrorMessage = "Il numero deve essere compreso tra le 9 e 10 cifre, incluso il prefisso di 4 cifre")]
        public string Telefono { get; set; }

        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "Campo obbligatorio")]
        [StringLength(15, MinimumLength = 9, ErrorMessage = "Il numero deve essere minimo di 9 cifre")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }

    }
}