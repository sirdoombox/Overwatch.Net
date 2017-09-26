using System.Collections;
using System.Collections.Generic;

namespace OverwatchAPI
{
    /// <summary>
    /// Represents a collection of statistics categories available for this hero.
    /// </summary>
    public sealed class Hero : IReadOnlyDictionary<string, StatCategory>
    {
        internal Dictionary<string, StatCategory> Contents = new Dictionary<string, StatCategory>();

        internal Hero() { }

        /// <summary>
        /// Get a category by name
        /// </summary>
        /// <param key="key">The name of the category.</param>
        /// <returns>A category object if one by such a name exists - otherwise null.</returns>
        public StatCategory this[string key] => Contents[key];

        public int Count => Contents.Count;

        public IEnumerable<string> Keys => Contents.Keys;

        public IEnumerable<StatCategory> Values => Contents.Values;

        public bool ContainsKey(string key) => Contents.ContainsKey(key);

        public bool TryGetValue(string key, out StatCategory value) => Contents.TryGetValue(key, out value);

        public IEnumerator<KeyValuePair<string, StatCategory>> GetEnumerator() => Contents.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Contents.GetEnumerator();
    }
}
