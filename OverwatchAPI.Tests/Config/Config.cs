using OverwatchAPI;
using OverwatchAPI.Config;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests
{
    [Trait("Config Tests", "")]
    public class Config
    {
        [Theory]
        [InlineData(Platform.pc)]
        [InlineData(Platform.pc, Platform.psn)]
        public void WithPlatforms_ValidArgs_ValidConfig(params Platform[] platforms)
        {
            var testConfig = new OverwatchConfig.Builder()
                .WithPlatforms(platforms)
                .Build();
            Assert.Equal(platforms.ToList(), testConfig.Platforms);
        }

        [Theory]
        [InlineData(Region.kr)]
        [InlineData(Region.eu, Region.us)]
        public void WithRegions_ValidArgs_ValidConfig(params Region[] regions)
        {
            var testConfig = new OverwatchConfig.Builder()
                .WithRegions(regions)
                .Build();
            Assert.Equal(regions.ToList(), testConfig.Regions);
        }

        [Fact]
        public void WithAllPlatformsWithAllRegion_ValidArgs_ValidConfig()
        {
            var testConfig = new OverwatchConfig.Builder()
                .WithAllPlatforms()
                .WithAllRegions()
                .Build();
            Assert.Equal(new List<Platform> { Platform.pc, Platform.xbl, Platform.psn }, testConfig.Platforms);
            Assert.Equal(new List<Region> { Region.us, Region.eu, Region.kr }, testConfig.Regions);
        }

        [Fact]
        public void FullMethodChain_ValidArgs_ValidConfig()
        {
            var testConfig = new OverwatchConfig.Builder()
                .WithPlatforms(Platform.pc, Platform.psn)
                .WithRegions(Region.eu, Region.kr)
                .Build();
            Assert.Equal(new List<Platform> { Platform.pc, Platform.psn }, testConfig.Platforms);
            Assert.Equal(new List<Region> { Region.eu, Region.kr }, testConfig.Regions);
        }
    }
}