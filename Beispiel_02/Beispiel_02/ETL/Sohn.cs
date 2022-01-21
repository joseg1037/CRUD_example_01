using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Beispiel_02.ETL
{
    public class Sohn
    {
        [Display(Name = "Ausweis")]
        [Required(ErrorMessage = "Ausweis eingeben")]
        public string Ausweis { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }        
    }
}