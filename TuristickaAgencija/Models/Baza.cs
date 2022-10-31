using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace TuristickaAgencija.Models
{
    public class Baza
    {


        public static int uzmiId(string putanja)
        {
            int a = 0;
            putanja = HostingEnvironment.MapPath(putanja);
            FileStream stream = new FileStream(putanja, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string red = "";

            while ((red = sr.ReadLine()) != null)
            {
                string[] dio = red.Split(';');
                a = Convert.ToInt32(dio[0]);
            }
            a++;
            sr.Close();
            stream.Close();
            return a;
        }
        public static void DodajKomentarUBazu(Komentar k)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/komentari.txt");
            FileStream stream = new FileStream(putanja, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            StreamWriter sw = new StreamWriter(stream);

            sr.ReadToEnd();

            sw.WriteLine(k.IdKomentara + ";" + k.Turista.KorisnickoIme + ";" + k.TekstKomentar + ";" + k.Ocena + ";" + k.Odobren + ";" + k.Aranzman.IdAranzmana);
            sw.Close();
            sr.Close();

            stream.Close();
        }

        public static List<Komentar> UcitajKomentareIzBaze(string putanja)
        {
            List<Komentar> lista = new List<Komentar>();
            putanja = HostingEnvironment.MapPath(putanja);
            FileStream stream = new FileStream(putanja, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string red = "";

            while ((red = sr.ReadLine()) != null)
            {
                Komentar k = new Komentar();
                string[] dio = red.Split(';');
                k.IdKomentara = Convert.ToInt32(dio[0]);
                k.Turista.KorisnickoIme = dio[1];
                k.TekstKomentar = dio[2];
                k.Ocena = Convert.ToInt32(dio[3]);
                k.Odobren = dio[4];
                k.Aranzman.IdAranzmana = Convert.ToInt32(dio[5]);

                lista.Add(k);
            }
            sr.Close();
            stream.Close();

            return lista;
        }

        public static void DodajRezervacijuUBazu(Rezervacija r)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/rezervacije.txt");
            FileStream stream = new FileStream(putanja, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            StreamWriter sw = new StreamWriter(stream);

            sr.ReadToEnd();

            sw.WriteLine(r.Identifikator + ";" + r.Turista.KorisnickoIme + ";" + r.AranzmanZaKojiSeVrsiRezervacija.IdAranzmana + ";" +r.IzabranaSmjestajnaJedinica.Id + ";" + r.Status);

            sw.Close();
            sr.Close();

            stream.Close();
        }
        public static List<Rezervacija>  UcitajRezervacije(string putanja)
        {
            List<Rezervacija> lista = new List<Rezervacija>();
            putanja = HostingEnvironment.MapPath(putanja);

           
            
            FileStream stream = new FileStream(putanja, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string red = "";


            while ((red = sr.ReadLine()) != null)
            {
                Rezervacija r = new Rezervacija();
                Korisnik k = new Korisnik();
                Aranzman a = new Aranzman();
                SmjestajnaJedinica sj = new SmjestajnaJedinica();

                string[] dio = red.Split(';');
                r.Identifikator = dio[0];
                k.KorisnickoIme = dio[1];
                r.Turista = k;
                a.IdAranzmana = Convert.ToInt32(dio[2]);
                r.AranzmanZaKojiSeVrsiRezervacija = a;
                sj.Id = Convert.ToInt32(dio[3]);
                r.IzabranaSmjestajnaJedinica = sj;
                r.Status = dio[4];
                lista.Add(r);
            }
            sr.Close();
            stream.Close();

            return lista;

        }

        public static List<SmjestajnaJedinica> UcitajSmjestajneJedinice(string putanja)
        {
            List<SmjestajnaJedinica> lista = new List<SmjestajnaJedinica>();

               putanja = HostingEnvironment.MapPath(putanja);
           
                FileStream stream = new FileStream(putanja, FileMode.Open);
                StreamReader sr = new StreamReader(stream);
                string red = "";

                while ((red = sr.ReadLine()) != null)
                {
                    string[] dio = red.Split(';');
                SmjestajnaJedinica sj = new SmjestajnaJedinica(Convert.ToInt32(dio[2]), dio[1], Convert.ToDouble(dio[3]), Convert.ToInt32(dio[0]));
                sj.Id = Convert.ToInt32(dio[4]);
                sj.Zauzeto = dio[5];
                    lista.Add(sj);
                }
                sr.Close();
                stream.Close();
 
            return lista;
        }
        public static void DodajSmjestajnuJedinicuUBazu(SmjestajnaJedinica sj)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/smjestajnejedinice.txt");
            FileStream stream = new FileStream(putanja, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            StreamWriter sw = new StreamWriter(stream);

            sr.ReadToEnd();
            sw.WriteLine(sj.PripadaSmjestaju + ";" + sj.DozvoljeniLjubimci + ";" + sj.DozvoljenBrojGostiju + ";" + sj.CenaSmestajneJedinice + ";" + sj.Id + ";" + sj.Zauzeto);

            sw.Close();
            sr.Close();

            stream.Close();

        }

        public static void DodajKorisnikaUBazu(Korisnik k)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/korisnici.txt");

            FileStream stream = new FileStream(putanja, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            StreamWriter sw = new StreamWriter(stream);

            sr.ReadToEnd();
            sw.WriteLine(k.KorisnickoIme + ";" + k.Ime + ";" + k.Prezime + ";" + k.Lozinka + ";" + k.Pol + ";" + k.Uloga + ";" + k.Email + ";" + k.Datum.ToString("dd/MM/yyyy")+ ";" + k.Blokiran);

            sw.Close();
            sr.Close();

            stream.Close();
        }

        public static void DodajAranzmanUBazu(Aranzman a)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/aranzmani.txt");

            FileStream stream = new FileStream(putanja, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            StreamWriter sw = new StreamWriter(stream);

            sr.ReadToEnd();
            sw.WriteLine(a.NazivAranzmana + ";" + a.Tip_aranzmana + ";" + a.Tip_prevoza + ";" + a.Lokacija + ";" + a.DatumPocetkaPutovanja.ToString("dd/MM/yyyy") + ";" + a.DatumZavrsetkaPutovanja.ToString("dd/MM/yyyy") + ";" + a.MjestoNalazenja.Adresa + ";" + a.MjestoNalazenja.GeografksaDuzina + ";" + a.MjestoNalazenja.GeografksaSirina + ";" + a.VrijemeNalazenje.ToString("HH:mm") + ";" + a.MaxBrojPutnika + ";" + a.OpisAranzmana + ";" + a.ProgramPutovanja + ";" + a.Smjestaj.IdSmjestaja + ";" + a.IdAranzmana + ";" + a.PosterAranzmana + ";"+ a.PripadaKorisniku);
            sw.Close();
            sr.Close();

            stream.Close();
        }

        public static void DodajSmjestajUBazu(Smjestaj s)
        {
            string putanja = HostingEnvironment.MapPath("~/App_Data/smjestaji.txt");

            FileStream stream = new FileStream(putanja, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            StreamWriter sw = new StreamWriter(stream);

            sr.ReadToEnd();
            sw.WriteLine(s.IdSmjestaja + ";" + s.NazivSmjestaja + ";" + s.Tip_smjestaja + ";" + s.BrojZvjezdica + ";" + s.Bazen + ";" + s.SpaCentar + ";" + s.WiFi + ";" + s.PrilagodjenostOsobamaSaInvaliditetom);
            sw.Close();
            sr.Close();

            stream.Close();
        }

        public static List<Aranzman> UcitajAranzmaneIzBaze(string putanja)
        {
            List<Aranzman> aranzmani = new List<Aranzman>();

            putanja = HostingEnvironment.MapPath(putanja);
            FileStream stream = new FileStream(putanja, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string red = "";

            while ((red = sr.ReadLine()) != null)
            {
                string[] dio = red.Split(';');
                string[] sati = dio[9].Split(':');
                DateTime dt = new DateTime(2021, 10, 2, Convert.ToInt32(sati[0]), Convert.ToInt32(sati[1]), 0);
              
              
                MjestoNalazenja mn = new MjestoNalazenja(dio[6], Convert.ToDouble(dio[7]), Convert.ToDouble(dio[8]));
                Aranzman a = new Aranzman(dio[0], (TIP_ARANZMANA)Enum.Parse(typeof(TIP_ARANZMANA), dio[1]), (TIP_PREVOZA)Enum.Parse(typeof(TIP_PREVOZA), dio[2]), (LOKACIJA)Enum.Parse(typeof(LOKACIJA), dio[3]), DateTime.ParseExact(dio[4], "dd/MM/yyyy", CultureInfo.CurrentCulture), DateTime.ParseExact(dio[5], "dd/MM/yyyy", CultureInfo.CurrentCulture), mn, dt, Convert.ToInt32(dio[10]), dio[11], dio[12], Convert.ToInt32(dio[13]),Convert.ToInt32(dio[14]),dio[15]);
                a.PripadaKorisniku = dio[16];
                Smjestaj s = new Smjestaj();
                s.IdSmjestaja = Convert.ToInt32(dio[13]);
                a.Smjestaj = s;
                aranzmani.Add(a);

            }
            sr.Close();
            stream.Close();
            return aranzmani;
        }

        public static Dictionary<string,Korisnik> UcitajKorisnikeIzBaze(string putanja)
        {
            Dictionary<string, Korisnik> korisnici = new Dictionary<string, Korisnik>();

            putanja = HostingEnvironment.MapPath(putanja);
            FileStream stream = new FileStream(putanja, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string red = "";
            while ((red = sr.ReadLine()) != null)
            {
                string[] dio = red.Split(';');
               
                Korisnik k = new Korisnik(dio[0], dio[3], dio[1], dio[2], (POL)Enum.Parse(typeof(POL), dio[4]), dio[6], DateTime.ParseExact(dio[7], "dd/MM/yyyy", CultureInfo.CurrentCulture));
                
                if ((ULOGA)Enum.Parse(typeof(ULOGA), dio[5]) == ULOGA.ADMINISTRATOR)
                {
                    k.Uloga = ULOGA.ADMINISTRATOR;
                }
                else if((ULOGA)Enum.Parse(typeof(ULOGA), dio[5]) == ULOGA.MENADZER)
                {
                    k.Uloga = ULOGA.MENADZER;
                }
                else
                {
                    k.Uloga = ULOGA.TURISTA;
                }
                k.Blokiran = dio[8];
                korisnici.Add(k.KorisnickoIme, k);
            }
            sr.Close();
            stream.Close();


            return korisnici;
        }

        public static List<Smjestaj> UcitajSmjestajeIzBaze(string putanja)
        {
            List<Smjestaj> lista = new List<Smjestaj>();

            putanja = HostingEnvironment.MapPath(putanja);
            FileStream stream = new FileStream(putanja, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            string red = "";
            while ((red = sr.ReadLine()) != null)
            {
                string[] dio = red.Split(';');

                Smjestaj s = new Smjestaj(Convert.ToInt32(dio[0]), (TIP_SMESTAJA)Enum.Parse(typeof(TIP_SMESTAJA), dio[2]), dio[1],Convert.ToInt32(dio[3]), dio[4], dio[5], dio[7], dio[6]);
                lista.Add(s);
            }
            sr.Close();
            stream.Close();

            return lista;
        }

        
    }
}