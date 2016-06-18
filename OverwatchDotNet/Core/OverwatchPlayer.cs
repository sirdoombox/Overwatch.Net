using AngleSharp;
using OverwatchAPI.Internal;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OverwatchAPI
{
    public class OverwatchPlayer
    {
        public OverwatchPlayer(string battletag, Region region = Region.None, string profileurl = null)
        {
            Battletag = battletag;
            BattletagUrlFriendly = battletag.Replace("#", "-");
            Region = region;
            ProfileURL = profileurl;
        }

        /// <summary>
        /// The players Battletag with Discriminator - e.g. "SomeUser#1234"
        /// </summary>
        public string Battletag { get; private set; }

        /// <summary>
        /// The players nickname - e.g. "SomeUser"
        /// </summary>
        public string Username
        {
            get
            {
                return Battletag.Substring(0, Battletag.IndexOf('#') - 1);
            }
        }

        /// <summary>
        /// The PlayOverwatch profile of the player - This is only available if the user has set a region
        /// </summary>
        public string ProfileURL { get; private set; }

        /// <summary>
        /// The players region - EU/US/None
        /// </summary>
        public Region Region { get; private set; } 

        /// <summary>
        /// The last time the profile was downloaded from PlayOverwatch.
        /// </summary>
        public DateTime ProfileLastDownloaded { get; private set; }
               
        /// <summary>
        /// Check if the players Battletag is valid. - e.g. "SomeUser#1234"
        /// </summary>
        public bool BattletagIsValid
        {
            get
            {
                return new Regex(@"\w+#\d+").IsMatch(Battletag);
            }
        }

        private string BattletagUrlFriendly { get; }

        /// <summary>
        /// Detect the region of the player (Also sets the players ProfileURL if it is currently un-set)
        /// </summary>
        /// <returns></returns>
        public async Task DetectRegion()
        {
            string baseUrl = "http://playoverwatch.com/en-gb/career/pc/";
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri(baseUrl);
            var responseNA = await _client.GetAsync($"us/{BattletagUrlFriendly}");
            if (responseNA.IsSuccessStatusCode)
            {
                Region = Region.US;
                ProfileURL = ProfileURL ?? baseUrl + $"us/{BattletagUrlFriendly}";
                return;
            }
            else
            {
                var responseEU = await _client.GetAsync($"eu/{BattletagUrlFriendly}");
                if (responseEU.IsSuccessStatusCode)
                {
                    Region = Region.EU;
                    ProfileURL = ProfileURL ?? baseUrl + $"eu/{BattletagUrlFriendly}";
                    return;
                }
            }
            Region = Region.None;
        }      
        
        /// <summary>
        /// Downloads the Users Profile and parses it to
        /// </summary>
        /// <returns></returns>
        public async Task DownloadUserProfile()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenAsync(ProfileURL);
            ProfileLastDownloaded = DateTime.UtcNow;
        }  
    }
}
