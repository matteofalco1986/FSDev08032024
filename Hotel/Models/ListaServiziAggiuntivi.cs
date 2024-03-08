using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class ListaServiziAggiuntivi
    {
        [Key]
        public int ServizioId { get; set; }

        [Display(Name = "Tipo servizio")]
        public int TipoServizio { get; set; }

        [Display(Name = "Prezzo")]
        public double Prezzo { get; set; }
    }
}