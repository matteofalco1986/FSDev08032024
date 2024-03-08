using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class TipiPrenotazione
    {
        [Key]
        public int TipoPrenotazioneId { get; set; }

        [Display(Name = "Trattamento")]
        public string TipoPrenotazione { get; set; }
    }
}