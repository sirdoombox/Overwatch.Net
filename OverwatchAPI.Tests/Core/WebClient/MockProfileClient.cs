using OverwatchAPI.WebClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
            _mockData = new ProfileRequestData("", File.ReadAllText("TestSource.txt"));
        }

        internal override Task<ProfileRequestData> GetProfileExact(string username, Platform platform, Region region = Region.None)
        {
            switch (region)
            {
                case Region.None when platform != Platform.Pc:
                    return null;
                case Region.Eu when platform == Platform.Pc:
                    return Task.FromResult(_mockData);
            }
            throw new ArgumentException("Invalid combination of Platform/Region.");
        }

        internal override Task<ProfileRequestData> GetProfileDetectRegion(string username, Platform platform)
        {
            if (platform != Platform.Pc) return null;
            return _config.Regions.Where(x => x != Region.None).Any(region => region == Region.Eu) ? Task.FromResult(_mockData) : null;
        }

        internal override Task<ProfileRequestData> GetProfileDetectPlatform(string username)
        {
            throw new NotImplementedException();
        }
    }
}
