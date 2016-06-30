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
            OverwatchPlayer player = new OverwatchPlayer("SirDoombox#2603", Platform.pc, Region.eu);
            Stopwatch stopwatch = Stopwatch.StartNew();
            await player.UpdateStats();
            stopwatch.Stop();
            WriteLine($"Completed download/parse for {player.Username} in: {stopwatch.Elapsed}");
            WriteLine($"Player Level: {player.PlayerLevel} | Player Rank: {player.CompetitiveRank}");
            WriteLine($"Casual Stats:");
            var output = player.CasualStats.AllHeroes.Game.GetModuleReadout();
            foreach (var item in output)
                WriteLine($"{item.Key}: {item.Value}");
            WriteLine("---------------------------");  
                    
            WriteLine($"Competitive Stats:");
            output = player.CompetitiveStats.AllHeroes.Game.GetModuleReadout();
            foreach (var item in output)
                WriteLine($"{item.Key}: {item.Value}");
            WriteLine("---------------------------");
        }
    }
}
