using OverwatchAPI;
using OverwatchAPI.Internal;
using System.Collections.Generic;
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
            List<string> PlayerNames = new List<string> { "SirDoombox#2603", "VeLo InFerno", "Rolingachu" };
            foreach(var username in PlayerNames)
            {
                OverwatchPlayer player = new OverwatchPlayer(username);
                await player.DetectPlatform();
                await player.DetectRegionPC();
                await player.UpdateStats();
                WritePlayer(player);
            }         
        }

        void WritePlayer(OverwatchPlayer player)
        {
            WriteLine($"{player.Username} | Platform: {player.Platform} | Level: {player.PlayerLevel} | Rank: {player.CompetitiveRank}");
            WriteLine("---------------------------");
            WriteLine($"Casual Stats:");
            var output = player.CasualStats.AllHeroes.Game.GetModuleReadout();
            foreach (var item in output)
                WriteLine($"{item.Key}: {item.Value}");
            WriteLine("---------------------------");
            try
            {
                output = player.CompetitiveStats.AllHeroes.Game.GetModuleReadout();
                WriteLine($"Competitive Stats:");
                foreach (var item in output)
                    WriteLine($"{item.Key}: {item.Value}");
                
            }
            catch
            {
                WriteLine("No Competitive Stats");
            }
            WriteLine("---------------------------\n\n");
        }
    }
}
