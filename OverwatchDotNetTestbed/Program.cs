using OverwatchAPI;
using System.Diagnostics;
using static System.Console;

namespace OverwatchDotNetTestbed
{
    class Program
    {      
        static void Main(string[] args) => new Program().Start();

        void Start()
        {
            OverwatchPlayer player = new OverwatchPlayer("SirDoombox#2603");
            Stopwatch stopwatch = Stopwatch.StartNew();
            player.DetectRegion().GetAwaiter().GetResult();
            player.UpdateStats().GetAwaiter().GetResult();
            WriteLine(player.Stats.AllHeroes.Best.DamageDoneMostinGame);
            ReadKey();
        }
    }
}
