using System.IO;
using OverwatchAPI;
using OverwatchAPI.Parser;
using OverwatchAPI.WebClient;
using Xunit;

namespace Tests.Core.Parser
{
    public class Parser
    {
        // These tests are run on the source from this page: https://playoverwatch.com/en-gb/career/pc/eu/SirDoombox-2603
        // The source of the page was downloaded and stored on 24/9/2017 
        // Will serve as the basis for parser tests barring a more reliable solution.

        private static readonly ProfileParser _parser;
        private static readonly Player _testPlayer;

        static Parser()
        {
            var source = File.ReadAllText("TestSource.txt");
            var data = new ProfileClient.ProfileRequestData("", source);
            _parser = new ProfileParser();
            _testPlayer = new Player();
            _parser.Parse(_testPlayer, data).GetAwaiter().GetResult();
        }       

        [Fact]
        public void PlayerLevel_ValidDoc_CorrectResult() => 
            Assert.Equal(84, _testPlayer.PlayerLevel);

        [Fact]
        public void CompetitiveRank_ValidDoc_CorrectResult() => 
            Assert.Equal(2215, _testPlayer.CompetitiveRank);

        [Fact]
        public void Stats_ModeCasual_CorrectResult() => 
            Assert.Equal(652428, _testPlayer.CasualStats["AllHeroes"]["Assists"]["Healing Done"]);

        [Fact]
        public void Stats_ModeCompetitive_CorrectResult() => 
            Assert.Equal(7200, _testPlayer.CompetitiveStats["Lucio"]["Game"]["Time Played"]);

        [Fact]
        public void Achievement_ValidDoc_CorrectResult()
        {
            Assert.Equal(true, _testPlayer.Achievements["Tank"]["Hog Wild"]);
            Assert.Equal(false, _testPlayer.Achievements["Defense"]["Did That Sting?"]);
        }

        [Fact]
        public void PortraitImage_ValidDoc_CorrectResult() => 
            Assert.Equal("https://d1u1mce87gyfbn.cloudfront.net/game/unlocks/0x0250000000000742.png", _testPlayer.ProfilePortraitUrl);

        [Fact]
        public void CompetitiveRankImage_ValidDoc_CorrectResult() => 
            Assert.Equal("https://d1u1mce87gyfbn.cloudfront.net/game/rank-icons/season-2/rank-3.png",_testPlayer.CompetitiveRankImageUrl);

        [Fact]
        public void PlayedLevelImage_ValidDoc_CorrectResult() => 
            Assert.Equal("https://d1u1mce87gyfbn.cloudfront.net/game/playerlevelrewards/0x0250000000000920_Border.png", _testPlayer.PlayerLevelImage);
    }
}