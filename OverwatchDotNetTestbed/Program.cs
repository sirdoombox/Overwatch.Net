using OverwatchAPI;
using OverwatchAPI.Internal;
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
            OverwatchPlayer player = new OverwatchPlayer("SirDoombox#2603", Region.eu);
            Stopwatch stopwatch = Stopwatch.StartNew();
            //await player.DetectRegion();
            await player.UpdateStats();
            stopwatch.Stop();
            var output = player.Stats.Junkrat.GetCategoryReadout(player.Stats.Junkrat.HeroSpecific);
            output.ForEach(x => WriteLine(x));
        }
    }
}
