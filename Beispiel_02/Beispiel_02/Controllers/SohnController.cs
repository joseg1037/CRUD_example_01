using Beispiel_02.ETL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Beispiel_02.Controllers
{
    public class SohnController : Controller
    {
        // GET: Sohn
        public ActionResult SohnHinzufuegen()
        {
            using (var datenbank = new PersonenEntities9())
            {
                var antwort01 = datenbank.PERSONEN_LADEN();
                List<Person> personenListe = new List<Person>();
                foreach (var person in antwort01)
                {
                    personenListe.Add(new Person
                    {
                        ID = person.ID,
                        NAME = person.NAME
                    });
                }
                List<SelectListItem> personenComboListe = new List<SelectListItem>();
                personenComboListe = (from item in personenListe
                                      select new SelectListItem { Value = item.ID.ToString(), Text = item.NAME }).ToList();
                Session["ID_Person_Combo"] = personenComboListe;
                ViewBag.ID_Person_Combo = personenComboListe;
                return View(new Sohn());
            }
        }
        [HttpPost]
        public ActionResult FormularLaden(Sohn Sohn, string submit)
        {
            if (ModelState.IsValid)
            {
                if (submit == "Hinzufügen")
                {
                    var antwort = SohnAddieren(Sohn);
                    return RedirectToAction("SohnHinzufuegen", "Sohn");
                }
                else if (submit == "Bearbeiten")
                {
                    var antwort = SohnBearbeiten(Sohn);
                    RedirectToAction("SoehneLaden", "Sohn");
                }
                else if (submit == "Löschen")
                {
                    SohnLoeschen(Sohn);
                    return RedirectToAction("SoehneLaden", "Sohn");
                }
            }
            return RedirectToAction("SohnHinzufuegen", Sohn);
        }
        [HttpPost]
        public Sohn SohnAddieren(Sohn Sohn)
        {
            using (var datenbank = new PersonenEntities9())
            {
                Sohn neuerSohn = new Sohn();
                neuerSohn.ID = 0;
                neuerSohn.AUSWEIS = Sohn.AUSWEIS;
                neuerSohn.NAME = Sohn.NAME;
                neuerSohn.FK_ID_PERSON = Sohn.FK_ID_PERSON;
                datenbank.Sohn.Add(neuerSohn);
                datenbank.SaveChanges();
            }
            return Sohn;
        }


        [HttpPost]
        public Sohn SohnBearbeiten(Sohn Sohn)
        {
            using (var datenbank = new PersonenEntities9())
            {
                var gefundenerSohn = (from sohnInDatenbank in datenbank.Sohn
                                      where sohnInDatenbank.AUSWEIS == Sohn.AUSWEIS
                                      select sohnInDatenbank).FirstOrDefault();
                gefundenerSohn.NAME = Sohn.NAME;
                gefundenerSohn.FK_ID_PERSON = Sohn.FK_ID_PERSON;
                datenbank.SaveChanges();
            }
            return Sohn;
        }
        [HttpPost]
        public Sohn SohnLoeschen(Sohn Sohn)
        {
            using (var datenbank = new PersonenEntities9())
            {
                var gefundeneSohn = (from SohnInDatenbank in datenbank.Sohn
                                     where SohnInDatenbank.AUSWEIS == Sohn.AUSWEIS
                                     select SohnInDatenbank).FirstOrDefault();
                datenbank.Sohn.Remove(gefundeneSohn);
                datenbank.SaveChanges();
            }
            return Sohn;
        }
        [HttpGet]
        public ActionResult SoehneLaden()
        {
            using (var datenbank = new PersonenEntities9())
            {
                var antwort = datenbank.SoehneLesen();

                return View(antwort.ToList());
            }
        }
        [HttpPost]
        public ActionResult NamenLesen(string sohnenAusweis)
        {

            using (var datenbank = new PersonenEntities9())
            {
                var gefundenerSohn = (from sohn in datenbank.Sohn
                                      where sohn.AUSWEIS == sohnenAusweis
                                      select sohn).FirstOrDefault();
                var gefundenePerson = (from person in datenbank.Person
                                       where person.ID == gefundenerSohn.FK_ID_PERSON
                                       select person).FirstOrDefault();
                long personenID = gefundenePerson.ID;
                string personennamen = gefundenePerson.NAME;
                if (gefundenerSohn == null)
                {
                    return Json("Name vom Sohn nicht gefunden, checken Sie Ausweis", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { sohnenNamen = gefundenerSohn.NAME, personenNamen = personennamen, FK_ID_PERSON = personenID }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}