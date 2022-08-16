using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjOtopark
{
    class Araba
    {
        string _sPlaka;
        double _dToplamTutar;
        DateTime _dtGirisSaati;
        DateTime _dtCikisSaati;
        bool _bDosyadanMiOkundu;
        public Araba(string sPlaka, DateTime dtGirisSaati)
        {
            _sPlaka = sPlaka.ToUpper();
            _dtGirisSaati = dtGirisSaati;
        }
        public void tutarHesapla()
        {
            double dBirimFiyat = 1;//saniyelik 1 kuruş
            _dtCikisSaati = DateTime.Now;
            var saniyeFarki = (_dtCikisSaati - _dtGirisSaati).TotalSeconds;
            _dToplamTutar = (double.Parse(saniyeFarki.ToString()) * dBirimFiyat)/100;
            _dToplamTutar = Math.Round(_dToplamTutar, 2);
        }
        public string sPlaka { get { return _sPlaka; } }
        public double dToplamTutar { get { return _dToplamTutar; } }
        public DateTime dtGirisSaati { get { return _dtGirisSaati; } }
        public DateTime dtCikisSaati { get { return _dtCikisSaati; } }
        public bool bDosyadanMiOkundu { get { return _bDosyadanMiOkundu; }set { _bDosyadanMiOkundu = value; } }
    }
}
