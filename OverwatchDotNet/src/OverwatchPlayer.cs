using AngleSharp;
using AngleSharp.Dom;
using OverwatchAPI.Internal;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static OverwatchAPI.OverwatchAPIHelpers;

namespace OverwatchAPI
{
    public class OverwatchPlayer
    {
        /// <summary>
        /// Construct a new Overwatch player.
        /// </summary>
        /// <param name="username">The players Battletag (SomeUser#1234) or Username for PSN/XBL</param>
        /// <param name="platform">The players platform - Defaults to "none" if a battletag is not given (use DetectPlatform() if platform is not known)</param>
        /// <param name="region">The players region (only required for PC) - Defaults to "none" (use DetectRegionPC if region is not known)</param>
        public OverwatchPlayer(string username, Platform platform = Platform.none, Region region = Region.none)
        {
            Username = username;
            Platform = platform;
            if (!IsValidBattletag(username) && platform == Platform.pc)
                throw new InvalidBattletagException();
            else if (IsValidBattletag(username))
            {
                Platform = Platform.pc;
                BattletagUrlFriendly = username.Replace("#", "-");
                Region = region;
            }
            if(Region != Region.none && Platform != Platform.none)
            {
                ProfileURL = ProfileURL(Username, Region, Platform);
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
        public OverwatchStats CasualStats { get; private set; }

        /// <summary>
        /// The players competitive stats.
        /// </summary>
        public OverwatchStats CompetitiveStats { get; private set; }

        /// <summary>
        /// The players achievements.
        /// </summary>
        public OverwatchAchievements Achievements { get; private set; }

        /// <summary>
        /// The last time the profile was downloaded from PlayOverwatch.
        /// </summary>
        public DateTime ProfileLastDownloaded { get; private set; }

        /// <summary>
        /// A direct link to the users profile portrait.
        /// </summary>
        public string ProfilePortraitURL { get; private set; }

        /// <summary>
        /// The URL friendly version of the users Battletag.
        /// </summary>
        private string BattletagUrlFriendly { get; }

        /// <summary>
        /// Detect the region of the player (Also sets the players ProfileURL if it is currently un-set) - This method will simply return if the player is not on PC.
        /// </summary>
        /// <returns></returns>
        public async Task DetectRegionPC()
        {
            if (Platform != Platform.pc)
                return;
            string baseUrl = "http://playoverwatch.com/en-gb/career/";
            string naAppend = $"pc/us/{BattletagUrlFriendly}";
            string euAppend = $"pc/eu/{BattletagUrlFriendly}";
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri(baseUrl);
            var responseNA = await _client.GetAsync(naAppend);
            if (responseNA.IsSuccessStatusCode)
            {
                Region = Region.us;
                ProfileURL = baseUrl + naAppend;
                return;
            }
            else
            {            
                var responseEU = await _client.GetAsync(euAppend);
                if (responseEU.IsSuccessStatusCode)
                {
                    Region = Region.eu;
                    ProfileURL = baseUrl + euAppend;
                    return;
                }
            }
            Region = Region.none;
        }   
        
        public async Task DetectPlatform()
        {
            if (IsValidBattletag(Username))
            {
                Platform = Platform.pc;
                return;
            }
            string baseUrl = "http://playoverwatch.com/en-gb/career/";
            string psnAppend = $"psn/{Username}";
            string xblAppend = $"xbl/{Username}";
            using (HttpClient _client = new HttpClient())
            {
                _client.BaseAddress = new Uri(baseUrl);
                var responsePsn = await _client.GetAsync(psnAppend);
                if (responsePsn.IsSuccessStatusCode)
                {
                    Platform = Platform.psn;
                    ProfileURL = baseUrl + psnAppend;
                    return;
                }
                else
                {
                    var responseXbl = await _client.GetAsync(xblAppend);
                    if (responseXbl.IsSuccessStatusCode)
                    {
                        Platform = Platform.xbl;
                        ProfileURL = baseUrl + xblAppend;
                        return;
                    }
                }
            }
            Platform = Platform.none;
        }

        /// <summary>
        /// Downloads and parses the players profile
        /// </summary>
        /// <returns></returns>
        public async Task UpdateStats()
        {
            if (Region == Region.none && Platform == Platform.pc)
                throw new UserRegionNotDefinedException();
            if (Platform == Platform.none)
                throw new UserPlatformNotDefinedException();
            var userpage = await DownloadUserPage();
            GetUserRanks(userpage);
            GetProfilePortrait(userpage);
            CasualStats = new OverwatchStats();
            CompetitiveStats = new OverwatchStats();
            Achievements = new OverwatchAchievements();
            Achievements.UpdateAchievementsFromPage(userpage);
            CasualStats.UpdateStatsFromPage(userpage, Mode.Casual);
            CompetitiveStats.UpdateStatsFromPage(userpage, Mode.Competitive);
            ProfileLastDownloaded = DateTime.UtcNow;
        }

        internal void GetUserRanks(IDocument doc)
        {
            ushort parsedPlayerLevel = 0;
            PlayerLevel = 0;
            ushort parsedCompetitiveRank = 0;
            CompetitiveRank = 0;
            if (ushort.TryParse(doc.QuerySelector("div.player-level div")?.TextContent, out parsedPlayerLevel))
                PlayerLevel = parsedPlayerLevel;
            string playerLevelImageId = playerRankImageRegex.Match(doc.QuerySelector("div.player-level")?.GetAttribute("style")).Value;
            PlayerLevel += prestigeDefinitions[playerLevelImageId];
            if (ushort.TryParse(doc.QuerySelector("div.competitive-rank div")?.TextContent, out parsedCompetitiveRank))
                CompetitiveRank = parsedCompetitiveRank;
        }

        internal void GetProfilePortrait(IDocument doc)
        {
            ProfilePortraitURL = doc.QuerySelector(".player-portrait").GetAttribute("src");
        }

        internal async Task<IDocument> DownloadUserPage()
        {
            var config = Configuration.Default.WithDefaultLoader();
            if (ProfileURL == null)
                throw new UserProfileUrlNullException();
            return await BrowsingContext.New(config).OpenAsync(ProfileURL);
        }

        private static Regex playerRankImageRegex = new Regex("(0x\\w*)(?=_)", RegexOptions.Compiled);

        // Ranked portrait definitions for the prestige levels.
        private static Dictionary<string, ushort> prestigeDefinitions = new Dictionary<string, ushort>
        {
            {"0x0250000000000918", 0},
            {"0x0250000000000919", 0},
            {"0x025000000000091A", 0},
            {"0x025000000000091B", 0},
            {"0x025000000000091C", 0},
            {"0x025000000000091D", 0},
            {"0x025000000000091E", 0},
            {"0x025000000000091F", 0},
            {"0x0250000000000920", 0},
            {"0x0250000000000921", 0},
            {"0x0250000000000922", 100},
            {"0x0250000000000924", 100},
            {"0x0250000000000925", 100},
            {"0x0250000000000926", 100},
            {"0x025000000000094C", 100},
            {"0x0250000000000927", 100},
            {"0x0250000000000928", 100},
            {"0x0250000000000929", 100},
            {"0x025000000000092B", 100},
            {"0x0250000000000950", 100},
            {"0x025000000000092A", 200},
            {"0x025000000000092C", 200},
            {"0x0250000000000937", 200},
            {"0x025000000000093B", 200},
            {"0x0250000000000933", 200},
            {"0x0250000000000923", 200},
            {"0x0250000000000944", 200},
            {"0x0250000000000948", 200},
            {"0x025000000000093F", 200},
            {"0x0250000000000951", 200},
            {"0x025000000000092D", 300},
            {"0x0250000000000930", 300},
            {"0x0250000000000934", 300},
            {"0x0250000000000938", 300},
            {"0x0250000000000940", 300},
            {"0x0250000000000949", 300},
            {"0x0250000000000952", 300},
            {"0x025000000000094D", 300},
            {"0x0250000000000945", 300},
            {"0x025000000000093C", 300},
            {"0x025000000000092E", 400},
            {"0x0250000000000931", 400},
            {"0x0250000000000935", 400},
            {"0x025000000000093D", 400},
            {"0x0250000000000946", 400},
            {"0x025000000000094A", 400},
            {"0x0250000000000953", 400},
            {"0x025000000000094E", 400},
            {"0x0250000000000939", 400},
            {"0x0250000000000941", 400},
            {"0x025000000000092F", 500},
            {"0x0250000000000932", 500},
            {"0x025000000000093E", 500},
            {"0x0250000000000936", 500},
            {"0x025000000000093A", 500},
            {"0x0250000000000942", 500},
            {"0x0250000000000947", 500},
            {"0x025000000000094F", 500},
            {"0x025000000000094B", 500},
            {"0x0250000000000954", 500}
        };
    }
}
