using System;
using System.Collections.Generic;
using System.Text;

namespace OverwatchAPI.Extensions
{
    public static class RegionExtensions
    {
        public static string ToLowerString(this Region region) => region.ToString().ToLower();
    }
}
