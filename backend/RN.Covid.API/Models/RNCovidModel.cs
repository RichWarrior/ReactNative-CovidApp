using System;

namespace RN.Covid.API.Models
{
    public class RNCovidModel
    {
        public DateTime Date { get; set; }
        public decimal DailyTest { get; set; }
        public decimal DailyCase { get; set; }
        public decimal DailySick { get; set; }
        public decimal DailyDeath { get; set; }
        public decimal DailyHealing { get; set; }
        public decimal TotalTest { get; set; }
        public decimal TotalSick { get; set; }
        public decimal TotalDeath { get; set; }
        public decimal TotalIntensiveCare { get; set; }
        public decimal TotalIntubated { get; set; }
        public decimal SickPneumonioRate { get; set; }
        public decimal SeriouslySickCount { get; set; }
        public decimal BedOccupancyRate { get; set; }
        public decimal IntensiveCareOccupancy { get; set; }
        public decimal VentilatorOccupancyRate { get; set; }
        public decimal AverageFilmingTime { get; set; }
        public string AverageContactDetectionTime { get; set; }
        public decimal FilmingRate { get; set; }
    }
}
