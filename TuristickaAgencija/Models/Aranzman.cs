using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace TuristickaAgencija.Models
{
    public enum TIP_ARANZMANA { NOCENJE_SA_DORUCKOM,POLUPANSION,PUN_PANSION,ALL_INCLUSIVE,NAJAM_APARTMANA};
    public enum TIP_PREVOZA { AUTOBUS,AVION,AUTOBUS_AVION,INDIVIDUALAN,OSTALO};
    public enum LOKACIJA { GRAD,DRZAVA,REGIJA};
    public class Aranzman
    {
       public static int ida = 0;

        public static void Initialize(int posljednjiId)
        {
            ida = posljednjiId + 1;
        }

        public int IdAranzmana { get; set; }

        public string NazivAranzmana { get; set; }
        public TIP_ARANZMANA Tip_aranzmana { get; set; }
        public TIP_PREVOZA Tip_prevoza { get; set; }
        public LOKACIJA Lokacija { get; set; }
        public DateTime DatumPocetkaPutovanja { get; set; }
        public DateTime DatumZavrsetkaPutovanja { get; set; }
        public MjestoNalazenja MjestoNalazenja { get; set; }
        public DateTime VrijemeNalazenje { get; set; } //format HH:mm
        public int MaxBrojPutnika { get; set; }
        public string OpisAranzmana { get; set; }
        public string ProgramPutovanja { get; set; }
        public string PosterAranzmana { get; set; }
        public Smjestaj Smjestaj { get; set; }
        public double CijenaAranzmana { get; set; }
        public int IdSmjestaja { get; set; }
        public string PripadaKorisniku { get; set; }
        public Aranzman() { IdAranzmana = ida; }

        public Aranzman(string nazivAranzmana, TIP_ARANZMANA tip_aranzmana, TIP_PREVOZA tip_prevoza, LOKACIJA lokacija, DateTime datumPocetkaPutovanja, DateTime datumZavrsetkaPutovanja, MjestoNalazenja mjestoNalazenja, DateTime vrijemeNalazenje, int maxBrojPutnika, string opisAranzmana, string programPutovanja,int idsmjestaja, string putanjadoslike)
        {
            NazivAranzmana = nazivAranzmana;
            Tip_aranzmana = tip_aranzmana;
            Tip_prevoza = tip_prevoza;
            Lokacija = lokacija;
            DatumPocetkaPutovanja = datumPocetkaPutovanja;
            DatumZavrsetkaPutovanja = datumZavrsetkaPutovanja;
            MjestoNalazenja = mjestoNalazenja;
            VrijemeNalazenje = vrijemeNalazenje;
            MaxBrojPutnika = maxBrojPutnika;
            OpisAranzmana = opisAranzmana;
            ProgramPutovanja = programPutovanja;
            PosterAranzmana = putanjadoslike;
            IdSmjestaja = idsmjestaja;
            IdAranzmana = ida;
            ida++;
        }

        public Aranzman(string nazivAranzmana, TIP_ARANZMANA tip_aranzmana, TIP_PREVOZA tip_prevoza, LOKACIJA lokacija, DateTime datumPocetkaPutovanja, DateTime datumZavrsetkaPutovanja, MjestoNalazenja mjestoNalazenja, DateTime vrijemeNalazenje, int maxBrojPutnika, string opisAranzmana, string programPutovanja, int idsmjestaja, int idaranzmana, string putanjadoslike)
        {
            NazivAranzmana = nazivAranzmana;
            Tip_aranzmana = tip_aranzmana;
            Tip_prevoza = tip_prevoza;
            Lokacija = lokacija;
            DatumPocetkaPutovanja = datumPocetkaPutovanja;
            DatumZavrsetkaPutovanja = datumZavrsetkaPutovanja;
            MjestoNalazenja = mjestoNalazenja;
            VrijemeNalazenje = vrijemeNalazenje;
            MaxBrojPutnika = maxBrojPutnika;
            OpisAranzmana = opisAranzmana;
            ProgramPutovanja = programPutovanja;
            PosterAranzmana = putanjadoslike;
            IdSmjestaja = idsmjestaja;
            IdAranzmana = idaranzmana;
          
        }


        //za pretragu koristim
        public DateTime maxPocetak { get; set; }
        public DateTime minPocetak { get; set; }
        public DateTime minKraj { get; set; }
        public DateTime maxKraj { get; set; }
    }
}