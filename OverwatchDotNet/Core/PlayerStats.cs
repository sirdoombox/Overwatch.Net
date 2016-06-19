using AngleSharp;
using AngleSharp.Dom;
using OverwatchAPI.Data;
using OverwatchAPI.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OverwatchAPI
{
    public class PlayerStats
    {
        public AllHeroes AllHeroes { get; private set; }
        public Bastion Bastion { get; private set; }
        public DVa DVa { get; private set; }
        public Genji Genji { get; private set; }
        public Junkrat Junkrat { get; private set; }
        public Lucio Lucio { get; private set; }
        public McCree McCree { get; private set; }
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
        

        internal async Task UpdateStats(OverwatchPlayer player)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenAsync(player.ProfileURL);
            Dictionary<string, string> idDictionary = new Dictionary<string, string>();
            foreach (var dropdownitem in document.QuerySelectorAll("select > option"))
            {
                string id = dropdownitem.GetAttribute("value");
                if (id.StartsWith("0x0"))
                {
                    idDictionary.Add(id, ParseClassName(dropdownitem.TextContent));
                }
            }
            foreach (var section in document.QuerySelectorAll("div[data-group-id='stats']"))
            {
                var catId = section.GetAttribute("data-category-id");
                List<OverwatchDataTable> heroTableCollection = new List<OverwatchDataTable>();
                foreach (var table in section.QuerySelectorAll($"div[data-category-id='{catId}'] table.data-table"))
                {
                    OverwatchDataTable heroTable = new OverwatchDataTable();
                    heroTable.Name = table.QuerySelector("thead").TextContent;
                    var statDict = new Dictionary<string, string>();
                    foreach (var row in table.QuerySelectorAll("tbody tr"))
                    {
                        if (statDict.ContainsKey(row.Children[0].TextContent))
                            continue;
                        statDict.Add(row.Children[0].TextContent, row.Children[1].TextContent);
                    }
                    heroTable.Stats = statDict;
                    heroTableCollection.Add(heroTable);
                }
                var prop = GetType().GetProperty(idDictionary[catId]);
                if (typeof(IStatGroup).IsAssignableFrom(prop.PropertyType))
                {
                    IStatGroup statGroup = (IStatGroup)Activator.CreateInstance(prop.PropertyType);
                    statGroup.SendPage(heroTableCollection);
                    prop.SetValue(this, statGroup);
                }
            }
        }

        private string ParseClassName(string input)
        {
            if (input.ToLower() == "all heroes")
                return "AllHeroes";
            return input.Replace("ú", "u").Replace(":", "").Replace(" ", "").Replace("ö", "o").Replace(".", "");
        }
    }
}
