using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace OverwatchAPI
{
    public class OverwatchStats : List<Hero>
    {        
        /// <summary>
        /// Get a hero by name.
        /// </summary>
        /// <param name="name">The name of the hero/</param>
        /// <returns>A hero object if one by such a name exists - otherwise null.</returns>
        public Hero GetHero(string name)
        {
            return this.FirstOrDefault(x => string.Compare(name, x.Name, StringComparison.OrdinalIgnoreCase) == 0);
        }

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
                    idDictionary.Add(id, ParseHeroName(dropdownitem.TextContent));
                }
            }
            foreach (var section in innerContent.QuerySelectorAll("div[data-group-id='stats']"))
            {
                var catId = section.GetAttribute("data-category-id");
                Hero hero = new Hero(idDictionary[catId]);
                this.Add(hero);
                foreach (var table in section.QuerySelectorAll($"div[data-category-id='{catId}'] table.data-table"))
                {
                    StatCategory cat = new StatCategory(table.QuerySelector("thead").TextContent);
                    var statDict = new Dictionary<string, double>();
                    foreach (var row in table.QuerySelectorAll("tbody tr"))
                    {
                        if (statDict.ContainsKey(row.Children[0].TextContent))
                            continue;
                        statDict.Add(row.Children[0].TextContent, OWValToDouble(row.Children[1].TextContent));
                    }
                    cat.AddRange(statDict.Select(x => new Stat(x.Key, x.Value)));
                    hero.Add(cat);
                }
            }
        }

        double OWValToDouble(string input)
        {
            if (input.ToLower().Contains("hour"))
                return TimeSpan.FromHours(int.Parse(input.Substring(0, input.IndexOf(" ")))).TotalSeconds;
            else if (input.ToLower().Contains("minute"))
                return TimeSpan.FromMinutes(int.Parse(input.Substring(0, input.IndexOf(" ")))).TotalSeconds;
            else if (input.Contains(":"))
            {
                TimeSpan outputTime;
                if (TimeSpan.TryParseExact(input, @"mm\:ss", CultureInfo.CurrentCulture, out outputTime))
                    return outputTime.TotalSeconds;
                else if (TimeSpan.TryParseExact(input, @"hh\:mm\:ss", CultureInfo.CurrentCulture, out outputTime))
                    return outputTime.TotalSeconds;
            }
            try { return double.Parse(input.Replace(",", "").Replace("%", "")); }
            catch { return 0; }
        }

        private string ParseHeroName(string input)
        {
            if (input.ToLower() == "all heroes")
                return "AllHeroes";
            return input.Replace("ú", "u").Replace(":", "").Replace(" ", "").Replace("ö", "o").Replace(".", "");
        }
    }

    public class Hero : List<StatCategory>
    {
        public string Name { get; }

        public Hero(string n)
        {
            Name = n;
        }

        /// <summary>
        /// Get a category by name
        /// </summary>
        /// <param name="name">The name of the category.</param>
        /// <returns>A category object if one by such a name exists - otherwise null.</returns>
        public StatCategory GetCategory(string name)
        {
            return this.FirstOrDefault(x => string.Compare(name, x.Name, StringComparison.OrdinalIgnoreCase) == 0);
        }
    }

    public class StatCategory : List<Stat>
    {
        public string Name { get; }

        public StatCategory(string n)
        {
            Name = n;
        }

        /// <summary>
        /// Get a stat by name.
        /// </summary>
        /// <param name="name">The name of the stat.</param>
        /// <returns>A stat object if one by such a name exists - otherwise null.</returns>
        public Stat GetStat(string name)
        {
            return this.FirstOrDefault(x => string.Compare(name, x.Name, StringComparison.OrdinalIgnoreCase) == 0);
        }
    }

    public class Stat
    {
        public string Name { get; }
        public double Value { get; }

        public Stat(string n, double v)
        {
            Name = n;
            Value = v;
        }
    }
}
