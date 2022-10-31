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
    public class MenadzerController : Controller
    {
        public static int brojacNaziv = 0;
        static int brojacdatumPocetak = 0;
        static int brojacdatumKraj = 0;

        public ActionResult OdobriKomentar(int id)
        {
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Komentar> komentari = (List<Komentar>)HttpContext.Application["komentari"];

            for (int i = 0; i < komentari.Count; i++)
            {
                if(komentari[i].IdKomentara == id)
                {
                    komentari[i].Odobren = "da";
                    
                }
            }
            string putanja = HostingEnvironment.MapPath("~/App_Data/komentari.txt");
            StreamWriter sw = new StreamWriter(putanja);

            foreach (Komentar k in komentari)
            {
                sw.WriteLine(k.IdKomentara + ";" + k.Turista.KorisnickoIme + ";" + k.TekstKomentar + ";" + k.Ocena + ";" + k.Odobren);
            }
            sw.Close();


            ViewBag.komentari = komentari;
            ViewBag.smjestaji = sm;
            ViewBag.smjestajnejedinice = smj;
            ViewBag.aranzmani = aranzmani;
            return RedirectToAction("Index", "Pocetna");
        }
        public ActionResult ModifikacijaSJ(SmjestajnaJedinica s)
        {
            PocetnaController.prviprikaz = 0;
            Korisnik k = (Korisnik)Session["korisnik"];
            List<Aranzman> sviAranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<Smjestaj> smjestaji = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<SmjestajnaJedinica> smjestajneJedinice = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];

            for (int i = 0; i < smjestajneJedinice.Count; i++)
            {
                if(smjestajneJedinice[i].Id == s.Id)
                {
                    smjestajneJedinice[i].DozvoljeniLjubimci = s.DozvoljeniLjubimci;
                    smjestajneJedinice[i].DozvoljenBrojGostiju = s.DozvoljenBrojGostiju;
                    smjestajneJedinice[i].CenaSmestajneJedinice = s.CenaSmestajneJedinice;
                    break;
                }
            }

            string putanja = HostingEnvironment.MapPath("~/App_Data/smjestajnejedinice.txt");
            StreamWriter sw = new StreamWriter(putanja);

            foreach (SmjestajnaJedinica sj in smjestajneJedinice)
            {
                sw.WriteLine(sj.PripadaSmjestaju + ";" + sj.DozvoljeniLjubimci + ";" + sj.DozvoljenBrojGostiju + ";" + sj.CenaSmestajneJedinice + ";" + sj.Id + ";" + sj.Zauzeto);

            }
            sw.Close();
            HttpContext.Application["smjestajnejedinice"] = smjestajneJedinice;
            ViewBag.smjestaji = smjestaji;
            ViewBag.smjestajnejedinice = smjestajneJedinice;
            ViewBag.aranzmani = sviAranzmani;
            return View("SpisakSmjestajnihJedinica");
        }
        [HttpPost]
        public ActionResult SortirajNaziv()
        {
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            ViewBag.rezervacije = rezervacije;
            ViewBag.smjestaji = sm;
            ViewBag.smjestajnejedinice = smj;
         


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

            return View("SpisakAranzmana");

        }
        [HttpPost]
        public ActionResult sortirajPoDatumPocetka()
        {
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            ViewBag.rezervacije = rezervacije;
            ViewBag.smjestaji = sm;
            ViewBag.smjestajnejedinice = smj;


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

            return View("SpisakAranzmana");
        }

        [HttpPost]
        public ActionResult sortirajPoDatumKraj()
        {
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            ViewBag.rezervacije = rezervacije;
            ViewBag.smjestaji = sm;
            ViewBag.smjestajnejedinice
 = smj;


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

            return View("SpisakAranzmana");
        }

        //pretraga aranzmana
        [HttpPost]
        public ActionResult Pretraga(string NazivAranzmana = "", string Tip_aranzmana = "", string Tip_prevoza = "", string minPocetak = "", string maxPocetak = "", string minKraj = "", string maxKraj = "")
        {
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            ViewBag.rezervacije = rezervacije;
            ViewBag.smjestaji = sm;
            ViewBag.smjestajnejedinice = smj;



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

            return View("SpisakAranzmana");
        }
       
        [HttpPost]
        public ActionResult PretragaSmjestaja(string NazivSmjestaja = "",string Bazen = "",string SpaCentar = "",string PrilagodjenostOsobamaSaInvaliditetom="",string WiFi ="",TIP_SMESTAJA Tip_smjestaja =TIP_SMESTAJA.HOTEL)
        {
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> smjestaji= (List<Smjestaj>)HttpContext.Application["smjestaji"];
            ViewBag.aranzmani = aranzmani;
            ViewBag.smjestajnejedinice = smj;

            var smjestajiQueryable = smjestaji.AsQueryable();

            if (!string.IsNullOrEmpty(NazivSmjestaja))
                smjestajiQueryable = smjestajiQueryable.Where(x => x.NazivSmjestaja.ToLower().Contains(NazivSmjestaja));


            if (!string.IsNullOrEmpty(Bazen))
                smjestajiQueryable = smjestajiQueryable.Where(x => x.Bazen.ToLower().Contains(Bazen));


            if (!string.IsNullOrEmpty(SpaCentar))
                smjestajiQueryable = smjestajiQueryable.Where(x => x.SpaCentar.ToLower().Contains(SpaCentar));
  

            if (!string.IsNullOrEmpty(PrilagodjenostOsobamaSaInvaliditetom))
                smjestajiQueryable = smjestajiQueryable.Where(x => x.PrilagodjenostOsobamaSaInvaliditetom.ToLower().Contains(PrilagodjenostOsobamaSaInvaliditetom));


            if (!string.IsNullOrEmpty(WiFi))
                smjestajiQueryable = smjestajiQueryable.Where(x => x.WiFi.ToLower().Contains(WiFi));


            if(Tip_smjestaja == TIP_SMESTAJA.HOTEL )
                smjestajiQueryable = smjestajiQueryable.Where(x => x.Tip_smjestaja.Equals(Tip_smjestaja));


            if (Tip_smjestaja == TIP_SMESTAJA.MOTEL)
                smjestajiQueryable = smjestajiQueryable.Where(x => x.Tip_smjestaja.Equals(Tip_smjestaja));

             if (Tip_smjestaja == TIP_SMESTAJA.VILA)
                smjestajiQueryable = smjestajiQueryable.Where(x => x.Tip_smjestaja.Equals(Tip_smjestaja));

            ViewBag.smjestaji = smjestajiQueryable.ToList();
            return View("SpisakSmjestaja");
        }

        [HttpPost]
        public ActionResult PretragaSJ(int DozvoljenBrojGostijuMin = 0,int DozvoljenBrojGostijuMax = 0,string DozvoljeniLjubimci = "",double CenaSmestajneJedinice = 0)
        {
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            List<Komentar> komentari = (List<Komentar>)HttpContext.Application["komentari"];
            ViewBag.komentari = komentari;
            ViewBag.rezervacije = rezervacije;

            var smjestajneJediniceQueryable = smj.AsQueryable();

            if (DozvoljenBrojGostijuMin != 0)
                smjestajneJediniceQueryable = smjestajneJediniceQueryable.Where(x => x.DozvoljenBrojGostiju >= DozvoljenBrojGostijuMin);

            if (DozvoljenBrojGostijuMax != 0)
                smjestajneJediniceQueryable = smjestajneJediniceQueryable.Where(x => x.DozvoljenBrojGostiju <= DozvoljenBrojGostijuMax);


            if (CenaSmestajneJedinice != 0)
                smjestajneJediniceQueryable = smjestajneJediniceQueryable.Where(x => x.CenaSmestajneJedinice.Equals(CenaSmestajneJedinice));


            if (!string.IsNullOrEmpty(DozvoljeniLjubimci))
            {
                smjestajneJediniceQueryable = smjestajneJediniceQueryable.Where(x => x.DozvoljeniLjubimci.Contains(DozvoljeniLjubimci.ToLower()));
            }

            ViewBag.smjestaji = sm;
            ViewBag.smjestajnejedinice = smjestajneJediniceQueryable.ToList();
           

            return View("SpisakSmjestajnihJedinica");
        }

        // GET: Menadzer
        public ActionResult Index()
        {
            PocetnaController.prviprikaz = 0;
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Aranzman> sviAranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            ViewBag.rezervacije = rezervacije;
            ViewBag.aranzmani = sviAranzmani;
            ViewBag.smjestaji = sm;
            ViewBag.SJedinice = smj;
            return View();
        }
        
        [HttpPost]
        public ActionResult SacuvajModifikacijuAranzmana(Aranzman a,MjestoNalazenja mn,Smjestaj s)
        {
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            Korisnik k = (Korisnik)Session["korisnik"];
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];

            if(a.PosterAranzmana == null)
            {
                a.PosterAranzmana = "/Slike/" + a.NazivAranzmana.ToLower() + ".jpg";
            }
            else
            {
                string slika = "/Slike/" + a.PosterAranzmana;
                a.PosterAranzmana = slika;
            }
            
            

            a.Smjestaj = s;
            a.MjestoNalazenja = mn;
            a.PripadaKorisniku = k.KorisnickoIme;
            foreach(Aranzman item in aranzmani)
            {
                if(item.IdAranzmana == a.IdAranzmana)
                {
                    item.NazivAranzmana = a.NazivAranzmana;
                    item.OpisAranzmana = a.OpisAranzmana;
                    item.ProgramPutovanja = a.ProgramPutovanja;
                    item.Tip_aranzmana = a.Tip_aranzmana;
                    item.Tip_prevoza = a.Tip_prevoza;
                    item.PripadaKorisniku = k.KorisnickoIme;
                    item.Lokacija = a.Lokacija;
                    item.DatumPocetkaPutovanja = a.DatumPocetkaPutovanja;
                    item.DatumZavrsetkaPutovanja = a.DatumZavrsetkaPutovanja;
                    item.MjestoNalazenja.Adresa = mn.Adresa;
                    item.MjestoNalazenja.GeografksaDuzina = mn.GeografksaDuzina;
                    item.MjestoNalazenja.GeografksaSirina = mn.GeografksaSirina;
                    item.VrijemeNalazenje = a.VrijemeNalazenje;
                    item.MaxBrojPutnika = a.MaxBrojPutnika;
                    item.Smjestaj.IdSmjestaja = s.IdSmjestaja;
                    item.PosterAranzmana = a.PosterAranzmana;
                }
            }

            string putanja = HostingEnvironment.MapPath("~/App_Data/aranzmani.txt");
            StreamWriter sw = new StreamWriter(putanja);
            foreach (Aranzman item in aranzmani)
            {
                sw.WriteLine(item.NazivAranzmana + ";" + item.Tip_aranzmana + ";" + item.Tip_prevoza + ";" + item.Lokacija + ";" + item.DatumPocetkaPutovanja.ToString("dd/MM/yyyy") + ";" + item.DatumZavrsetkaPutovanja.ToString("dd/MM/yyyy") + ";" + item.MjestoNalazenja.Adresa + ";" + item.MjestoNalazenja.GeografksaDuzina + ";" + item.MjestoNalazenja.GeografksaSirina + ";" + item.VrijemeNalazenje.ToString("HH:mm") + ";" + item.MaxBrojPutnika + ";" + item.OpisAranzmana + ";" + item.ProgramPutovanja + ";" + item.Smjestaj.IdSmjestaja + ";" + item.IdAranzmana + ";" + item.PosterAranzmana + ";" + item.PripadaKorisniku);
            }
            sw.Close();
            ViewBag.aranzmani = aranzmani;
            ViewBag.smjestaji = sm;
            ViewBag.smjestajnejedinice = smj;

            return View("SpisakAranzmana");
        }

        public ActionResult Prosledi(int id)
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

            TempData["aranzman"] = zaprikaz;

            return RedirectToAction("Modifikacija", "Informacija");
        }

        public ActionResult ObrisiAranzman(int id)
        {
            Korisnik k = (Korisnik)Session["korisnik"];
            List<Aranzman> sviAranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<Smjestaj> smjestaji = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<SmjestajnaJedinica> smjestajneJedinice = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            ViewBag.rezervacije = rezervacije;


           


            for (int i = 0; i < sviAranzmani.Count; i++)
            {
               if(sviAranzmani[i].IdAranzmana == id )
               {
                    sviAranzmani.Remove(sviAranzmani[i]);
                    break;
               }
            }

          
            ViewBag.smjestaji = smjestaji;
            ViewBag.smjestajnejedinice = smjestajneJedinice;
            ViewBag.aranzmani = sviAranzmani;
            return View("SpisakAranzmana");
        }
        public ActionResult ObrisiSmjestaj(int id)
        {
            Korisnik k = (Korisnik)Session["korisnik"];
            List<Aranzman> sviAranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<Smjestaj> smjestaji = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<SmjestajnaJedinica> smjestajneJedinice = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            foreach (Aranzman a in sviAranzmani)
            {
                for (int i = 0; i < smjestaji.Count; i++)
                {
                    for (int j = 0; j < smjestajneJedinice.Count; j++)
                    {
                        if (smjestaji[i].IdSmjestaja == id && smjestajneJedinice[j].Zauzeto == "ne" && smjestajneJedinice[j].PripadaSmjestaju == smjestaji[i].IdSmjestaja)
                        {
                            smjestaji.Remove(smjestaji[i]);
                            break;
                        }
                    }
                    
                }
            }
            


            ViewBag.smjestaji = smjestaji;
            ViewBag.SJedinice = smjestajneJedinice;
            ViewBag.aranzmanZaPrikaz = sviAranzmani;
            return View("SpisakSmjestaja");
        }
        public ActionResult ObrisiSmjestajnuJedinicu(int id)
        {
            Korisnik k = (Korisnik)Session["korisnik"];
            List<Aranzman> sviAranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<Smjestaj> smjestaji = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<SmjestajnaJedinica> smjestajneJedinice = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];

            for (int i = 0; i < sviAranzmani.Count; i++)
            {
                for (int j = 0; j < smjestaji.Count; j++)
                {
                    for (int o = 0; o < smjestajneJedinice.Count; o++)
                    {
                        if(smjestajneJedinice[o].Id == id)
                        {
                            smjestajneJedinice.Remove(smjestajneJedinice[o]);
                        }
                    }
                }
            }

            ViewBag.smjestaji = smjestaji;
            ViewBag.smjestajnejedinice = smjestajneJedinice;
            ViewBag.aranzmani = sviAranzmani;
            return View("SpisakSmjestajnihJedinica");
        }

        public ActionResult SpisakSmjestaja()
        {
            PocetnaController.prviprikaz = 0;
            Korisnik k = (Korisnik)Session["korisnik"];
            List<Aranzman> sviAranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<Smjestaj> smjestaji = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<SmjestajnaJedinica> smjestajneJedinice = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            ViewBag.rezervacije = rezervacije;

            for (int i = 0; i < smjestaji.Count; i++)
            {
                for (int j = 0; j < smjestajneJedinice.Count; j++)
                {
                    if(smjestaji[i].IdSmjestaja == smjestajneJedinice[j].PripadaSmjestaju)
                    {
                        smjestaji[i].listaSmjestajnihJedinica.Add(smjestajneJedinice[j]);
                    }
                }
            }

            foreach (SmjestajnaJedinica sj in smjestajneJedinice)
            {
                for (int i = 0; i < smjestaji.Count; i++)
                {
                    if (smjestaji[i].IdSmjestaja == sj.PripadaSmjestaju && sj.Zauzeto == "da")
                        smjestaji[i].BarJednaSMjestajnaJedinicaZauzeta = true;
                }
            }

            ViewBag.smjestaji = smjestaji;
            ViewBag.smjestajnejedinice = smjestajneJedinice;
            ViewBag.aranzmani = sviAranzmani;
            return View("SpisakSmjestaja");
        }

        public ActionResult SpisakAranzmana()
        {
            PocetnaController.prviprikaz = 0;
            Korisnik k = (Korisnik)Session["korisnik"];
            List<Aranzman> sviAranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<Smjestaj> smjestaji = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<SmjestajnaJedinica> smjestajneJedinice = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];


           
                foreach (SmjestajnaJedinica sj in smjestajneJedinice)
                {
                    for (int i = 0; i < smjestaji.Count; i++)
                    {
                        if (smjestaji[i].IdSmjestaja == sj.PripadaSmjestaju && sj.Zauzeto == "da")
                            smjestaji[i].BarJednaSMjestajnaJedinicaZauzeta = true;
                    }
                }
           

            ViewBag.rezervacije = rezervacije;
            ViewBag.smjestaji = smjestaji;
            ViewBag.smjestajnejedinice = smjestajneJedinice;
            ViewBag.aranzmani = sviAranzmani;
            return View("SpisakAranzmana");
        }

        public ActionResult SpisakSmjestajnihJedinica()
        {
            PocetnaController.prviprikaz = 0;
            Korisnik k = (Korisnik)Session["korisnik"];
            List<Aranzman> sviAranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            List<Smjestaj> smjestaji = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<SmjestajnaJedinica> smjestajneJedinice = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];

            ViewBag.smjestaji = smjestaji;
            ViewBag.smjestajnejedinice = smjestajneJedinice;
            ViewBag.aranzmani = sviAranzmani;
            return View("SpisakSmjestajnihJedinica");
        }

        [HttpPost]
        public ActionResult DodajSmjestajnuJedinicu(SmjestajnaJedinica sj)
        {
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            ViewBag.smjestaji = sm;
            ViewBag.SJedinice = smj;

            if (sj.DozvoljenBrojGostiju <= 0)
            {
                ViewBag.Greska = "Mora bar jedan gost biti dozvoljen!";
                return View("Index");
            }

            if (sj.DozvoljeniLjubimci == null)
            {
                ViewBag.Greska = "Polje za ljubimce nije popunjeno!";
                return View("Index");
            }

            if (sj.DozvoljeniLjubimci.ToLower().Trim() != "da" && sj.DozvoljeniLjubimci.ToLower().Trim() != "ne")
            {
                ViewBag.Greska = "Unesite da ili ne u polje koje se odnosi na ljubimce!";
                return View("Index");
            }

           

            if(sj.CenaSmestajneJedinice <= 0)
            {
                ViewBag.Greska = "Cena mora biti pozitivan broj!";
                return View("Index");
            }

            

            sj.Zauzeto = "ne";
            
            Baza.DodajSmjestajnuJedinicuUBazu(sj);
            SmjestajnaJedinica.id++;
            ViewBag.Uspjesno = "Smjestajna jedinica uspjesno dodata!";
            return View("Index");
        }

        [HttpPost]
        public ActionResult DodajAranzman(Aranzman a,MjestoNalazenja mn)
        {
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Aranzman> aranzmani = (List<Aranzman>)HttpContext.Application["aranzmani"];
            Korisnik k = (Korisnik)Session["korisnik"];
            ViewBag.smjestaji = sm;
            ViewBag.SJedinice = smj;

            if (a.NazivAranzmana == null)
            {
                ViewBag.Greska = "Unesite naziv aranzmana!";
                return View("Index");
            }


            if (a.DatumPocetkaPutovanja == DateTime.Parse("1 / 1 / 0001 12:00:00 AM"))
            {
                ViewBag.Greska = "Izaberite datum pocetka putovanja!";
                return View("Index");
            }

            if(a.DatumZavrsetkaPutovanja == DateTime.Parse("1 / 1 / 0001 12:00:00 AM"))
            {
                ViewBag.Greska = "Izaberite datum pocetka putovanja!";
                return View("Index");
            }

            if(a.DatumPocetkaPutovanja > a.DatumZavrsetkaPutovanja)
            {
                ViewBag.Greska = "Datum pocetka ne mozete biti posle datuma kraja!";
                return View("Index");
            }

            if (a.VrijemeNalazenje.Hour == DateTime.Parse("12:00:00 AM").Hour) 
            {
                ViewBag.Greska = "Odredite vrijeme nalazenja!";
                return View("index");
            }

            if(a.MaxBrojPutnika == 0)
            {
                ViewBag.Greska = "Odredite maksimalan broj putnika!!";
                return View("index");
            }

            if(a.OpisAranzmana == null)
            {
                ViewBag.Greska = "Morate dodati opis aranzmana!";
                return View("Index");
            }

            if(a.ProgramPutovanja == null)
            {
                ViewBag.Greska = "Morate dodati program putovanja!";
                return View("Index");
            }

            if(mn.Adresa == null)
            {
                ViewBag.Greska = "Morate dodati adresu nalazenja!";
                return View("Index");
           
            }

            if(mn.GeografksaDuzina == 0)
            {
                ViewBag.Greska = "Morate dodati geografsku duzinu!";
                return View("Index");
            }

            if (mn.GeografksaSirina == 0)
            {
                ViewBag.Greska = "Morate dodati geografsku sirina!";
                return View("Index");
            }

            if(a.PosterAranzmana == null)
            {
                ViewBag.Greska = "Morate izabrati sliku!";
                return View("Index");
            }
            string privremeni = a.PosterAranzmana;
            a.PosterAranzmana = "/Slike/" + privremeni;
            Smjestaj s = new Smjestaj();
            for (int i = 0; i < sm.Count; i++)
            {
                if (sm[i].IdSmjestaja == a.IdSmjestaja)
                    s = sm[i];
            }
            a.Smjestaj = s;
            a.MjestoNalazenja = mn;
            a.PripadaKorisniku = k.KorisnickoIme;
            Baza.DodajAranzmanUBazu(a);
            aranzmani.Add(a);
            Aranzman.ida++;
            HttpContext.Application["aranzmani"] = aranzmani;
            ViewBag.aranzmani = aranzmani;
            ViewBag.UspjesnoAranzman = "Aranzman uspjesno dodat!";
            return View("Index");
        }

        [HttpPost]
        public ActionResult DodajSmjestaj(Smjestaj s)
        {
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            ViewBag.SJedinice = smj;
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            ViewBag.smjestaji = sm;

            if (s.NazivSmjestaja == null)
            {
                ViewBag.GreskaSmjestaj = "Unesite naziv smjestaja";
                return View("Index");
            }

            if(s.PrilagodjenostOsobamaSaInvaliditetom.ToLower().Trim() != "da" && s.PrilagodjenostOsobamaSaInvaliditetom.ToLower().Trim() != "ne")
            {
                ViewBag.GreskaSmjestaj = "Za prilagodjenost osobama sa invaliditetom unestie da ili ne";
                return View("Index");
            }

            if(s.SpaCentar.ToLower().Trim() != "da" && s.SpaCentar.ToLower().Trim() != "ne")
            {
                ViewBag.GreskaSmjestaj = "Za spa centar unestie da ili ne";
                return View("Index");
            }

            if(s.Bazen.ToLower().Trim() != "da" && s.Bazen.ToLower().Trim() != "ne")
            {
                ViewBag.GreskaSmjestaj = "Za bazen unestie da ili ne";
                return View("Index");
            }

            if(s.BrojZvjezdica <= 0 || s.BrojZvjezdica > 5)
            {
                ViewBag.GreskaSmjestaj = "Minimalan broj zvjezdica je 1, a maksimalan je 5";
                return View("Index");
            }

            if(s.WiFi.ToLower().Trim() != "da" && s.WiFi.ToLower().Trim() != "ne")
            {
                ViewBag.GreskaSmjestaj = "Za WiFi unestie da ili ne";
                return View("Index");
            }

            Baza.DodajSmjestajUBazu(s);
            Smjestaj.id++;
            ViewBag.UspjesanSmjestaj = "Smjestaj uspjesno dodat!";
            return View("Index");
        }
    }
}