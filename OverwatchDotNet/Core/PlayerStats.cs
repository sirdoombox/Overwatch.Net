using AngleSharp.Dom;
using OverwatchAPI.Data;
using OverwatchAPI.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OverwatchAPI
{
    public class PlayerStats
    {
        public AllHeroes AllHeroes { get; private set; } = new AllHeroes();
        public Bastion Bastion { get; private set; } = new Bastion();
        public DVa DVa { get; private set; } = new DVa();
        public Genji Genji { get; private set; } = new Genji();
        public Hanzo Hanzo { get; private set; } = new Hanzo();
        public Junkrat Junkrat { get; private set; } = new Junkrat();
        public Lucio Lucio { get; private set; } = new Lucio();
        public McCree McCree { get; private set; } = new McCree();
        public Mei Mei { get; private set; } = new Mei();
        public Mercy Mercy { get; private set; } = new Mercy();
        public Pharah Pharah { get; private set; } = new Pharah();
        public Reaper Reaper { get; private set; } = new Reaper();
        public Reinhardt Reinhardt { get; private set; } = new Reinhardt();
        public Roadhog Roadhog { get; private set; } = new Roadhog();
        public Soldier76 Soldier76 { get; private set; } = new Soldier76();
        public Symmetra Symmetra { get; private set; } = new Symmetra();
        public Torbjorn Torbjorn { get; private set; } = new Torbjorn();
        public Tracer Tracer { get; private set; } = new Tracer();
        public Widowmaker Widowmaker { get; private set; } = new Widowmaker();
        public Winston Winston { get; private set; } = new Winston();
        public Zarya Zarya { get; private set; } = new Zarya();
        public Zenyatta Zenyatta { get; private set; } = new Zenyatta();

        internal void UpdateStatsFromPage(IDocument doc, Mode mode)
        {
            string divModeId = "";
            switch (mode)
            {
                case Mode.Casual:
                    divModeId = "quick-play";
                    break;
                case Mode.Competitive:
                    divModeId = "competitive-play";
                    break;
            }
            var innerContent = doc.QuerySelector($"div[id='{divModeId}']");
            Dictionary<string, string> idDictionary = new Dictionary<string, string>();
            foreach (var dropdownitem in innerContent.QuerySelectorAll("select > option"))
            {
                string id = dropdownitem.GetAttribute("value");
                if (id.StartsWith("0x0"))
                {
                    idDictionary.Add(id, ParseClassName(dropdownitem.TextContent));
                }
            }
            foreach (var section in innerContent.QuerySelectorAll("div[data-group-id='stats']"))
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
                PropertyInfo prop = GetType().GetProperty(idDictionary[catId]);
                if (typeof(IHeroStats).IsAssignableFrom(prop.PropertyType))
                {
                    IHeroStats statGroup = (IHeroStats)Activator.CreateInstance(prop.PropertyType);
                    statGroup?.SendPage(heroTableCollection);
                    prop.SetValue(this, statGroup);
                }
            }
            foreach (var someProp in GetType().GetProperties())
            {
                var somePropInstance = someProp.GetValue(this);
                foreach(var subProp in somePropInstance.GetType().GetProperties())
                {
                    if (subProp.GetValue(somePropInstance) == null)
                        subProp.SetValue(somePropInstance, Activator.CreateInstance(subProp.PropertyType));
                }            
            }
        }

        public IHeroStats GetHeroStats(string heroName)
        {
            return (IHeroStats)GetType().GetProperties().FirstOrDefault(x => string.Compare(x.Name, ParseClassName(heroName), true) == 0).GetValue(this);
        }

        private string ParseClassName(string input)
        {
            if (input.ToLower() == "all heroes")
                return "AllHeroes";
            return input.Replace("ú", "u").Replace(":", "").Replace(" ", "").Replace("ö", "o").Replace(".", "");
        }
    }
}
