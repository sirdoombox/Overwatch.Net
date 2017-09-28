using System.Collections.Generic;
using System.Linq;
using OverwatchAPI.Data;

namespace OverwatchAPI.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<Stat> FilterByHero(this IEnumerable<Stat> stats, string heroName)
        {
            heroName = heroName.Contains(" ") ? heroName.Replace(" ", string.Empty) : heroName;
            return stats.Where(x => x.HeroName.EqualsIgnoreCase(heroName));
        }     

        public static IEnumerable<Stat> FilterByCategory(this IEnumerable<Stat> stats, string categoryName) =>
            stats.Where(x => x.CategoryName.EqualsIgnoreCase(categoryName));

        public static IEnumerable<Stat> FilterByName(this IEnumerable<Stat> stats, string statName) =>
            stats.Where(x => x.Name.EqualsIgnoreCase(statName));

        public static Stat GetStatExact(this IEnumerable<Stat> stats, string heroName, string categoryName, string statName) =>
            stats.FilterByHero(heroName)
                .FilterByCategory(categoryName)
                .FilterByName(statName)
                .FirstOrDefault();

        public static IEnumerable<Achievement> FilterByCategory(this IEnumerable<Achievement> achievements, string categoryName) =>
            achievements.Where(x => x.CategoryName.EqualsIgnoreCase(categoryName));

        public static Achievement FilterByName(this IEnumerable<Achievement> achievements, string achievementName) =>
            achievements.FirstOrDefault(x => x.Name.EqualsIgnoreCase(achievementName));
    }
}
