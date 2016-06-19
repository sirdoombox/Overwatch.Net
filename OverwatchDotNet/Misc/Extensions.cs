using System;
using System.Globalization;

namespace OverwatchAPI.Internal
{
    public static class Extensions
    {
        public static int OWValToInt(this string input)
        {
            return int.Parse(input.Replace(",", "").Replace("%", ""));
        }

        public static TimeSpan OWValToTimeSpan(this string input)
        {
            if (input.ToLower().Contains("hour"))
                return TimeSpan.FromHours(int.Parse(input.Substring(0, input.IndexOf(" "))));
            else if (input.ToLower().Contains("minute"))
                return TimeSpan.FromMinutes(int.Parse(input.Substring(0, input.IndexOf(" "))));
            else if (input.Contains(":"))
            {
                TimeSpan outputTime;
                if (TimeSpan.TryParseExact(input, @"mm\:ss", CultureInfo.CurrentCulture, out outputTime))
                    return outputTime;
                else if (TimeSpan.TryParseExact(input, @"hh\:mm\:ss", CultureInfo.CurrentCulture, out outputTime))
                    return outputTime;
            }
            return new TimeSpan();
        }

        public static float OWValToFloat(this string input)
        {
            return float.Parse(input.Replace(",", ""));
        }
    }
}
