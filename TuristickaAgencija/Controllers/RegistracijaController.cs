using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuristickaAgencija.Models;

namespace TuristickaAgencija.Controllers
{
    public class RegistracijaController : Controller
    {
        // GET: Registracija
        public ActionResult Index()
        {
            PocetnaController.prviprikaz = 0;
            return View();
        }

        [HttpPost]
        public ActionResult DodajKorisnika(Korisnik k)
        {
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
    
            if(string.IsNullOrEmpty(k.KorisnickoIme))
            {
                ViewBag.PraznoPolje = "Morate da unesete korisnicko ime jer je to obavezno polje!";
                return View("Index");
            }

            if(string.IsNullOrEmpty(k.Lozinka))
            {
                ViewBag.PraznoPolje = "Morate da unesete lozinku jer je to obavezno polje!";
                return View("Index");
            }

            if (string.IsNullOrEmpty(k.Ime))
            {
                ViewBag.PraznoPolje = "Morate da unesete ime jer je to obavezno polje!";
                return View("Index");
            }

            if(string.IsNullOrEmpty(k.Prezime))
            {
                ViewBag.PraznoPolje = "Morate da unesete prezime jer je to obavezno polje!";
                return View("Index");
            }

            if(k.Pol != POL.MUSKO && k.Pol != POL.ZENSKO)
            {
                ViewBag.PraznoPolje = "Morate da izaberete pol jer je to obavezno polje!";
                return View("Index");
            }

            if(k.Email == null)
            {
                ViewBag.PraznoPolje = "Morate da unesete e-mail jer je to obavezno polje!";
                return View("Index");
            }

             

            if (korisnici.Keys.Contains(k.KorisnickoIme))
            {
                ViewBag.PraznoPolje = $"Korisnicko ime {k.KorisnickoIme} je zauzeto!";
                return View("Index");
            }
            k.Uloga = ULOGA.TURISTA;
            k.Blokiran = "ne";
            korisnici.Add(k.KorisnickoIme, k);
            Baza.DodajKorisnikaUBazu(k);

            return RedirectToAction("Index", "LogIn");
        }
    }
}