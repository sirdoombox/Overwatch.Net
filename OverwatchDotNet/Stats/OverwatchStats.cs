using AngleSharp.Dom;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace OverwatchAPI
{
    /// <summary>
    /// Represents a collection of Heroes for which the player has recorded at least one game of play with.
    /// </summary>
    public class OverwatchStats : IReadOnlyDictionary<string, Hero>
    {
        private Dictionary<string, Hero> contents = new Dictionary<string, Hero>();

        internal OverwatchStats() { }

        /// <summary>
        /// Get a hero by name
        /// </summary>
        /// <param key="key">The name of the hero.</param>
        /// <returns>A hero object if one by such a name exists - otherwise null.</returns>
        public Hero this[string key] => contents[key];

        public IEnumerable<string> Keys => contents.Keys;

        public IEnumerable<Hero> Values => contents.Values;

        public int Count => contents.Count;

        public bool ContainsKey(string key) => contents.ContainsKey(key);

        public IEnumerator<KeyValuePair<string, Hero>> GetEnumerator() => contents.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => contents.GetEnumerator();

        public bool TryGetValue(string key, out Hero value) => contents.TryGetValue(key, out value);

        internal void UpdateStatsFromPage(IDocument doc, Mode mode)
        {
            string divModeId = "";
            switch (mode)
            {
                case Mode.Casual:
                    divModeId = "quickplay";
                    break;
                case Mode.Competitive:
                    divModeId = "competitive";
                    break;
            }
            var innerContent = doc.QuerySelector($"div[id='{divModeId}']");
            var idDictionary = new Dictionary<string, string>();
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
                var hero = new Hero();
                contents.Add(idDictionary[catId], hero);
                foreach (var table in section.QuerySelectorAll($"div[data-category-id='{catId}'] table.data-table"))
                {
                    var cat = new StatCategory();
                    hero.contents.Add(table.QuerySelector("thead").TextContent, cat);
                    foreach (var row in table.QuerySelectorAll("tbody tr"))
                    {
                        if (cat.ContainsKey(row.Children[0].TextContent))
                            continue;
                        cat.contents.Add(row.Children[0].TextContent, OWValToDouble(row.Children[1].TextContent));
                    }
                }
            }
        }

        private double OWValToDouble(string input)
        {
            if (input.ToLower().Contains("hour"))
                return TimeSpan.FromHours(int.Parse(input.Substring(0, input.IndexOf(" ")))).TotalSeconds;
            else if (input.ToLower().Contains("minute"))
                return TimeSpan.FromMinutes(int.Parse(input.Substring(0, input.IndexOf(" ")))).TotalSeconds;
            else if (input.Contains(":"))
            {
                if (TimeSpan.TryParseExact(input, @"mm\:ss", CultureInfo.CurrentCulture, out var outputTime))
                    return outputTime.TotalSeconds;
                else if (TimeSpan.TryParseExact(input, @"hh\:mm\:ss", CultureInfo.CurrentCulture, out var outputTime1))
                    return outputTime1.TotalSeconds;
            }
            return double.TryParse(input.Replace(",", "").Replace("%", ""), out var rslt) ? rslt : 0;
        }

        private string ParseHeroName(string input)
        {
            if (input.ToLower() == "all heroes")
                return "AllHeroes";
            return input.Replace("ú", "u").Replace(":", "").Replace(" ", "").Replace("ö", "o").Replace(".", "");
        }
    }
}
