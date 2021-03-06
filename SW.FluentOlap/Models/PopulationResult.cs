﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using SW.FluentOlap.Utilities;

namespace SW.FluentOlap.Models
{
    public class PopulationResult : IReadOnlyDictionary<string, object>
    {
        public string Raw { get; }
        private readonly IDictionary<string, object> inner;
        public string TargetTable { get; set; }
        public object this[string key] => inner[key];
        public IEnumerable<string> Keys => inner.Keys;
        public IEnumerable<object> Values => inner.Values;
        public int Count => inner.Count;
        public bool ContainsKey(string key) => inner.ContainsKey(key);
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => inner.GetEnumerator();
        public bool TryGetValue(string key, out object value) => inner.TryGetValue(key, out value);
        IEnumerator IEnumerable.GetEnumerator() => inner.GetEnumerator();

        public PopulationResult(IDictionary<string, object> flattened)
        {
            inner = flattened;
        }

        public PopulationResult(string raw, string prefix = "")
        {
            Raw = raw;
            inner = JsonHelper.DeserializeAndFlatten(raw, prefix);
            
            
        }
    }
}
