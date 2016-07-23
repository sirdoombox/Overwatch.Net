using System.Collections.Generic;

namespace OverwatchAPI.Data
{
    public class OverwatchHeroDictionary : Dictionary<string, List<OverwatchStatTable>>
    {
        public List<OverwatchStatTable>
    }

    public class OverwatchStatTable
    {
        public string Name { get; set; }
        public Dictionary<string, double> Stats { get; set; }
    }
}
