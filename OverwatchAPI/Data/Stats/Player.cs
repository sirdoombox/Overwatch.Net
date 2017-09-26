namespace OverwatchAPI
{
    public sealed class Player
    {
        public string Username { get; set; }
        internal string UsernameUrlFriendly => Username.BattletagToUrlFriendlyString();
        public Region Region { get; set; }
        public Platform Platform { get; set; }
        public string ProfileUrl { get; set; }
        public ushort PlayerLevel { get; set; }
        public string PlayerLevelImage { get; set; }
        public ushort CompetitiveRank { get; set; }
        public Stats CasualStats { get; set; }
        public Stats CompetitiveStats { get; set; }
        public Achievements Achievements { get; set; }
        public string CompetitiveRankImageUrl { get; set; }
        public string ProfilePortraitUrl { get; set; }
    }
}
