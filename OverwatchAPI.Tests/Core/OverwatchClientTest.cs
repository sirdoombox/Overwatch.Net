using System;
using OverwatchAPI;
using Tests.Core.WebClient;
using Xunit;

namespace Tests.Core
{
    public class OverwatchClientTest
    {
        [Fact]
        public async void GetPlayer_Username_Only_Overload_With_Battletag_Argument_Returns_Valid_Page()
        {
            var mockWebClient = new MockProfileClient();
            using (var owClient = new OverwatchClient(mockWebClient))
            {
                var result = await owClient.GetPlayerAsync("moiph#1288");
                Assert.Equal("https://playoverwatch.com/en-gb/career/pc/eu/moiph-1288", result.ProfileUrl);
            }
        }

        [Fact]
        public async void GetPlayer_Username_Only_Overload_With_Battletag_Argument_And_No_Pc_Region_Should_Throw_Exception()
        {
            var mockWebClient = new MockProfileClient();
            using (var owClient = new OverwatchClient(mockWebClient, Platform.Psn, Platform.Xbl))
            {
                await Assert.ThrowsAsync<ArgumentException>(async () => await owClient.GetPlayerAsync("moiph#1288"));
            }
        }
    }
}
