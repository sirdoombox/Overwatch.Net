using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom;
using OverwatchDotNet.Internal;
using AngleSharp;

namespace OverwatchDotNet.OverwatchData
{
    public class AverageData : IStatModule
    {
        public float MeleeFinalBlows { get; private set; }
        public float FinalBlows { get; private set; }
        public float TimeSpentonFire { get; private set; }
        public float SoloKills { get; private set; }
        public float ObjectiveTime { get; private set; }
        public float ObjectiveKills { get; private set; }
        public float HealingDone { get; private set; }
        public float Deaths { get; private set; }
        public float DamageDone { get; private set; }
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
