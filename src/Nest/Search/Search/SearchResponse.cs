using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{
	public interface ISearchResponse<T> : IResponse where T : class
	{
        /// <summary>
        /// Gets the meta data about the shards on which the search query was executed.
        /// </summary>
        ShardsMetaData Shards { get; }

        /// <summary>
        /// Gets the meta data about the hits that match the search query criteria.
        /// </summary>
        HitsMetaData<T> HitsMetaData { get; }

        /// <summary>
        /// Gets the collection of aggregations
        /// </summary>
        IDictionary<string, IAggregate> Aggregations { get; }

        /// <summary>
        /// Gets the results of profiling the search query. Has a value only when
        /// <see cref="ISearchRequest.Profile"/> is set to <c>true</c> on the search request.
        /// </summary>
        Profile Profile { get; }

        /// <summary>
        /// Gets the aggregations helper that can be used to more easily handle aggregation
        /// results.
        /// </summary>
        AggregationsHelper Aggs { get; }

        /// <summary>
        /// Gets the suggester results.
        /// </summary>
		IDictionary<string, Suggest[]> Suggest { get; }

		/// <summary>
		/// Time in milliseconds for Elasticsearch to execute the search
		/// </summary>
		[Obsolete(@"returned value may be larger than int. In this case, value will be int.MaxValue and TookAsLong field can be checked. Took is long in 5.0.0")]
		int Took { get; }

		/// <summary>
		/// Time in milliseconds for Elasticsearch to execute the search
		/// </summary>
		long TookAsLong { get; }

        /// <summary>
        /// Gets a value indicating whether the search timed out or not
        /// </summary>
        bool TimedOut { get; }

        /// <summary>
        /// Gets a value indicating whether the search was terminated early
        /// </summary>
        bool TerminatedEarly { get; }

        /// <summary>
        /// Gets the scroll id which can be passed to the Scroll API in order to retrieve the next batch
        /// of results. Has a value only when <see cref="SearchRequest{T}.Scroll"/> is specified on the
        /// search request
        /// </summary>
        string ScrollId { get; }

        /// <summary>
        /// Gets the total number of documents matching the search query criteria
        /// </summary>
        long Total { get; }

        /// <summary>
        /// Gets the maximum score for documents matching the search query criteria
        /// </summary>
        double MaxScore { get; }

		/// <summary>
		/// Gets the documents inside the hits, by deserializing each <see cref="IHit{T}.Source"/> in <see cref="Hits"/> into T.
		/// <para>NOTE: if you use <see cref="ISearchRequest.Fields"/> on the search request,
		/// <see cref="Documents"/> will be empty and you should use <see cref="Fields"/>
		/// instead to get the field values. As an alternative to
		/// <see cref="Fields"/>, try source filtering using <see cref="ISearchRequest.Source"/> on the
		/// search request to return <see cref="Documents"/> with partial fields selected
		/// </para>
		/// </summary>
		IEnumerable<T> Documents { get; }

        /// <summary>
        /// Gets the collection of hits that matched the query
        /// </summary>
        /// <value>
        /// The hits.
        /// </value>
		IEnumerable<IHit<T>> Hits { get; }

		/// <summary>
        /// Gets the field values inside the hits, when the search request uses
        /// <see cref="SearchRequest{T}.Fields"/>.
		/// </summary>
		IEnumerable<FieldValues> Fields { get; }

		/// <summary>
		/// Gets the highlights for each document, keyed by id
		/// </summary>
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

		private AggregationsHelper _agg;
		[JsonIgnore]
		public AggregationsHelper Aggs => _agg ?? (_agg = new AggregationsHelper(this.Aggregations));

		[JsonProperty(PropertyName = "suggest")]
		public IDictionary<string, Suggest[]> Suggest { get; internal set; }

		/// <summary>
		/// Time in milliseconds for Elasticsearch to execute the search
		/// </summary>
		[JsonProperty("took")]
		public long TookAsLong { get; internal set; }

		/// <summary>
		/// Time in milliseconds for Elasticsearch to execute the search
		/// </summary>
		[Obsolete(@"returned value may be larger than int. In this case, value will be int.MaxValue and TookAsLong field can be checked. Took is long in 5.0.0")]
		[JsonIgnore]
		public int Took => TookAsLong > int.MaxValue ? int.MaxValue : (int)TookAsLong;

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
		[Obsolete("This highlights by document id dictionary is the wrong abstraction in cases where a search can yield the same ids, " +
		          "for example, different types in the same index or a search across multiple indices. Removed in 5.0.0.")]
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
