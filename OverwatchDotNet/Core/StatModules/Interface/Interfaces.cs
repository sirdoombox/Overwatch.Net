using System.Collections.Generic;

namespace OverwatchAPI.Internal
{
    public interface IHeroStats
    {
        void SendPage(IEnumerable<OverwatchDataTable> TableCollection);
    }

    public interface IStatModule
    {
        void SendTable(OverwatchDataTable table);
    }
}
