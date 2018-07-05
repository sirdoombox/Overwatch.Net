using System.Collections.Generic;
using Newtonsoft.Json;
using OverwatchAPI.Data;

namespace OverwatchAPI
{
    public sealed class Player
    {
        public string Username { get; set; }
        internal string UsernameUrlFriendly => Username.BattletagToUrlFriendlyString();
        /// <summary>
        /// The platform for this specific set of stats.
        /// </summary>
        public Platform Platform { get; set; }
        /// <summary>
        /// If a player has profiles on other systems they can be gathered using <see cref="OverwatchClient.GetOtherProfileInfo(Player)"/>
        /// The keys will be populated by gathering profile info (if there are any other profiles
        /// The values will be only populated if you use "GetOtherProfiles" method of <see cref="OverwatchClient"/>
        /// </summary>
        public Dictionary<Alias,Player> OtherKnownProfiles { get; set; }

        /// <summary>
        /// Hidden Blizzard ID for the player - this ties accounts on seperate platforms together.
        /// </summary>
        public string PlayerId { get; set; }
        public string ProfileUrl { get; set; }
        public ushort PlayerLevel { get; set; }
        public string PlayerLevelImage { get; set; }
        public ushort CompetitiveRank { get; set; }
        public ushort EndorsementLevel { get; set; }
        /// <summary>
        /// If the players profile is private - No stats/achievements will be available.
        /// </summary>
        public bool IsProfilePrivate { get; set; }
        public List<Stat> CasualStats { get; set; }
        public List<Stat> CompetitiveStats { get; set; }
        public List<Stat> Endorsements { get; set; }
        public List<Achievement> Achievements { get; set; }
        public string CompetitiveRankImageUrl { get; set; }
        public string ProfilePortraitUrl { get; set; }        

        public sealed class Alias
        {
            public Platform Platform { get; set; }
            public string Username { get; set; }
            internal string UrlName;
            public Visibility ProfileVisibility { get; set; }
        }
    }
}
