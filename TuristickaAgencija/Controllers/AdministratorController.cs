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
    public class AdministratorController : Controller
    {
        
        static int brojacIme = 0;
        static int brojacPrezime = 0;
        static int brojacUloga = 0;
        // GET: Administrator
        public ActionResult Index()
        {
            PocetnaController.prviprikaz = 0;
            return View();
        }
        public ActionResult BlokirajKorisnika(string ime)
        {
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];


            for (int i = 0; i < korisnici.Count; i++)
            {
                var item = korisnici.ElementAt(i);
                var kljuc = item.Key;
                if (item.Value.KorisnickoIme == ime)
                    item.Value.Blokiran = "da";
            }


            string putanja = HostingEnvironment.MapPath("~/App_Data/korisnici.txt");
            StreamWriter sw = new StreamWriter(putanja);

            foreach (Korisnik k in korisnici.Values)
            {
                sw.WriteLine(k.KorisnickoIme + ";" + k.Ime + ";" + k.Prezime + ";" + k.Lozinka + ";" + k.Pol + ";" + k.Uloga + ";" + k.Email + ";" + k.Datum.ToString("dd/MM/yyyy") + ";" + k.Blokiran);

            }
            sw.Close();

            ViewBag.smjestaji = sm;
            ViewBag.smjestajnejedinice = smj;
            ViewBag.aranzmani = aranzmani;
            ViewBag.korisnici = korisnici.Values;
            ViewBag.rezervacije = rezervacije;
            return View("SpisakKorisnika");

        }

        public ActionResult OdblokirajKorisnika(string ime)
        {
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];


            for (int i = 0; i < korisnici.Count; i++)
            {
                var item = korisnici.ElementAt(i);
                var kljuc = item.Key;
                if (item.Value.KorisnickoIme == ime)
                    item.Value.Blokiran = "ne";
            }


            string putanja = HostingEnvironment.MapPath("~/App_Data/korisnici.txt");
            StreamWriter sw = new StreamWriter(putanja);

            foreach (Korisnik k in korisnici.Values)
            {
                sw.WriteLine(k.KorisnickoIme + ";" + k.Ime + ";" + k.Prezime + ";" + k.Lozinka + ";" + k.Pol + ";" + k.Uloga + ";" + k.Email + ";" + k.Datum.ToString("dd/MM/yyyy") + ";" + k.Blokiran);

            }
            sw.Close();

            ViewBag.smjestaji = sm;
            ViewBag.smjestajnejedinice = smj;
            ViewBag.aranzmani = aranzmani;
            ViewBag.korisnici = korisnici.Values;
            ViewBag.rezervacije = rezervacije;
            return View("SpisakKorisnika");
        }
        public ActionResult SpisakKorisnika()
        {
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];

            foreach (Korisnik k in korisnici.Values)
            {
                k.BrojOtkazanih = 0;
                for (int i = 0; i < rezervacije.Count; i++)
                {
                    if(k.KorisnickoIme == rezervacije[i].Turista.KorisnickoIme && rezervacije[i].Status == "otkazana")
                    {
                        k.BrojOtkazanih++;
                    }
                }
            }

            ViewBag.smjestaji = sm;
            ViewBag.smjestajnejedinice = smj;
            ViewBag.aranzmani = aranzmani;
            ViewBag.korisnici = korisnici.Values;
            ViewBag.rezervacije = rezervacije;
            return View("SpisakKorisnika");
        }

        [HttpPost]
        public ActionResult sortirajPoImenu()
        {
           
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            List<Korisnik> lista = new List<Korisnik>();
            Dictionary<string, Korisnik> sortirani = new Dictionary<string, Korisnik>();

            foreach (Korisnik item in korisnici.Values)
            {
                lista.Add(item);
            }
             

            if(brojacIme %2 == 0)
            {
                try
                {
                    foreach (Korisnik k in lista)
                    {
                        lista.Sort((x, y) => x.Ime.CompareTo(y.Ime));
                    }
                }
                catch (Exception )
                {

                    ViewBag.GreskaSort = "Doslo je do greske, ne mogu da sortiram!";
                }
            }
            else
            {
                try
                {
                    foreach (Korisnik k in lista)
                    {
                        lista.Sort((y, x) => x.Ime.CompareTo(y.Ime));
                    }
                }
                catch (Exception)
                {

                    ViewBag.GreskaSort = "Doslo je do greske, ne mogu da sortiram!";
                }
            }

            foreach (Korisnik item in lista)
            {
                sortirani.Add(item.KorisnickoIme, item);
            }

            brojacIme++;
            ViewBag.korisnici = sortirani.Values;
            return View("SpisakKorisnika");
        }

        [HttpPost]
        public ActionResult sortirajPoPrezimenu()
        {
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            List<Korisnik> lista = new List<Korisnik>();
            Dictionary<string, Korisnik> sortirani = new Dictionary<string, Korisnik>();

            foreach (Korisnik item in korisnici.Values)
            {
                lista.Add(item);
            }
            if (brojacPrezime % 2 == 0)
            {
                try
                {
                    foreach (Korisnik k in lista)
                    {
                        lista.Sort((x, y) => x.Prezime.CompareTo(y.Prezime));
                    }
                }
                catch (Exception)
                {

                    ViewBag.GreskaSort = "Doslo je do greske, ne mogu da sortiram!";
                }
            }
            else
            {
                try
                {
                    foreach (Korisnik k in lista)
                    {
                        lista.Sort((y, x) => x.Prezime.CompareTo(y.Prezime));
                    }
                }
                catch (Exception)
                {

                    ViewBag.GreskaSort = "Doslo je do greske, ne mogu da sortiram!";
                }
            }

            foreach (Korisnik item in lista)
            {
                sortirani.Add(item.KorisnickoIme, item);
            }

            brojacPrezime++;
            ViewBag.korisnici = sortirani.Values;
            return View("SpisakKorisnika");
        }

        [HttpPost]
        public ActionResult sortirajPoUlozi()
        {
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];
            List<Korisnik> lista = new List<Korisnik>();
            Dictionary<string, Korisnik> sortirani = new Dictionary<string, Korisnik>();

            foreach (Korisnik item in korisnici.Values)
            {
                lista.Add(item);
            }
            if (brojacUloga % 2 == 0)
            {
                try
                {
                    foreach (Korisnik k in lista)
                    {
                        lista.Sort((x, y) => x.Uloga.CompareTo(y.Uloga));
                    }
                }
                catch (Exception)
                {

                    ViewBag.GreskaSort = "Doslo je do greske, ne mogu da sortiram!";
                }
            }
            else
            {
                try
                {
                    foreach (Korisnik k in lista)
                    {
                        lista.Sort((y, x) => x.Uloga.CompareTo(y.Uloga));
                    }
                }
                catch (Exception)
                {

                    ViewBag.GreskaSort = "Doslo je do greske, ne mogu da sortiram!";
                }
            }

            foreach (Korisnik item in lista)
            {
                sortirani.Add(item.KorisnickoIme, item);
            }

            brojacUloga++;
            ViewBag.korisnici = sortirani.Values;
            return View("SpisakKorisnika");
        }

        [HttpPost]
        public ActionResult DodajMenadzera(Korisnik k)
        {
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];

            if (k.KorisnickoIme == null)
            {
                ViewBag.PraznoPolje = "Morate da unesete korisnicko ime jer je to obavezno polje!";
                return View("Index");
            }

            if (k.Lozinka == null)
            {
                ViewBag.PraznoPolje = "Morate da unesete lozinku jer je to obavezno polje!";
                return View("Index");
            }

            if (k.Ime == null)
            {
                ViewBag.PraznoPolje = "Morate da unesete ime jer je to obavezno polje!";
                return View("Index");
            }

            if (k.Prezime == null)
            {
                ViewBag.PraznoPolje = "Morate da unesete prezime jer je to obavezno polje!";
                return View("Index");
            }

            if (k.Pol != POL.MUSKO && k.Pol != POL.ZENSKO)
            {
                ViewBag.PraznoPolje = "Morate da izaberete pol jer je to obavezno polje!";
                return View("Index");
            }

            if (k.Email == null)
            {
                ViewBag.PraznoPolje = "Morate da unesete e-mail jer je to obavezno polje!";
                return View("Index");
            }

            //DODAJ PROVJERU KAD SE NE IZABERE DATUM

            if (korisnici.Keys.Contains(k.KorisnickoIme))
            {
                ViewBag.PraznoPolje = $"Korisnicko ime {k.KorisnickoIme} je zauzeto!";
                return View("Index");
            }
            k.Uloga = ULOGA.MENADZER;
            korisnici.Add(k.KorisnickoIme, k);
            Baza.DodajKorisnikaUBazu(k);

            return View("Index");

        }

        [HttpPost]
        public ActionResult PretragaKorisnika(string Ime = "", string Prezime = "", string Uloga = "")
        {
            Dictionary<string, Korisnik> korisnici = (Dictionary<string, Korisnik>)HttpContext.Application["korisnici"];


            var korisniciQueryable = korisnici.Values.AsQueryable();

            if (!string.IsNullOrEmpty(Ime))
            {
                korisniciQueryable = korisniciQueryable.Where(x => x.Ime.ToLower().Contains(Ime.ToLower()));
            }

            if (!string.IsNullOrEmpty(Prezime))
            {
                korisniciQueryable = korisniciQueryable.Where(x => x.Prezime.ToLower().Contains(Prezime.ToLower()));
            }

            if (!string.IsNullOrEmpty(Uloga))
            {
                korisniciQueryable = korisniciQueryable.Where(x => x.Uloga.ToString().Equals(Uloga));
            }
            ViewBag.korisnici = korisniciQueryable.ToList();
            return View("SpisakKorisnika");
        }
    }
}