using GraphQL.Types;
using RN.Covid.API.Models;
using RN.Covid.API.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RN.Covid.API.RNGraphql
{
    public class GraphqlCovidQuery : ObjectGraphType<object>
    {
        public GraphqlCovidQuery()
        {
            Name = "Covid_Query";

            Field<ListGraphType<RNCovidModelType>>(
               "getDataDaily",
               resolve: ctx => DataUtility.GetAllData()
               );
            Field<ListGraphType<RNCovidModelType>>(
                "getDataBetweenDatesDaily",
                arguments: new QueryArguments
                {
                    new QueryArgument<DateGraphType>
                    {
                        Name="startDate"
                    },
                    new QueryArgument<DateGraphType>
                    {
                        Name="endDate"
                    }
                },
                resolve: ctx =>
                {
                    object _startDate = null;
                    object _endDate = null;
                    ctx.Arguments.TryGetValue("startDate", out _startDate);
                    ctx.Arguments.TryGetValue("endDate", out _endDate);
                    DateTime startDate = (DateTime)_startDate;
                    DateTime endDate = (DateTime)_endDate;
                    if (endDate < startDate)
                        ctx.Errors.Add(new GraphQL.ExecutionError("Date Error"));
                    var datas = DataUtility.GetAllData();
                    return datas.Where(x => x.Date >= startDate && x.Date <= endDate).OrderByDescending(x => x.Date);
                }
            );

            Field<ListGraphType<RNCovidModelType>>(
                "getDataMonthly",
                resolve: ctx =>
                {
                    var data = DataUtility.GetAllData();
                    var returnData = new List<RNCovidModel>();
                    var groupBy = data.GroupBy(x => x.Date.Month);
                    foreach (var item in groupBy)
                    {
                        returnData.Add(GetGroupBy(item));
                    }
                    return returnData;
                }
                );

            Field<ListGraphType<RNCovidModelType>>(
                "getDataMonthlyBetweenDates",
                arguments: new QueryArguments
                {
                    new QueryArgument<DateGraphType>
                    {
                        Name="startDate"
                    },
                    new QueryArgument<DateGraphType>
                    {
                        Name="endDate"
                    }
                },
                resolve: ctx =>
                {
                    object _startDate = null;
                    object _endDate = null;
                    ctx.Arguments.TryGetValue("startDate", out _startDate);
                    ctx.Arguments.TryGetValue("endDate", out _endDate);
                    DateTime startDate = (DateTime)_startDate;
                    DateTime endDate = (DateTime)_endDate;
                    if (endDate < startDate)
                        ctx.Errors.Add(new GraphQL.ExecutionError("Date Error"));
                    var groupBy = DataUtility.GetAllData().Where(x => x.Date >= startDate && x.Date <= endDate).OrderByDescending(x => x.Date).GroupBy(x=>x.Date.Month);
                    List<RNCovidModel> items = new List<RNCovidModel>();
                    foreach (var item in groupBy)
                    {
                        items.Add(GetGroupBy(item));
                    }
                    return items;
                }
            );

            Field<ListGraphType<RNCovidModelType>>(
               "getDataYear",
               resolve: ctx =>
               {
                   var data = DataUtility.GetAllData();
                   var returnData = new List<RNCovidModel>();
                   var groupBy = data.GroupBy(x => x.Date.Year);
                   foreach (var item in groupBy)
                   {
                       returnData.Add(GetGroupBy(item));
                   }
                   return returnData;
               }
               );

            Field<ListGraphType<RNCovidModelType>>(
              "getDataYearBetweenDates",
              arguments: new QueryArguments
              {
                    new QueryArgument<DateGraphType>
                    {
                        Name="startDate"
                    },
                    new QueryArgument<DateGraphType>
                    {
                        Name="endDate"
                    }
              },
              resolve: ctx =>
              {
                  object _startDate = null;
                  object _endDate = null;
                  ctx.Arguments.TryGetValue("startDate", out _startDate);
                  ctx.Arguments.TryGetValue("endDate", out _endDate);
                  DateTime startDate = (DateTime)_startDate;
                  DateTime endDate = (DateTime)_endDate;
                  if (endDate < startDate)
                      ctx.Errors.Add(new GraphQL.ExecutionError("Date Error"));
                  var groupBy = DataUtility.GetAllData().Where(x => x.Date >= startDate && x.Date <= endDate).OrderByDescending(x => x.Date).GroupBy(x => x.Date.Year);
                  List<RNCovidModel> items = new List<RNCovidModel>();
                  foreach (var item in groupBy)
                  {
                      items.Add(GetGroupBy(item));
                  }
                  return items;
              }
          );
        }

        private RNCovidModel GetGroupBy(IGrouping<int, RNCovidModel>item)
        {
            return new RNCovidModel()
            {
                Date = item.FirstOrDefault().Date,
                DailyTest = item.Sum(x => x.DailyTest),
                DailyCase = item.Sum(x => x.DailyCase),
                DailySick = item.Sum(x => x.DailySick),
                DailyDeath = item.Sum(x => x.DailyDeath),
                DailyHealing = item.Sum(x => x.DailyHealing),
                TotalTest = item.Sum(x => x.TotalTest),
                TotalSick = item.Sum(x => x.TotalSick),
                TotalDeath = item.Sum(x => x.TotalDeath),
                TotalIntensiveCare = item.Sum(x => x.TotalIntensiveCare),
                TotalIntubated = item.Sum(x => x.TotalIntubated),
                SickPneumonioRate = item.Sum(x => x.SickPneumonioRate),
                SeriouslySickCount = item.Sum(x => x.SeriouslySickCount),
                BedOccupancyRate = item.Sum(x => x.BedOccupancyRate),
                IntensiveCareOccupancy = item.Sum(x => x.IntensiveCareOccupancy),
                VentilatorOccupancyRate = item.Sum(x => x.VentilatorOccupancyRate),
                AverageFilmingTime = item.Sum(x => x.AverageFilmingTime),
                AverageContactDetectionTime = item.FirstOrDefault().AverageContactDetectionTime,
                FilmingRate = item.Sum(x => x.FilmingRate)
            };
        }
    }
}
