using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace prjOtopark
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Araba> lArabalar = new List<Araba>();
            if (File.Exists("arabalar.json"))
            {
                lArabalar = JSONoku();
            }
            int iSecim;
            do
            {
                Console.WriteLine("1- Araba girişi yap");
                Console.WriteLine("2- Araba çıkışı yap");
                Console.WriteLine("3- Otoparktaki Araba sayısı");
                Console.WriteLine("4- Otoparktaki Arabaları Göster");
                Console.WriteLine("5- Kaydet ve çıkış yap");
                iSecim = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (iSecim)
                {
                    case 1:
                        ArabaGirisi(lArabalar);
                        break;
                    case 2:
                        ArabaCikisi(lArabalar);
                        break;
                    case 3:
                        ArabaSayisiGoster(lArabalar);
                        break;
                    case 4:
                        ArabaListele(lArabalar);
                        break;
                }
            } while (iSecim != 5);
            JSONyaz(lArabalar);
            Console.WriteLine("Bitti. Kapatmak için bir tuşa basın.");
            Console.ReadKey();
        }
        public static void ArabaGirisi(List<Araba> lArabalar)
        {
            string sPlaka;
            Console.WriteLine("Arabanın plakasını giriniz(10 ABC 100):");
            sPlaka = Console.ReadLine();

            Araba araba = new Araba(sPlaka, DateTime.Now);
            lArabalar.Add(araba);

            Console.Clear();
            Console.WriteLine("----");
            Console.WriteLine(sPlaka + " plakalı arabanın girişi yapıldı!");
            Console.WriteLine("----");
        }
        public static void ArabaCikisi(List<Araba> arabaList)
        {
            int iSecim;
            ArabaListele(arabaList);
            Console.WriteLine("Çıkacak arabanın numarasını giriniz:");
            iSecim = int.Parse(Console.ReadLine())-1;
            Console.Clear();

            arabaList[iSecim].tutarHesapla();
            Console.WriteLine("-----");
            Console.WriteLine("{0,-20} : {1,-30}","Plaka", arabaList[iSecim].sPlaka);
            Console.WriteLine("{0,-20} : {1,-30}","Giriş Saati", arabaList[iSecim].dtGirisSaati);
            Console.WriteLine("{0,-20} : {1,-30}","Çıkış Saati",arabaList[iSecim].dtCikisSaati);
            Console.WriteLine("{0,-20} : {1,-30}","Ücret",arabaList[iSecim].dToplamTutar+" TL");
            Console.WriteLine("\t\t" + arabaList[iSecim].sPlaka + " plakalı arabanın çıkışı yapılmıştır.");
            Console.WriteLine("----");
            arabaList.RemoveAt(iSecim);
        }
        public static void ArabaSayisiGoster(List<Araba> lArabalar)
        {
            Console.WriteLine("----");
            Console.WriteLine("Otoparkta " + lArabalar.Count + " adet araba bulunmakta.");
            Console.WriteLine("----");
        }
        public static void ArabaListele(List<Araba> lArabalar)
        {
            int iSıraNo=1;
            Console.WriteLine("----");
            Console.WriteLine("  {0,-20} | {1,-50}","Plaka","Giriş Saati");
            foreach (Araba araba in lArabalar)
            {
                Console.WriteLine(iSıraNo+" {0,-20} : {1,-50}", araba.sPlaka, araba.dtGirisSaati.ToString());
                iSıraNo++;
            }
            Console.WriteLine("----");
        }
        public static List<Araba> JSONoku()
        {
            if (File.Exists("arabalar.json") && File.ReadAllText("arabalar.json").Length>0)
            {
                using (StreamReader r = new StreamReader("arabalar.json")) 
                {
                    string json = r.ReadToEnd();
                    JArray textArray = JArray.Parse(json);

                    List<Araba> items = JsonConvert.DeserializeObject<List<Araba>>(textArray.ToString());
                    return items;
                }
            }
            List<Araba> arabalar = new List<Araba>();
            return arabalar;
        }
        public static void JSONyaz(List<Araba> arabaList)
        {
            File.Delete("arabalar.json");
            string dosyaYolu = "arabalar.json";
            using(var tw = new StreamWriter(dosyaYolu,true))
            {
                tw.WriteLine("[");
                tw.Close();
            }
            foreach (Araba araba in arabaList)
            {
                araba.tutarHesapla();
                araba.bDosyadanMiOkundu = true;
                string JSONresult = JsonConvert.SerializeObject(araba);
                using (var tw = new StreamWriter(dosyaYolu, true))
                {
                    tw.WriteLine(JSONresult.ToString() + ",");   
                    tw.Close();
                }  
            }
            using (var tw = new StreamWriter(dosyaYolu, true))
            {
                tw.WriteLine("]");
                tw.Close();
            }
        }
    }
}
