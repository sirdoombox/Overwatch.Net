using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverwatchDotNet.Core
{
    [AttributeUsage(AttributeTargets.Property)]
    class OverwatchStatGroup : Attribute
    {
        public string StatGroupName { get; private set; }

        public OverwatchStatGroup(string statgroupname)
        {
            StatGroupName = statgroupname;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    class OverwatchStat : Attribute
    {
        public string StatName { get; private set; }

        public OverwatchStat(string statname)
        {
            StatName = statname;
        }
    }
}
