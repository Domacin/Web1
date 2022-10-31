using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuristickaAgencija.Models
{
    public class SmjestajnaJedinica
    {
        
       
        public static int id = 0;
        public static void Initialize(int posljednjiId)
        {
            id = posljednjiId + 1;
        }
        public SmjestajnaJedinica(int dozvoljenBrojGostiju, string dozvoljeniLjubimci, double cena)
        {
            DozvoljenBrojGostiju = dozvoljenBrojGostiju;
            DozvoljeniLjubimci = dozvoljeniLjubimci;
            CenaSmestajneJedinice = cena;
            Id = id;
            id++;
        }

        public SmjestajnaJedinica(int brGostiju,string ljubimci,double cena, int id)
        {
            DozvoljenBrojGostiju = brGostiju;
            DozvoljeniLjubimci = ljubimci;
            CenaSmestajneJedinice = cena;
            PripadaSmjestaju = id;
            id++;
        }

        public SmjestajnaJedinica() { Id = id; }
       
        public int DozvoljenBrojGostiju { get; set; }
        public string DozvoljeniLjubimci { get; set; }
        public double CenaSmestajneJedinice { get; set; }
        public int Id { get; set; } 
        public int PripadaSmjestaju { get; set; }
        public string Zauzeto { get; set; }


        public int DozvoljenBrojGostijuMin { get; set; }
        public int DozvoljenBrojGostijuMax { get; set; }
    }
}