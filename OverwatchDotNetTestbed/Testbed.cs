using OverwatchAPI;
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
            OverwatchPlayerCollection playerCollection = new OverwatchPlayerCollection
            {
                new OverwatchPlayer("SirDoombox#2603"),
                new OverwatchPlayer("VeLo InFerno"),
                new OverwatchPlayer("Rolingachu")
            };

            await playerCollection.DetectPlayerPlatforms();
            await playerCollection.DetectPlayerRegions();
            await playerCollection.UpdatePlayers();

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
