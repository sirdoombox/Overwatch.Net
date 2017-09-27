using System;
using System.Collections.Generic;
using System.Linq;
using OverwatchAPI;
using OverwatchAPI.Config;
using Xunit;

namespace Tests.Config
{
    public class OverwatchConfigTest
    {

        [Fact]
        public void Config_Builder_WithAllRegions_And_WithAllPlatforms_Should_Produce_Valid_Config()
        {
            var testConfig = new OverwatchConfig.Builder()
                .WithAllPlatforms()
                .WithAllRegions()
                .Build();
            Assert.Equal(new List<Platform> { Platform.Pc, Platform.Xbl, Platform.Psn }, testConfig.Platforms);
            Assert.Equal(new List<Region> { Region.Us, Region.Eu, Region.Kr }, testConfig.Regions);
        }

        [Fact]
        public void Config_Builder_Full_Chain_With_Arguments_Should_Produce_Valid_Config()
        {
            var testConfig = new OverwatchConfig.Builder()
                .WithPlatforms(Platform.Pc, Platform.Psn)
                .WithRegions(Region.Eu, Region.Kr)
                .Build();
            Assert.Equal(new List<Platform> { Platform.Pc, Platform.Psn }, testConfig.Platforms);
            Assert.Equal(new List<Region> { Region.Eu, Region.Kr }, testConfig.Regions);
        }

        [Fact]
        public void Config_Builder_With_Only_Regions_Should_Throw_Exception()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                new OverwatchConfig.Builder()
                    .WithRegions(Region.Eu, Region.Us, Region.None)
                    .Build();
            });
        }

        [Fact]
        public void Config_Builder_With_No_Regions_Or_Platforms_Should_Throw_Exception()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                new OverwatchConfig.Builder()
                    .Build();
            });
        }

        [Fact]
        public void Config_Builder_With_Pc_But_No_Regions_Should_Throw_Exception()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                new OverwatchConfig.Builder()
                    .WithPlatforms(Platform.Pc)
                    .Build();
            });
        }

        [Fact]
        public void Config_Builder_With_Regions_And_Console_Platforms_Only_Should_Throw_Exception()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                new OverwatchConfig.Builder()
                    .WithRegions(Region.Eu)
                    .WithPlatforms(Platform.Psn, Platform.Xbl)
                    .Build();
            });
        }
    }
}