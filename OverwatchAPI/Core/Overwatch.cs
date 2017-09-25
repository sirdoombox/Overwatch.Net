using AngleSharp.Parser.Html;
using OverwatchAPI.Config;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using OverwatchAPI.Internal;

namespace OverwatchAPI
{
    public sealed class Overwatch : IDisposable
    {
        public OverwatchConfig Config { get; set; }

        private readonly HttpClient httpClient;
        private readonly HtmlParser htmlParser;

        public Overwatch()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://playoverwatch.com/en-gb/career/");
            htmlParser = new HtmlParser();
            Config = Config ?? new OverwatchConfig.Builder().Default();
        }

        public Overwatch(OverwatchConfig config) : this()
        {
            Config = config ?? throw new ArgumentNullException("config");
        }

        /// <summary>
        /// Uses both region and platform detection to find a player.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>A "Player" object if it was succesfully found, otherwise returns null.</returns>
        public async Task<Player> GetPlayerAsync(string username)
        {
            if (username.IsValidBattletag())
                return await GetPlayerAsync(username, Platform.pc);
            if (!username.IsValidPsnId() && !username.IsValidXblId())
                throw new ArgumentException("Not a valid XBL, PSN or Battlenet ID", "username");
            var player = new Player { Username = username };
            return await DetectPlatformAndParse(player);
        }

        /// <summary>
        /// The fastest and most precise method of finding a player.
        /// Note: Consoles do not have seperate regions for profiles.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="platform"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        public Task<Player> GetPlayerAsync(string username, Platform platform, Region region = Region.none)
        {
            if (platform != Platform.pc && region != Region.none) throw new ArgumentException("Console players do not have regions.");
            Player player = new Player()
            {
                Username = username,
                Platform = platform,
                Region = region
            };
            if (platform == Platform.pc)
            {
                if (!username.IsValidBattletag())
                    throw new ArgumentException("Not a valid battletag for the PC platform - valid example: Example#1234", "username");              
                if(region == Region.none) return DetectRegionAndParse(player);
                return GetPlayerExact(player);
            }
            return GetPlayerExact(player);
        }

        internal async Task<Player> DetectRegionAndParse(Player player)
        {
            foreach(var region in Config.Regions.Where(x => x != Region.none))
            {
                using (var rslt = await httpClient.GetAsync($"pc/{region}/{player.Username.BattletagToUrlFriendlyString()}"))
                {
                    if (rslt.IsSuccessStatusCode)
                    {
                        player.Region = region;
                        player.Platform = Platform.pc;
                        player.ProfileUrl = rslt.RequestMessage.RequestUri.ToString();
                        return await Parse(player, await rslt.Content.ReadAsStringAsync());
                    }
                }                  
            }
            return null;
        }

        internal async Task<Player> DetectPlatformAndParse(Player player)
        {
            foreach(var platform in Config.Platforms.Where(x => x != Platform.pc))
            {
                using (var rslt = await httpClient.GetAsync($"{platform}/{player.Username}"))
                {
                    if (rslt.IsSuccessStatusCode)
                    {
                        player.Region = Region.none;
                        player.Platform = platform;
                        player.ProfileUrl = rslt.RequestMessage.RequestUri.ToString();
                        return await Parse(player, await rslt.Content.ReadAsStringAsync());
                    }
                }
            }
            return null;
        }

        internal async Task<Player> GetPlayerExact(Player player)
        {
            var reqString = $"{player.Platform}/{(player.Platform == Platform.pc ? player.Region+"/" : "")}{player.Username.BattletagToUrlFriendlyString()}";
            using (var rslt = await httpClient.GetAsync(reqString))
            {
                if (rslt.IsSuccessStatusCode)
                {
                    player.ProfileUrl = rslt.RequestMessage.RequestUri.ToString();
                    return await Parse(player, await rslt.Content.ReadAsStringAsync());
                }
            }
            return null;
        }

        internal async Task<Player> Parse(Player player, string pageHtml)
        {
            using (var doc = await htmlParser.ParseAsync(pageHtml))
            {
                player.CompetitiveRank = doc.CompetitiveRank();
                player.CompetitiveRankImageUrl = doc.CompetitiveRankImage();
                player.CompetitiveStats = doc.Stats(Mode.Competitive);
                player.CasualStats = doc.Stats(Mode.Casual);
                player.Achievements = doc.Achievements();
                player.PlayerLevel = doc.PlayerLevel();
                player.ProfilePortraitUrl = doc.PortraitImage();
                player.PlayerLevelImage = doc.PlayerLevelImage();
                return player;
            }
        }

        public void Dispose() => httpClient.Dispose();
    }
}