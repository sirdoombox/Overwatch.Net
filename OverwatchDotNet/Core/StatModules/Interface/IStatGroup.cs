using System.Collections.Generic;

namespace OverwatchAPI.Internal
{
    interface IStatGroup
    {
        void SendPage(IEnumerable<OverwatchDataTable> TableCollection);
    }
}
