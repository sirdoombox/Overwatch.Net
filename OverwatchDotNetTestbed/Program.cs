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
            OverwatchPlayer playerPC = new OverwatchPlayer("SirDoombox#2603", Platform.pc , Region.eu);
            OverwatchPlayer playerXBL = new OverwatchPlayer("VeLo InFerno", Platform.xbl);
            OverwatchPlayer playerPSN = new OverwatchPlayer("Eztun", Platform.psn);
            Stopwatch stopwatch = Stopwatch.StartNew();
            await playerPC.UpdateStats();
            await playerXBL.UpdateStats();
            await playerPSN.UpdateStats();
            stopwatch.Stop();
            WriteLine($"Completed download/parse in: {stopwatch.Elapsed}");
            var output = playerPC.Stats.AllHeroes.GetCategoryReadout(playerPC.Stats.AllHeroes.Game);
            WriteLine($"{playerPC.Username} Stats:");
            output.ForEach(x => WriteLine(x));
            WriteLine("---------------------------");
            WriteLine($"{playerXBL.Username} Stats:");
            output = playerXBL.Stats.AllHeroes.GetCategoryReadout(playerXBL.Stats.AllHeroes.Game);
            output.ForEach(x => WriteLine(x));
            WriteLine("---------------------------");
            WriteLine($"{playerPSN.Username} Stats:");
            output = playerPSN.Stats.AllHeroes.GetCategoryReadout(playerPSN.Stats.AllHeroes.Game);
            output.ForEach(x => WriteLine(x));
            WriteLine("---------------------------");
        }
    }
}
