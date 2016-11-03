using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[ContractJsonConverter(typeof(DefaultHitJsonConverter))]
	public interface IHit<out T> where T : class
	{
		FieldValues Fields { get; }
		T Source { get; }
		string Index { get; }
		string Type { get; }
		long? Version { get; }
		double? Score { get; }
		string Id { get; }
		string Parent { get; }
		string Routing { get; }
		[Obsolete("This feature is no longer supported on indices created in Elasticsearch 5.x and up")]
		long? Timestamp { get; }
		[Obsolete("This feature is no longer supported on indices created in Elasticsearch 5.x and up")]
		long? Ttl { get; }
		IEnumerable<object> Sorts { get; }
		HighlightFieldDictionary Highlights { get; }
		Explanation Explanation { get; }
		IEnumerable<string> MatchedQueries { get; }
		IDictionary<string, InnerHitsResult> InnerHits { get; }
	}

	[JsonObject]
	public class Hit<T> : IHit<T> where T : class
	{
		[JsonProperty("fields")]
		public FieldValues Fields { get; internal set; }

		[JsonProperty("_source")]
		public T Source { get; internal set; }

		[JsonProperty("_index")]
		public string Index { get; internal set; }

		[JsonProperty("inner_hits")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		public IDictionary<string, InnerHitsResult> InnerHits { get; internal set; }

		[JsonProperty("_score")]
		public double? Score { get; set; }

		[JsonProperty("_type")]
		public string Type { get; internal set; }

		[JsonProperty("_version")]
		public long? Version { get; internal set; }

		[JsonProperty("_id")]
		public string Id { get; internal set; }

		[JsonProperty("_parent")]
		public string Parent { get; internal set; }

		[JsonProperty("_routing")]
		public string Routing { get; internal set; }

		[JsonProperty("_timestamp")]
		[Obsolete("This property is no longer returned on indices created in Elasticsearch 5.0.0 and up")]
		public long? Timestamp { get; internal set; }

		[JsonProperty("_ttl")]
		[Obsolete("This property is no longer returned on indices created in Elasticsearch 5.0.0 and up")]
		public long? Ttl { get; internal set; }

		[JsonProperty("sort")]
		public IEnumerable<object> Sorts { get; internal set; }

		[JsonProperty("highlight")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		internal Dictionary<string, List<string>> _Highlight { get; set; }

		public HighlightFieldDictionary Highlights
		{
			get
			{
				if (_Highlight == null)
					return new HighlightFieldDictionary();

				var highlights = _Highlight.Select(kv => new HighlightHit
				{
					DocumentId = this.Id,
					Field = kv.Key,
					Highlights = kv.Value
				}).ToDictionary(k => k.Field, v => v);

				return new HighlightFieldDictionary(highlights);
			}
		}

		[JsonProperty("_explanation")]
		public Explanation Explanation { get; internal set; }

		[JsonProperty("matched_queries")]
		public IEnumerable<string> MatchedQueries { get; internal set; }
	}
}
