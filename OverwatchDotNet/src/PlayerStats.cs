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
        public OverwatchHeroDictionary Heroes { get; private set; } = new OverwatchHeroDictionary();

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
                List<OverwatchStatTable> heroTableCollection = new List<OverwatchStatTable>();
                foreach (var table in section.QuerySelectorAll($"div[data-category-id='{catId}'] table.data-table"))
                {
                    OverwatchStatTable heroTable = new OverwatchStatTable();
                    heroTable.Name = table.QuerySelector("thead").TextContent;
                    var statDict = new Dictionary<string, double>();
                    foreach (var row in table.QuerySelectorAll("tbody tr"))
                    {
                        if (statDict.ContainsKey(row.Children[0].TextContent))
                            continue;
                        statDict.Add(row.Children[0].TextContent, row.Children[1].TextContent.OWValToDouble());
                    }
                    heroTable.Stats = statDict;
                    heroTableCollection.Add(heroTable);
                }
                Heroes.Add(idDictionary[catId], heroTableCollection);
            }
        }

        public static double OWValToDouble(this string input)
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
}
