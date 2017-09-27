using OverwatchAPI.WebClient;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OverwatchAPI;
using OverwatchAPI.Config;

namespace Tests.Core.WebClient
{
    internal sealed class MockProfileClient : ProfileClient
    {
        private readonly ProfileRequestData _mockData;

        public MockProfileClient(OverwatchConfig config) : base(config)
        {
            _mockData = new ProfileRequestData("https://playoverwatch.com/en-gb/career/pc/eu/SirDoombox-2603", File.ReadAllText("TestSource.txt"));
        }

        internal override Task<ProfileRequestData> GetProfileExact(string username, Platform platform, Region region = Region.None)
        {
            switch (region)
            {
                case Region.None when platform != Platform.Pc:
                    return Task.FromResult<ProfileRequestData>(null);
                case Region.Eu when platform == Platform.Pc:
                    return Task.FromResult(_mockData);
            }
            throw new ArgumentException("Invalid combination of Platform/Region.");
        }

        internal override Task<ProfileRequestData> GetProfileDetectRegion(string username, Platform platform)
        {
            if (platform != Platform.Pc) return null;
            var any = _config.Regions.Where(x => x != Region.None).Any(x => x == Region.Eu);
            return any ? Task.FromResult(_mockData) : Task.FromResult<ProfileRequestData>(null);
        }

        internal override Task<ProfileRequestData> GetProfileDetectPlatform(string username)
        {
            throw new NotImplementedException();
        }
    }
}
