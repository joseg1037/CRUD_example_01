using Beispiel_02.ETL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Beispiel_02.Controllers
{
    public class PersonController : Controller
    {
        // GET: Person
        public ActionResult PersonHinzufuegen()
        {
            return View(new Beispiel_02.ETL.Person());
        }
        [HttpPost]
        public ActionResult FormularLaden(Beispiel_02.ETL.Person person, string submit)
        {
            if (submit == "Hinzufügen")
            {
                var antwort = PersonHinzufuegen(person);
                return View("PersonHinzufuegen", antwort);
            }
            else if (submit == "Bearbeiten")
            {
                PersonBearbeiten(person);
                RedirectToAction("PersonenLaden", "Person");
            }
            else if (submit == "Löschen")
            {
                PersonLoeschen(person);
                return RedirectToAction("PersonenLaden", "Person");
            }
            return View("PersonHinzufuegen", person);
        }
        [HttpPost]
        public Beispiel_02.ETL.Person PersonHinzufuegen(Beispiel_02.ETL.Person person)
        {            
            using (var datenbank = new PersonenEntities9())
            {
                Person neuePerson = new Person();
                neuePerson.ID = 0;
                neuePerson.AUSWEIS = person.Ausweis;
                neuePerson.NAME = person.Name;
                neuePerson.AGE = JahreRechnen(person.Startdatum);
                neuePerson.STARTDATUM = person.Startdatum;
                datenbank.Person.Add(neuePerson);
                datenbank.SaveChanges();
            }
            return person;
        }

        
        [HttpPost]
        public void PersonBearbeiten(Beispiel_02.ETL.Person person)
        {
            using (var datenbank = new PersonenEntities9())
            {
                var gefundenePerson = (from personInDatenbank in datenbank.Person
                                       where personInDatenbank.AUSWEIS == person.Ausweis
                                       select personInDatenbank).FirstOrDefault();
                gefundenePerson.NAME = person.Name;
                gefundenePerson.AGE = JahreRechnen(person.Startdatum);
                gefundenePerson.STARTDATUM = person.Startdatum;
                datenbank.SaveChanges();
            }
        }
        [HttpPost]
        public void PersonLoeschen(Beispiel_02.ETL.Person person)
        {
            using (var datenbank = new PersonenEntities9())
            {
                var gefundenePerson = (from personInDatenbank in datenbank.Person
                                       where personInDatenbank.AUSWEIS == person.Ausweis
                                       select personInDatenbank).FirstOrDefault();
                datenbank.Person.Remove(gefundenePerson);
                datenbank.SaveChanges();
            }
        }
        public ActionResult PersonenLaden()
        {
            using (var datenbank = new PersonenEntities9())
            {
                var antwort = datenbank.PERSONEN_LADEN();
                List<Person> personenListe = new List<Person>();
                foreach (var geladenePerson in antwort)
                {
                    personenListe.Add(new Person
                    {
                        AUSWEIS = geladenePerson.AUSWEIS,
                        NAME = geladenePerson.NAME,
                        AGE = geladenePerson.AGE,
                        STARTDATUM = geladenePerson.STARTDATUM                        
                });
                }
                return View("PersonenLaden", personenListe);
            }
        }
        public int JahreRechnen(DateTime startdatum)
        {
            DateTime zeroTime = new DateTime(1, 1, 1);
            var heute = DateTime.Now;
            var datum = startdatum;
            var span = heute - datum;

            int jahre = (zeroTime + span).Year - 1;

            return jahre;
        }
    }
}