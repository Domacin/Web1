using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TuristickaAgencija.Models;

namespace TuristickaAgencija.Controllers
{
    public class InformacijaController : Controller
    {
        static int brojacCena = 0;
        static int brojacGosti = 0;
        // GET: Informacija
        public ActionResult Index()
        {
            PocetnaController.prviprikaz = 0;
            List<Aranzman> lista = TempData["aranzman"] as List<Aranzman>;
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            List<Komentar> komentari = (List<Komentar>)HttpContext.Application["komentari"];
            ViewBag.komentari = komentari;
            ViewBag.rezervacije = rezervacije;
            ViewBag.smjestaji = sm;
            ViewBag.SJedinice = smj;
            ViewBag.aranzmanZaPrikaz = lista;
            return View();
        }

        public ActionResult Modifikacija()
        {
            PocetnaController.prviprikaz = 0;
            Session["listaModifikacija"] = null;
            List<Aranzman> lista = TempData["aranzman"] as List<Aranzman>;
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            List<Komentar> komentari = (List<Komentar>)HttpContext.Application["komentari"];
            ViewBag.komentari = komentari;
            ViewBag.rezervacije = rezervacije;
            ViewBag.smjestaji = sm;
            ViewBag.SJedinice = smj;
            ViewBag.aranzmanZaPrikaz = lista;
            return View("Modifikacija");
        }

        public ActionResult ModifikacijaSmjestajnaJedinica(int id)
        {
            List<Aranzman> lista = TempData["aranzman"] as List<Aranzman>;
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            List<Komentar> komentari = (List<Komentar>)HttpContext.Application["komentari"];

            var smjQueryable = smj.AsQueryable();

            smjQueryable = smjQueryable.Where(x => x.Id.Equals(id));

            ViewBag.komentari = komentari;
            ViewBag.rezervacije = rezervacije;
            ViewBag.smjestaji = sm;
            ViewBag.SJedinice = smjQueryable.ToList();
            ViewBag.aranzmanZaPrikaz = lista;

            return View("ModifikacijaSmjestajnaJedinica");
        }
        [HttpPost]
        public ActionResult sortirajPoBrojuDozvoljenihGostijuModifikacija()
        {
            List<Aranzman> lista = Session["listaModifikacija"] as List<Aranzman>;
            Session["listaModifikacija"] = null;
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            List<Komentar> komentari = (List<Komentar>)HttpContext.Application["komentari"];
            ViewBag.komentari = komentari;
            ViewBag.rezervacije = rezervacije;

            if (brojacGosti % 2 == 0)
            {
                smj.Sort((x, y) => x.DozvoljenBrojGostiju.CompareTo(y.DozvoljenBrojGostiju));
            }
            else
            {
                smj.Sort((y, x) => x.DozvoljenBrojGostiju.CompareTo(y.DozvoljenBrojGostiju));
            }

            ViewBag.smjestaji = sm;
            ViewBag.SJedinice = smj;
            ViewBag.aranzmanZaPrikaz = lista;
            brojacGosti++;
            return View("Modifikacija");
        }

        [HttpPost]
        public ActionResult sortirajPoCeniModifikacija()
        {
            List<Aranzman> lista = Session["listaModifikacija"] as List<Aranzman>;
            Session["listaModifikacija"] = null;
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            List<Komentar> komentari = (List<Komentar>)HttpContext.Application["komentari"];
            ViewBag.komentari = komentari;
            ViewBag.rezervacije = rezervacije;
            if (brojacCena % 2 == 0)
            {
                smj.Sort((x, y) => x.CenaSmestajneJedinice.CompareTo(y.CenaSmestajneJedinice));
            }
            else
            {
                smj.Sort((y, x) => x.CenaSmestajneJedinice.CompareTo(y.CenaSmestajneJedinice));
            }

            ViewBag.smjestaji = sm;
            ViewBag.SJedinice = smj;
            ViewBag.aranzmanZaPrikaz = lista;
            brojacCena++;
            return View("Modifikacija");
        }

      
        [HttpPost]
        public ActionResult sortirajPoBrojuDozvoljenihGostiju()
        {
            List<Aranzman> lista = Session["lista"] as List<Aranzman>;
            Session["lista"] = null;
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            List<Komentar> komentari = (List<Komentar>)HttpContext.Application["komentari"];
            ViewBag.komentari = komentari;
            ViewBag.rezervacije = rezervacije;

            if (brojacGosti % 2 == 0)
            {
                smj.Sort((x, y) => x.DozvoljenBrojGostiju.CompareTo(y.DozvoljenBrojGostiju));
            }
            else
            {
                smj.Sort((y, x) => x.DozvoljenBrojGostiju.CompareTo(y.DozvoljenBrojGostiju));
            }

            ViewBag.smjestaji = sm;
            ViewBag.SJedinice = smj;
            ViewBag.aranzmanZaPrikaz = lista;
            brojacGosti++;
            return View("Index");
        }

