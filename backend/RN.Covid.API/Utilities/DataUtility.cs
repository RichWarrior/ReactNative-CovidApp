using HtmlAgilityPack;
using Newtonsoft.Json;
using RN.Covid.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RN.Covid.API.Utilities
{
    public static class DataUtility
    {
        const string BASE_URL = "https://covid19.saglik.gov.tr/TR-66935/genel-koronavirus-tablosu.html";
        public static List<RNCovidModel> GetAllData()
        {
            List<RNCovidModel> result = new List<RNCovidModel>();
            try
            {
                HtmlWeb web = new HtmlWeb();
                var doc = web.Load(BASE_URL);
                var scripts = doc.DocumentNode.Descendants("script");
                var currentScript = (scripts).LastOrDefault(x => x.InnerText.Contains("geneldurumjson"));
                if (currentScript != null)
                {
                    var startIndex = currentScript.InnerText.IndexOf('{') - 1;
                    var endIndex = currentScript.InnerText.LastIndexOf(';');
                    if (startIndex > -1 && endIndex > -1)
                    {
                        var json = currentScript.InnerText.Substring(startIndex, endIndex - startIndex);
                        var dummyModel = JsonConvert.DeserializeObject<List<DummyCovidModel>>(json);
                        result = dummyModel.Select(x => new RNCovidModel
                        {
                            Date = DateTime.Parse(x.tarih.Replace(".", "/")),
                            DailyTest = String.IsNullOrEmpty(x.gunluk_test) ? 0 : decimal.Parse(x.gunluk_test),
                            DailyCase = String.IsNullOrEmpty(x.gunluk_vaka) ? 0 : decimal.Parse(x.gunluk_vaka),
                            DailySick = String.IsNullOrEmpty(x.gunluk_hasta) ? 0 : decimal.Parse(x.gunluk_hasta),
                            DailyDeath = String.IsNullOrEmpty(x.gunluk_vefat) ? 0 : decimal.Parse(x.gunluk_vefat),
                            DailyHealing = String.IsNullOrEmpty(x.gunluk_iyilesen) ? 0 : decimal.Parse(x.gunluk_iyilesen),
                            TotalTest = String.IsNullOrEmpty(x.toplam_test) ? 0 : decimal.Parse(x.toplam_test),
                            TotalSick = String.IsNullOrEmpty(x.toplam_hasta) ? 0 : decimal.Parse(x.toplam_hasta),
                            TotalDeath = String.IsNullOrEmpty(x.toplam_vefat) ? 0 : decimal.Parse(x.toplam_vefat),
                            TotalIntensiveCare = String.IsNullOrEmpty(x.toplam_yogun_bakim) ? 0 : decimal.Parse(x.toplam_yogun_bakim),
                            TotalIntubated = String.IsNullOrEmpty(x.toplam_entube) ? 0 : decimal.Parse(x.toplam_entube),
                            SickPneumonioRate = String.IsNullOrEmpty(x.hastalarda_zaturre_oran) ? 0 : decimal.Parse(x.hastalarda_zaturre_oran),
                            SeriouslySickCount = String.IsNullOrEmpty(x.agir_hasta_sayisi) ? 0 : decimal.Parse(x.agir_hasta_sayisi),
                            BedOccupancyRate = String.IsNullOrEmpty(x.yatak_doluluk_orani) ? 0 : decimal.Parse(x.yatak_doluluk_orani),
                            IntensiveCareOccupancy = String.IsNullOrEmpty(x.eriskin_yogun_bakim_doluluk_orani) ? 0 : decimal.Parse(x.eriskin_yogun_bakim_doluluk_orani),
                            VentilatorOccupancyRate = String.IsNullOrEmpty(x.ventilator_doluluk_orani) ? 0 : decimal.Parse(x.ventilator_doluluk_orani),
                            AverageFilmingTime = String.IsNullOrEmpty(x.ortalama_filyasyon_suresi) ? 0 : decimal.Parse(x.ortalama_filyasyon_suresi),
                            AverageContactDetectionTime = x.ortalama_temasli_tespit_suresi,
                            FilmingRate = String.IsNullOrEmpty(x.filyasyon_orani) ? 0 : decimal.Parse(x.filyasyon_orani)

                        }).ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
