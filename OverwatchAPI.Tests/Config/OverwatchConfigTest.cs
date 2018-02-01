using System;
using System.Collections.Generic;
using OverwatchAPI;
using OverwatchAPI.Config;
using Xunit;

namespace Tests.Config
{
    public class OverwatchConfigTest
    {

        [Fact]
        public void Config_Builder_WithAllPlatforms_Should_Produce_Valid_Config()
        {
            var testConfig = new OverwatchConfig.Builder()
                .WithAllPlatforms()
                .Build();
            Assert.Equal(new List<Platform> { Platform.Pc, Platform.Xbl, Platform.Psn }, testConfig.Platforms);
        }

        [Fact]
        public void Config_Builder_Full_Chain_With_Arguments_Should_Produce_Valid_Config()
        {
            var testConfig = new OverwatchConfig.Builder()
                .WithPlatforms(Platform.Pc, Platform.Psn)
                .Build();
            Assert.Equal(new List<Platform> { Platform.Pc, Platform.Psn }, testConfig.Platforms);
        }

        [Fact]
        public void Config_Builder_With_Platforms_Should_Throw_Exception()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                new OverwatchConfig.Builder()
                    .Build();
            });
        }
    }
}