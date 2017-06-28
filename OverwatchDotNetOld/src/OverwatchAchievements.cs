using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OverwatchAPI
{
    public class OverwatchAchievements : List<AchievementCategory>
    {
        /// <summary>
        /// Get an achievement category by name.
        /// </summary>
        /// <param name="name">The name of the achievement category/</param>
        /// <returns>An achievement category object if one by such a name exists - otherwise null.</returns>
        public AchievementCategory GetCategory(string name)
        {
            return this.FirstOrDefault(x => string.Compare(x.Name, name, StringComparison.OrdinalIgnoreCase) == 0);
        }

        internal void UpdateAchievementsFromPage(IDocument doc)
        {
            Dictionary<string, string> idDictionary = new Dictionary<string, string>();
            var innerContent = doc.QuerySelector("section[id='achievements-section']");
            foreach (var dropdownitem in innerContent.QuerySelectorAll("select > option"))
                idDictionary.Add(dropdownitem.GetAttribute("value"), dropdownitem.GetAttribute("option-id"));
            foreach(var item in idDictionary)
            {
                var achievementBlock = innerContent.QuerySelector($"div[data-category-id='{item.Key}']");
                AchievementCategory cat = new AchievementCategory(item.Value);
                this.Add(cat);
                foreach (var achievement in achievementBlock.QuerySelectorAll("div.achievement-card"))
                {
                    cat.Add(new Achievement
                        (
                            achievement.QuerySelector("div.media-card-title").TextContent,
                            !achievement.GetAttribute("class").Contains("m-disabled")
                        ));
                }
            }
        }
    }

    public class AchievementCategory : List<Achievement>
    {
        public string Name { get; }

        public AchievementCategory(string n)
        {
            Name = n;
        }

        /// <summary>
        /// Get an achievement by name.
        /// </summary>
        /// <param name="name">The name of the achievement/</param>
        /// <returns>An achievement object if one by such a name exists - otherwise null.</returns>
        public Achievement GetAchievement(string name)
        {
            return this.FirstOrDefault(x => string.Compare(x.Name, name, StringComparison.OrdinalIgnoreCase) == 0);
        }
    }

    public class Achievement
    {
        public string Name { get; }
        public bool IsUnlocked { get; }

        public Achievement(string n, bool u)
        {
            Name = n;
            IsUnlocked = u;
        }
    }
}
