using OverwatchAPI;
using System.Diagnostics;
using System.Threading.Tasks;
using static System.Console;

namespace OverwatchDotNetTestbed
{
    class Program
    {
        static void Main(string[] args)
        {
            Task runWithDetection = new Task(new Program().PopulatePlayerWithRegionDetection);
            runWithDetection.Start();
            ReadKey();
        }

        async void PopulatePlayerWithRegionDetection()
        {
            OverwatchPlayer player = new OverwatchPlayer("SirDoombox#2603");
            Stopwatch stopwatch = Stopwatch.StartNew();
            //await player.DetectRegion();
            await player.UpdateStats();
            stopwatch.Stop();
            WriteLine($"Region detected, data downloaded and parsed for {player.Battletag} in: {stopwatch.Elapsed}");
            WriteLine($"Most damage done in one game: {player.Stats.AllHeroes.Best.DamageDoneMostinGame}");
            
            WriteLine($"Gold medals as Reinhardt: {player.Stats.Reinhardt.MatchAwards.MedalsGold}");
            WriteLine($"Average amount of healing done as Lucio: {player.Stats.Lucio.Average.HealingDoneAverage}");
        }
    }
}
