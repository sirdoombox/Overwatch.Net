using OverwatchAPI;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using static System.Console;

namespace OverwatchDotNetTestbed
{
    class Testbed
    {
        static void Main(string[] args)
        {
            new Task(new Testbed().RunDemo).Start();
            ReadKey();
        }

        async void RunDemo()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            List<OverwatchPlayer> playerCollection = new List<OverwatchPlayer>
            {
                new OverwatchPlayer("moiph#1288", Platform.pc, Region.us),
                new OverwatchPlayer("SirDoombox#2603", Platform.pc),
                new OverwatchPlayer("Rolingachu")
            };
            foreach (var player in playerCollection)
            {
                await player.UpdateStatsAsync();
                Thread.Sleep(10000); // Dirty rate limiting workaround - Seems to hang for a very long time (if not indefinitely) when making many requests quickly               
            }
            foreach (OverwatchPlayer player in playerCollection)
                WritePlayer(player);
            stopwatch.Stop();
            WriteLine($"\n\nOperation Completed in {stopwatch.Elapsed}");
        }

        void WritePlayer(OverwatchPlayer player)
        {
            WriteLine($"{player.Username} | Platform: {player.Platform} | Level: {player.PlayerLevel} | Rank: {player.CompetitiveRank}");
            WriteLine($"{player.ProfilePortraitURL}");
            WriteLine("---------------------------");
            WriteLine($"Casual Stats:");
            foreach (var item in player.CasualStats["AllHeroes"]["Game"])
                WriteLine($"{item.Key}: {item.Value}");
            WriteLine("---------------------------");
            WriteLine($"Competitive Stats:");
            if (player.CompetitiveStats != null)
            {
                foreach (var item in player.CompetitiveStats["AllHeroes"]["Game"])
                    WriteLine($"{item.Key}: {item.Value}");
            }
            WriteLine("---------------------------");
            WriteLine($"General Achievements: ");
            foreach (var item in player.Achievements["General"])
                WriteLine($"{item.Key}: { item.Value} ");
            WriteLine("---------------------------\n\n");
        }
    }
}
