using AngleSharp;
using OverwatchAPI.Data;
using OverwatchAPI.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace OverwatchAPI
{
    public enum Region { US, EU, None }

    public class PlayerStats
    {
        public AllHeroes AllHeroes { get; private set; }
        public Bastion Bastion { get; private set; }
        public DVa DVa { get; private set; }
        public Genji Genji { get; private set; }
        public Junkrat Junkrat { get; private set; }
        public Lucio Lucio { get; private set; }
        public McCree McRee { get; private set; }
        public Mei Mei { get; private set; }
        public Mercy Mercy { get; private set; }
        public Pharah Pharah { get; private set; }
        public Reaper Reaper { get; private set; }
        public Reinhardt Reinhardt { get; private set; }
        public Roadhog Roadhog { get; private set; }
        public Soldier76 Soldier76 { get; private set; }
        public Symmetra Symmetra { get; private set; }
        public Torbjorn Torbjorn { get; private set; }
        public Tracer Tracer { get; private set; }
        public Widowmaker Widowmaker { get; private set; }
        public Winston Winston { get; private set; }
        public Zarya Zarya { get; private set; }
        public Zenyatta Zenyatta { get; private set; }

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
