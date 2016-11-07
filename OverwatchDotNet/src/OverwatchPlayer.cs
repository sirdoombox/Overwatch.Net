using AngleSharp;
using AngleSharp.Dom;
using OverwatchAPI.Internal;
using OverwatchAPI.intrnl;
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
            string playerLevelImageId = StaticVars.playerRankImageRegex.Match(doc.QuerySelector("div.player-level")?.GetAttribute("style")).Value;
            PlayerLevel += StaticVars.prestigeDefinitions[playerLevelImageId];
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
    }
}
