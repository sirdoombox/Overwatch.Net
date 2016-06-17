using OverwatchAPI.Internal;
using System;

namespace OverwatchAPI.Data
{
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
}
