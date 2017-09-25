using OverwatchAPI;
using System;
using Xunit;

namespace Tests
{
    [Trait("Overwatch","Integration Tests")]
    public class Integration
    {
        [Fact]
        public async void GetPlayerAsync_InvalidUsername_ExceptionThrown()
        {
            using (var ow = new Overwatch())
            {
                await Assert.ThrowsAsync<ArgumentException>(async () => await ow.GetPlayerAsync("SirDoombox", Platform.pc));
            }
        }

        [Fact]
        public async void GetPlayerAsync_InvalidRegionPlatformCombo_ExceptionThrown()
        {
            using (var ow = new Overwatch())
            {
                await Assert.ThrowsAsync<ArgumentException>(async () => await ow.GetPlayerAsync("SomeUsername", Platform.psn, Region.kr));
            }
        }

        [Fact]
        public async void GetPlayerAsync_ValidPcUsername_ReturnsValidPlayer()
        {
            using (var ow = new Overwatch())
            {
                var rslt = await ow.GetPlayerAsync("SirDoombox#2603");
                Assert.Equal(rslt.Region, Region.eu);
                Assert.Equal(rslt.Platform, Platform.pc);
            }
        }

        [Fact]
        public async void GetPlayerAsync_RandomNonExistantUsername_ReturnsNull()
        {
            using (var ow = new Overwatch())
            {
                var rslt = await ow.GetPlayerAsync("asdasdasdasdasd");
                Assert.Equal(rslt, null);
            }
        }
    }
}
