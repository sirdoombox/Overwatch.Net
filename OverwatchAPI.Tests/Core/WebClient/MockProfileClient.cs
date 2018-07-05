using OverwatchAPI.WebClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using OverwatchAPI;

namespace Tests.Core.WebClient
{
    internal sealed class MockProfileClient : ProfileClient
    {
        private readonly ProfileRequestData _mockData;

        public MockProfileClient()
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

        internal override Task<List<Alias>> GetAliases(string id)
        {
            throw new NotImplementedException(); // TODO: Maybe test this too.
        }
    }
}
