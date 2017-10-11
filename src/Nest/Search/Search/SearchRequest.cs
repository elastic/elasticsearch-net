using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace Nest
{

	[JsonConverter(typeof(ReadAsTypeJsonConverter<SearchRequest>))]
	public partial interface ISearchRequest : ICovariantSearchRequest
	{
		[JsonProperty("timeout")]
		string Timeout { get; set; }

		[JsonProperty("from")]
		int? From { get; set; }

		[JsonProperty("size")]
		int? Size { get; set; }

		[JsonProperty("explain")]
		bool? Explain { get; set; }

		[JsonProperty("version")]
		bool? Version { get; set; }

		[JsonProperty("track_scores")]
		bool? TrackScores { get; set; }

		[JsonProperty("profile")]
		bool? Profile { get; set; }

		[JsonProperty("min_score")]
		double? MinScore { get; set; }

		[JsonProperty("terminate_after")]
		long? TerminateAfter { get; set; }

		[JsonProperty("indices_boost")]
		[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<IndexName, double>))]
		IDictionary<IndexName, double> IndicesBoost { get; set; }

		[JsonProperty("sort")]
		IList<ISort> Sort { get; set; }

		[JsonProperty("search_after")]
		IList<object> SearchAfter { get; set; }

		[JsonProperty("suggest")]
		ISuggestContainer Suggest { get; set; }

		[JsonProperty("highlight")]
		IHighlight Highlight { get; set; }

		[JsonProperty("collapse")]
		IFieldCollapse Collapse { get; set; }

		[JsonProperty("rescore")]
		IList<IRescore> Rescore { get; set; }

		[JsonProperty("stored_fields")]
		Fields StoredFields { get; set; }

		[JsonProperty("script_fields")]
		IScriptFields ScriptFields { get; set; }

		[JsonProperty("_source")]
		Union<bool, ISourceFilter> Source { get; set; }

		[JsonProperty("aggs")]
		AggregationDictionary Aggregations { get; set; }

		[JsonProperty("slice")]
		ISlicedScroll Slice { get; set; }

		[JsonProperty("query")]
		QueryContainer Query { get; set; }

		[JsonProperty("post_filter")]
		QueryContainer PostFilter { get; set; }

		string Preference { get; }

		string Routing { get; }

		SearchType? SearchType { get; }

		bool? IgnoreUnavalable { get; }
	}

	public partial interface ISearchRequest<T> : ISearchRequest { }

	public partial class SearchRequest
	{
		private Type _clrType { get; set; }
		Type ICovariantSearchRequest.ClrType => this._clrType;
		Types ICovariantSearchRequest.ElasticsearchTypes => ((ISearchRequest)this).Type;
		protected override HttpMethod HttpMethod =>
			RequestState.RequestParameters?.ContainsKey("_source") == true || RequestState.RequestParameters?.ContainsKey("q") == true ? HttpMethod.GET : HttpMethod.POST;

		public string Timeout { get; set; }
		public int? From { get; set; }
		public int? Size { get; set; }
		public bool? Explain { get; set; }
		public bool? Version { get; set; }
		public bool? TrackScores { get; set; }
		public bool? Profile { get; set; }
		public double? MinScore { get; set; }
		public long? TerminateAfter { get; set; }
		public Fields StoredFields { get; set; }
		public IScriptFields ScriptFields { get; set; }
		public Union<bool, ISourceFilter> Source { get; set; }
		public IList<ISort> Sort { get; set; }
		public IList<object> SearchAfter { get; set; }
		public IDictionary<IndexName, double> IndicesBoost { get; set; }
		public QueryContainer PostFilter { get; set; }
		public ISlicedScroll Slice { get; set; }
		public QueryContainer Query { get; set; }
		public IList<IRescore> Rescore { get; set; }
		public ISuggestContainer Suggest { get; set; }
		public IHighlight Highlight { get; set; }
		public IFieldCollapse Collapse { get; set; }
		public AggregationDictionary Aggregations { get; set; }

		SearchType? ISearchRequest.SearchType => RequestState.RequestParameters?.GetQueryStringValue<SearchType?>("search_type");

		string ISearchRequest.Preference => RequestState.RequestParameters?.GetQueryStringValue<string>("preference");

		string ISearchRequest.Routing => RequestState.RequestParameters?.GetQueryStringValue<string[]>("routing") == null
			? null
			: string.Join(",", RequestState.RequestParameters?.GetQueryStringValue<string[]>("routing"));

		bool? ISearchRequest.IgnoreUnavalable => RequestState.RequestParameters?.GetQueryStringValue<bool?>("ignore_unavailable");

		public Func<dynamic, Hit<dynamic>, Type> TypeSelector { get; set; }
	}

	public partial class SearchRequest<T>
	{
		Type ICovariantSearchRequest.ClrType => typeof(T);
		Types ICovariantSearchRequest.ElasticsearchTypes => ((ISearchRequest)this).Type;
		protected override HttpMethod HttpMethod =>
			RequestState.RequestParameters?.ContainsKey("_source") == true || RequestState.RequestParameters?.ContainsKey("q") == true ? HttpMethod.GET : HttpMethod.POST;

		public string Timeout { get; set; }
		public int? From { get; set; }
		public int? Size { get; set; }
		public bool? Explain { get; set; }
		public bool? Version { get; set; }
		public bool? TrackScores { get; set; }
		public bool? Profile { get; set; }
		public double? MinScore { get; set; }
		public long? TerminateAfter { get; set; }
		public Fields StoredFields { get; set; }
		public IScriptFields ScriptFields { get; set; }
		public Union<bool, ISourceFilter> Source { get; set; }
		public IList<ISort> Sort { get; set; }
		public IList<object> SearchAfter { get; set; }
		public IDictionary<IndexName, double> IndicesBoost { get; set; }
		public QueryContainer PostFilter { get; set; }
		public ISlicedScroll Slice { get; set; }
		public QueryContainer Query { get; set; }
		public IList<IRescore> Rescore { get; set; }
		public ISuggestContainer Suggest { get; set; }
		public IHighlight Highlight { get; set; }
		public IFieldCollapse Collapse { get; set; }
		public AggregationDictionary Aggregations { get; set; }

		SearchType? ISearchRequest.SearchType => RequestState.RequestParameters?.GetQueryStringValue<SearchType?>("search_type");

		string ISearchRequest.Preference => RequestState.RequestParameters?.GetQueryStringValue<string>("preference");

		string ISearchRequest.Routing => RequestState.RequestParameters?.GetQueryStringValue<string[]>("routing") == null
			? null
			: string.Join(",", RequestState.RequestParameters?.GetQueryStringValue<string[]>("routing"));

		bool? ISearchRequest.IgnoreUnavalable => RequestState.RequestParameters?.GetQueryStringValue<bool?>("ignore_unavailable");

		public Func<dynamic, Hit<dynamic>, Type> TypeSelector { get; set; }
	}

	/// <summary>
	/// A descriptor wich describes a search operation for _search and _msearch
	/// </summary>
	public partial class SearchDescriptor<T> where T : class
	{
		Type ICovariantSearchRequest.ClrType => typeof(T);
		Types ICovariantSearchRequest.ElasticsearchTypes => ((ISearchRequest)this).Type;
		Func<dynamic, Hit<dynamic>, Type> ICovariantSearchRequest.TypeSelector { get; set; }
		protected override HttpMethod HttpMethod =>
			RequestState.RequestParameters?.ContainsKey("_source") == true || RequestState.RequestParameters?.ContainsKey("q") == true ? HttpMethod.GET : HttpMethod.POST;

		SearchType? ISearchRequest.SearchType => RequestState.RequestParameters.GetQueryStringValue<SearchType?>("search_type");

		string ISearchRequest.Preference => RequestState.RequestParameters.GetQueryStringValue<string>("preference");

		string ISearchRequest.Routing => RequestState.RequestParameters.GetQueryStringValue<string[]>("routing") == null
			? null : string.Join(",", RequestState.RequestParameters.GetQueryStringValue<string[]>("routing"));

		bool? ISearchRequest.IgnoreUnavalable => RequestState.RequestParameters.GetQueryStringValue<bool?>("ignore_unavailable");

		string ISearchRequest.Timeout { get; set; }
		int? ISearchRequest.From { get; set; }
		int? ISearchRequest.Size { get; set; }
		bool? ISearchRequest.Explain { get; set; }
		bool? ISearchRequest.Version { get; set; }
		bool? ISearchRequest.TrackScores { get; set; }
		bool? ISearchRequest.Profile { get; set; }
		double? ISearchRequest.MinScore { get; set; }
		long? ISearchRequest.TerminateAfter { get; set; }

		IDictionary<IndexName, double> ISearchRequest.IndicesBoost { get; set; }
		IList<ISort> ISearchRequest.Sort { get; set; }
		IList<object> ISearchRequest.SearchAfter { get; set; }
		ISuggestContainer ISearchRequest.Suggest { get; set; }
		IHighlight ISearchRequest.Highlight { get; set; }
		IFieldCollapse ISearchRequest.Collapse { get; set; }
		IList<IRescore> ISearchRequest.Rescore { get; set; }
		ISlicedScroll ISearchRequest.Slice { get; set; }
		QueryContainer ISearchRequest.Query { get; set; }
		QueryContainer ISearchRequest.PostFilter { get; set; }
		Fields ISearchRequest.StoredFields { get; set; }
		IScriptFields ISearchRequest.ScriptFields { get; set; }
		Union<bool, ISourceFilter> ISearchRequest.Source { get; set; }
		AggregationDictionary ISearchRequest.Aggregations { get; set; }

		public SearchDescriptor<T> Aggregations(Func<AggregationContainerDescriptor<T>, IAggregationContainer> aggregationsSelector) =>
			Assign(a => a.Aggregations = aggregationsSelector(new AggregationContainerDescriptor<T>())?.Aggregations);

		public SearchDescriptor<T> Source(bool enabled = true) => Assign(a => a.Source = enabled);

		public SearchDescriptor<T> Source(Func<SourceFilterDescriptor<T>, ISourceFilter> selector) =>
			Assign(a => a.Source = new Union<bool, ISourceFilter>(selector?.Invoke(new SourceFilterDescriptor<T>())));

		/// <summary>
		/// The number of hits to return. Defaults to 10.
		/// </summary>
		public SearchDescriptor<T> Size(int size) => Assign(a => a.Size = size);

		/// <summary>
		/// The number of hits to return. Alias for <see cref="Size"/>. Defaults to 10.
		/// </summary>
		public SearchDescriptor<T> Take(int take) => this.Size(take);

		/// <summary>
		/// The starting from index of the hits to return. Defaults to 0.
		/// </summary>
		public SearchDescriptor<T> From(int from) => Assign(a => a.From = from);

		/// <summary>
		/// The starting from index of the hits to return. Alias for <see cref="From"/>. Defaults to 0.
		/// </summary>
		public SearchDescriptor<T> Skip(int skip) => this.From(skip);

		/// <summary>
		/// A search timeout, bounding the search request to be executed within the
		/// specified time value and bail with the hits accumulated up
		/// to that point when expired. Defaults to no timeout.
		/// </summary>
		public SearchDescriptor<T> Timeout(string timeout) => Assign(a => a.Timeout = timeout);

		/// <summary>
		/// Enables explanation for each hit on how its score was computed.
		/// (Use .DocumentsWithMetaData on the return results)
		/// </summary>
		public SearchDescriptor<T> Explain(bool explain = true) => Assign(a => a.Explain = explain);

		/// <summary>
		/// Returns a version for each search hit. (Use .DocumentsWithMetaData on the return results)
		/// </summary>
		public SearchDescriptor<T> Version(bool version = true) => Assign(a => a.Version = version);

		/// <summary>
		/// Make sure we keep calculating score even if we are sorting on a field.
		/// </summary>
		public SearchDescriptor<T> TrackScores(bool trackscores = true) => Assign(a => a.TrackScores = trackscores);

		/// <summary>
		/// The Profile API provides detailed timing information about the execution of individual components in a query.
		/// It gives the user insight into how queries are executed at a low level so that the user can understand
		/// why certain queries are slow, and take steps to improve their slow queries.
		/// </summary>
		public SearchDescriptor<T> Profile(bool profile = true) => Assign(a => a.Profile = profile);

		/// <summary>
		/// Allows to filter out documents based on a minimum score:
		/// </summary>
		public SearchDescriptor<T> MinScore(double minScore) => Assign(a => a.MinScore = minScore);

		/// <summary>
		/// The maximum number of documents to collect for each shard, upon reaching which the query execution will terminate early.
		/// If set, the response will have a boolean field terminated_early to indicate whether the query execution has actually terminated_early.
		/// </summary>
		public SearchDescriptor<T> TerminateAfter(long terminateAfter) => Assign(a => a.TerminateAfter = terminateAfter);

		/// <summary>
		/// <para>
		/// Controls a preference of which shard replicas to execute the search request on.
		/// By default, the operation is randomized between the each shard replicas.
		/// </para>
		/// <para>
		/// The operation will go and be executed only on the primary shards.
		/// </para>
		/// </summary>
		public SearchDescriptor<T> ExecuteOnPrimary() => this.Preference("_primary");

		/// <summary>
		/// <para>
		/// Controls a preference of which shard replicas to execute the search request on.
		/// By default, the operation is randomized between the each shard replicas.
		/// </para>
		/// <para>
		/// The operation will go and be executed on the primary shard, and if not available (failover),
		/// will execute on other shards.
		/// </para>
		/// </summary>
		public SearchDescriptor<T> ExecuteOnPrimaryFirst() => this.Preference("_primary_first");

		/// <summary>
		/// <para>
		/// Controls a preference of which shard replicas to execute the search request on.
		/// By default, the operation is randomized between the each shard replicas.
		/// </para>
		/// <para>
		/// The operation will prefer to be executed on a local allocated shard is possible.
		/// </para>
		/// </summary>
		public SearchDescriptor<T> ExecuteOnLocalShard() => this.Preference("_local");

		/// <summary>
		/// <para>
		/// Controls a preference of which shard replicas to execute the search request on.
		/// By default, the operation is randomized between the each shard replicas.
		/// </para>
		/// <para>
		/// Restricts the search to execute only on a node with the provided node id
		/// </para>
		/// </summary>
		public SearchDescriptor<T> ExecuteOnNode(string node) => this.Preference($"_only_node:{node}");

		/// <summary>
		/// <para>
		/// Controls a preference of which shard replicas to execute the search request on.
		/// By default, the operation is randomized between the each shard replicas.
		/// </para>
		/// <para>
		/// Prefers execution on the node with the provided node id if applicable.
		/// </para>
		/// </summary>
		public SearchDescriptor<T> ExecuteOnPreferredNode(string node) => this.Preference(node.IsNullOrEmpty() ? null : $"_prefer_node:{node}");

		/// <summary>
		/// Allows to configure different boost level per index when searching across
		/// more than one indices. This is very handy when hits coming from one index
		/// matter more than hits coming from another index (think social graph where each user has an index).
		/// </summary>
		public SearchDescriptor<T> IndicesBoost(Func<FluentDictionary<IndexName, double>, FluentDictionary<IndexName, double>> boost) =>
			Assign(a => a.IndicesBoost = boost?.Invoke(new FluentDictionary<IndexName, double>()));

		/// <summary>
		/// Allows to selectively load specific fields for each document
		/// represented by a search hit. Defaults to load the internal _source field.
		/// </summary>
		public SearchDescriptor<T> StoredFields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.StoredFields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public SearchDescriptor<T> StoredFields(Fields fields) => Assign(a => a.StoredFields = fields);

		public SearchDescriptor<T> ScriptFields(Func<ScriptFieldsDescriptor, IPromise<IScriptFields>> selector) =>
			Assign(a => a.ScriptFields = selector?.Invoke(new ScriptFieldsDescriptor())?.Value);

		///<summary>
		///A comma-separated list of fields to return as the field data representation of a field for each hit
		///</summary>
		public SearchDescriptor<T> Sort(Func<SortDescriptor<T>, IPromise<IList<ISort>>> selector) => Assign(a => a.Sort = selector?.Invoke(new SortDescriptor<T>())?.Value);

		///<summary>
		/// Sort values that can be used to start returning results "after" any document in the result list.
		///</summary>
		public SearchDescriptor<T> SearchAfter(IList<object> searchAfter) => Assign(a => a.SearchAfter = searchAfter);

		///<summary>
		/// Sort values that can be used to start returning results "after" any document in the result list.
		///</summary>
		public SearchDescriptor<T> SearchAfter(params object[] searchAfter) => Assign(a => a.SearchAfter = searchAfter);

		///<summary>
		/// The suggest feature suggests similar looking terms based on a provided text by using a suggester
		///</summary>
		public SearchDescriptor<T> Suggest(Func<SuggestContainerDescriptor<T>, IPromise<ISuggestContainer>> selector) =>
			Assign(a => a.Suggest = selector?.Invoke(new SuggestContainerDescriptor<T>())?.Value);

		/// <summary>
		/// Describe the query to perform using a query descriptor lambda
		/// </summary>
		public SearchDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> query) =>
			Assign(a => a.Query = query?.Invoke(new QueryContainerDescriptor<T>()));

		/// <summary>
		/// For scroll queries that return a lot of documents it is possible to split the scroll in multiple slices which can be consumed independently
		/// </summary>
		public SearchDescriptor<T> Slice(Func<SlicedScrollDescriptor<T>, ISlicedScroll> selector) =>
			Assign(a => a.Slice = selector?.Invoke(new SlicedScrollDescriptor<T>()));

		/// <summary>
		/// Shortcut to default to a match all query
		/// </summary>
		public SearchDescriptor<T> MatchAll(Func<MatchAllQueryDescriptor, IMatchAllQuery> selector = null) => this.Query(q => q.MatchAll(selector));

		/// <summary>
		/// Filter search using a filter descriptor lambda
		/// </summary>
		public SearchDescriptor<T> PostFilter(Func<QueryContainerDescriptor<T>, QueryContainer> filter) =>
			Assign(a => a.PostFilter = filter?.Invoke(new QueryContainerDescriptor<T>()));

		/// <summary>
		/// Allow to highlight search results on one or more fields. The implementation uses the either lucene fast-vector-highlighter or highlighter.
		/// </summary>
		public SearchDescriptor<T> Highlight(Func<HighlightDescriptor<T>, IHighlight> highlightSelector) =>
			Assign(a => a.Highlight = highlightSelector?.Invoke(new HighlightDescriptor<T>()));

		/// <summary>
		/// Allows to collapse search results based on field values.
		/// The collapsing is done by selecting only the top sorted document per collapse key.
		/// For instance the query below retrieves the best tweet for each user and sorts them by number of likes.
		/// <para>
		/// NOTE: The collapsing is applied to the top hits only and does not affect aggregations.
		/// </para>
		/// </summary>
		public SearchDescriptor<T> Collapse(Func<FieldCollapseDescriptor<T>, IFieldCollapse> collapseSelector) =>
			Assign(a => a.Collapse = collapseSelector?.Invoke(new FieldCollapseDescriptor<T>()));

		/// <summary>
		/// Allows you to specify one or more queries to use for rescoring
		/// </summary>
		public SearchDescriptor<T> Rescore(Func<RescoringDescriptor<T>, IPromise<IList<IRescore>>> rescoreSelector) =>
			Assign(a => a.Rescore = rescoreSelector?.Invoke(new RescoringDescriptor<T>()).Value);

		/// <summary>
		/// Specify the concrete types to deserialize to when returning covariant search results
		/// </summary>
		public SearchDescriptor<T> ConcreteTypeSelector(Func<dynamic, Hit<dynamic>, Type> typeSelector) =>
			Assign(a => a.TypeSelector = typeSelector);
	}
}
