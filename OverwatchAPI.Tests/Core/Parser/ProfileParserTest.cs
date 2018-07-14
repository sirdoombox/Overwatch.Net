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
        // The source of the page was downloaded and stored on 02/07/2018
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
            Assert.Equal(867, _testPlayer.PlayerLevel);

        [Fact]
        public void Parsed_Profile_EndorsementLevel_Should_Be_Correct() =>
            Assert.Equal(2, _testPlayer.EndorsementLevel);

        [Fact]
        public void Parsed_Profile_EndorsementStats_Should_Be_Correct() =>
            Assert.Equal(0.6111111111111112m, _testPlayer.Endorsements[Endorsement.GoodTeammate]);

        [Fact]
        public void Parsed_Profile_CompetitiveRank_Should_Be_Correct() => 
            Assert.Equal(2377, _testPlayer.CompetitiveRank);

        [Fact]
        public void Parsed_Profile_CasualStats_Should_Be_Correct() => 
            Assert.Equal(2576219, _testPlayer.CasualStats.GetStatExact("AllHeroes", "Assists", "Healing Done").Value);

        [Fact]
        public void Parsed_Profile_CompetitiveStats_Should_Be_Correct() => 
            Assert.Equal(1560, _testPlayer.CompetitiveStats.GetStatExact("Lucio", "Game", "Time Played").Value);

        [Fact]
        public void Parsed_Profile_Achievements_Should_Be_Correct()
        {
            Assert.True(_testPlayer.Achievements.FilterByName("Hog Wild").IsEarned);
            Assert.False(_testPlayer.Achievements.FilterByName("Rapid Discord").IsEarned);
        }

        [Fact]
        public void Parsed_Profile_PortraitImage_Should_Be_Correct() => 
            Assert.Equal("https://assets.webn.mobi/overwatch/5122deb567422e30496f656856f70d028bfc70a89eaa28d8ea662308b5df42fa.png", _testPlayer.ProfilePortraitUrl);

        [Fact]
        public void Parsed_Profile_CompetitiveRankImage_Should_Be_Correct() => 
            Assert.Equal("https://d1u1mce87gyfbn.cloudfront.net/game/rank-icons/season-2/rank-3.png", _testPlayer.CompetitiveRankImageUrl);

        [Fact]
        public void Parsed_Profile_PlayerLevelImage_Should_Be_Correct() => 
            Assert.Equal("https://d1u1mce87gyfbn.cloudfront.net/game/playerlevelrewards/0x0250000000000974_Border.png", _testPlayer.PlayerLevelImage);
    }
}