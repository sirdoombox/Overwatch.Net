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
                    idDictionary.Add(id, ParseClassName(dropdownitem.TextContent));
                }
            }
            foreach (var section in innerContent.QuerySelectorAll("div[data-group-id='stats']"))
            {
                var catId = section.GetAttribute("data-category-id");
                Hero hero = new Hero(idDictionary[catId]);
                foreach (var table in section.QuerySelectorAll($"div[data-category-id='{catId}'] table.data-table"))
                {
                    Category cat = new Category(table.QuerySelector("thead").TextContent);
                    var statDict = new Dictionary<string, double>();
                    foreach (var row in table.QuerySelectorAll("tbody tr"))
                    {
                        if (statDict.ContainsKey(row.Children[0].TextContent))
                            continue;
                        statDict.Add(row.Children[0].TextContent, OWValToDouble(row.Children[1].TextContent));
                    }
                    cat.AddRange(statDict.Select(x => new Stat(x.Key, x.Value)).ToList());
                    hero.Add(cat);
                }
                this.Add(hero);
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

        private string ParseClassName(string input)
        {
            if (input.ToLower() == "all heroes")
                return "AllHeroes";
            return input.Replace("ú", "u").Replace(":", "").Replace(" ", "").Replace("ö", "o").Replace(".", "");
        }
    }

    public class Hero : List<Category>
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
        public Category GetCategory(string name)
        {
            return this.FirstOrDefault(x => string.Compare(name, x.Name, StringComparison.OrdinalIgnoreCase) == 0);
        }
    }

    public class Category : List<Stat>
    {
        public string Name { get; }

        public Category(string n)
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
