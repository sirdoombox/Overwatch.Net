using AngleSharp;
using AngleSharp.Dom;
using OverwatchDotNet.Internal;
using System.Threading.Tasks;

namespace OverwatchDotNet.OverwatchData
{
    /// <summary>
    /// The featured stats of an Overwatch player - Averages across all heroes in all games.
    /// </summary>
    public class FeaturedStats : IStatModule
    {
        public float Eliminations { get; private set; }
        public float DamageDone { get; private set; }
        public float Deaths { get; private set; }
        public float FinalBlows { get; private set; }
        public float HealingDone { get; private set; }
        public float ObjectiveKills { get; private set; }
        public float ObjectiveTime { get; private set; }
        public float SoloKills { get; private set; }

        public void LoadFromURL(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var document = BrowsingContext.New(config).OpenAsync(url).Result;
            var cards = document.QuerySelectorAll(".card");
            foreach(var card in cards)
            {
                AssignValue(card.QuerySelector(".card-copy").TextContent.Replace(" - Average", ""), card.QuerySelector(".card-heading").TextContent.OverwatchValueStringToFloat());
            }
        }

        public async Task LoadFromURLAsync(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenAsync(url);
            var cards = document.QuerySelectorAll(".card");
            foreach (var card in cards)
            {
                AssignValue(card.QuerySelector(".card-copy").TextContent.Replace(" - Average", ""), card.QuerySelector(".card-heading").TextContent.OverwatchValueStringToFloat());
            }
        }

        public void LoadFromDocument(IDocument document)
        {
            var cards = document.QuerySelectorAll(".card");
            foreach (var card in cards)
            {
                AssignValue(card.QuerySelector(".card-copy").TextContent.Replace(" - Average", ""), card.QuerySelector(".card-heading").TextContent.OverwatchValueStringToFloat());
            }
        }

        void AssignValue(string statName, float statValue)
        {
            switch(statName)
            {
                case "Eliminations":
                    Eliminations = statValue;
                    break;
                case "Damage Done":
                    DamageDone = statValue;
                    break;
                case "Deaths":
                    Deaths = statValue;
                    break;
                case "Final Blows":
                    FinalBlows = statValue;
                    break;
                case "Healing Done":
                    HealingDone = statValue;
                    break;
                case "Objective Kills":
                    ObjectiveKills = statValue;
                    break;
                case "Objective Time":
                    ObjectiveTime = statValue;
                    break;
                case "Solo Kills":
                    SoloKills = statValue;
                    break;
            }
        }
    }
}
