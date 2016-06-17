using System;

namespace OverwatchAPI.Internal
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
