using AngleSharp;
using OverwatchDotNet.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace OverwatchDotNet.OverwatchData
{
    public enum Region { NA, EU, OCE }

    public class OverwatchDataTable
    {
        public string Name { get; set; }
        public Dictionary<string, string> Stats { get; set; }
    }

    public class PlayerStats
    {
        public string BattleTag { get; private set; }
        
        public FeaturedStats Featured { get; private set; }

        [OverwatchStatGroup("Game")]
        public GameStats Game { get; private set; }

        [OverwatchStatGroup("Assists")]
        public AssistsStats Assists { get; private set; }

        [OverwatchStatGroup("Average")]
        public AverageStats Average { get; private set; }

        [OverwatchStatGroup("Combat")]
        public CombatStats Combat { get; private set; }

        [OverwatchStatGroup("Deaths")]
        public DeathStats Deaths { get; private set; }

        [OverwatchStatGroup("Match Awards")]
        public MatchAwardsStats MatchAwards { get; private set; }

        [OverwatchStatGroup("Miscellaneous")]
        public MiscellaneousStats Miscellaneous { get; private set; }

        [OverwatchStatGroup("Best")]
        public BestStats Best { get; private set; }

        public void PopulatePlayer(string url)
        {
            var props = GetType().GetProperties().Where(prop => Attribute.IsDefined(prop,typeof(OverwatchStatGroup)));
            var playerTables = GetOverwatchStats(url);
            foreach(var item in playerTables)
            {
                var prop = props.FirstOrDefault(x => ((OverwatchStatGroup)x.GetCustomAttribute(typeof(OverwatchStatGroup))).StatGroupName == item.Name);
                if (prop != null)
                {
                    PopulateStat(prop, item);
                }
            }
        }

        void PopulateStat(PropertyInfo prop, OverwatchDataTable table)
        {
            var statGroup = Activator.CreateInstance(prop.PropertyType);
            var statGroupProps = statGroup.GetType().GetProperties().Where(p => Attribute.IsDefined(p, typeof(OverwatchStat)));
            foreach(var item in table.Stats)
            {
                var statProp = statGroupProps.FirstOrDefault(x => ((OverwatchStat)x.GetCustomAttribute(typeof(OverwatchStat))).StatName == item.Key);
                if(statProp != null)
                {
                    statProp.SetValue(statGroup, ParseValue(item.Value));
                }                
            }
            prop.SetValue(this, statGroup);
        }

        object ParseValue(string input)
        {
            string cleanInput = input.Replace(",", "").ToLower();
            if (cleanInput.Contains("."))
                return float.Parse(cleanInput);
            else if (cleanInput.Contains("hour"))
                return TimeSpan.FromHours(int.Parse(cleanInput.Substring(0, cleanInput.IndexOf(" ") - 1)));
            else if (cleanInput.Contains(":"))
            {
                TimeSpan outputTime;
                if (TimeSpan.TryParseExact(cleanInput, @"mm\:ss", CultureInfo.CurrentCulture, out outputTime))
                    return outputTime;
                else if (TimeSpan.TryParseExact(cleanInput, @"hh\:mm\:ss", CultureInfo.CurrentCulture, out outputTime))
                    return outputTime;
            }
            else
                return int.Parse(cleanInput);
            return null;
        }

        IEnumerable<OverwatchDataTable> GetOverwatchStats(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var document = BrowsingContext.New(config).OpenAsync(url).Result;

            List<OverwatchDataTable> tables = new List<OverwatchDataTable>();
               
            foreach (var section in document.QuerySelectorAll("div[data-category-id='0x02E00000FFFFFFFF'] table.data-table"))
            {
                var table = new Dictionary<string, string>();
                foreach (var row in section.QuerySelectorAll("tbody tr"))
                {
                    if (table.ContainsKey(row.Children[0].TextContent))
                        continue;                   
                    table.Add(row.Children[0].TextContent, row.Children[1].TextContent);
                }
                tables.Add(new OverwatchDataTable
                {
                    Name = section.QuerySelector("thead").TextContent,
                    Stats = table
                });
            }
            return tables;
        }
    }
}
