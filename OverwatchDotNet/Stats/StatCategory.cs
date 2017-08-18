using System.Collections;
using System.Collections.Generic;

namespace OverwatchAPI
{
    public class StatCategory : IReadOnlyDictionary<string, double>
    {
        internal Dictionary<string, double> contents = new Dictionary<string, double>();

        internal StatCategory() { }

        /// <summary>
        /// Get a stat by name
        /// </summary>
        /// <param key="key">The name of the stat.</param>
        /// <returns>The double value of the stat.</returns>
        public double this[string key] => contents[key];

        public IEnumerable<string> Keys => contents.Keys;

        public IEnumerable<double> Values => contents.Values;

        public int Count => contents.Count;

        public bool ContainsKey(string key) => contents.ContainsKey(key);

        public IEnumerator<KeyValuePair<string, double>> GetEnumerator() => contents.GetEnumerator();

        public bool TryGetValue(string key, out double value) => contents.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => contents.GetEnumerator();
    }
}
