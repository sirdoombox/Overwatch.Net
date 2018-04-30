using OverwatchAPI.Config;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using OverwatchAPI.Extensions;
using OverwatchAPI.Parser;

namespace OverwatchAPI.WebClient
{
    internal sealed class HttpProfileClient : ProfileClient
    {
        private readonly HttpClient _client;

        internal HttpProfileClient(OverwatchConfig config) : base(config)
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("https://playoverwatch.com/en-gb/career/")
            };
        }

        public override void Dispose() => _client.Dispose();

        internal override Task<ProfileRequestData> GetProfileExact(string username, Platform platform)
        {
            var reqUrl = platform != Platform.Pc 
                ? $"{platform.ToLowerString()}/{username}" 
                : $"pc/{username.BattletagToUrlFriendlyString()}";
            return GetProfileUrl(reqUrl, platform);
        }

        internal override async Task<ProfileRequestData> GetProfileDetectPlatform(string username)
        {
            if (username.IsValidBattletag()) return await GetProfileUrl($"pc/{username.BattletagToUrlFriendlyString()}", Platform.Pc);
            foreach(var platform in _config.Platforms.Where(x => x != Platform.Pc))
            {
                var result = await GetProfileUrl($"{platform.ToLowerString()}/{username.BattletagToUrlFriendlyString()}", platform);
                if (result == null) continue;
                return result;
            }
            return null;
        }

        private async Task<ProfileRequestData> GetProfileUrl(string reqString, Platform platform)
        {
            using (var result = await _client.GetAsync(reqString))
            {
                if (!result.IsSuccessStatusCode) return null;
                var rsltContent = await result.Content.ReadAsStringAsync();
                var rsltUrl = result.RequestMessage.RequestUri.ToString();
                var rslt = new ProfileRequestData(rsltUrl, rsltContent,platform);
                return ProfileParser.IsValidPlayerProfile(rslt) ? rslt : null;
            }
        }
    }
}