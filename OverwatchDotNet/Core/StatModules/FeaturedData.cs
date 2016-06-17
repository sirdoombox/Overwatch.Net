using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Threading.Tasks;

namespace OverwatchAPI.Data
{
    /// <summary>
    /// The featured stats of an Overwatch player - Averages across all heroes in all games.
    /// </summary>
    public class FeaturedStats
    {
        public float Eliminations { get; private set; }
        public int DamageDone { get; private set; }
        public float Deaths { get; private set; }
        public float FinalBlows { get; private set; }
        public int HealingDone { get; private set; }
        public float ObjectiveKills { get; private set; }
        public TimeSpan ObjectiveTime { get; private set; }
        public float SoloKills { get; private set; }

        public void LoadFromURL(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var document = BrowsingContext.New(config).OpenAsync(url).Result;
            var cards = document.QuerySelectorAll(".card");
            foreach(var card in cards)
            {
                //AssignValue(card.QuerySelector(".card-copy").TextContent.Replace(" - Average", ""), card.QuerySelector(".card-heading").TextContent);
            }
        }

        public async Task LoadFromURLAsync(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenAsync(url);
            var cards = document.QuerySelectorAll(".card");
            foreach (var card in cards)
            {
                //AssignValue(card.QuerySelector(".card-copy").TextContent.Replace(" - Average", ""), card.QuerySelector(".card-heading").TextContent);
            }
        }

        public void LoadFromDocument(IDocument document)
        {
            var cards = document.QuerySelectorAll(".card");
            foreach (var card in cards)
            {
                //AssignValue(card.QuerySelector(".card-copy").TextContent.Replace(" - Average", ""), card.QuerySelector(".card-heading").TextContent);
            }
        }
    }
}
