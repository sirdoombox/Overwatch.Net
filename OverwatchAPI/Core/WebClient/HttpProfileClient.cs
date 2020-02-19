using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OverwatchAPI.Extensions;

namespace OverwatchAPI.WebClient
{
    internal sealed class HttpProfileClient : ProfileClient
    {
        private readonly HttpClient _client;

        internal HttpProfileClient()
        {
            // TODO: Keep an eye on this to see if TLS 1.2 support makes it's way into older framework versions - seems unlikely though.
            try { ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; }
            catch { ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11; }
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
                var rsltContent = await result.Content.ReadAsStringAsync();
                if (rsltContent.Contains("Profile Not Found")) return null;
                var rsltUrl = result.RequestMessage.RequestUri.ToString();
                return new ProfileRequestData(rsltUrl, rsltContent,platform);
            }
        }
        
        internal override async Task<List<Alias>> GetAliases(string id)
        {
            throw new Exception($"Called an obsolete method - {nameof(GetAliases)}");
            var url = _client.BaseAddress + $"platforms/{id}";
            using (var result = await _client.GetAsync(url))
            {
                if (!result.IsSuccessStatusCode) return null;
                var jsonText = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Alias>>(jsonText);
            }
        }
    }
}