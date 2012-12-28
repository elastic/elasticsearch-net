using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using Nest.Resolvers.Converters;

namespace Nest
{
    /// <summary>
    /// Writing these uses a custom converter that ignores the json props
    /// </summary>
    [JsonConverter(typeof(IndexSettingsConverter))]
    [JsonObject(MemberSerialization.OptIn)]
    public class IndexSettings : IDictionary<string, string>
    {
        internal static readonly IEnumerable<string> UpdateWhiteList = new List<string>
        {
            "number_of_replicas",
            "auto_expand_replicas",
            "refresh_interval",
            "term_index_interval",
            "term_index_divisor",
            "translog.flush_threshold_ops",
            "translog.flush_threshold_size",
            "translog.flush_threshold_period",
            "translog.disable_flush",
            "cache.filter.max_size",
            "cache.filter.expire",
            "gateway.snapshot_interval",
            "routing.allocation.include.",
            "routing.allocation.exclude.",
            "gc_deletes",
            //Merge can only handle updates if the setting was initially set
            "merge."
        };

        public IndexSettings()
        {
            this.Analysis = new AnalysisSettings();
            this.Mappings = new List<MapRootObject>();
            this.Settings = new Dictionary<string, string>();
        }
        public int? NumberOfShards
        {
          get
          {
            return this.GetIntegerValue("number_of_shards");
          }
          set
          {
            this.TryAdd("number_of_shards", value);
          }
        }
        public int? NumberOfReplicas { 
          get 
          {
            return this.GetIntegerValue("number_of_replicas");
          }
          set
          {
            this.TryAdd("number_of_replicas", value);
          }
        }
        
        internal int? GetIntegerValue(string key)
        {
          string value;
          int i = 0;
          if (!this.TryGetValue(key, out value) || !int.TryParse(value, out i))
            return null;
          return i;
        }

        public void TryAdd(string key, object value)
        {
          if (this.ContainsKey(key))
            this[key] = value.ToString();
          else
            this.Add(key, value.ToString());
        }

        internal Dictionary<string, string> Settings { get; set; }

        public AnalysisSettings Analysis { get; private set; }

        public IList<MapRootObject> Mappings { get; private set; }

        public void Add(string key, string value)
        {
            this.Settings.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            return this.Settings.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return this.Settings.Keys; }
        }

        public bool Remove(string key)
        {
            return this.Settings.Remove(key);
        }

        public bool TryGetValue(string key, out string value)
        {
            return this.Settings.TryGetValue(key, out value);
        }

        public ICollection<string> Values
        {
            get { return this.Settings.Values; }
        }
        [JsonIgnore]
        public string this[string key]
        {
            get
            {
                return this.Settings[key];
            }
            set
            {
                this.Settings[key] = value;
            }
        }

        public void Add(KeyValuePair<string, string> item)
        {
            this.Settings.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            this.Settings.Clear();
        }

        public bool Contains(KeyValuePair<string, string> item)
        {
            return this.Settings.ContainsKey(item.Key) && this.Settings[item.Key] == item.Value;
        }

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
        {
           throw new NotImplementedException();
        }
        [JsonIgnore]
        public int Count
        {
            get { return this.Settings.Count; }
        }
        [JsonIgnore]
        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<string, string> item)
        {
            return this.Settings.Remove(item.Key);
        }


        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return this.Settings.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.Settings.GetEnumerator();
        }
    }
}