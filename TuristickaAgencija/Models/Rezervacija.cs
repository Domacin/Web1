using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuristickaAgencija.Models
{
    public class Rezervacija
    {
        public string Identifikator { get; set;} //jedinstveno
        public Korisnik Turista { get; set; }
        public string Status { get; set; } //aktivna/neaktivna
        public Aranzman AranzmanZaKojiSeVrsiRezervacija { get; set; }
        public SmjestajnaJedinica IzabranaSmjestajnaJedinica { get; set; }
        public  Rezervacija() { }
        
    }
}