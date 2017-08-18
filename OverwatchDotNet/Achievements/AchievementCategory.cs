using System.Collections;
using System.Collections.Generic;

namespace OverwatchAPI
{
    public class AchievementCategory : IReadOnlyDictionary<string, bool>
    {
        internal Dictionary<string, bool> contents = new Dictionary<string, bool>();

        internal AchievementCategory() { }

        /// <summary>
        /// Get an achievement by name
        /// </summary>
        /// <param key="key">The name of the achievement.</param>
        /// <returns>The bool value of the achievement.</returns>
        public bool this[string key] => contents[key];

        public IEnumerable<string> Keys => contents.Keys;

        public IEnumerable<bool> Values => contents.Values;

        public int Count => contents.Count;

        public bool ContainsKey(string key) => contents.ContainsKey(key);

        public IEnumerator<KeyValuePair<string, bool>> GetEnumerator() => contents.GetEnumerator();

        public bool TryGetValue(string key, out bool value) => contents.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => contents.GetEnumerator();
    }
}
