using OverwatchAPI;
using Xunit;

namespace Tests.Core
{
    public class OverwatchClientIntergrationTests
    {
        private const string psnUsername = "couturier30";
        private const string psnUrl = "https://playoverwatch.com/en-gb/career/psn/couturier30";
        private const string pcUsername = "moiph#1288";
        private const string pcUrl = "https://playoverwatch.com/en-gb/career/pc/moiph-1288";
        private static readonly OverwatchClient _client = new OverwatchClient();

        [Fact]
        public async void GetPlayer_Psn_AutoDetect_Should_Return_Correct_Private_Page()
        { 
            var result = await _client.GetPlayerAsync(psnUsername);
            Assert.Equal(psnUrl, result.ProfileUrl);
            Assert.Equal(Platform.Psn, result.Platform);
            Assert.True(result.IsProfilePrivate);
        }

        [Fact]
        public async void GetPlayer_Pc_AutoDetect_Should_Return_Correct_Public_Page()
        {
            var result = await _client.GetPlayerAsync(pcUsername);
            Assert.Equal(pcUrl, result.ProfileUrl);
            Assert.Equal(Platform.Pc, result.Platform);
            Assert.False(result.IsProfilePrivate);
        }
    }
}