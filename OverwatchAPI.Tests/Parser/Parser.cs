using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using OverwatchAPI.Internal;
using System.IO;
using Xunit;
using OverwatchAPI;

namespace Tests
{
    [Trait("Parsing Test", "Run from 'TestSource.txt' see test comments for info,")]
    public class Parser
    {
        // These tests are run on the source from this page: https://playoverwatch.com/en-gb/career/pc/eu/SirDoombox-2603
        // The source of the page was downloaded and stored on 24/9/2017 
        // Will serve as the basis for parser tests barring a more reliable solution.

        static IHtmlDocument doc;
        static Parser()
        {
            doc = new HtmlParser().Parse(File.ReadAllText("TestSource.txt"));
        }

        [Fact]
        public void PlayerLevel_ValidDoc_CorrectResult() => 
            Assert.Equal(84, doc.PlayerLevel());

        [Fact]
        public void CompetitiveRank_ValidDoc_CorrectResult() => 
            Assert.Equal(2215, doc.CompetitiveRank());

        [Fact]
        public void Stats_ModeCasual_CorrectResult() => 
            Assert.Equal(652428, doc.Stats(Mode.Casual)["AllHeroes"]["Assists"]["Healing Done"]);

        [Fact]
        public void Stats_ModeCompetitive_CorrectResult() => 
            Assert.Equal(7200, doc.Stats(Mode.Competitive)["Lucio"]["Game"]["Time Played"]);

        [Fact]
        public void Achievement_ValidDoc_CorrectResult()
        {
            Assert.Equal(true, doc.Achievements()["Tank"]["Hog Wild"]);
            Assert.Equal(false, doc.Achievements()["Defense"]["Did That Sting?"]);
        }

        [Fact]
        public void PortraitImage_ValidDoc_CorrectResult() => 
            Assert.Equal("https://d1u1mce87gyfbn.cloudfront.net/game/unlocks/0x0250000000000742.png", doc.PortraitImage());

        [Fact]
        public void CompetitiveRankImage_ValidDoc_CorrectResult() => 
            Assert.Equal("https://d1u1mce87gyfbn.cloudfront.net/game/rank-icons/season-2/rank-3.png", doc.CompetitiveRankImage());

        [Fact]
        public void PlayedLevelImage_ValidDoc_CorrectResult() => 
            Assert.Equal("https://d1u1mce87gyfbn.cloudfront.net/game/playerlevelrewards/0x0250000000000920_Border.png", doc.PlayerLevelImage());
    }
}