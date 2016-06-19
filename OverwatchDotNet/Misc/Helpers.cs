using System.Text.RegularExpressions;

namespace OverwatchAPI
{
    static class OverwatchAPIHelpers
    {
        public static bool IsValidBattletag(string battletag)
        {
            return new Regex(@"\w+#\d+").IsMatch(battletag);
        }
    }
}
