using OverwatchAPI.WebClient;
using System;
using System.IO;
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
            _mockData = new ProfileRequestData("https://playoverwatch.com/en-gb/career/pc/eu/moiph-1288", File.ReadAllText("TestSource.txt"), Platform.Pc);
        }

        internal override Task<ProfileRequestData> GetProfileExact(string username, Platform platform)
        {
            return Task.FromResult(_mockData);
        }

        internal override Task<ProfileRequestData> GetProfileDetectPlatform(string username)
        {
            throw new NotImplementedException(); // TODO: no real way to test this but maybe someday...
        }
    }
}
