using System;
using System.Text.RegularExpressions;

namespace OverwatchAPI
{
    static class OverwatchAPIHelpers
    {
        public static bool IsValidBattletag(string battletag)
        {
            return new Regex(@"\w+#\d+").IsMatch(battletag);
        }

        public static string ProfileURL(string battletag, Region region)
        {
            return $"http://playoverwatch.com/en-gb/career/pc/{region}/{battletag.Replace("#", "-")}";
        }
    }
}
