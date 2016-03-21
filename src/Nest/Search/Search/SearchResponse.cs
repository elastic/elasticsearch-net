using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface ISearchResponse<T> : IResponse where T : class
	{
		ShardsMetaData Shards { get; }
		HitsMetaData<T> HitsMetaData { get; }
		IDictionary<string, IAggregate> Aggregations { get; }
		Profile Profile { get; }
		AggregationsHelper Aggs { get; }
		IDictionary<string, Suggest[]> Suggest { get; }
		int Took { get; }
		bool TimedOut { get; }
		bool TerminatedEarly { get; }
		string ScrollId { get; }
		long Total { get; }
		double MaxScore { get; }
		/// <summary>
		/// Returns a view on the documents inside the hits that are returned.
		/// <para>NOTE: if you use Fields() on the search descriptor .Documents will be empty use
		/// .Fields instead or try the 'source filtering' feature introduced in Elasticsearch 1.0
		/// using .Source() on the search descriptor to get Documents of type T with only certain parts selected
		/// </para>
		/// </summary>
		IEnumerable<T> Documents { get; }
		IEnumerable<IHit<T>> Hits { get; }

		/// <summary>
		/// Will return the field values inside the hits when the search descriptor specified .Fields.
		/// Otherwise this will always be an empty collection.
		/// </summary>
		IEnumerable<FieldValues> Fields { get; }
		HighlightDocumentDictionary Highlights { get; }
	}

	[JsonObject]
	public class SearchResponse<T> : ResponseBase, ISearchResponse<T> where T : class
	{
		internal ServerError MultiSearchError { get; set; }
		public override IApiCallDetails ApiCall => MultiSearchError != null ? new ApiCallDetailsOverride(base.ApiCall, MultiSearchError) : base.ApiCall;

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }

		[JsonProperty(PropertyName = "hits")]
		public HitsMetaData<T> HitsMetaData { get; internal set; }

		[JsonProperty(PropertyName = "aggregations")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter))]
		public IDictionary<string, IAggregate> Aggregations { get; internal set; } = new Dictionary<string, IAggregate>();

		[JsonProperty(PropertyName = "profile")]
		public Profile Profile { get; internal set; }

		private AggregationsHelper _agg = null;
		[JsonIgnore]
		public AggregationsHelper Aggs => _agg ?? (_agg = new AggregationsHelper(this.Aggregations));

		[JsonProperty(PropertyName = "suggest")]
		public IDictionary<string, Suggest[]> Suggest { get; internal set; }

		[JsonProperty(PropertyName = "took")]
		public int Took { get; internal set; }

		[JsonProperty("timed_out")]
		public bool TimedOut { get; internal set; }

		[JsonProperty("terminated_early")]
		public bool TerminatedEarly { get; internal set; }

		/// <summary>
		/// Only set when search type = scan and scroll specified
		/// </summary>
		[JsonProperty(PropertyName = "_scroll_id")]
		public string ScrollId { get; internal set; }

		[JsonIgnore]
		public long Total => this.HitsMetaData?.Total ?? 0;

		[JsonIgnore]
		public double MaxScore => this.HitsMetaData?.MaxScore ?? 0;

		private IList<T> _documents;
		/// <inheritdoc/>
		[JsonIgnore]
		public IEnumerable<T> Documents =>
			this._documents ?? (this._documents = this.Hits
				.Select(h => h.Source)
				.ToList());

		[JsonIgnore]
		public IEnumerable<IHit<T>> Hits => this.HitsMetaData?.Hits ?? Enumerable.Empty<IHit<T>>();

		private IList<FieldValues> _fields;
		/// <inheritdoc/>
		public IEnumerable<FieldValues> Fields =>
				this._fields ?? (this._fields = this.Hits
					.Select(h => h.Fields)
					.ToList());


		private HighlightDocumentDictionary _highlights = null;
		/// <summary>
		/// IDictionary of id -Highlight Collection for the document
		/// </summary>
		public HighlightDocumentDictionary Highlights
		{
			get
			{
				if (_highlights != null) return _highlights;

				var dict = new HighlightDocumentDictionary();
				if (this.HitsMetaData == null || !this.HitsMetaData.Hits.HasAny())
					return dict;


				foreach (var hit in this.HitsMetaData.Hits)
				{
					if (!hit.Highlights.Any())
						continue;
					dict.Add(hit.Id, hit.Highlights);
				}
				this._highlights = dict;
				return dict;
			}
		}
	}
}
