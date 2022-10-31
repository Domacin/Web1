using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using TuristickaAgencija.Models;

namespace TuristickaAgencija.Controllers
{
    public class PocetnaController : Controller
    {
       public static int prviprikaz = 0;
        public static int brojacNaziv = 0;
        static int brojacdatumPocetak = 0;
        static int brojacdatumKraj = 0;

        // GET: Pocetna
        public ActionResult Index()
        {
            Session["lista"] = null;
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            ViewBag.rezervacije = rezervacije;

            if (prviprikaz == 0)
                aranzmani.Sort((x, y) => x.DatumZavrsetkaPutovanja.CompareTo(y.DatumZavrsetkaPutovanja));

            prviprikaz++;
            ViewBag.smjestaji = sm;
            ViewBag.SJedinice = smj;
            ViewBag.aranzmani = aranzmani;
       
            return View();
        }

        public ActionResult GotoviAranzmani()
        {
            prviprikaz = 0;
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            ViewBag.smjestaji = sm;
            ViewBag.SJedinice = smj;
            ViewBag.aranzmani = aranzmani;

            return View("GotoviAranzmani");
        }

        [HttpPost]
        public ActionResult sortirajPoDatumPocetkaZavrseni()
        {
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
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

            return View("GotoviAranzmani");
        }

        [HttpPost]
        public ActionResult sortirajPoDatumKrajZavrseni()
        {
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            ViewBag.smjestaji = sm;
            ViewBag.SJedinice = smj;


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

            return View("GotoviAranzmani");
        }

        [HttpPost]
        public ActionResult sortirajPoNazivuZavrseni()
        {
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            ViewBag.smjestaji = sm;
            ViewBag.SJedinice = smj;


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

            return View("GotoviAranzmani");
        }

        public ActionResult Prosledi(int id)
        {
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            ViewBag.rezervacije = rezervacije;
            ViewBag.smjestaji = sm;
            ViewBag.SJedinice = smj;

            List<Aranzman> zaprikaz = new List<Aranzman>();
            

            Smjestaj s = new Smjestaj();

            for (int i = 0; i < aranzmani.Count; i++)
            {
                if (aranzmani[i].IdAranzmana == id)
                {
                    zaprikaz.Add(aranzmani[i]);
                   
                }
            }

            for (int i = 0; i < sm.Count; i++)
            {
                for (int j = 0; j < zaprikaz.Count; j++)
                {
                    if (zaprikaz[j].IdSmjestaja == sm[i].IdSmjestaja)
                    {
                        s = sm[i];

                    }
                }
                    
                
            }
 
            zaprikaz[0].Smjestaj = s;
            
            TempData["aranzman"] = zaprikaz;

            return RedirectToAction("Index", "Informacija");
        }

        public ActionResult ProslediZaRezervaciju(int id)
        {
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            ViewBag.smjestaji = sm;
            ViewBag.SJedinice = smj;

            List<Aranzman> zaprikaz = new List<Aranzman>();


            Smjestaj s = new Smjestaj();

            for (int i = 0; i < aranzmani.Count; i++)
            {
                if (aranzmani[i].IdAranzmana == id)
                {
                    zaprikaz.Add(aranzmani[i]);

                }
            }

            for (int i = 0; i < sm.Count; i++)
            {
                for (int j = 0; j < zaprikaz.Count; j++)
                {
                    if (zaprikaz[j].IdSmjestaja == sm[i].IdSmjestaja)
                    {
                        s = sm[i];

                    }
                }


            }

            zaprikaz[0].Smjestaj = s;

            Session["rezervacija"] = zaprikaz;

            return RedirectToAction("Index", "Turista");
        }
        

        [HttpPost]
        public ActionResult sortirajPoNazivu()
        {
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            ViewBag.smjestaji = sm;
            ViewBag.SJedinice = smj;
           

            if(brojacNaziv %2 == 0)
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

            return View("Index");
        }

        [HttpPost]
        public ActionResult sortirajPoDatumPocetka()
        {
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
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

            return View("Index");
        }

        [HttpPost]
        public ActionResult sortirajPoDatumKraj()
        {
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            ViewBag.smjestaji = sm;
            ViewBag.SJedinice = smj;


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

            return View("Index");
        }

        [HttpPost]
        public ActionResult Pretraga(string NazivAranzmana = "", string Tip_aranzmana = "",string Tip_prevoza="", string minPocetak = "",string maxPocetak = "",string minKraj = "",string maxKraj = "")
        {
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            ViewBag.smjestaji = sm;
            ViewBag.SJedinice = smj;
        


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

            return View("Index");
        }
        [HttpPost]
        public ActionResult PretragaZavrsenih(string NazivAranzmana = "", string Tip_aranzmana = "", string Tip_prevoza = "", string minPocetak = "", string maxPocetak = "", string minKraj = "", string maxKraj = "")
        {
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            ViewBag.smjestaji = sm;
            ViewBag.SJedinice = smj;

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

            return View("GotoviAranzmani");
        }

    }
}