using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuristickaAgencija.Models
{
    public enum POL { MUSKO,ZENSKO};
    public enum ULOGA { ADMINISTRATOR,MENADZER,TURISTA};
    public class Korisnik
    {
        public string KorisnickoIme { get; set; } //jedinstveno
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public POL Pol { get; set; }
        public string Email { get; set; }
        public DateTime Datum { get; set;}
        public ULOGA Uloga { get; set; }
        public int BrojOtkazanih { get; set; }
        public string Blokiran { get; set; }
       
        List<Rezervacija> listaRezervacijaTurista { get; set; }// ako je korisnik Turista
        List<Aranzman> listaAranzmanaMenadzera { get; set; }// ako je korisnik menadzer

        public Korisnik(string korisnickoime,string lozinka,string ime,string prezime,POL pol,string email,DateTime datumrodjenja)
        {
            KorisnickoIme = korisnickoime;
            Lozinka = lozinka;
            Ime = ime;
            Prezime = prezime;
            Pol = pol;
            Email = email;
            Datum = datumrodjenja;
        }

        public Korisnik() { }
    }
}