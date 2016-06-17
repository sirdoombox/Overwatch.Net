using AngleSharp;
using AngleSharp.Dom;
using OverwatchDotNet.Internal;
using System.Linq;
using System.Threading.Tasks;

namespace OverwatchDotNet.OverwatchData
{
    public class DeathData : IStatModule
    {
        public float Deaths { get; private set; }
        public float EnvironmentalDeaths { get; private set; }

        public void LoadFromURL(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var document = BrowsingContext.New(config).OpenAsync(url).Result;
            var table = document.QuerySelectorAll("table.data-table").FirstOrDefault(t => t.QuerySelector("thead").TextContent == "Deaths");
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
            var table = document.QuerySelectorAll("table.data-table").FirstOrDefault(t => t.QuerySelector("thead").TextContent == "Deaths");
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
            var table = document.QuerySelectorAll("table.data-table").FirstOrDefault(t => t.QuerySelector("thead").TextContent == "Deaths");
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
            switch (statName)
            {
                case "Deaths":
                    Deaths = statValue;
                    break;
                case "Environmental Deaths":
                    EnvironmentalDeaths = statValue;
                    break;
            }
        }             
    }
}
