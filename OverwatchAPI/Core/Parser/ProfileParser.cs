using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp.Parser.Html;
using System.Globalization;
using AngleSharp.Dom.Html;
using System.Text.RegularExpressions;
using OverwatchAPI.StaticResources;
using OverwatchAPI.WebClient;

namespace OverwatchAPI.Parser
{
    internal sealed class ProfileParser
    {
        private readonly HtmlParser _parser = new HtmlParser();

        internal async Task<Player> Parse(Player player, ProfileClient.ProfileRequestData pageData)
        {
            using (var doc = await _parser.ParseAsync(pageData.ReqContent))
            {
                player.ProfileUrl = pageData.ReqUrl;
                player.CompetitiveRank = CompetitiveRank(doc);
                player.CompetitiveRankImageUrl = CompetitiveRankImage(doc);
                player.CompetitiveStats = Stats(doc, Mode.Competitive);
                player.CasualStats = Stats(doc, Mode.Casual);
                player.Achievements = Achievements(doc);
                player.PlayerLevel = PlayerLevel(doc);
                player.ProfilePortraitUrl = PortraitImage(doc);
                player.PlayerLevelImage = PlayerLevelImage(doc);
                return player;
            }
        }

        private static readonly Regex PlayerLevelImageRegex = new Regex("(0x\\w*)(?=_)");

        private static string PortraitImage(IHtmlDocument doc) => doc.QuerySelector(".player-portrait").GetAttribute("src");

        private static ushort CompetitiveRank(IHtmlDocument doc)
        {
            ushort.TryParse(doc.QuerySelector("div.competitive-rank div")?.TextContent, out var parsedCompetitiveRank);
            return parsedCompetitiveRank;
        }

        private static string CompetitiveRankImage(IHtmlDocument doc)
        {
            var compImg = doc.QuerySelector("div.competitive-rank img")?.OuterHtml;
            if (!string.IsNullOrEmpty(compImg))
                return compImg.Replace("<img src=\"", "").Replace("\">", "");
            return string.Empty;
        }

        private static ushort PlayerLevel(IHtmlDocument doc)
        {
            ushort.TryParse(doc.QuerySelector("div.player-level div")?.TextContent, out var parsedPlayerLevel);
            var playerLevelImageId = PlayerLevelImageRegex.Match(doc.QuerySelector("div.player-level").GetAttribute("style")).Value;
            if (!string.IsNullOrEmpty(playerLevelImageId))
                parsedPlayerLevel += Prestige.Definitions[playerLevelImageId];
            return parsedPlayerLevel;
        }

        private static string PlayerLevelImage(IHtmlDocument doc)
        {
            var str = doc.QuerySelector("div.player-level").GetAttribute("style");
            var startIndex = str.IndexOf('(') + 1;
            return str.Substring(startIndex, str.IndexOf(')') - startIndex);
        }

        private static Achievements Achievements(IHtmlDocument doc)
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
                    cat.Contents.Add(achievement.QuerySelector("div.media-card-title").TextContent,
                                     !achievement.GetAttribute("class").Contains("m-disabled"));
                }
            }
            return contents;
        }

        private static Stats Stats(IHtmlDocument doc, Mode mode)
        {
            var contents = new Stats();
            var divModeId = "";
            switch (mode)
            {
                case Mode.Casual:
                    divModeId = "quickplay";
                    break;
                case Mode.Competitive:
                    divModeId = "competitive";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
            var innerContent = doc.QuerySelector($"div[id='{divModeId}']");
            var idDictionary = new Dictionary<string, string>();
            foreach (var dropdownitem in innerContent.QuerySelectorAll("select > option"))
            {
                var id = dropdownitem.GetAttribute("value");
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
                    hero.Contents.Add(table.QuerySelector("thead").TextContent, cat);
                    foreach (var row in table.QuerySelectorAll("tbody tr"))
                    {
                        if (cat.ContainsKey(row.Children[0].TextContent))
                            continue;
                        cat.Contents.Add(row.Children[0].TextContent, OwValToDouble(row.Children[1].TextContent));
                    }
                }
            }
            return contents;
        }

        private static double OwValToDouble(string input)
        {
            if (input.ToLower().Contains("hour"))
                return TimeSpan.FromHours(int.Parse(input.Substring(0, input.IndexOf(" ")))).TotalSeconds;
            if (input.ToLower().Contains("minute"))
                return TimeSpan.FromMinutes(int.Parse(input.Substring(0, input.IndexOf(" ")))).TotalSeconds;
            if (!input.Contains(":"))
                return double.TryParse(input.Replace(",", "").Replace("%", ""), out var rslt1) ? rslt1 : 0;
            if (TimeSpan.TryParseExact(input, @"mm\:ss", CultureInfo.CurrentCulture, out var outputTime))
                return outputTime.TotalSeconds;
            if (TimeSpan.TryParseExact(input, @"hh\:mm\:ss", CultureInfo.CurrentCulture, out var outputTime1))
                return outputTime1.TotalSeconds;
            return double.TryParse(input.Replace(",", "").Replace("%", ""), out var rslt2) ? rslt2 : 0;
        }

        private static string ParseHeroName(string input)
        {
            return input.ToLower() == "all heroes" ? "AllHeroes" : input.Replace("ú", "u").Replace(":", "").Replace(" ", "").Replace("ö", "o").Replace(".", "");
        }
    }
}