using OverwatchAPI.Internal;
using System;

// This code was not auto-generated but it damn well should've been.

namespace OverwatchAPI.Data
{
    public class CombatStats : IStatModule
    {
        public int MeleeFinalBlows { get; private set; }
        public int SoloKills { get; private set; }
        public int ObjectiveKills { get; private set; }       
        public int FinalBlows { get; private set; }
        public int DamageDone { get; private set; }
        public int Eliminations { get; private set; }
        public int EnvironmentKills { get; private set; }
        public int Multikills { get; private set; }

        public void SendTable(OverwatchDataTable table)
        {
            MeleeFinalBlows = table.Stats["Melee Final Blows"].OWValToInt();
            SoloKills = table.Stats["Solo Kills"].OWValToInt();
            ObjectiveKills = table.Stats["Objective Kills"].OWValToInt();
            FinalBlows = table.Stats["Final Blows"].OWValToInt();
            DamageDone = table.Stats["Damage Done"].OWValToInt();
            Eliminations = table.Stats["Eliminations"].OWValToInt();
            EnvironmentKills = table.Stats["Environmental Kills"].OWValToInt();
            Multikills = table.Stats["Multikills"].OWValToInt();
        }
    }

    public class AssistsStats : IStatModule
    {
        public int HealingDone { get; private set; }
        public int ReconAssists { get; private set; }
        public int TeleporterPadsDestroyed { get; private set; }

        public void SendTable(OverwatchDataTable table)
        {
            HealingDone = table.Stats["Healing Done"].OWValToInt();
            ReconAssists = table.Stats["Recon Assists"].OWValToInt();
            TeleporterPadsDestroyed = table.Stats["Teleporter Pads Destroyed"].OWValToInt();
        }
    }

    public class AverageStats : IStatModule
    {
        public float MeleeFinalBlows { get; private set; }
        public float FinalBlows { get; private set; }
        public float SoloKills { get; private set; }
        public float ObjectiveKills { get; private set; }
        public int HealingDone { get; private set; }
        public float Deaths { get; private set; }
        public int DamageDone { get; private set; }
        public float Eliminations { get; private set; }
        public TimeSpan ObjectiveTime { get; private set; }
        public TimeSpan TimeSpentOnFire { get; private set; }

        public void SendTable(OverwatchDataTable table)
        {
            MeleeFinalBlows = table.Stats["Melee Final Blows - Average"].OWValToFloat();
            FinalBlows = table.Stats["Final Blows - Average"].OWValToFloat();
            SoloKills = table.Stats["Solo Kills - Average"].OWValToFloat();
            ObjectiveKills = table.Stats["Objective Kills - Average"].OWValToFloat();
            HealingDone = table.Stats["Healing Done - Average"].OWValToInt();
            Deaths = table.Stats["Deaths - Average"].OWValToFloat();
            DamageDone = table.Stats["Damage Done - Average"].OWValToInt();
            Eliminations = table.Stats["Eliminations - Average"].OWValToFloat();
            ObjectiveTime = table.Stats["Objective Time - Average"].OWValToTimeSpan();
            TimeSpentOnFire = table.Stats["Time Spent on Fire - Average"].OWValToTimeSpan();
        }
    }

    public class BestStats : IStatModule
    {
        public int Eliminations { get; private set; }
        public int FinalBlows { get; private set; }
        public int DamageDone { get; private set; }
        public int HealingDone { get; private set; }
        public int DefensiveAssists { get; private set; }
        public int OffensiveAssists { get; private set; }
        public int ObjectiveKills { get; private set; }
        public int MultiKill { get; private set; }
        public int SoloKills { get; private set; }
        public TimeSpan TimeOnFire { get; private set; }
        public TimeSpan ObjectiveTime { get; private set; }

