using System.Collections.Generic;
using System.Linq;
using OverwatchAPI;
using OverwatchAPI.Config;
using Xunit;

namespace Tests.Config
{
    public class Config
    {
        [Theory]
        [InlineData(Platform.Pc)]
        [InlineData(Platform.Pc, Platform.Psn)]
        public void WithPlatforms_ValidArgs_ValidConfig(params Platform[] platforms)
        {
            var testConfig = new OverwatchConfig.Builder()
                .WithPlatforms(platforms)
                .Build();
            Assert.Equal(platforms.ToList(), testConfig.Platforms);
        }

        [Theory]
        [InlineData(Region.Kr)]
        [InlineData(Region.Eu, Region.Us)]
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
            Assert.Equal(new List<Platform> { Platform.Pc, Platform.Xbl, Platform.Psn }, testConfig.Platforms);
            Assert.Equal(new List<Region> { Region.Us, Region.Eu, Region.Kr }, testConfig.Regions);
        }

        [Fact]
        public void FullMethodChain_ValidArgs_ValidConfig()
        {
            var testConfig = new OverwatchConfig.Builder()
                .WithPlatforms(Platform.Pc, Platform.Psn)
                .WithRegions(Region.Eu, Region.Kr)
                .Build();
            Assert.Equal(new List<Platform> { Platform.Pc, Platform.Psn }, testConfig.Platforms);
            Assert.Equal(new List<Region> { Region.Eu, Region.Kr }, testConfig.Regions);
        }
    }
}