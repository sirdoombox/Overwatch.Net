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
        public async void GetPlayerUsernameOnly_ValidUsernameValidConfig_CorrectPage()
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
        public async void GetPlayerUsernameOnly_DoesNotExistInGivenRegions_IsNull()
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
        public async void GetPlayerUsernameOnly_ConfigNoPC_ThrowsException()
        {
            var config = new OverwatchConfig.Builder()
                .WithAllRegions()
                .WithPlatforms(Platform.Psn, Platform.Xbl);
            var mockWebClient = new MockProfileClient(config);
            using (var owClient = new OverwatchClient(mockWebClient, config))
            {
                await Assert.ThrowsAsync<ArgumentException>(async () => await owClient.GetPlayerAsync("SirDoombox#2603"));
            }
        }
    }
}
