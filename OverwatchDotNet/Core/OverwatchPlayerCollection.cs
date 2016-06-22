using System.Collections;
using System.Collections.Generic;

namespace OverwatchAPI
{
    class OverwatchPlayerCollection : IEnumerable<OverwatchPlayer>
    {
        private List<OverwatchPlayer> OverwatchPlayers;

        public OverwatchPlayerCollection()
        {
            OverwatchPlayers = new List<OverwatchPlayer>();
        }

        public void Add(OverwatchPlayer player)
        {
            OverwatchPlayers.Add(player);
        }

        public void Remove(OverwatchPlayer player)
        {
            OverwatchPlayers.Remove(player);
        }

        public OverwatchPlayer this[int index]
        {
            get { return OverwatchPlayers[index]; }
            set { OverwatchPlayers.Insert(index, value); }
        }

        public IEnumerator<OverwatchPlayer> GetEnumerator()
        {
            return OverwatchPlayers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
