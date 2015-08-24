using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// Writing these uses a custom converter that ignores the json props
	/// </summary>
	public class IndexState : IIndexState
	{
		public IndexState()
		{
			this.Analysis = new AnalysisSettings();
			this.Similarity = new SimilaritySettings();
			this.Mappings = new List<TypeMapping>();
			this.Warmers = new Dictionary<string, WarmerMapping>();
			this.Settings = new Dictionary<string, object>();
		}

		public int? NumberOfReplicas
		{
			get { return this.GetIntegerValue("number_of_replicas"); }
			set { this.Settings["number_of_replicas"] = value; }
		}
		public int? NumberOfShards
		{
			get { return this.GetIntegerValue("number_of_shards"); }
			set { this.Settings["number_of_shards"] = value; }
		}

		internal int? GetIntegerValue(string key)
		{
			object value;
			int i = 0;
			if (!this.Settings.TryGetValue(key, out value)
				|| value == null
				|| !int.TryParse(value.ToString(), out i))
				return null;
			return i;
		}

		public IDictionary<string, object> Settings { get; set; }
		
		/// <summary>
		/// Dynamic view of the settings object, useful for reading value from the settings
		/// as it allows you to chain without nullrefs. Cannot be used to assign setting values though
		/// </summary>
		
		public dynamic AsExpando => DynamicResponse.Create(this.Settings);		
		public AnalysisSettings Analysis { get; set; }

		//TODO NEST 2.0 change this to dictionary to better reflect the actual elasticsearch structure
		public IList<TypeMapping> Mappings { get; set; }

		[JsonConverter(typeof(DictionaryKeysAreNotFieldNamesJsonConverter))]
		public Dictionary<string, ICreateAliasOperation> Aliases { get; set; }
			
		[JsonConverter(typeof(DictionaryKeysAreNotFieldNamesJsonConverter))]
		public Dictionary<string, WarmerMapping> Warmers { get; set; }

		public SimilaritySettings Similarity { get; internal set; }
	}





}