using OverwatchAPI;
using System.Collections.Generic;
using System.Diagnostics;
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
                new OverwatchPlayer("GMK#11870"),
                new OverwatchPlayer("SirDoombox#2603"),
                new OverwatchPlayer("VeLo InFerno"),
                new OverwatchPlayer("Rolingachu")
            };

            foreach(var player in playerCollection)
            {
                await player.DetectPlatform();
                await player.DetectRegionPC();
                await player.UpdateStats();
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
            foreach (var item in player.CasualStats.GetHero("AllHeroes").GetCategory("Game"))
                WriteLine($"{item.Name}: {item.Value}");
            WriteLine("---------------------------");
            WriteLine($"Competitive Stats:");
            foreach (var item in player.CompetitiveStats.GetHero("AllHeroes").GetCategory("Game"))
                WriteLine($"{item.Name}: {item.Value}");
            WriteLine("---------------------------");
            WriteLine($"General Achievements: ");
            foreach (var item in player.Achievements.GetCategory("General"))
                WriteLine($"{item.Name}: { item.IsUnlocked} ");
            WriteLine("---------------------------\n\n");
        }
    }
}
