using AngleSharp.Dom.Html;
using OverwatchAPI.StaticResources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace OverwatchAPI.Internal
{
    public static class IHtmlDocExtensions
    {
        private static readonly Regex playerLevelImageRegex = new Regex("(0x\\w*)(?=_)");

        internal static string PortraitImage(this IHtmlDocument doc) => doc.QuerySelector(".player-portrait").GetAttribute("src");

        internal static ushort CompetitiveRank(this IHtmlDocument doc)
        {
            ushort.TryParse(doc.QuerySelector("div.competitive-rank div")?.TextContent, out ushort parsedCompetitiveRank);
            return parsedCompetitiveRank;
        }

        internal static string CompetitiveRankImage(this IHtmlDocument doc)
        {
            var compImg = doc.QuerySelector("div.competitive-rank img")?.OuterHtml;
            if (!string.IsNullOrEmpty(compImg))
                return compImg.Replace("<img src=\"", "").Replace("\">", "");
            return string.Empty;
        }

        internal static ushort PlayerLevel(this IHtmlDocument doc)
        {
            ushort.TryParse(doc.QuerySelector("div.player-level div")?.TextContent, out ushort parsedPlayerLevel);
            string playerLevelImageId = playerLevelImageRegex.Match(doc.QuerySelector("div.player-level").GetAttribute("style")).Value;
            if (!string.IsNullOrEmpty(playerLevelImageId))
                parsedPlayerLevel += Prestige.Definitions[playerLevelImageId];
            return parsedPlayerLevel;
        }

        internal static string PlayerLevelImage(this IHtmlDocument doc)
        {
            var str = doc.QuerySelector("div.player-level").GetAttribute("style");
            var startIndex = str.IndexOf('(') + 1;
            return str.Substring(startIndex, str.IndexOf(')') - startIndex);
        }

        internal static Achievements Achievements(this IHtmlDocument doc)
        {
            var contents = new Achievements();
            var innerContent = doc.QuerySelector("section[id='achievements-section']");
            foreach (var dropdownitem in innerContent.QuerySelectorAll("select > option"))
            {
                var achievementBlock = innerContent.QuerySelector($"div[data-category-id='{dropdownitem.GetAttribute("value")}']");
                var cat = new AchievementCategory();
                contents.Add(dropdownitem.GetAttribute("option-id"), cat);
                foreach (var achievement in achievementBlock.QuerySelectorAll("div.achievement-card"))
                {
                    cat.contents.Add(achievement.QuerySelector("div.media-card-title").TextContent,
                                     !achievement.GetAttribute("class").Contains("m-disabled"));
                }
            }
            return contents;
        }

        internal static Stats Stats(this IHtmlDocument doc, Mode mode)
        {
            var contents = new Stats();
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
            return contents;
        }

        private static double OWValToDouble(string input)
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

        private static string ParseHeroName(string input)
        {
            if (input.ToLower() == "all heroes")
                return "AllHeroes";
            return input.Replace("ú", "u").Replace(":", "").Replace(" ", "").Replace("ö", "o").Replace(".", "");
        }
    }
}