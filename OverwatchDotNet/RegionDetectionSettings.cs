namespace OverwatchAPI
{
    public sealed class RegionDetectionSettings
    {
        internal Region[] regions;

        /// <summary>
        /// Default Configuration - US -> EU -> KR
        /// </summary>
        public RegionDetectionSettings Default
        {
            get { return new RegionDetectionSettings(); }
        }

        /// <summary>
        /// Create a Region detection settings object with all the regions you wish to detect IN ORDER.
        /// </summary>
        /// <param name="regions">The regions to detect and the order in which to detect them.</param>
        public RegionDetectionSettings(params Region[] regions)
        {
            this.regions = regions;
        }

        internal RegionDetectionSettings()
        {
            this.regions = new Region[] { Region.us, Region.eu, Region.kr };
        }
    }
}
