using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuristickaAgencija.Models
{
    public class Komentar
    {
        public static int id = 0;

        public static void Initialize(int posljednjiId)
        {
            id = posljednjiId + 1;
            
        }

        public Korisnik Turista { get; set; }//turista koji je ostavio komentar
        public Aranzman Aranzman { get; set; }//aranzman na koji se odnosi komentar
        public string TekstKomentar { get; set; }
        public int Ocena { get; set; }
        public string Odobren { get; set; }
        public int IdKomentara { get; set; }

        public Komentar() { Turista = new Korisnik();Aranzman = new Aranzman(); IdKomentara = id; }
    }
}