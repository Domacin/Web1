using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuristickaAgencija.Models
{
    public class MjestoNalazenja
    {
      

        public string Adresa { get; set; } //format adrese: Ulica i broj, Mjesto/grad,postanski broj
        public double GeografksaDuzina { get; set; }
        public double GeografksaSirina { get; set; }

        public MjestoNalazenja(string adresa, double geografksaDuzina, double geografksaSirina)
        {
            Adresa = adresa;
            GeografksaDuzina = geografksaDuzina;
            GeografksaSirina = geografksaSirina;
        }
        public MjestoNalazenja() { }
    }
}