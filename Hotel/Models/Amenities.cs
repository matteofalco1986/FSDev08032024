using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Amenities
    {
        [Key]
        public int AmenityId { get; set; }

        [Display(Name = "Numero prenotazione")]
        [Required(ErrorMessage = "Campo obbligatorio")]
        public int PrenotazioneId { get; set; }

        [Display(Name = "Tipo servizio")]
        [Required(ErrorMessage = "Campo obbligatorio")]
        public int ServizioId { get; set; }

        [Display(Name = "Data")]
        [Required(ErrorMessage = "Campo obbligatorio")]
        [DataType(DataType.DateTime)]
        public DateTime Data { get; set; }

        [Display(Name = "Quantita")]
        [Required(ErrorMessage = "Campo obbligatorio")]
        [Range(0, 20, ErrorMessage = "La quantita deve essere tra 0 e 20")]
        public int Quantita { get; set; }
        
        public ListaServiziAggiuntivi ServizioAggiuntivo { get; set; }
        public Prenotazioni Prenotazione { get; set; }
    }
}