using OverwatchAPI;
using Xunit;

namespace Tests.Core
{
    public class OverwatchClientIntergrationTests
    {
        private const string cootesPsnUsername = "couturier30";
        private const string cootesPsnUrl = "https://playoverwatch.com/en-gb/career/psn/couturier30";
        private const string cootesPcUsername = "Cootes#1162";
        private const string cootesPcUrl = "https://playoverwatch.com/en-gb/career/pc/Cootes-1162";

        private const string pcUsername = "moiph#1288";
        private const string pcUrl = "https://playoverwatch.com/en-gb/career/pc/moiph-1288";

        private static readonly OverwatchClient Client = new OverwatchClient();

        [Fact]
        public async void GetPlayer_Psn_AutoDetect_Should_Return_Correct_Private_Page_With_Other_Platforms_Listed()
        { 
            var result = await Client.GetPlayerAsync(cootesPsnUsername);
            Assert.Equal(cootesPsnUrl, result.ProfileUrl);
            Assert.Equal(Platform.Psn, result.Platform);
            Assert.True(result.IsProfilePrivate);
            Assert.Contains(result.Aliases, x => x.Platform == Platform.Pc);
            var otherProfiles = await Client.GetOtherProfilesAsync(result);
            Assert.NotEmpty(otherProfiles);
            Assert.Contains(otherProfiles, x => x.ProfileUrl == cootesPcUrl);
            Assert.Contains(otherProfiles, x => x.Username == cootesPcUsername);
        }

        [Fact]
        public async void GetPlayer_Pc_AutoDetect_Should_Return_Correct_Public_Page_With_No_Other_Platforms_Listed()
        {
            var result = await Client.GetPlayerAsync(pcUsername);
            Assert.Equal(pcUrl, result.ProfileUrl);
            Assert.Equal(Platform.Pc, result.Platform);
            Assert.False(result.IsProfilePrivate);
        }
    }
}