using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hotel.Models
{
    public class TipologieCamera
    {
        [Key]
        public int TipologiaCameraId { get; set; }

        [Display(Name = "Tipo camera")]
        public string TipologiaCamera { get; set; }
    }
}