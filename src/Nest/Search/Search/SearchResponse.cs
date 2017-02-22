using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public interface ISearchResponse<T> : IResponse where T : class
	{
		ShardsMetaData Shards { get; }
		HitsMetaData<T> HitsMetaData { get; }
		IReadOnlyDictionary<string, IAggregate> Aggregations { get; }
		Profile Profile { get; }
		AggregationsHelper Aggs { get; }
		IReadOnlyDictionary<string, Suggest<T>[]> Suggest { get; }
		long Took { get; }
		bool TimedOut { get; }
		bool TerminatedEarly { get; }
		string ScrollId { get; }
		long Total { get; }
		double MaxScore { get; }

		/// <summary>
		/// Returns a view on the documents inside the hits that are returned.
		/// <para>NOTE: if you use Fields() on the search descriptor, .Documents will be empty. Use
		/// .Fields instead or try Source Filtering using .Source() on the Search call
		/// to get Documents with partial fields selected
		/// </para>
		/// </summary>
		IReadOnlyCollection<T> Documents { get; }
		IReadOnlyCollection<IHit<T>> Hits { get; }

		/// <summary>
		/// Will return the field values inside the hits when the search descriptor specified .Fields.
		/// Otherwise this will always be an empty collection.
		/// </summary>
		IReadOnlyCollection<FieldValues> Fields { get; }
	}

	[JsonObject]
	public class SearchResponse<T> : ResponseBase, ISearchResponse<T> where T : class
	{
		internal ServerError MultiSearchError { get; set; }
		protected override IApiCallDetails ApiCall => MultiSearchError != null ? new ApiCallDetailsOverride(base.ApiCall, MultiSearchError) : base.ApiCall;

		[JsonProperty(PropertyName = "_shards")]
		public ShardsMetaData Shards { get; internal set; }

		[JsonProperty(PropertyName = "aggregations")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, IAggregate>))]
		public IReadOnlyDictionary<string, IAggregate> Aggregations { get; internal set; } = EmptyReadOnly<string, IAggregate>.Dictionary;

		[JsonProperty(PropertyName = "profile")]
		public Profile Profile { get; internal set; }

		private AggregationsHelper _agg = null;
		[JsonIgnore]
		public AggregationsHelper Aggs => _agg ?? (_agg = new AggregationsHelper(this.Aggregations));

		[JsonProperty(PropertyName = "suggest")]
		public IReadOnlyDictionary<string, Suggest<T>[]> Suggest { get; internal set; } =
			EmptyReadOnly<string, Suggest<T>[]>.Dictionary;

		[JsonProperty(PropertyName = "took")]
		public long Took { get; internal set; }

		[JsonProperty("timed_out")]
		public bool TimedOut { get; internal set; }

		[JsonProperty("terminated_early")]
		public bool TerminatedEarly { get; internal set; }

		/// <summary>
		/// Only set when search type = scan and scroll specified
		/// </summary>
		[JsonProperty(PropertyName = "_scroll_id")]
		public string ScrollId { get; internal set; }

		[JsonProperty(PropertyName = "hits")]
		public HitsMetaData<T> HitsMetaData { get; internal set; }

		[JsonIgnore]
		public long Total => this.HitsMetaData?.Total ?? 0;

		[JsonIgnore]
		public double MaxScore => this.HitsMetaData?.MaxScore ?? 0;

		private IReadOnlyCollection<T> _documents;
		/// <inheritdoc/>
		[JsonIgnore]
		public IReadOnlyCollection<T> Documents =>
			this._documents ?? (this._documents = this.Hits
				.Select(h => h.Source)
				.ToList());

		private IReadOnlyCollection<IHit<T>> _hits;
		[JsonIgnore]
		public IReadOnlyCollection<IHit<T>> Hits =>
			this._hits ?? (this._hits = this.HitsMetaData?.Hits ?? EmptyReadOnly<IHit<T>>.Collection);

		private IReadOnlyCollection<FieldValues> _fields;

		/// <inheritdoc/>
		public IReadOnlyCollection<FieldValues> Fields =>
				this._fields ?? (this._fields = this.Hits
					.Select(h => h.Fields)
					.ToList());

	}
}
