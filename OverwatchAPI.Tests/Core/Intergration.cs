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
            using (var ow = new OverwatchClient())
            {
                await Assert.ThrowsAsync<ArgumentException>(async () => await ow.GetPlayerAsync("SirDoombox", Platform.pc));
            }
        }

        [Fact]
        public async void GetPlayerAsync_InvalidRegionPlatformCombo_ExceptionThrown()
        {
            using (var ow = new OverwatchClient())
            {
                await Assert.ThrowsAsync<ArgumentException>(async () => await ow.GetPlayerAsync("SomeUsername", Platform.psn, Region.kr));
            }
        }

        [Fact]
        public async void GetPlayerAsync_ValidPcUsername_ReturnsValidPlayer()
        {
            using (var ow = new OverwatchClient())
            {
                var rslt = await ow.GetPlayerAsync("SirDoombox#2603");
                Assert.Equal(rslt.Region, Region.eu);
                Assert.Equal(rslt.Platform, Platform.pc);
            }
        }

        [Fact]
        public async void GetPlayerAsync_RandomNonExistantUsername_ReturnsNull()
        {
            using (var ow = new OverwatchClient())
            {
                var rslt = await ow.GetPlayerAsync("asdasdasdasdasd");
                Assert.Null(rslt);
            }
        }

        [Fact]
        public async void UpdatePlayerAsync_ValidPlayerObject_ReturnsValidPlayer()
        {
            using (var ow = new OverwatchClient())
            {
                var testPlayer = new Player
                {
                    Username = "SirDoombox#2603",
                    Platform = Platform.pc,
                    Region = Region.eu
                };
                var rslt = await ow.UpdatePlayerAsync(testPlayer);
                Assert.NotNull(rslt);
            }
        }

        [Fact]
        public async void UpdatePlayerAsync_InvalidPlayerObject_ThrowsException()
        {
            using (var ow = new OverwatchClient())
            {
                var testPlayer = new Player
                {
                    Username = "SirDoombox#2603",
                    Platform = Platform.psn,
                    Region = Region.eu
                };
                await Assert.ThrowsAnyAsync<Exception>(async () => await ow.UpdatePlayerAsync(testPlayer));
            }
        }
    }
}
