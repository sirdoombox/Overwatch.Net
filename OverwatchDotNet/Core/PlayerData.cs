using AngleSharp;
using AngleSharp.Dom;
using OverwatchAPI.Data;
using OverwatchAPI.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OverwatchAPI
{
    public enum Region { US, EU, None }

    public class PlayerStats
    {
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
                    var statProp = (IStatModule)Activator.CreateInstance(prop.PropertyType);
                    statProp.SendTable(item);
                    prop.SetValue(this, statProp);
                }
            }
        }

        public async Task GetPlayerFromBtag(string battletag, Region region = Region.None)
        {
            OverwatchPlayer _owp = new OverwatchPlayer(battletag, region);
            if (_owp.Region == Region.None)
                await _owp.DetectRegion();
            //if(_owp.Region != Region.None)
                
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
