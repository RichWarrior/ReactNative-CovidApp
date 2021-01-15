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
                var currentScript = (scripts).LastOrDefault(item => item.InnerText.Contains("geneldurumjson"));
                if (currentScript != null)
                {
                    var startIndeitem = currentScript.InnerText.IndexOf('{') - 1;
                    var endIndeitem = currentScript.InnerText.LastIndexOf(';');
                    if (startIndeitem > -1 && endIndeitem > -1)
                    {
                        var json = currentScript.InnerText.Substring(startIndeitem, endIndeitem - startIndeitem);
                        var dummyModel = JsonConvert.DeserializeObject<List<DummyCovidModel>>(json);
                        foreach (var item in dummyModel)
                        {
                            item.tarih = item.tarih.Replace(".", "/");
                            var pieces = item.tarih.Split('/');
                            var model = new RNCovidModel();
                            model.Date = new DateTime(int.Parse(pieces[2]), int.Parse(pieces[1]),int.Parse(pieces[0]), 0, 0, 0);
                            model.DailyTest = String.IsNullOrEmpty(item.gunluk_test) ? 0 : decimal.Parse(FormatNumber(item.gunluk_test));
                            model.DailyCase = String.IsNullOrEmpty(item.gunluk_vaka) ? 0 : decimal.Parse(FormatNumber(item.gunluk_vaka));
                            model.DailySick = String.IsNullOrEmpty(item.gunluk_hasta) ? 0 : decimal.Parse(FormatNumber(item.gunluk_hasta));
                            model.DailyDeath = String.IsNullOrEmpty(item.gunluk_vefat) ? 0 : decimal.Parse(FormatNumber(item.gunluk_vefat));
                            model.DailyHealing = String.IsNullOrEmpty(item.gunluk_iyilesen) ? 0 : decimal.Parse(FormatNumber(item.gunluk_iyilesen));
                            model.TotalTest = String.IsNullOrEmpty(item.toplam_test) ? 0 : decimal.Parse(FormatNumber(item.toplam_test));
                            model.TotalSick = String.IsNullOrEmpty(item.toplam_hasta) ? 0 : decimal.Parse(FormatNumber(item.toplam_hasta));
                            model.TotalDeath = String.IsNullOrEmpty(item.toplam_vefat) ? 0 : decimal.Parse(FormatNumber(item.toplam_vefat));
                            model.TotalIntensiveCare = String.IsNullOrEmpty(item.toplam_yogun_bakim) ? 0 : decimal.Parse(FormatNumber(item.toplam_yogun_bakim));
                            model.TotalIntubated = String.IsNullOrEmpty(item.toplam_entube) ? 0 : decimal.Parse(FormatNumber(item.toplam_entube));
                            model.SickPneumonioRate = String.IsNullOrEmpty(item.hastalarda_zaturre_oran) ? 0 : decimal.Parse(FormatNumber(item.hastalarda_zaturre_oran));
                            model.SeriouslySickCount = String.IsNullOrEmpty(item.agir_hasta_sayisi) ? 0 : decimal.Parse(FormatNumber(item.agir_hasta_sayisi));
                            model.BedOccupancyRate = String.IsNullOrEmpty(item.yatak_doluluk_orani) ? 0 : decimal.Parse(FormatNumber(item.yatak_doluluk_orani));
                            model.IntensiveCareOccupancy = String.IsNullOrEmpty(item.eriskin_yogun_bakim_doluluk_orani) ? 0 : decimal.Parse(FormatNumber(item.eriskin_yogun_bakim_doluluk_orani));
                            model.VentilatorOccupancyRate = String.IsNullOrEmpty(item.ventilator_doluluk_orani) ? 0 : decimal.Parse(FormatNumber(item.ventilator_doluluk_orani));
                            model.AverageFilmingTime = String.IsNullOrEmpty(item.ortalama_filyasyon_suresi) ? 0 : decimal.Parse(FormatNumber(item.ortalama_filyasyon_suresi));
                            model.AverageContactDetectionTime = item.ortalama_temasli_tespit_suresi;
                            model.FilmingRate = String.IsNullOrEmpty(item.filyasyon_orani) ? 0 : decimal.Parse(FormatNumber(item.filyasyon_orani));
                            result.Add(model);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public static string FormatNumber(string number)
        {
            var pieces = number.Split('.');
            if (pieces.Length > 2)
            {
                string result = "";
                for (var i = 0; i < pieces.Length; i++)
                {
                    if (i == pieces.Length - 1)
                    {
                        result += "." + pieces[i];
                    }
                    else
                        result += pieces[i];
                }
                return result;
            }
            return number;
        }
    }
}
