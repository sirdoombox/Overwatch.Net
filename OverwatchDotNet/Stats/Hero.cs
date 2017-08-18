using System.Collections;
using System.Collections.Generic;

namespace OverwatchAPI
{
    /// <summary>
    /// Represents a collection of statistics categories available for this hero.
    /// </summary>
    public sealed class Hero : IReadOnlyDictionary<string, StatCategory>
    {
        internal Dictionary<string, StatCategory> contents = new Dictionary<string, StatCategory>();

        internal Hero() { }

        /// <summary>
        /// Get a category by name
        /// </summary>
        /// <param key="key">The name of the category.</param>
        /// <returns>A category object if one by such a name exists - otherwise null.</returns>
        public StatCategory this[string key] => contents[key];

        public int Count => contents.Count;

        public IEnumerable<string> Keys => contents.Keys;

        public IEnumerable<StatCategory> Values => contents.Values;

        public bool ContainsKey(string key) => contents.ContainsKey(key);

        public bool TryGetValue(string key, out StatCategory value) => contents.TryGetValue(key, out value);

        public IEnumerator<KeyValuePair<string, StatCategory>> GetEnumerator() => contents.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => contents.GetEnumerator();
    }
}
