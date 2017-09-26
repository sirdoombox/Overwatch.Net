using OverwatchAPI.Config;
using System;
using System.Linq;
using System.Threading.Tasks;
using OverwatchAPI.WebClient;
using OverwatchAPI.Parser;

namespace OverwatchAPI
{
    public sealed class OverwatchClient : IDisposable
    {
        public OverwatchConfig Config { get; set; }

        private readonly ProfileClient _profileClient;
        private readonly ProfileParser _profileParser;

        public OverwatchClient(OverwatchConfig config = null)
        {
            _profileParser = new ProfileParser();
            Config = config ?? new OverwatchConfig.Builder().Default();
            _profileClient = new HttpProfileClient(Config);
        }

        internal OverwatchClient(ProfileClient profileClient, OverwatchConfig config)
        {
            _profileClient = profileClient;
            _profileParser = new ProfileParser();
            Config = config;
        }

        /// <summary>
        /// Uses both region and platform detection to find a player.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>A <see cref="Player"/> if it was succesfully found, otherwise returns null.</returns>
        public async Task<Player> GetPlayerAsync(string username)
        {
            if (username.IsValidBattletag())
                return await GetPlayerAsync(username, Platform.Pc);
            if (!username.IsValidPsnId() && !username.IsValidXblId())
                throw new ArgumentException("Not a valid XBL, PSN or Battlenet ID", nameof(username));
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
        /// <returns>A <see cref="Player"/> object if it was succesfully found, otherwise returns null.</returns>
        public Task<Player> GetPlayerAsync(string username, Platform platform, Region region = Region.None)
        {
            if (platform != Platform.Pc && region != Region.None) throw new ArgumentException("Console players do not have regions.");
            var player = new Player()
            {
                Username = username,
                Platform = platform,
                Region = region
            };
            if (platform != Platform.Pc) return GetPlayerExact(player);
            if (!username.IsValidBattletag())
                throw new ArgumentException("Not a valid battletag for the PC platform - valid example: Example#1234", nameof(username));         
            return region == Region.None ? DetectRegionAndParse(player) : GetPlayerExact(player);
        }

        /// <summary>
        /// Updates a Players stats using a pre-existing Player object as the basis for the request.
        /// </summary>
        /// <param name="player">An existing "Player" object</param>
        /// <returns>A "Player" object if it was succesfully found, otherwise returns null.</returns>
        public Task<Player> UpdatePlayerAsync(Player player)
        {
            if (string.IsNullOrEmpty(player.Username)) throw new ArgumentException("Player Username is Null or Empty",nameof(player));
            if (player.Username.IsValidBattletag() && player.Platform != Platform.Pc) throw new ArgumentException("Invalid Username for Platform", nameof(player));
            if (!player.Username.IsValidBattletag() && player.Platform == Platform.Pc) throw new ArgumentException("Invalid Username for PC", nameof(player));
            if (player.Region == Region.None && player.Platform == Platform.Pc) throw new ArgumentException("PC players must have a region", nameof(player));
            if (player.Region != Region.None && player.Platform != Platform.Pc) throw new ArgumentException("Console players cannot have a region", nameof(player));
            return GetPlayerExact(player);
        }

        internal async Task<Player> DetectRegionAndParse(Player player)
        {
            foreach(var region in Config.Regions.Where(x => x != Region.None))
            {
                var result = await _profileClient.GetProfileExact(player.UsernameUrlFriendly, player.Platform, region);
                if (result == null) continue;
                player.Region = region;
                player.ProfileUrl = result.ReqUrl;
                return await _profileParser.Parse(player, result);
            }
            return null;
        }

        internal async Task<Player> DetectPlatformAndParse(Player player)
        {
            foreach(var platform in Config.Platforms.Where(x => x != Platform.Pc))
            {
                var result = await _profileClient.GetProfileExact(player.Username, platform);
                if(result == null) continue;
                player.Platform = platform;
                return await _profileParser.Parse(player,result);
            }
            return null;
        }

        internal async Task<Player> GetPlayerExact(Player player)
        {
            var result = await _profileClient.GetProfile(player);
            if (result == null) return null;
            return await _profileParser.Parse(player,result);
        }

        public void Dispose() => _profileClient.Dispose();
    }
}