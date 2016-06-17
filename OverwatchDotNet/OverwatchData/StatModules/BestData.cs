using OverwatchDotNet.Core;
using System;

namespace OverwatchDotNet.OverwatchData
{
    public class BestStats
    {
        [OverwatchStat("Eliminations - Most in Game")]
        public int Eliminations { get; private set; }

        [OverwatchStat("Final Blows - Most in Game")]
        public int FinalBlows { get; private set; }

        [OverwatchStat("Damage Done - Most in Game")]
        public int DamageDone { get; private set; }

        [OverwatchStat("Healing Done - Most in Game")]
        public int HealingDone { get; private set; }

        [OverwatchStat("Defensive Assists - Most in Game")]
        public int DefensiveAssists { get; private set; }

        [OverwatchStat("Offensive Assists - Most in Game")]
        public int OffensiveAssists { get; private set; }

        [OverwatchStat("Objective Kills - Most in Game")]
        public int ObjectiveKills { get; private set; }

        [OverwatchStat("Objective Time - Most in Game")]
        public TimeSpan ObjectiveTime { get; private set; }

        [OverwatchStat("Multikill - Best")]
        public int MultiKill { get; private set; }

        [OverwatchStat("Solo Kills - Most in Game")]
        public int SoloKills { get; private set; }
        
        [OverwatchStat("Time Spent on Fire - Most in Game")]
        public TimeSpan TimeOnFire { get; private set; }
    }
}
