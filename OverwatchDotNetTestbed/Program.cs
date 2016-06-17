using OverwatchDotNet.OverwatchData;
using System.Diagnostics;
using static System.Console;

namespace OverwatchDotNetTestbed
{
    class Program
    {
        static string profileUrl = "https://playoverwatch.com/en-gb/career/pc/eu/SirDoombox-2603";
        static void Main(string[] args)
        {
            PlayerStats temp = new PlayerStats();
            Stopwatch stopwatch = Stopwatch.StartNew();
            temp.PopulatePlayer(profileUrl);
            stopwatch.Stop();
            WriteLine($"Completed In: {stopwatch.Elapsed}");
            WriteLine($"Deaths: {temp.Deaths.Deaths}");
            WriteLine($"Best Eliminations: {temp.Best.Eliminations}");
            WriteLine($"Assists Healing Done: {temp.Assists.HealingDone}");
            WriteLine($"Time Spent On Fire Total: {temp.Game.TimeSpentOnFire}");
            ReadKey();
        }
    }
}
