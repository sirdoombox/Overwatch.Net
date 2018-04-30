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
        /// Uses both platform detection to find a player. Not as accurate or fast as providing a platform.
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
            var result = await _profileClient.GetProfileDetectPlatform(player);
            return result == null ? null : await _profileParser.Parse(player, result);
        }

        /// <summary>
        /// The fastest and most precise method of finding a player.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="platform"></param>
        /// <param name="region"></param>
        /// <returns>A <see cref="Player"/> object if it was succesfully found, otherwise returns null.</returns>
        public async Task<Player> GetPlayerAsync(string username, Platform platform)
        {
            if (!username.IsValidBattletag() && platform == Platform.Pc)
                throw new ArgumentException("Not a valid battletag for the PC platform - valid example: Example#1234", nameof(username));
            if (Config.Platforms.All(x => x != Platform.Pc) && username.IsValidBattletag())
                throw new ArgumentException($"{username} is a PC username, however your config does not allow for PC.", nameof(username));

            var player = new Player()
            {
                Username = username,
                Platform = platform
            };

            ProfileClient.ProfileRequestData pageData;
            if (platform != Platform.Pc)
                pageData = await _profileClient.GetProfileExact(player);
            else
                pageData = await _profileClient.GetProfileExact(player);
            return pageData == null ? null : await _profileParser.Parse(player, pageData);
        }

        /// <summary>
        /// Updates a Players stats using a pre-existing Player object as the basis for the request.
        /// </summary>
        /// <param name="player">An existing "Player" object</param>
        /// <returns>A "Player" object if it was succesfully found, otherwise returns null.</returns>
        public async Task<Player> UpdatePlayerAsync(Player player)
        {
            if (string.IsNullOrEmpty(player.Username))
                throw new ArgumentException("Player Username is Null or Empty",nameof(player));
            if (player.Username.IsValidBattletag() && player.Platform != Platform.Pc)
                throw new ArgumentException("Invalid Username for Platform", nameof(player));
            if (!player.Username.IsValidBattletag() && player.Platform == Platform.Pc)
                throw new ArgumentException("Invalid Username for PC", nameof(player));
            var pageData = await _profileClient.GetProfileExact(player);
            return pageData == null ? null : await _profileParser.Parse(player, pageData);
        }

        public void Dispose() => _profileClient.Dispose();
    }
}