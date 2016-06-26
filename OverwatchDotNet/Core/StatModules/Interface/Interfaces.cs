using System.Collections.Generic;

namespace OverwatchAPI.Internal
{
    public interface IStatGroup
    {
        void SendPage(IEnumerable<OverwatchDataTable> TableCollection);
    }

    public interface IStatModule
    {
        void SendTable(OverwatchDataTable table);
    }
}
