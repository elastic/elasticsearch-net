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
	public class IndexSettings : IDictionary<string, object>
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
			this.Mappings = new List<RootObjectMapping>();
			this.Warmers = new Dictionary<string, WarmerMapping>();
			this.Settings = new Dictionary<string, object>();
			this.Similarity = new SimilaritySettings();
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
		public int? NumberOfReplicas
		{
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
			object value;
			int i = 0;
			if (!this.TryGetValue(key, out value)
				|| value == null
				|| !int.TryParse(value.ToString(), out i))
				return null;
			return i;
		}

		public void TryAdd(string key, object value)
		{
			if (this.ContainsKey(key))
				this[key] = value;
			else
				this.Add(key, value);
		}

		internal Dictionary<string, object> Settings { get; set; }

		public AnalysisSettings Analysis { get; internal set; }

		public IList<RootObjectMapping> Mappings { get; internal set; }

		public Dictionary<string, WarmerMapping> Warmers { get; internal set; }

		public SimilaritySettings Similarity { get; internal set; }

		public void Add(string key, object value)
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

		public bool TryGetValue(string key, out object value)
		{
			return this.Settings.TryGetValue(key, out value);
		}

		public ICollection<object> Values
		{
			get { return this.Settings.Values; }
		}
		[JsonIgnore]
		public object this[string key]
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

		public void Add(KeyValuePair<string, object> item)
		{
			this.Settings.Add(item.Key, item.Value);
		}

		public void Clear()
		{
			this.Settings.Clear();
		}

		public bool Contains(KeyValuePair<string, object> item)
		{
			return this.Settings.ContainsKey(item.Key) && this.Settings[item.Key] == item.Value;
		}

		public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
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

		public bool Remove(KeyValuePair<string, object> item)
		{
			return this.Settings.Remove(item.Key);
		}


		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			return this.Settings.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.Settings.GetEnumerator();
		}
	}
}