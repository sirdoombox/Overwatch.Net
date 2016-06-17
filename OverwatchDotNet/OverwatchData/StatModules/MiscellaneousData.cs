using OverwatchDotNet.Core;

namespace OverwatchDotNet.OverwatchData
{
    public class MiscellaneousStats
    {
        [OverwatchStat("Melee Final Blows - Most in Game")]
        public int MostMeleeFinalBlows { get; private set; }

        [OverwatchStat("Defensive Assists")]
        public int DefensiveAssists { get; private set; }

        [OverwatchStat("Defensive Assists - Average")]
        public int DefensiveAssistsAverage { get; private set; }

        [OverwatchStat("Offensive Assists")]
        public int OffensiveAssists { get; private set; }

        [OverwatchStat("Offensive Assists - Average")]
        public int OffensiveAssistsAverage { get; private set; }       
    }
}
