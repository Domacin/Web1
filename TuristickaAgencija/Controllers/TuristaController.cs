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
    public class TuristaController : Controller
    {
        static int brojacNaziv = 0;
        static int brojacdatumPocetak = 0;
        static int brojacdatumKraj = 0;
        // GET: Turista
        public ActionResult Index()
        {
            PocetnaController.prviprikaz = 0;
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
           
          

            for (int i = 0; i < aranzmani.Count; i++)
            {
                for (int j = 0; j < rezervacije.Count; j++)
                {
                   if (rezervacije[j].AranzmanZaKojiSeVrsiRezervacija.IdAranzmana == aranzmani[i].IdAranzmana)
                      rezervacije[j].AranzmanZaKojiSeVrsiRezervacija = aranzmani[i];

                }
                  
            }

            for (int i = 0; i < smj.Count; i++)
            {
                for (int j = 0; j < rezervacije.Count; j++)
                {
                    if (rezervacije[j].IzabranaSmjestajnaJedinica.Id == smj[i].Id)
                        rezervacije[j].IzabranaSmjestajnaJedinica = smj[i];
                }
            }

           
            ViewBag.rezervacije = rezervacije;
            ViewBag.smjestaji = sm;
            ViewBag.smjestajneJedinice = smj;
            ViewBag.aranzmani = aranzmani;
            return View("Rezervisi");
        }

        public string IdentifikatorRezervacije()
        {
            var karakteri = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var identifikator = new char[15];
            var random = new Random();

            for(int i = 0; i < identifikator.Length; i++)
            {
                identifikator[i] = karakteri[random.Next(karakteri.Length)];
            }

            return new String(identifikator);
        }

      

      
        public ActionResult Rezervisi(int id)
        {
            Korisnik k = (Korisnik)Session["korisnik"];

            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];

            for (int i = 0; i < aranzmani.Count; i++)
            {
                for (int j = 0; j < rezervacije.Count; j++)
                {
                    if (rezervacije[j].AranzmanZaKojiSeVrsiRezervacija.IdAranzmana == aranzmani[i].IdAranzmana)
                          rezervacije[j].AranzmanZaKojiSeVrsiRezervacija = aranzmani[i];

                }

            }

            for (int i = 0; i < smj.Count; i++)
            {
                for (int j = 0; j < rezervacije.Count; j++)
                {
                    if (rezervacije[j].IzabranaSmjestajnaJedinica.Id == smj[i].Id)
                          rezervacije[j].IzabranaSmjestajnaJedinica = smj[i];
                }
            }


            ViewBag.smjestaji = sm;
            ViewBag.SmjestajneJedinice = smj;
           
            Rezervacija r = new Rezervacija();
            if (k == null)
            {
                return RedirectToAction("Index", "LogIn");
            }

            if(k.Uloga == ULOGA.ADMINISTRATOR)
            {
                return RedirectToAction("Index", "Pocetna");
            }

            if(k.Uloga == ULOGA.MENADZER)
            {
                return RedirectToAction("Index", "Pocetna");
            }

            if(k.Uloga == ULOGA.TURISTA)
            {
                for (int i = 0; i < aranzmani.Count; i++)
                {
                    for (int j = 0; j < sm.Count; j++)
                    {
                        for (int o = 0; o < smj.Count; o++)
                        {
                            if(smj[o].Id ==id && smj[o].PripadaSmjestaju == sm[j].IdSmjestaja && aranzmani[i].IdSmjestaja == sm[j].IdSmjestaja)
                            {
                                    r.Identifikator = IdentifikatorRezervacije();
                                    r.AranzmanZaKojiSeVrsiRezervacija = aranzmani[i];
                                    r.Status = "aktivna";
                                    r.Turista = k;
                                    smj[o].Zauzeto = "da";
                                    r.IzabranaSmjestajnaJedinica = smj[o];
                                    Baza.DodajRezervacijuUBazu(r);
                               
                                
                        }
                    }
                }
                
                }
            }


            string putanja = HostingEnvironment.MapPath("~/App_Data/smjestajnejedinice.txt");
            StreamWriter sw = new StreamWriter(putanja);

            foreach (SmjestajnaJedinica sj in smj)
            {
                sw.WriteLine(sj.PripadaSmjestaju + ";" + sj.DozvoljeniLjubimci + ";" + sj.DozvoljenBrojGostiju + ";" + sj.CenaSmestajneJedinice + ";" + sj.Id + ";" + sj.Zauzeto);
            }
            sw.Close();
            rezervacije = Baza.UcitajRezervacije("~/App_Data/rezervacije.txt");
            ViewBag.rezervacije = rezervacije;
            ViewBag.aranzmani = aranzmani;
            HttpContext.Application["rezervacije"] = rezervacije;
            return RedirectToAction("Index");
        }

        public ActionResult OtkaziRezervaciju(string idRezervacije)
        {
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            Korisnik k = (Korisnik)Session["korisnik"];

            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];

            for (int i = 0; i < rezervacije.Count; i++)
            {
                if(rezervacije[i].Identifikator == idRezervacije && rezervacije[i].Status == "aktivna")
                {
                    rezervacije[i].Status = "otkazana";
                    break;
                }
            }


            for (int j = 0; j < rezervacije.Count; j++)
            {
                for (int i = 0; i < smj.Count; i++)
                {
                    if (rezervacije[j].IzabranaSmjestajnaJedinica.Id == smj[i].Id && rezervacije[j].Identifikator == idRezervacije)
                    {
                        smj[i].Zauzeto = "ne";
                        break;
                    }
                        
                }
            }


            string putanja = HostingEnvironment.MapPath("~/App_Data/rezervacije.txt");
            StreamWriter sw = new StreamWriter(putanja);

            foreach (Rezervacija r in rezervacije)
            {
                sw.WriteLine(r.Identifikator + ";" + r.Turista.KorisnickoIme + ";" + r.AranzmanZaKojiSeVrsiRezervacija.IdAranzmana + ";" + r.IzabranaSmjestajnaJedinica.Id + ";" + r.Status);
            }
            sw.Close();

             putanja = HostingEnvironment.MapPath("~/App_Data/smjestajnejedinice.txt");
             sw = new StreamWriter(putanja);

            foreach (SmjestajnaJedinica sj in smj)
            {
                sw.WriteLine(sj.PripadaSmjestaju + ";" + sj.DozvoljeniLjubimci + ";" + sj.DozvoljenBrojGostiju + ";" + sj.CenaSmestajneJedinice + ";" + sj.Id + ";" + sj.Zauzeto);
            }

            sw.Close();

            ViewBag.smjestaji = sm;
            ViewBag.SmjestajneJedinice = smj;
            ViewBag.rezervacije = rezervacije;
            ViewBag.aranzmani = aranzmani;
            HttpContext.Application["rezervacije"] = rezervacije;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult sortirajPoDatumPocetka()
        {
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            ViewBag.smjestaji = sm;
            ViewBag.SJedinice = smj;


            if (brojacdatumPocetak % 2 == 0)
            {
                try
                {
                    foreach (Aranzman a in aranzmani)
                    {
                        aranzmani.Sort((x, y) => x.DatumPocetkaPutovanja.CompareTo(y.DatumPocetkaPutovanja));
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                try
                {

                    foreach (Aranzman a in aranzmani)
                    {
                        aranzmani.Sort((y, x) => x.DatumPocetkaPutovanja.CompareTo(y.DatumPocetkaPutovanja));
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }



            brojacdatumPocetak++;
            ViewBag.aranzmani = aranzmani;
            ViewBag.rezervacije = rezervacije;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult sortirajPoNazivu()
        {
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            ViewBag.smjestaji = sm;
            ViewBag.SJedinice = smj;
            ViewBag.rezervacije = rezervacije;

            if (brojacNaziv % 2 == 0)
            {
                try
                {
                    foreach (Aranzman a in aranzmani)
                    {
                        aranzmani.Sort((x, y) => x.NazivAranzmana.CompareTo(y.NazivAranzmana));
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                try
                {

                    foreach (Aranzman a in aranzmani)
                    {
                        aranzmani.Sort((y, x) => x.NazivAranzmana.CompareTo(y.NazivAranzmana));
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }



            brojacNaziv++;
            ViewBag.aranzmani = aranzmani;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult sortirajPoDatumKraj()
        {
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            ViewBag.smjestaji = sm;
            ViewBag.SJedinice = smj;
            ViewBag.rezervacije = rezervacije;

            if (brojacdatumKraj % 2 == 0)
            {
                try
                {
                    foreach (Aranzman a in aranzmani)
                    {
                        aranzmani.Sort((x, y) => x.DatumPocetkaPutovanja.CompareTo(y.DatumPocetkaPutovanja));
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else
            {
                try
                {

                    foreach (Aranzman a in aranzmani)
                    {
                        aranzmani.Sort((y, x) => x.DatumPocetkaPutovanja.CompareTo(y.DatumPocetkaPutovanja));
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }



            brojacdatumKraj++;
            ViewBag.aranzmani = aranzmani;

            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Pretraga(string NazivAranzmana = "", string Tip_aranzmana = "", string Tip_prevoza = "", string minPocetak = "", string maxPocetak = "", string minKraj = "", string maxKraj = "")
        {
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            ViewBag.smjestaji = sm;
            ViewBag.SmjestajneJedinice = smj;
            ViewBag.rezervacije = rezervacije;


            var aranzmaniQueryable = aranzmani.AsQueryable();

            if (!string.IsNullOrEmpty(NazivAranzmana))
            {
                aranzmaniQueryable = aranzmaniQueryable.Where(x => x.NazivAranzmana.ToLower().Contains(NazivAranzmana.ToLower()));
            }

            if (!string.IsNullOrEmpty(Tip_aranzmana))
            {
                aranzmaniQueryable = aranzmaniQueryable.Where(x => x.Tip_aranzmana.ToString().Equals(Tip_aranzmana));
            }

            if (!string.IsNullOrEmpty(Tip_prevoza))
            {
                aranzmaniQueryable = aranzmaniQueryable.Where(x => x.Tip_prevoza.ToString().Equals(Tip_prevoza));
            }

            DateTime minpocetakdt = DateTime.Now;
            DateTime maxpocetakdt = DateTime.Now;
            DateTime minkrajdt = DateTime.Now;
            DateTime maxkrajdt = DateTime.Now;


            if (!string.IsNullOrEmpty(minPocetak))
            {
                string[] dio = minPocetak.Split('-');
                string pravi = dio[2] + "/" + dio[1] + "/" + dio[0];
                minpocetakdt = DateTime.ParseExact(pravi, "dd/MM/yyyy", null);
                aranzmaniQueryable = aranzmaniQueryable.Where(x => x.DatumPocetkaPutovanja >= minpocetakdt);

            }

            if (maxPocetak != string.Empty)
            {
                string[] dio = maxPocetak.Split('-');
                string pravi = dio[2] + "/" + dio[1] + "/" + dio[0];
                maxpocetakdt = DateTime.ParseExact(pravi, "dd/MM/yyyy", null);
                aranzmaniQueryable = aranzmaniQueryable.Where(x => x.DatumPocetkaPutovanja <= maxpocetakdt);
            }

            if (minKraj != string.Empty)
            {
                string[] dio = minKraj.Split('-');
                string pravi = dio[2] + "/" + dio[1] + "/" + dio[0];
                minkrajdt = DateTime.ParseExact(pravi, "dd/MM/yyyy", null);
                aranzmaniQueryable = aranzmaniQueryable.Where(x => x.DatumZavrsetkaPutovanja >= minkrajdt);
            }

            if (maxKraj != string.Empty)
            {
                string[] dio = maxKraj.Split('-');
                string pravi = dio[2] + "/" + dio[1] + "/" + dio[0];
                maxkrajdt = DateTime.ParseExact(pravi, "dd/MM/yyyy", null);
                aranzmaniQueryable = aranzmaniQueryable.Where(x => x.DatumZavrsetkaPutovanja <= minkrajdt);
            }


            ViewBag.aranzmani = aranzmaniQueryable.ToList();

            return View("Rezervisi");
        }

        [HttpPost]
        public ActionResult OstaviKomentar(string TekstKomentar ="",int Ocena=0,string KorisnickoIme = "",int IdAranzmana = 0)
        {
            Korisnik k = (Korisnik)Session["korisnik"];
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];

            Komentar komentar = new Komentar();

            if (IdAranzmana >0)
                komentar.Aranzman.IdAranzmana = IdAranzmana;
            if (!string.IsNullOrEmpty(TekstKomentar))
                komentar.TekstKomentar = TekstKomentar;
            if (Ocena > 0)
                komentar.Ocena = Ocena;
            if (!string.IsNullOrEmpty(KorisnickoIme))
                komentar.Turista.KorisnickoIme = KorisnickoIme;

            komentar.Odobren = "ne";

            Baza.DodajKomentarUBazu(komentar);

            ViewBag.smjestaji = sm;
            ViewBag.SmjestajneJedinice = smj;
            ViewBag.rezervacije = rezervacije;
            ViewBag.aranzmani = aranzmani;
            return RedirectToAction("Index","Pocetna");
        }
    }
}