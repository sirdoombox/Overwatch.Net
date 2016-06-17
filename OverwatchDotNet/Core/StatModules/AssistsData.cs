using OverwatchAPI.Internal;

namespace OverwatchAPI.Data
{
    public class AssistsStats
    {
        [OverwatchStat("Healing Done")]
        public float HealingDone { get; private set; }

        [OverwatchStat("Recon Assists")]
        public float ReconAssists { get; private set; }

        [OverwatchStat("Teleporter Pads Destroyed")]
        public float TeleporterPadsDestroyed { get; private set; }
    }
}
