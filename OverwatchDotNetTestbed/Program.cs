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
            WriteLine($"Completed download/parse for {playerPC.Username} in: {stopwatch.Elapsed}");
            var output = playerPC.CasualStats.AllHeroes.Game.GetModuleReadout();
            WriteLine($"Casual Stats:");
            foreach (var item in output)
                WriteLine($"{item.Key}: {item.Value}");
            WriteLine("---------------------------");
            output = playerPC.CompetitiveStats.AllHeroes.Game.GetModuleReadout();
            WriteLine($"Competitive Stats:");
            foreach (var item in output)
                WriteLine($"{item.Key}: {item.Value}");
            WriteLine("---------------------------");
        }
    }
}
