using System;
using System.Collections.Generic;
using System.Linq;

namespace OverwatchAPI.Config
{
    public sealed class OverwatchConfig
    {
        public IReadOnlyList<Platform> Platforms { get; internal set; }

        internal OverwatchConfig() { }

        public sealed class Builder
        {
            private List<Platform> _platforms = new List<Platform>();
            
            /// <summary>
            /// Sets the platforms to use when auto-detecting. Order is preserved and will dictate the order that the platforms are detected in.
            /// Console profiles do not have a set region, as such the first succesful result will be returned if a player has a profile on multiple systems.
            /// </summary>
            /// <param name="platforms"></param>
            /// <returns></returns>
            public Builder WithPlatforms(params Platform[] platforms)
            {
                _platforms = platforms.Distinct().ToList();
                return this;
            }

            /// <summary>
            /// Sets the platform detection to use the following platforms in this order - PC, XBL, PSN
            /// </summary>
            /// <returns></returns>
            public Builder WithAllPlatforms()
            {
                _platforms = Enum.GetValues(typeof(Platform)).Cast<Platform>().ToList();
                return this;
            }

            public static implicit operator OverwatchConfig(Builder bldr)
            {
                return bldr.Build();
            }

            public OverwatchConfig Build()
            {
                if (_platforms.Count <= 0)
                    throw new InvalidOperationException("A Configuration must have at least 1 platform.");
                return new OverwatchConfig()
                {
                    Platforms = _platforms
                };
            }

            /// <summary>
            /// Default configuration - Includes all regions and all platforms.
            /// </summary>
            /// <returns></returns>
            public OverwatchConfig Default()
            {
                return WithAllPlatforms();
            }
        }
    }
}
