using System.Collections.Generic;
using System.Collections;

namespace OverwatchAPI
{
    /// <summary>
    /// Represents a set of achievment categories.
    /// </summary>
    public sealed class Achievements : IReadOnlyDictionary<string, AchievementCategory>
    {
        private Dictionary<string, AchievementCategory> contents = new Dictionary<string, AchievementCategory>();

        /// <summary>
        /// Get an achievement category by name.
        /// </summary>
        /// <param key="key">The name of the achievement category.</param>
        /// <returns>An achievement category object if one by such a name exists - otherwise null.</returns>
        public AchievementCategory this[string key] => contents[key];

        public IEnumerable<string> Keys => contents.Keys;

        public IEnumerable<AchievementCategory> Values => contents.Values;

        public int Count => contents.Count;

        public bool ContainsKey(string key) => contents.ContainsKey(key);
        
        public IEnumerator<KeyValuePair<string, AchievementCategory>> GetEnumerator() => contents.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => contents.GetEnumerator();

        public bool TryGetValue(string key, out AchievementCategory value) => contents.TryGetValue(key, out value);

        internal void Add(string catId, AchievementCategory cat) => contents.Add(catId, cat);        
    }
}
