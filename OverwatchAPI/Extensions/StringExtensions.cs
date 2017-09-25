using System.Text.RegularExpressions;

namespace OverwatchAPI
{
    public static class StringExtensions
    {
        private static readonly Regex battletagRegex = new Regex(@"\w+#\d+");
        private static readonly Regex psnIdRegex = new Regex(@"^[a-zA-Z]{1}[\w\d-]{2,12}$");
        private static readonly Regex xblIdRegex = new Regex(@"^[a-zA-Z0-9\s]{1,15}$");

        internal static bool IsValidBattletag(this string battletag) => battletagRegex.IsMatch(battletag);

        internal static bool IsValidXblId(this string xblId) => xblIdRegex.IsMatch(xblId);

        internal static bool IsValidPsnId(this string psnId) => psnIdRegex.IsMatch(psnId);

        internal static string ToProfileURL(this string username, Region region, Platform platform)
        {
            if (platform == Platform.pc)
                return $"https://playoverwatch.com/en-gb/career/{platform}/{region}/{username.Replace("#", "-")}";
            else
                return $"https://playoverwatch.com/en-gb/career/{platform}/{username}";
        }

        internal static string BattletagToUrlFriendlyString(this string battletag) => battletag.Replace('#','-');
    }
}
