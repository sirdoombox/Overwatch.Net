using OverwatchAPI.Internal;
using System;

// This code was not auto-generated but it damn well should've been.

namespace OverwatchAPI.Data
{
    public class CombatStats
    {
        [OverwatchStat("Melee Final Blows")]
        public int MeleeFinalBlows { get; private set; }

        [OverwatchStat("Solo Kills")]
        public int SoloKills { get; private set; }

        [OverwatchStat("Objective Kills")]
        public int ObjectiveKills { get; private set; }

        [OverwatchStat("Final Blows")]
        public int FinalBlows { get; private set; }

        [OverwatchStat("Damage Done")]
        public int DamageDone { get; private set; }

        [OverwatchStat("Eliminations")]
        public int Eliminations { get; private set; }

        [OverwatchStat("Environmental Kills")]
        public int EnvironmentKills { get; private set; }

        [OverwatchStat("Multikills")]
        public int Multikills { get; private set; }
    }

    public class AssistsStats
    {
        [OverwatchStat("Healing Done")]
        public float HealingDone { get; private set; }

        [OverwatchStat("Recon Assists")]
        public float ReconAssists { get; private set; }

        [OverwatchStat("Teleporter Pads Destroyed")]
        public float TeleporterPadsDestroyed { get; private set; }
    }

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

    public class DeathStats
    {
        [OverwatchStat("Deaths")]
        public int Deaths { get; private set; }
        [OverwatchStat("Environmental Deaths")]
        public int EnvironmentalDeaths { get; private set; }
    }

    public class GameStats
    {
        [OverwatchStat("Games Won")]
        public int GamesWon { get; private set; }

        public int GamesLost { get { return GamesPlayed - GamesWon; } private set { } }

        [OverwatchStat("Games Played")]
        public int GamesPlayed { get; private set; }

        [OverwatchStat("Time Spent on Fire")]
        public TimeSpan TimeSpentOnFire { get; private set; }

        [OverwatchStat("Objective Time")]
        public TimeSpan ObjectiveTime { get; private set; }

        [OverwatchStat("Score")]
        public int Score { get; private set; }

        [OverwatchStat("Time Played")]
        public TimeSpan TimePlayed { get; private set; }
    }

    public class MatchAwardsStats
    {
        [OverwatchStat("Cards")]
        public float Cards { get; private set; }

        [OverwatchStat("Medals")]
        public float Medals { get; private set; }

        [OverwatchStat("Medals - Gold")]
        public float MedalsGold { get; private set; }

        [OverwatchStat("Medals - Silver")]
        public float MedalsSilver { get; private set; }

        [OverwatchStat("Medals - Bronze")]
        public float MedalsBronze { get; private set; }
    }

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
