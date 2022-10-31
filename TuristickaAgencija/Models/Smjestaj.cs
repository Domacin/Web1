using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuristickaAgencija.Models
{
    public enum TIP_SMESTAJA { HOTEL,MOTEL,VILA};
    public class Smjestaj
    {
        public static int id = 0;

        public static void Initialize(int posljednjiId)
        {
            id = posljednjiId + 1;
        }


        public int PripadaSmjestajnojJedinici { get; set; }
        public int IdSmjestaja { get; set; }
        public bool BarJednaSMjestajnaJedinicaZauzeta { get; set; } = false;
        public TIP_SMESTAJA Tip_smjestaja { get; set; }
        public string NazivSmjestaja { get; set;}
        public int BrojZvjezdica { get; set; }
        public string Bazen { get; set; }
        public string SpaCentar { get; set; }
        public string PrilagodjenostOsobamaSaInvaliditetom { get; set; }
        public string WiFi { get; set; }
        public List<SmjestajnaJedinica> listaSmjestajnihJedinica { get; set; }

        public Smjestaj(TIP_SMESTAJA tip_smjestaja, string nazivSmjestaja, int brojZvjezdica, string bazen, string spaCentar, string prilagodjenostOsobamaSaInvaliditetom, string wiFi)
        {
            Tip_smjestaja = tip_smjestaja;
            NazivSmjestaja = nazivSmjestaja;
            BrojZvjezdica = brojZvjezdica;
            Bazen = bazen;
            SpaCentar = spaCentar;
            PrilagodjenostOsobamaSaInvaliditetom = prilagodjenostOsobamaSaInvaliditetom;
            WiFi = wiFi;
            IdSmjestaja = id;
            id++;
            listaSmjestajnihJedinica = new List<SmjestajnaJedinica>();
            
        }
        public Smjestaj()
        {
            IdSmjestaja = id;
            listaSmjestajnihJedinica = new List<SmjestajnaJedinica>();
        }

        public Smjestaj(int idSmjestaja, TIP_SMESTAJA tip_smjestaja, string nazivSmjestaja, int brojZvjezdica, string bazen, string spaCentar, string prilagodjenostOsobamaSaInvaliditetom, string wiFi)
        {
            IdSmjestaja = idSmjestaja;
            Tip_smjestaja = tip_smjestaja;
            NazivSmjestaja = nazivSmjestaja;
            BrojZvjezdica = brojZvjezdica;
            Bazen = bazen;
            SpaCentar = spaCentar;
            PrilagodjenostOsobamaSaInvaliditetom = prilagodjenostOsobamaSaInvaliditetom;
            WiFi = wiFi;
            listaSmjestajnihJedinica = new List<SmjestajnaJedinica>();
            id++;
        }
    }
}