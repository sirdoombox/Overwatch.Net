using System;
using System.Threading.Tasks;
using OverwatchAPI.Config;

namespace OverwatchAPI.WebClient
{
    internal abstract class ProfileClient : IDisposable
    {
        protected readonly OverwatchConfig _config;

        protected ProfileClient(OverwatchConfig config)
        {
            _config = config;
        }

        public virtual void Dispose() { }

        internal Task<ProfileRequestData> GetProfileExact(Player player) =>
            GetProfileExact(player.UsernameUrlFriendly, player.Platform, player.Region);

        internal abstract Task<ProfileRequestData> GetProfileExact(string username, Platform platform,
            Region region = Region.None);

        internal Task<ProfileRequestData> GetProfileDetectRegion(Player player) =>
            GetProfileDetectRegion(player.UsernameUrlFriendly, player.Platform);

        internal abstract Task<ProfileRequestData> GetProfileDetectRegion(string username, Platform platform);

        internal Task<ProfileRequestData> GetProfileDetectPlatform(Player player) =>
            GetProfileDetectPlatform(player.UsernameUrlFriendly);

        internal abstract Task<ProfileRequestData> GetProfileDetectPlatform(string username);

        internal sealed class ProfileRequestData
        {
            internal string ReqUrl;
            internal string ReqContent;

            internal ProfileRequestData(string reqUrl, string reqContent)
            {
                ReqUrl = reqUrl;
                ReqContent = reqContent;
            }
        }
    }
}