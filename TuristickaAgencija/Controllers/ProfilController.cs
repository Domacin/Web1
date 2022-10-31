using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using TuristickaAgencija.Models;

namespace TuristickaAgencija.Controllers
{
    public class ProfilController : Controller
    {
        // GET: Profil
        public ActionResult Index()
        {
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            Dictionary<string, Korisnik> trenutni = new Dictionary<string, Korisnik>();
            Korisnik k = (Korisnik)Session["korisnik"];
            trenutni.Add(k.KorisnickoIme, k);

            ViewBag.korisnici = trenutni.Values;
            return View();
        }

        [HttpPost]
        public ActionResult Edit(string Ime="",string Prezime = "",string Lozinka = "",string Email="")
        {
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            

            Korisnik ko = (Korisnik)Session["korisnik"];


            for (int i = 0; i < korisnici.Count; i++)
            {
                if(korisnici[ko.KorisnickoIme].KorisnickoIme == ko.KorisnickoIme)
                {
                    korisnici[ko.KorisnickoIme].Ime = Ime;
                    ko.Ime = Ime;
                    korisnici[ko.KorisnickoIme].Prezime = Prezime;
                    ko.Prezime = Prezime;
                    korisnici[ko.KorisnickoIme].Lozinka = Lozinka;
                    ko.Lozinka = Lozinka;
                    korisnici[ko.KorisnickoIme].Email = Email;
                    ko.Email = Email;
                   
                }
            }
            

            string putanja = HostingEnvironment.MapPath("~/App_Data/korisnici.txt");
            StreamWriter sw = new StreamWriter(putanja);

            foreach (Korisnik k in korisnici.Values)
            {
                sw.WriteLine(k.KorisnickoIme + ";" + k.Ime + ";" + k.Prezime + ";" + k.Lozinka + ";" + k.Pol + ";" + k.Uloga + ";" + k.Email + ";" + k.Datum.ToString("dd/MM/yyyy")+";"+k.Blokiran);

            }
            sw.Close();

            

            ViewBag.korisnici = korisnici.Values;
            HttpContext.Application["korisnici"] = korisnici;
            return RedirectToAction("Index");
        }
    }
}