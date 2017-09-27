using System.IO;
using OverwatchAPI;
using OverwatchAPI.Parser;
using OverwatchAPI.WebClient;
using Xunit;

namespace Tests.Core.Parser
{
    public class ProfileParserTest
    {
        // These tests are run on the source from this page: https://playoverwatch.com/en-gb/career/pc/eu/SirDoombox-2603
        // The source of the page was downloaded and stored on 24/9/2017 
        // Will serve as the basis for parser tests barring a more reliable solution.

        private static readonly Player _testPlayer;

        static ProfileParserTest()
        {
            var source = File.ReadAllText("TestSource.txt");
            var data = new ProfileClient.ProfileRequestData("", source);
            var parser = new ProfileParser();
            _testPlayer = new Player();
            parser.Parse(_testPlayer, data).GetAwaiter().GetResult();
        }       

        [Fact]
        public void Parsed_Profile_PlayerLevel_Should_Be_Correct() => 
            Assert.Equal(84, _testPlayer.PlayerLevel);

        [Fact]
        public void Parsed_Profile_CompetitiveRank_Should_Be_Correct() => 
            Assert.Equal(2215, _testPlayer.CompetitiveRank);

        [Fact]
        public void Parsed_Profile_CasualStats_Should_Be_Correct() => 
            Assert.Equal(652428, _testPlayer.CasualStats["AllHeroes"]["Assists"]["Healing Done"]);

        [Fact]
        public void Parsed_Profile_CompetitiveStats_Should_Be_Correct() => 
            Assert.Equal(7200, _testPlayer.CompetitiveStats["Lucio"]["Game"]["Time Played"]);

        [Fact]
        public void Parsed_Profile_Achievements_Should_Be_Correct()
        {
            Assert.Equal(true, _testPlayer.Achievements["Tank"]["Hog Wild"]);
            Assert.Equal(false, _testPlayer.Achievements["Defense"]["Did That Sting?"]);
        }

        [Fact]
        public void Parsed_Profile_PortraitImage_Should_Be_Correct() => 
            Assert.Equal("https://d1u1mce87gyfbn.cloudfront.net/game/unlocks/0x0250000000000742.png", _testPlayer.ProfilePortraitUrl);

        [Fact]
        public void Parsed_Profile_CompetitiveRankImage_Should_Be_Correct() => 
            Assert.Equal("https://d1u1mce87gyfbn.cloudfront.net/game/rank-icons/season-2/rank-3.png",_testPlayer.CompetitiveRankImageUrl);

        [Fact]
        public void Parsed_Profile_PlayerLevelImage_Should_Be_Correct() => 
            Assert.Equal("https://d1u1mce87gyfbn.cloudfront.net/game/playerlevelrewards/0x0250000000000920_Border.png", _testPlayer.PlayerLevelImage);
    }
}