using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using OverwatchAPI.Extensions;

namespace OverwatchAPI.WebClient
{
    internal sealed class HttpProfileClient : ProfileClient
    {
        private readonly HttpClient _client;

        internal HttpProfileClient()
        {
            // TODO: Figure out a way to support TLS 1.2 based on framework - 1.1 will have to do for now.
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
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
            foreach(var platform in Enum.GetValues(typeof(Platform)).Cast<Platform>().Where(x => x != Platform.Pc))
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
                if ((await result.Content.ReadAsStringAsync()).Contains("Profile Not Found")) return null;
                var rsltContent = await result.Content.ReadAsStringAsync();
                var rsltUrl = result.RequestMessage.RequestUri.ToString();
                return new ProfileRequestData(rsltUrl, rsltContent,platform);
            }
        }
    }
}