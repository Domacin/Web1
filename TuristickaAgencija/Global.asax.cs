using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TuristickaAgencija.Models;

namespace TuristickaAgencija
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Dictionary<string, Korisnik> korisnici = Baza.UcitajKorisnikeIzBaze("~/App_Data/korisnici.txt");
            HttpContext.Current.Application["korisnici"] = korisnici;

            List<Smjestaj> smjestaji = Baza.UcitajSmjestajeIzBaze("~/App_Data/smjestaji.txt");
            HttpContext.Current.Application["smjestaji"] = smjestaji;
            Smjestaj.Initialize(smjestaji.Last().IdSmjestaja);

            List<SmjestajnaJedinica> smjestajneJedinice = Baza.UcitajSmjestajneJedinice("~/App_Data/smjestajnejedinice.txt");
            HttpContext.Current.Application["smjestajnejedinice"] = smjestajneJedinice;
            SmjestajnaJedinica.Initialize(smjestajneJedinice.Last().Id);

            List<Aranzman> aranzmani = Baza.UcitajAranzmaneIzBaze("~/App_Data/aranzmani.txt");
            HttpContext.Current.Application["aranzmani"] = aranzmani;
            Aranzman.Initialize(aranzmani.Last().IdAranzmana);

            List<Rezervacija> rezervacije = Baza.UcitajRezervacije("~/App_Data/rezervacije.txt");
            HttpContext.Current.Application["rezervacije"] = rezervacije;

            List<Komentar> komentari = Baza.UcitajKomentareIzBaze("~/App_Data/komentari.txt");
            HttpContext.Current.Application["komentari"] = komentari;
            Komentar.Initialize(komentari.Last().IdKomentara);

        }

        private void LoadData() { }
    }
}
