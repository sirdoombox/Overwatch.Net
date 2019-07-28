using System.IO;
using OverwatchAPI;
using OverwatchAPI.Extensions;
using OverwatchAPI.Parser;
using OverwatchAPI.WebClient;
using Xunit;

namespace Tests.Core.Parser
{
    public class ProfileParserTest
    {
        // These tests are run on the source from this page: http://playoverwatch.com/en-us/career/pc/moiph-1288
        // The source of the page was downloaded and stored on 07/10/2018
        // Will serve as the basis for parser tests barring a more reliable solution.

        private static readonly Player _testPlayer;

        static ProfileParserTest()
        {
            var source = File.ReadAllText("TestSource.txt");
            var data = new ProfileClient.ProfileRequestData("", source, Platform.Pc);
            var parser = new ProfileParser();
            _testPlayer = new Player();
            parser.Parse(_testPlayer, data).GetAwaiter().GetResult();
        }       

        [Fact]
        public void Parsed_Profile_PlayerLevel_Should_Be_Correct() => 
            Assert.Equal(19, _testPlayer.PlayerLevel);

        [Fact]
        public void Parsed_Profile_EndorsementLevel_Should_Be_Correct() =>
            Assert.Equal(3, _testPlayer.EndorsementLevel);

        [Fact]
        public void Parsed_Profile_EndorsementStats_Should_Be_Correct() =>
            Assert.Equal(0.53m, _testPlayer.Endorsements[Endorsement.GoodTeammate]);

        [Fact]
        public void Parsed_Profile_CompetitiveRank_Should_Be_Correct() => 
            Assert.Equal(0, _testPlayer.CompetitiveRank);

        [Fact]
        public void Parsed_Profile_CasualStats_Should_Be_Correct() => 
            Assert.Equal(3342873, _testPlayer.CasualStats.GetStatExact("AllHeroes", "Assists", "Healing Done").Value);

        [Fact]
        public void Parsed_Profile_CompetitiveStats_Should_Be_Correct() => 
            Assert.Equal(1957, _testPlayer.CompetitiveStats.GetStatExact("Lucio", "Game", "Time Played").Value);

        [Fact]
        public void Parsed_Profile_Achievements_Should_Be_Correct()
        {
            Assert.True(_testPlayer.Achievements.FilterByName("Hog Wild").IsEarned);
            Assert.False(_testPlayer.Achievements.FilterByName("Rapid Discord").IsEarned);
        }

        [Fact]
        public void Parsed_Profile_PortraitImage_Should_Be_Correct() => 
            Assert.Equal("https://d15f34w2p8l1cc.cloudfront.net/overwatch/70652fca537bcd2aef36608fb308353c9004961672f83929ad949095e2192d3b.png", _testPlayer.ProfilePortraitUrl);

        [Fact]
        public void Parsed_Profile_CompetitiveRankImage_Should_Be_Correct() => 
            Assert.Equal("", _testPlayer.CompetitiveRankImageUrl);

        [Fact]
        public void Parsed_Profile_PlayerLevelImage_Should_Be_Correct() => 
            Assert.Equal("https://d15f34w2p8l1cc.cloudfront.net/overwatch/7fd73e680007054dbb8ac5ea8757a565858b9d7dba19f389228101bda18f36b0.png", _testPlayer.PlayerLevelImage);
    }
}