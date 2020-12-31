using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RN.Covid.API.Models
{
    public class DummyCovidModel
    {
        public string tarih { get; set; }
        public string gunluk_test { get; set; }
        public string gunluk_vaka { get; set; }
        public string gunluk_hasta { get; set; }
        public string gunluk_vefat { get; set; }
        public string gunluk_iyilesen { get; set; }
        public string toplam_test { get; set; }
        public string toplam_hasta { get; set; }
        public string toplam_vefat { get; set; }
        public string toplam_iyilesen { get; set; }
        public string toplam_yogun_bakim { get; set; }
        public string toplam_entube { get; set; }
        public string hastalarda_zaturre_oran { get; set; }
        public string agir_hasta_sayisi { get; set; }
        public string yatak_doluluk_orani { get; set; }
        public string eriskin_yogun_bakim_doluluk_orani { get; set; }
        public string ventilator_doluluk_orani { get; set; }
        public string ortalama_filyasyon_suresi { get; set; }
        public string ortalama_temasli_tespit_suresi { get; set; }
        public string filyasyon_orani { get; set; }
    }
}
