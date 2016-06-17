using System.Collections.Generic;
using System.Linq;
using AngleSharp;

namespace OverwatchDotNet
{
    public class MainTest
    {
        public Dictionary<string, string> GetOverwatchStats(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var document = BrowsingContext.New(config).OpenAsync(url).Result;
            var cards = document.QuerySelectorAll(".card");

            return cards.ToDictionary(d => d.QuerySelector(".card-copy").TextContent.Replace(" - Average", ""), d => d.QuerySelector(".card-heading").TextContent);
        }       

        public void StatTest(string url)
        {

        }
    }
}
