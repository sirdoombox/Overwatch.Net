using OverwatchAPI.Internal;
using System;

namespace OverwatchAPI.Data
{
    public class AverageStats
    {
        [OverwatchStat("Melee Final Blows - Average")]
        public float MeleeFinalBlows { get; private set; }

        [OverwatchStat("Final Blows - Average")]
        public float FinalBlows { get; private set; }

        [OverwatchStat("Time Spent on Fire - Average")]
        public TimeSpan TimeSpentonFire { get; private set; }

        [OverwatchStat("Solo Kills - Average")]
        public float SoloKills { get; private set; }

        [OverwatchStat("Objective Time - Average")]
        public TimeSpan ObjectiveTime { get; private set; }

        [OverwatchStat("Objective Kills - Average")]
        public float ObjectiveKills { get; private set; }

        [OverwatchStat("Healing Done - Average")]
        public int HealingDone { get; private set; }

        [OverwatchStat("Deaths - Average")]
        public float Deaths { get; private set; }

        [OverwatchStat("Damage Done - Average")]
        public int DamageDone { get; private set; }

        [OverwatchStat("Eliminations - Average")]
        public float Eliminations { get; private set; }
    }
}