        public void SendTable(OverwatchDataTable table)
        {
            Eliminations = table.Stats["Eliminations - Most in Game"].OWValToInt();
            FinalBlows = table.Stats["Final Blows - Most in Game"].OWValToInt();
            DamageDone = table.Stats["Damage Done - Most in Game"].OWValToInt();
            HealingDone = table.Stats["Healing Done - Most in Game"].OWValToInt();
            DefensiveAssists = table.Stats["Defensive Assists - Most in Game"].OWValToInt();
            OffensiveAssists = table.Stats["Offensive Assists - Most in Game"].OWValToInt();
            ObjectiveKills = table.Stats["Objective Kills - Most in Game"].OWValToInt();
            MultiKill = table.Stats["Multikill - Best"].OWValToInt();
            SoloKills = table.Stats["Solo Kills - Most in Game"].OWValToInt();
            TimeOnFire = table.Stats["Time Spent on Fire - Most in Game"].OWValToTimeSpan();
            ObjectiveTime = table.Stats["Objective Time - Most in Game"].OWValToTimeSpan();
        }
    }

    public class DeathStats : IStatModule
    {
        public int Deaths { get; private set; }
        public int EnvironmentalDeaths { get; private set; }

        public void SendTable(OverwatchDataTable table)
        {
            Deaths = table.Stats["Deaths"].OWValToInt();
            EnvironmentalDeaths = table.Stats["Environmental Deaths"].OWValToInt();
        }
    }

    public class GameStats : IStatModule
    {
        public int GamesPlayed { get; private set; }
        public int GamesWon { get; private set; }
        public int GamesLost { get; private set; }
        public int Score { get; private set; }
        public TimeSpan TimeSpentOnFire { get; private set; }
        public TimeSpan ObjectiveTime { get; private set; }
        public TimeSpan TimePlayed { get; private set; }

        public void SendTable(OverwatchDataTable table)
        {
            GamesWon = table.Stats["Games Won"].OWValToInt();
            GamesPlayed = table.Stats["Games Played"].OWValToInt();
            GamesLost = GamesPlayed - GamesWon;
            TimeSpentOnFire = table.Stats["Time Spent on Fire"].OWValToTimeSpan();
            ObjectiveTime = table.Stats["Objective Time"].OWValToTimeSpan();
            Score = table.Stats["Score"].OWValToInt();
            TimePlayed = table.Stats["Time Played"].OWValToTimeSpan();
        }
    }

    public class MatchAwardsStats : IStatModule
    {
        public int Cards { get; private set; }
        public int Medals { get; private set; }
        public int MedalsGold { get; private set; }
        public int MedalsSilver { get; private set; }
        public int MedalsBronze { get; private set; }

        public void SendTable(OverwatchDataTable table)
        {
            Cards = table.Stats["Cards"].OWValToInt();
            Medals = table.Stats["Medals"].OWValToInt();
            MedalsGold = table.Stats["Medals - Gold"].OWValToInt();
            MedalsSilver = table.Stats["Medals - Silver"].OWValToInt();
            MedalsBronze = table.Stats["Medals - Bronze"].OWValToInt();
        }
    }

    public class MiscellaneousStats : IStatModule
    {
        public int MostMeleeFinalBlows { get; private set; }
        public int DefensiveAssists { get; private set; }
        public int DefensiveAssistsAverage { get; private set; }
        public int OffensiveAssists { get; private set; }
        public int OffensiveAssistsAverage { get; private set; }

        public void SendTable(OverwatchDataTable table)
        {
            MostMeleeFinalBlows = table.Stats["Melee Final Blows - Most in Game"].OWValToInt();
            DefensiveAssists = table.Stats["Defensive Assists"].OWValToInt();
            DefensiveAssistsAverage = table.Stats["Defensive Assists - Average"].OWValToInt();
            OffensiveAssists = table.Stats["Offensive Assists"].OWValToInt();
            OffensiveAssistsAverage = table.Stats["Offensive Assists - Average"].OWValToInt();
        }
    }
}
