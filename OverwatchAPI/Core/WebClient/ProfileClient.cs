using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OverwatchAPI.WebClient
{
    internal abstract class ProfileClient : IDisposable
    {
        public virtual void Dispose() { }

        internal Task<ProfileRequestData> GetProfileExact(Player player) =>
            GetProfileExact(player.UsernameUrlFriendly, player.Platform);

        internal abstract Task<ProfileRequestData> GetProfileExact(string username, Platform platform);

        internal Task<ProfileRequestData> GetProfileDetectPlatform(Player player) =>
            GetProfileDetectPlatform(player.UsernameUrlFriendly);
        
        internal abstract Task<ProfileRequestData> GetProfileDetectPlatform(string username);

        internal abstract Task<List<Alias>> GetAliases(string id);

        internal sealed class ProfileRequestData
        {
            internal string ReqUrl;
            internal string ReqContent;
            internal Platform PlayerPlatform;

            internal ProfileRequestData(string reqUrl, string reqContent, Platform playerPlatform)
            {
                ReqUrl = reqUrl;
                ReqContent = reqContent;
                PlayerPlatform = playerPlatform;
            }
        }

        internal sealed class Alias
        {
            public string platform { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public string urlName { get; set; }
            public bool isPublic { get; set; }
            public int playerLevel { get; set; }
            public string portrait { get; set; }
        }
    }
}