        [HttpPost]
        public ActionResult sortirajPoCeni()
        {
            List<Aranzman> lista = Session["lista"] as List<Aranzman>;
            Session["lista"] = null;
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            List<Komentar> komentari = (List<Komentar>)HttpContext.Application["komentari"];
            ViewBag.komentari = komentari;
            ViewBag.rezervacije = rezervacije;
            if (brojacCena %2 == 0)
            {
                smj.Sort((x, y) => x.CenaSmestajneJedinice.CompareTo(y.CenaSmestajneJedinice));
            }
            else
            {
                smj.Sort((y, x) => x.CenaSmestajneJedinice.CompareTo(y.CenaSmestajneJedinice));
            }
            
            ViewBag.smjestaji = sm;
            ViewBag.SJedinice = smj;
            ViewBag.aranzmanZaPrikaz = lista;
            brojacCena++;
            return View("Index");
        }

        [HttpPost]
        public ActionResult Pretraga(int DozvoljenBrojGostijuMin = 0,int DozvoljenBrojGostijuMax = 0,string DozvoljeniLjubimci="",double CenaSmestajneJedinice = 0)
        {
            List<Aranzman> lista = Session["lista"] as List<Aranzman>;
            Session["lista"] = null;
            List<SmjestajnaJedinica> smj = (List<SmjestajnaJedinica>)HttpContext.Application["smjestajnejedinice"];
            List<Smjestaj> sm = (List<Smjestaj>)HttpContext.Application["smjestaji"];
            List<Rezervacija> rezervacije = (List<Rezervacija>)HttpContext.Application["rezervacije"];
            List<Komentar> komentari = (List<Komentar>)HttpContext.Application["komentari"];
            ViewBag.komentari = komentari;
            ViewBag.rezervacije = rezervacije;

            var smjestajneJediniceQueryable = smj.AsQueryable();

            if (DozvoljenBrojGostijuMin != 0)
                smjestajneJediniceQueryable = smjestajneJediniceQueryable.Where(x => x.DozvoljenBrojGostiju >= DozvoljenBrojGostijuMin);

            if(DozvoljenBrojGostijuMax != 0)
                smjestajneJediniceQueryable = smjestajneJediniceQueryable.Where(x => x.DozvoljenBrojGostiju <= DozvoljenBrojGostijuMax);

            
             if(CenaSmestajneJedinice != 0)    
              smjestajneJediniceQueryable = smjestajneJediniceQueryable.Where(x => x.CenaSmestajneJedinice.Equals(CenaSmestajneJedinice));
        

            if(!string.IsNullOrEmpty(DozvoljeniLjubimci))
            {
                smjestajneJediniceQueryable = smjestajneJediniceQueryable.Where(x => x.DozvoljeniLjubimci.Contains(DozvoljeniLjubimci.ToLower()));
            }

            ViewBag.smjestaji = sm;
            ViewBag.SJedinice = smjestajneJediniceQueryable.ToList();
            ViewBag.aranzmanZaPrikaz = lista;

            return View("Index");
        }

        [HttpPost]
        public ActionResult PretragaModifikacija(int DozvoljenBrojGostijuMin = 0, int DozvoljenBrojGostijuMax = 0, string DozvoljeniLjubimci = "", double CenaSmestajneJedinice = 0)
        {
            List<Aranzman> lista = Session["listaModifikacija"] as List<Aranzman>;
            Session["listaModifikacija"] = null;
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
                smjestajneJediniceQueryable = smjestajneJediniceQueryable.Where(x => x.DozvoljeniLjubimci.ToLower().Contains(DozvoljeniLjubimci.ToLower()));
            }

            ViewBag.smjestaji = sm;
            ViewBag.SJedinice = smjestajneJediniceQueryable.ToList();
            ViewBag.aranzmanZaPrikaz = lista;

            return View("Modifikacija");
        }
    }
}