using OverwatchDotNet.Core;

namespace OverwatchDotNet.OverwatchData
{
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
}
