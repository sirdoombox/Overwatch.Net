using System;
using OverwatchAPI;
using OverwatchAPI.Config;
using Tests.Core.WebClient;
using Xunit;

namespace Tests.Core
{
    public class OverwatchClientTest
    {
        [Fact]
        public async void GetPlayer_Username_Only_Overload_With_Battletag_Argument_Returns_Valid_Page()
        {
            var config = new OverwatchConfig.Builder().Default();
            var mockWebClient = new MockProfileClient(config);
            using (var owClient = new OverwatchClient(mockWebClient, config))
            {
                var result = await owClient.GetPlayerAsync("SirDoombox#2603");
                Assert.Equal("https://playoverwatch.com/en-gb/career/pc/eu/SirDoombox-2603", result.ProfileUrl);
            }
        }

        [Fact]
        public async void GetPlayer_Username_Only_Overload_With_Battletag_Argument_With_Player_Not_Existing_In_Region_Should_Return_Null()
        {
            var config = new OverwatchConfig.Builder()
                .WithAllPlatforms()
                .WithRegions(Region.Kr, Region.Us);
            var mockWebClient = new MockProfileClient(config);
            using (var owClient = new OverwatchClient(mockWebClient, config))
            {
                var result = await owClient.GetPlayerAsync("SirDoombox#2603");
                Assert.Null(result);
            }
        }

        [Fact]
        public async void GetPlayer_Username_Only_Overload_With_Battletag_Argument_And_Config_With_No_Pc_Region_Should_Throw_Exception()
        {
            var config = new OverwatchConfig.Builder()
                .WithPlatforms(Platform.Psn, Platform.Xbl);
            var mockWebClient = new MockProfileClient(config);
            using (var owClient = new OverwatchClient(mockWebClient, config))
            {
                await Assert.ThrowsAsync<ArgumentException>(async () => await owClient.GetPlayerAsync("SirDoombox#2603"));
            }
        }
    }
}
