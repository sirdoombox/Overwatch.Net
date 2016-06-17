using OverwatchDotNet.OverwatchData;
using System;
using System.Collections.Generic;

namespace OverwatchDotNetTestbed
{
    class Program
    {
        static string profileUrl = "https://playoverwatch.com/en-gb/career/pc/eu/SirDoombox-2603";
        static void Main(string[] args)
        {
            CombatStats tempStats = new CombatStats();
            tempStats.LoadFromURL(profileUrl);
            Console.WriteLine($"Damage Done: {tempStats.DamageDone}");
            Console.WriteLine($"Elimination: {tempStats.Eliminations}");
            Console.ReadKey();
        }
    }
}
