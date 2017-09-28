using OverwatchAPI.Config;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using OverwatchAPI.Extensions;

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

        internal override Task<ProfileRequestData> GetProfileExact(string username, Platform platform, Region region = Region.None)
        {
            string reqUrl;
            if (region == Region.None && platform != Platform.Pc) reqUrl = $"{platform.ToLowerString()}/{username}";
            else if (region != Region.None && platform == Platform.Pc) reqUrl = $"{platform.ToLowerString()}/{region.ToLowerString()}/{username.BattletagToUrlFriendlyString()}";
            else throw new ArgumentException("Invalid combination of Platform/Region.");
            return GetProfileUrl(reqUrl);
        }

        internal override async Task<ProfileRequestData> GetProfileDetectPlatform(string username)
        {
            if (username.IsValidBattletag()) return await GetProfileDetectRegion(username, Platform.Pc);
            foreach(var platform in _config.Platforms.Where(x => x != Platform.Pc))
            {
                var result = await GetProfileUrl($"{platform.ToLowerString()}/{username.BattletagToUrlFriendlyString()}");
                if (result == null) continue;
                return result;
            }
            return null;
        }

        internal override async Task<ProfileRequestData> GetProfileDetectRegion(string username, Platform platform)
        {
            foreach(var region in _config.Regions.Where(x => x != Region.None))
            {
                var result = await GetProfileUrl($"{platform.ToLowerString()}/{region.ToLowerString()}/{username}");
                if (result == null) continue;
                return result;
            }
            return null;
        }

        private async Task<ProfileRequestData> GetProfileUrl(string reqString)
        {
            using (var result = await _client.GetAsync(reqString))
            {
                if (!result.IsSuccessStatusCode) return null;
                var rsltContent = await result.Content.ReadAsStringAsync();
                var rsltUrl = result.RequestMessage.RequestUri.ToString();
                return new ProfileRequestData(rsltUrl, rsltContent);
            }
        }
    }
}