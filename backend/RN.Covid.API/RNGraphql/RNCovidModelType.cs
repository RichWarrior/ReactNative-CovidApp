using GraphQL.Types;
using RN.Covid.API.Models;

namespace RN.Covid.API.RNGraphql
{
    public class RNCovidModelType : ObjectGraphType<RNCovidModel>
    {
        public RNCovidModelType()
        {
            Name = "Covid";
            Field(f => f.Date);
            Field(f => f.DailyTest);
            Field(f => f.DailyCase);
            Field(f => f.DailySick);
            Field(f => f.DailyDeath);
            Field(f => f.DailyHealing);
            Field(f => f.TotalTest);
            Field(f => f.TotalSick);
            Field(f => f.TotalDeath);
            Field(f => f.TotalIntensiveCare);
            Field(f => f.TotalIntubated);
            Field(f => f.SickPneumonioRate);
            Field(f => f.SeriouslySickCount);
            Field(f => f.BedOccupancyRate);
            Field(f => f.IntensiveCareOccupancy);
            Field(f => f.VentilatorOccupancyRate);
            Field(f => f.AverageFilmingTime);
            Field(f => f.AverageContactDetectionTime);
            Field(f => f.FilmingRate);
        }
    }
}
