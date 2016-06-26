using System.Text.RegularExpressions;

namespace OverwatchAPI
{
    static class OverwatchAPIHelpers
    {
        public static bool IsValidBattletag(string battletag)
        {
            return new Regex(@"\w+#\d+").IsMatch(battletag);
        }

        public static string ProfileURL(string username, Region region, Platform platform)
        {
            if(platform == Platform.pc)
            {
                return $"https://playoverwatch.com/en-gb/career/{platform}/{region}/{username.Replace("#", "-")}";
            }
            else
                return $"https://playoverwatch.com/en-gb/career/{platform}/{username.Replace("#", "-")}";
        }
    }
}
