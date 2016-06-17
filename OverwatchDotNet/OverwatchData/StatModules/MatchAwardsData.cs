using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp;
using OverwatchDotNet.Internal;

namespace OverwatchDotNet.OverwatchData
{
    public class MatchAwardsData : IStatModule
    {
        public float Cards { get; private set; }
        public float Medals { get; private set; }
        public float MedalsGold { get; private set; }
        public float MedalsSilver { get; private set; }
        public float MedalsBronze { get; private set; }

        public void LoadFromURL(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var document = BrowsingContext.New(config).OpenAsync(url).Result;
            var table = document.QuerySelectorAll("table.data-table").FirstOrDefault(t => t.QuerySelector("thead").TextContent == "Match Awards");
            var tableContents = table.QuerySelectorAll("tbody > tr");
            foreach (var row in tableContents)
            {
                string[] tempArray = new string[2];
                int i = 0;
                foreach (var item in row.QuerySelectorAll("td"))
                {
                    tempArray[i] = item.TextContent;
                    i++;
                }
                AssignValue(tempArray[0], tempArray[1].OverwatchValueStringToFloat());
            }
        }

        public async Task LoadFromURLAsync(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenAsync(url);
            var table = document.QuerySelectorAll("table.data-table").FirstOrDefault(t => t.QuerySelector("thead").TextContent == "Match Awards");
            var tableContents = table.QuerySelectorAll("tbody > tr");
            foreach (var row in tableContents)
            {
                string[] tempArray = new string[2];
                int i = 0;
                foreach (var item in row.QuerySelectorAll("td"))
                {
                    tempArray[i] = item.TextContent;
                    i++;
                }
                AssignValue(tempArray[0], tempArray[1].OverwatchValueStringToFloat());
            }
        }

        public void LoadFromDocument(IDocument document)
        {
            var table = document.QuerySelectorAll("table.data-table").FirstOrDefault(t => t.QuerySelector("thead").TextContent == "Match Awards");
            var tableContents = table.QuerySelectorAll("tbody > tr");
            foreach (var row in tableContents)
            {
                string[] tempArray = new string[2];
                int i = 0;
                foreach (var item in row.QuerySelectorAll("td"))
                {
                    tempArray[i] = item.TextContent;
                    i++;
                }
                AssignValue(tempArray[0], tempArray[1].OverwatchValueStringToFloat());
            }
        }

        void AssignValue(string statName, float statValue)
        {
            switch(statName)
            {
                case "Cards":
                    Cards = statValue;
                    break;
                case "Medals":
                    Medals = statValue;
                    break;
                case "Medals - Gold":
                    MedalsGold = statValue;
                    break;
                case "Medals - Silver":
                    MedalsSilver = statValue;
                    break;
                case "Medals - Bronze":
                    MedalsBronze = statValue;
                    break;
            }
        }
    }
}
