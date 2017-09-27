using System.Collections;
using System.Collections.Generic;

namespace OverwatchAPI
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a collection of Heroes for which the player has recorded at least one game of play with.
    /// </summary>
    public sealed class Stats : IReadOnlyDictionary<string, Hero>
    {
        private Dictionary<string, Hero> _contents = new Dictionary<string, Hero>();

        internal Stats() { }

        /// <summary>
        /// Get a hero by name (Special characters and spaces are ignored.
        /// </summary>
        /// <param key="key">The name of the hero.</param>
        /// <returns>A hero object if one by such a name exists - otherwise null.</returns>
        public Hero this[string key] => _contents[key];

        public IEnumerable<string> Keys => _contents.Keys;

        public IEnumerable<Hero> Values => _contents.Values;

        public int Count => _contents.Count;

        public bool ContainsKey(string key) => _contents.ContainsKey(key);

        public IEnumerator<KeyValuePair<string, Hero>> GetEnumerator() => _contents.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _contents.GetEnumerator();

        public bool TryGetValue(string key, out Hero value) => _contents.TryGetValue(key, out value);

        internal void Add(string key, Hero value) => _contents.Add(key, value);
    }
}