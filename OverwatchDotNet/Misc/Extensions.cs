using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace OverwatchAPI.Internal
{
    public static class Extensions
    {
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
            return float.Parse(input.Replace(",", "").Replace("%", ""));
        }

        /// <summary>
        /// Provides a list of strings that act as a readout of the statistic name and it's value.
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        public static Dictionary<string,string> GetModuleReadout(this IStatModule module)
        {
            var propProps = module.GetType().GetProperties();
            var stringList = new Dictionary<string,string>();
            foreach(var prop in propProps)
                stringList.Add(prop.Name.AddSpacesToSentence(false), prop.GetValue(module).ToString());         
            return stringList;
        }

        static string AddSpacesToSentence(this string text, bool preserveAcronyms)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]))
                    if ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) ||
                        (preserveAcronyms && char.IsUpper(text[i - 1]) &&
                         i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                        newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }
    }
}
