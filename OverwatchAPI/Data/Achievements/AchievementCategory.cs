using System.Collections;
using System.Collections.Generic;

namespace OverwatchAPI
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a set of achievements of a given category.
    /// </summary>
    public sealed class AchievementCategory : IReadOnlyDictionary<string, bool>
    {
        internal Dictionary<string, bool> Contents = new Dictionary<string, bool>();

        internal AchievementCategory() { }

        /// <summary>
        /// Get an achievement by name
        /// </summary>
        /// <param key="key">The name of the achievement.</param>
        /// <returns>The bool value of the achievement.</returns>
        public bool this[string key] => Contents[key];

        public IEnumerable<string> Keys => Contents.Keys;

        public IEnumerable<bool> Values => Contents.Values;

        public int Count => Contents.Count;

        public bool ContainsKey(string key) => Contents.ContainsKey(key);

        public IEnumerator<KeyValuePair<string, bool>> GetEnumerator() => Contents.GetEnumerator();

        public bool TryGetValue(string key, out bool value) => Contents.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => Contents.GetEnumerator();
    }
}
