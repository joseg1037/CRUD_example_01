using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Beispiel_02.ETL
{
    public class Person
    {
        [Display(Name = "Ausweis")]
        [Required(ErrorMessage = "Ausweis eingeben")]
        public string Ausweis { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Alter")]
        [Range(0, 100, ErrorMessage ="Gültige Zahl wählen")]
        public int Age { get; set; }
        [Display(Name = "Startdatum")]        
        public DateTime Startdatum { get; set; }
    }
}