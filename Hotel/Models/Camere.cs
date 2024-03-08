using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class Camere
    {
        [Key]
        public int CameraId { get; set; }

        [Display(Name = "Tipo camera")]
        public int TipologiaCameraId { get; set; }

        [Display(Name = "Descrizione camera")]
        [DataType(DataType.MultilineText)]
        public string Descrizione { get; set; }

        public TipologieCamera TipologiaCamera { get; set; }
    }
}