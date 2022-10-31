using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuristickaAgencija.Models;

namespace TuristickaAgencija.Controllers
{
    public class LogInController : Controller
    {
        // GET: LogIn
        public ActionResult Index()
        {
            PocetnaController.prviprikaz = 0;
            return View();
        }

        public ActionResult IzlogujSe()
        {
            Session["korisnik"] = null;
            return View("Index");
        }

        [HttpPost]
        public ActionResult UlogujSe(string korisnickoime,string lozinka)
        {
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            Korisnik k = new Korisnik();

            if(korisnickoime == null)
            {
                ViewBag.Greska = "Unesite korisnicko ime!";
                return View("Index");
            }

            if(korisnici[korisnickoime].Blokiran == "da")
            {
                ViewBag.Greska = "Korisnik je blokiran i ne moze se ulogovati!";
                return View("Index");
            }

            if (korisnici.ContainsKey(korisnickoime))
            {
                if (korisnici[korisnickoime].Lozinka.Equals(lozinka))
                {
                    k.KorisnickoIme = korisnickoime;
                    k.Lozinka = korisnici[korisnickoime].Lozinka;
                    k.Ime = korisnici[korisnickoime].Ime;
                    k.Prezime = korisnici[korisnickoime].Prezime;
                    k.Pol = korisnici[korisnickoime].Pol;
                    k.Uloga = korisnici[korisnickoime].Uloga;
                    k.Email = korisnici[korisnickoime].Email;
                    k.Datum = korisnici[korisnickoime].Datum;
                    Session["korisnik"] = k;
                   
                   return RedirectToAction("Index", "Pocetna");
                     
                }
                else
                {
                    ViewBag.Greska = "Pogresna lozinka!";
                    return View("Index");
                }
            }
            else
            {
                ViewBag.Greska = "Nepostojece korisnicko ime!";
                return View("Index");
            }
            
        }
    }
}