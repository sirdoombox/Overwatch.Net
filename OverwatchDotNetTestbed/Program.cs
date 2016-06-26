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
            OverwatchPlayer playerPC = new OverwatchPlayer("SirDoombox#2603", Platform.pc, Region.eu);
            Stopwatch stopwatch = Stopwatch.StartNew();
            await playerPC.UpdateStats();
            stopwatch.Stop();
            WriteLine($"Completed download/parse in: {stopwatch.Elapsed}");
            var output = playerPC.Stats.AllHeroes.Game.GetModuleReadout();
            WriteLine($"{playerPC.Username} Stats:");
            foreach (var item in output)
                WriteLine($"{item.Key}: {item.Value}");
            WriteLine("---------------------------");
        }
    }
}
