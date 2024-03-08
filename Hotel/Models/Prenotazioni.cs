using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Prenotazioni
    {
        [Key]
        public int PrenotazioneId { get; set; }

        [Display(Name = "Id cliente")]
        [Required(ErrorMessage = "Campo obbligatorio")]
        public int ClienteId { get; set; }

        [Display(Name = "Numero camera")]
        [Required(ErrorMessage = "Campo obbligatorio")]
        public int CameraId { get; set; }

        [Display(Name = "Trattamento")]
        [Required(ErrorMessage = "Campo obbligatorio")]
        public int TipoPrenotazioneId { get; set; }

        [Display(Name = "Data")]
        [Required(ErrorMessage = "Campo obbligatorio")]
        [DataType(DataType.DateTime)]
        public DateTime Data { get; set; }

        [Display(Name = "Anno")]
        [Required(ErrorMessage = "Campo obbligatorio")]
        public int Anno { get; set; } = DateTime.Now.Year;

        [Display(Name = "Check-in")]
        [Required(ErrorMessage = "Campo obbligatorio")]
        [DataType(DataType.DateTime)]
        public DateTime InizioSoggiorno { get; set; }

        [Display(Name = "Check-out previsto")]
        [Required(ErrorMessage = "Campo obbligatorio")]
        [DataType(DataType.DateTime)]
        public DateTime FineSoggiorno { get; set; }

        [Display(Name = "Caparra")]
        [Required(ErrorMessage = "Campo obbligatorio")]
        public double Caparra { get; set; }

        [Display(Name = "Tariffa")]
        [Required(ErrorMessage = "Campo obbligatorio")]
        public double Tariffa { get; set; }

        public Clienti Cliente { get; set; }
        public Camere Camera { get; set; }
        public TipiPrenotazione TipoPrenotazione { get; set; }
    }
}