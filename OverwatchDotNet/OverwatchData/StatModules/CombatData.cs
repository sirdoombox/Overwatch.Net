using AngleSharp;
using AngleSharp.Dom;
using OverwatchDotNet.Internal;
using System.Linq;
using System.Threading.Tasks;

namespace OverwatchDotNet.OverwatchData
{
    public class CombatStats : IStatModule
    {
        public float MeleeFinalBlows { get; private set; }
        public float SoloKills { get; private set; }
        public float ObjectiveKills { get; private set; }
        public float FinalBlows { get; private set; }
        public float DamageDone { get; private set; }
        public float Eliminations { get; private set; }
        public float EnvironmentKills { get; private set; }
        public float Multikills { get; private set; }

        public void LoadFromURL(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var document = BrowsingContext.New(config).OpenAsync(url).Result;
            var table = document.QuerySelectorAll("table.data-table").FirstOrDefault(t => t.QuerySelector("thead").TextContent == "Combat");
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
            var table = document.QuerySelectorAll("table.data-table").FirstOrDefault(t => t.QuerySelector("thead").TextContent == "Combat");
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
            var table = document.QuerySelectorAll("table.data-table").FirstOrDefault(t => t.QuerySelector("thead").TextContent == "Combat");
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
                case "Melee Final Blows":
                    MeleeFinalBlows = statValue;
                    break;
                case "Solo Kills":
                    SoloKills = statValue;
                    break;
                case "Objective Kills":
                    ObjectiveKills = statValue;
                    break;
                case "Final Blows":
                    FinalBlows = statValue;
                    break;
                case "Damage Done":
                    DamageDone = statValue;
                    break;
                case "Eliminations":
                    Eliminations = statValue;
                    break;
                case "Environmental Kills":
                    EnvironmentKills = statValue;
                    break;
                case "Multikills":
                    Multikills = statValue;
                    break;
            }
        }
    }
}
