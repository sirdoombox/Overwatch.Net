using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using OverwatchDotNet.Internal;
using AngleSharp;
using OverwatchDotNet.Core;
using System;

namespace OverwatchDotNet.OverwatchData
{
    public class AverageStats
    {
        [OverwatchStat("Melee Final Blows - Average")]
        public float MeleeFinalBlows { get; private set; }

        [OverwatchStat("Final Blows - Average")]
        public float FinalBlows { get; private set; }

        [OverwatchStat("Time Spent on Fire - Average")]
        public float TimeSpentonFire { get; private set; }

        [OverwatchStat("Solo Kills - Average")]
        public float SoloKills { get; private set; }

        [OverwatchStat("Objective Time - Average")]
        public float ObjectiveTime { get; private set; }

        [OverwatchStat("Objective Kills - Average")]
        public float ObjectiveKills { get; private set; }

        [OverwatchStat("Healing Done - Average")]
        public float HealingDone { get; private set; }

        [OverwatchStat("Final Blows - Average")]
        public float Deaths { get; private set; }

        [OverwatchStat("Deaths - Average")]
        public float DamageDone { get; private set; }

        [OverwatchStat("Eliminations - Average")]
        public float Eliminations { get; private set; }

        public void LoadFromURL(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var document = BrowsingContext.New(config).OpenAsync(url).Result;
            var table = document.QuerySelectorAll("table.data-table").FirstOrDefault(t => t.QuerySelector("thead").TextContent == "Average");
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
            var table = document.QuerySelectorAll("table.data-table").FirstOrDefault(t => t.QuerySelector("thead").TextContent == "Average");
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
            var table = document.QuerySelectorAll("table.data-table").FirstOrDefault(t => t.QuerySelector("thead").TextContent == "Average");
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
                case "MeleeFinalBlows":
                    MeleeFinalBlows = statValue;
                    break;
                case "FinalBlows":
                    FinalBlows = statValue;
                    break;
                case "TimeSpentonFire":
                    TimeSpentonFire = statValue;
                    break;
                case "SoloKills":
                    SoloKills = statValue;
                    break;
                case "ObjectiveTime":
                    ObjectiveTime = statValue;
                    break;
                case "ObjectiveKills":
                    ObjectiveKills = statValue;
                    break;
                case "HealingDone":
                    HealingDone = statValue;
                    break;
                case "Deaths":
                    Deaths = statValue;
                    break;
                case "DamageDone":
                    DamageDone = statValue;
                    break;
                case "Eliminations":
                    Eliminations = statValue;
                    break;
            }
        }
    }
}
