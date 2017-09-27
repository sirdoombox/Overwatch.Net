using System.Collections;
using System.Collections.Generic;

namespace OverwatchAPI
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a set of common statistics for a hero.
    /// </summary>
    public sealed class StatCategory : IReadOnlyDictionary<string, double>
    {
        internal Dictionary<string, double> Contents = new Dictionary<string, double>();

        internal StatCategory() { }

        /// <summary>
        /// Get a stat by name
        /// </summary>
        /// <param key="key">The name of the stat.</param>
        /// <returns>The double value of the stat.</returns>
        public double this[string key] => Contents[key];

        public IEnumerable<string> Keys => Contents.Keys;

        public IEnumerable<double> Values => Contents.Values;

        public int Count => Contents.Count;

        public bool ContainsKey(string key) => Contents.ContainsKey(key);

        public IEnumerator<KeyValuePair<string, double>> GetEnumerator() => Contents.GetEnumerator();

        public bool TryGetValue(string key, out double value) => Contents.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => Contents.GetEnumerator();
    }
}