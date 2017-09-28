using OverwatchAPI;
using Xunit;

namespace Tests.Core
{
    public class OverwatchClientIntergrationTests
    {
        private const string psnUsername = "Rolingachu";
        private const string psnUrl = "https://playoverwatch.com/en-gb/career/psn/Rolingachu";
        private const string pcUsUsername = "GMK#11870";
        private const string pcUsUrl = "https://playoverwatch.com/en-gb/career/pc/us/GMK-11870";
        private const string pcEuUsername = "SirDoombox#2603";
        private const string pcEuUrl = "https://playoverwatch.com/en-gb/career/pc/eu/SirDoombox-2603";
        private static readonly OverwatchClient _client = new OverwatchClient();

        [Fact]
        public async void GetPlayer_Psn_AutoDetect_Should_Return_Correct_Page()
        { 
            var result = await _client.GetPlayerAsync(psnUsername);
            Assert.Equal(psnUrl, result.ProfileUrl);
        }

        [Fact]
        public async void GetPlayer_Pc_Us_AutoDetect_Should_Return_Correct_Page()
        {
            var result = await _client.GetPlayerAsync(pcUsUsername);
            Assert.Equal(pcUsUrl, result.ProfileUrl);
        }

        [Fact]
        public async void GetPlayer_Pc_Eu_AutoDetect_Should_Return_Correct_Page()
        {
            var result = await _client.GetPlayerAsync(pcEuUsername);
            Assert.Equal(pcEuUrl, result.ProfileUrl);
        }
    }
}