using OverwatchDotNet.Core;

namespace OverwatchDotNet.OverwatchData
{
    public class DeathStats
    {
        [OverwatchStat("Deaths")]
        public int Deaths { get; private set; }
        [OverwatchStat("Environmental Deaths")]
        public int EnvironmentalDeaths { get; private set; }                     
    }
}
