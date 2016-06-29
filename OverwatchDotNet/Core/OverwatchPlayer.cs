using AngleSharp;
using AngleSharp.Dom;
using OverwatchAPI.Internal;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OverwatchAPI
{
    public class OverwatchPlayer
    {
        public OverwatchPlayer(string username, Platform platform = Platform.pc, Region region = Region.none, string profileurl = null)
        {
            if (!new Regex(@"\w+#\d+").IsMatch(username) && platform == Platform.pc)
                throw new InvalidBattletagException();
            Username = username;
            BattletagUrlFriendly = username.Replace("#", "-");
            Region = region;
            Platform = platform;
            if((Region != Region.none || platform != Platform.pc) && profileurl == null)
            {
                ProfileURL = OverwatchAPIHelpers.ProfileURL(Username, Region, Platform);
            }
        }

        /// <summary>
        /// The players Battletag with Discriminator - e.g. "SomeUser#1234"
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// The PlayOverwatch profile of the player - This is only available if the user has set a region
        /// </summary>
        public string ProfileURL { get; private set; }

        /// <summary>
        /// The Player Level of the player
        /// </summary>
        public ushort PlayerLevel { get; private set; }

        /// <summary>
        /// The Competitive Rank of the player
        /// </summary>
        public ushort CompetitiveRank { get; private set; }

        /// <summary>
        /// The player's region - EU/US/None
        /// </summary>
        public Region Region { get; private set; } 

        /// <summary>
        /// The player's platform - PC/XBL/PSN
        /// </summary>
        public Platform Platform { get; private set; }

        /// <summary>
        /// The players quick-play stats.
        /// </summary>
        public PlayerStats CasualStats { get; private set; }

        /// <summary>
        /// The players competitive stats.
        /// </summary>
        public PlayerStats CompetitiveStats { get; private set; }

        /// <summary>
        /// The last time the profile was downloaded from PlayOverwatch.
        /// </summary>
        public DateTime ProfileLastDownloaded { get; private set; }

        /// <summary>
        /// The URL friendly version of the users Battletag.
        /// </summary>
        private string BattletagUrlFriendly { get; }

        /// <summary>
        /// Detect the region of the player (Also sets the players ProfileURL if it is currently un-set) - THIS ONLY WORKS FOR PC PLAYERS. CONSOLE PLAYERS DO NOT HAVE REGIONS.
        /// </summary>
        /// <returns></returns>
        public async Task DetectRegionPC()
        {
            string baseUrl = "http://playoverwatch.com/en-gb/career/";
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri(baseUrl);
            var responseNA = await _client.GetAsync($"{Platform}/us/{BattletagUrlFriendly}");
            if (responseNA.IsSuccessStatusCode)
            {
                Region = Region.us;
                ProfileURL = ProfileURL ?? baseUrl + $"{Platform}/us/{BattletagUrlFriendly}";
                return;
            }
            else
            {
                var responseEU = await _client.GetAsync($"{Platform}/eu/{BattletagUrlFriendly}");
                if (responseEU.IsSuccessStatusCode)
                {
                    Region = Region.eu;
                    ProfileURL = ProfileURL ?? baseUrl + $"{Platform}/eu/{BattletagUrlFriendly}";
                    return;
                }
            }
            Region = Region.none;
        }      
        
        /// <summary>
        /// Downloads the Users Profile and parses it to
        /// </summary>
        /// <returns></returns>
        public async Task UpdateStats()
        {
            if (Region == Region.none && Platform == Platform.pc)
                throw new UserRegionNotDefinedException();
            var userpage = await DownloadUserPage();
            PlayerLevel = 0;
            var levelElement = userpage.QuerySelector("div.player-level div");
            ushort parsedPlayerLevel = 0;
            if (levelElement != null && ushort.TryParse(levelElement.TextContent, out parsedPlayerLevel))
                PlayerLevel = parsedPlayerLevel;
            CompetitiveRank = 0;
            var rankElement = userpage.QuerySelector("div.competitive-rank div");
            ushort parsedCompetitiveRank = 0;
            if (rankElement != null &&ushort.TryParse(rankElement.TextContent, out parsedCompetitiveRank))
                CompetitiveRank = parsedCompetitiveRank;
            CasualStats = new PlayerStats();
            CompetitiveStats = new PlayerStats();
            CasualStats.UpdateStatsFromPage(userpage, Mode.Casual);
            CompetitiveStats.UpdateStatsFromPage(userpage, Mode.Competitive);
            ProfileLastDownloaded = DateTime.UtcNow;
        }

        internal async Task<IDocument> DownloadUserPage()
        {
            var config = Configuration.Default.WithDefaultLoader();
            if (ProfileURL == null)
                throw new UserProfileUrlNullException();
            return await BrowsingContext.New(config).OpenAsync(ProfileURL);
        }
    }

}
