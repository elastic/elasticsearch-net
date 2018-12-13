using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Utf8Json;

namespace Nest
{
	[ReadAs(typeof(SearchRequest))]
	public partial interface ISearchRequest : ICovariantSearchRequest
	{
		[DataMember(Name = "aggs")]
		AggregationDictionary Aggregations { get; set; }

		[DataMember(Name = "collapse")]
		IFieldCollapse Collapse { get; set; }

		[DataMember(Name = "explain")]
		bool? Explain { get; set; }

		[DataMember(Name = "from")]
		int? From { get; set; }

		[DataMember(Name = "highlight")]
		IHighlight Highlight { get; set; }

		[DataMember(Name = "indices_boost")]
		//[JsonFormatter(typeof(IndicesBoostFormatter))]
		IDictionary<IndexName, double> IndicesBoost { get; set; }

		[DataMember(Name = "min_score")]
		double? MinScore { get; set; }

		[DataMember(Name = "post_filter")]
		QueryContainer PostFilter { get; set; }

		[DataMember(Name = "profile")]
		bool? Profile { get; set; }

		[DataMember(Name = "query")]
		QueryContainer Query { get; set; }

		[DataMember(Name = "rescore")]
		IList<IRescore> Rescore { get; set; }

		[DataMember(Name = "script_fields")]
		IScriptFields ScriptFields { get; set; }

		[DataMember(Name = "search_after")]
		IList<object> SearchAfter { get; set; }

		[DataMember(Name = "size")]
		int? Size { get; set; }

		[DataMember(Name = "slice")]
		ISlicedScroll Slice { get; set; }

		[DataMember(Name = "sort")]
		IList<ISort> Sort { get; set; }

		[DataMember(Name = "_source")]
		Union<bool, ISourceFilter> Source { get; set; }

		[DataMember(Name = "suggest")]
		ISuggestContainer Suggest { get; set; }

		[DataMember(Name = "terminate_after")]
		long? TerminateAfter { get; set; }

		[DataMember(Name = "timeout")]
		string Timeout { get; set; }

		[DataMember(Name = "track_scores")]
		bool? TrackScores { get; set; }

		[DataMember(Name = "version")]
		bool? Version { get; set; }
	}

	[ReadAs(typeof(SearchRequest<>))]
	[InterfaceDataContract]
	public partial interface ISearchRequest<T> : ISearchRequest { }

	[DataContract]
	public partial class SearchRequest
	{
		public Fields StoredFields { get; set; }
		public Fields DocValueFields { get; set; }
		public AggregationDictionary Aggregations { get; set; }
		public IFieldCollapse Collapse { get; set; }
		public bool? Explain { get; set; }
		public int? From { get; set; }
		public IHighlight Highlight { get; set; }
		public IDictionary<IndexName, double> IndicesBoost { get; set; }
		public double? MinScore { get; set; }
		public QueryContainer PostFilter { get; set; }
		public bool? Profile { get; set; }
		public QueryContainer Query { get; set; }
		public IList<IRescore> Rescore { get; set; }
		public IScriptFields ScriptFields { get; set; }
		public IList<object> SearchAfter { get; set; }
		public int? Size { get; set; }
		public ISlicedScroll Slice { get; set; }
		public IList<ISort> Sort { get; set; }
		public Union<bool, ISourceFilter> Source { get; set; }
		public ISuggestContainer Suggest { get; set; }
		public long? TerminateAfter { get; set; }

		public string Timeout { get; set; }
		public bool? TrackScores { get; set; }

		public Func<dynamic, Hit<dynamic>, Type> TypeSelector { get; set; }
		public bool? Version { get; set; }

		protected override HttpMethod HttpMethod =>
			RequestState.RequestParameters?.ContainsQueryString("source") == true || RequestState.RequestParameters?.ContainsQueryString("q") == true
				? HttpMethod.GET
				: HttpMethod.POST;

		private Type _clrType { get; set; }
		Type ICovariantSearchRequest.ClrType => _clrType;

		protected sealed override void Initialize() => TypedKeys = true;
	}

	[DataContract]
	public partial class SearchRequest<T> : ISearchRequest<T>
	{
		Type ICovariantSearchRequest.ClrType => typeof(T);
	}

	/// <summary>
	/// A descriptor which describes a search operation for _search and _msearch
	/// </summary>
	[DataContract]
	public partial class SearchDescriptor<T> where T : class
	{
		protected override HttpMethod HttpMethod =>
			RequestState.RequestParameters?.ContainsQueryString("source") == true || RequestState.RequestParameters?.ContainsQueryString("q") == true
				? HttpMethod.GET
				: HttpMethod.POST;

		AggregationDictionary ISearchRequest.Aggregations { get; set; }
		Type ICovariantSearchRequest.ClrType => typeof(T);
		IFieldCollapse ISearchRequest.Collapse { get; set; }
		Fields ISearchRequest.DocValueFields { get; set; }
		bool? ISearchRequest.Explain { get; set; }
		int? ISearchRequest.From { get; set; }
		IHighlight ISearchRequest.Highlight { get; set; }

		IDictionary<IndexName, double> ISearchRequest.IndicesBoost { get; set; }
		double? ISearchRequest.MinScore { get; set; }
		QueryContainer ISearchRequest.PostFilter { get; set; }
		bool? ISearchRequest.Profile { get; set; }
		QueryContainer ISearchRequest.Query { get; set; }
		IList<IRescore> ISearchRequest.Rescore { get; set; }
		IScriptFields ISearchRequest.ScriptFields { get; set; }
		IList<object> ISearchRequest.SearchAfter { get; set; }
		int? ISearchRequest.Size { get; set; }
		ISlicedScroll ISearchRequest.Slice { get; set; }
		IList<ISort> ISearchRequest.Sort { get; set; }
		Union<bool, ISourceFilter> ISearchRequest.Source { get; set; }
		Fields ISearchRequest.StoredFields { get; set; }
		ISuggestContainer ISearchRequest.Suggest { get; set; }
		long? ISearchRequest.TerminateAfter { get; set; }

		string ISearchRequest.Timeout { get; set; }
		bool? ISearchRequest.TrackScores { get; set; }
		bool? ISearchRequest.Version { get; set; }

		protected sealed override void Initialize() => TypedKeys();

		public SearchDescriptor<T> Aggregations(Func<AggregationContainerDescriptor<T>, IAggregationContainer> aggregationsSelector) =>
			Assign(a => a.Aggregations = aggregationsSelector(new AggregationContainerDescriptor<T>())?.Aggregations);

		public SearchDescriptor<T> Aggregations(AggregationDictionary aggregations) =>
			Assign(a => a.Aggregations = aggregations);

		public SearchDescriptor<T> Source(bool enabled = true) => Assign(a => a.Source = enabled);

		public SearchDescriptor<T> Source(Func<SourceFilterDescriptor<T>, ISourceFilter> selector) =>
			Assign(a => a.Source = new Union<bool, ISourceFilter>(selector?.Invoke(new SourceFilterDescriptor<T>())));

		/// <summary> The number of hits to return. Defaults to 10. </summary>
		public SearchDescriptor<T> Size(int? size) => Assign(a => a.Size = size);

		/// <summary>
		/// The number of hits to return. Alias for <see cref="Size" />. Defaults to 10.
		/// </summary>
		public SearchDescriptor<T> Take(int? take) => Size(take);

		/// <summary>
		/// The starting from index of the hits to return. Defaults to 0.
		/// </summary>
		public SearchDescriptor<T> From(int? from) => Assign(a => a.From = from);

		/// <summary>
		/// The starting from index of the hits to return. Alias for <see cref="From" />. Defaults to 0.
		/// </summary>
		public SearchDescriptor<T> Skip(int? skip) => From(skip);

		/// <summary>
		/// A search timeout, bounding the search request to be executed within the
		/// specified time value and bail with the hits accumulated up
		/// to that point when expired. Defaults to no timeout.
		/// </summary>
		public SearchDescriptor<T> Timeout(string timeout) => Assign(a => a.Timeout = timeout);

		/// <summary>
		/// Enables explanation for each hit on how its score was computed.
		/// (Use .DocumentsWithMetadata on the return results)
		/// </summary>
		public SearchDescriptor<T> Explain(bool? explain = true) => Assign(a => a.Explain = explain);

		/// <summary>
		/// Returns a version for each search hit. (Use .DocumentsWithMetadata on the return results)
		/// </summary>
		public SearchDescriptor<T> Version(bool? version = true) => Assign(a => a.Version = version);

		/// <summary>
		/// Make sure we keep calculating score even if we are sorting on a field.
		/// </summary>
		public SearchDescriptor<T> TrackScores(bool? trackscores = true) => Assign(a => a.TrackScores = trackscores);

		/// <summary>
		/// The Profile API provides detailed timing information about the execution of individual components in a query.
		/// It gives the user insight into how queries are executed at a low level so that the user can understand
		/// why certain queries are slow, and take steps to improve their slow queries.
		/// </summary>
		public SearchDescriptor<T> Profile(bool? profile = true) => Assign(a => a.Profile = profile);

		/// <summary>
		/// Allows to filter out documents based on a minimum score:
		/// </summary>
		public SearchDescriptor<T> MinScore(double? minScore) => Assign(a => a.MinScore = minScore);

		/// <summary>
		/// The maximum number of documents to collect for each shard, upon reaching which the query execution will terminate
		/// early.
		/// If set, the response will have a boolean field terminated_early to indicate whether the query execution has actually
		/// terminated_early.
		/// </summary>
		public SearchDescriptor<T> TerminateAfter(long? terminateAfter) => Assign(a => a.TerminateAfter = terminateAfter);

		/// <summary>
		/// <para>
		/// Controls a preference of which shard replicas to execute the search request on.
		/// By default, the operation is randomized between the each shard replicas.
		/// </para>
		/// <para>
		/// The operation will go and be executed only on the primary shards.
		/// </para>
		/// </summary>
		public SearchDescriptor<T> ExecuteOnPrimary() => Preference("_primary");

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
		public SearchDescriptor<T> ExecuteOnPrimaryFirst() => Preference("_primary_first");

		/// <summary>
		/// <para>
		/// Controls a preference of which shard replicas to execute the search request on.
		/// By default, the operation is randomized between the each shard replicas.
		/// </para>
		/// <para>
		/// The operation will prefer to be executed on a local allocated shard is possible.
		/// </para>
		/// </summary>
		public SearchDescriptor<T> ExecuteOnLocalShard() => Preference("_local");

		/// <summary>
		/// <para>
		/// Controls a preference of which shard replicas to execute the search request on.
		/// By default, the operation is randomized between the each shard replicas.
		/// </para>
		/// <para>
		/// Restricts the search to execute only on a node with the provided node id
		/// </para>
		/// </summary>
		public SearchDescriptor<T> ExecuteOnNode(string node) => Preference($"_only_node:{node}");

		/// <summary>
		/// <para>
		/// Controls a preference of which shard replicas to execute the search request on.
		/// By default, the operation is randomized between the each shard replicas.
		/// </para>
		/// <para>
		/// Prefers execution on the node with the provided node id if applicable.
		/// </para>
		/// </summary>
		public SearchDescriptor<T> ExecuteOnPreferredNode(string node) => Preference(node.IsNullOrEmpty() ? null : $"_prefer_node:{node}");

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

		public SearchDescriptor<T> DocValueFields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.DocValueFields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public SearchDescriptor<T> DocValueFields(Fields fields) => Assign(a => a.DocValueFields = fields);

		/// <summary>
		/// A comma-separated list of fields to return as the field data representation of a field for each hit
		/// </summary>
		public SearchDescriptor<T> Sort(Func<SortDescriptor<T>, IPromise<IList<ISort>>> selector) =>
			Assign(a => a.Sort = selector?.Invoke(new SortDescriptor<T>())?.Value);

		/// <summary>
		///  Sort values that can be used to start returning results "after" any document in the result list.
		/// </summary>
		public SearchDescriptor<T> SearchAfter(IList<object> searchAfter) => Assign(a => a.SearchAfter = searchAfter);

		/// <summary>
		///  Sort values that can be used to start returning results "after" any document in the result list.
		/// </summary>
		public SearchDescriptor<T> SearchAfter(params object[] searchAfter) => Assign(a => a.SearchAfter = searchAfter);

		/// <summary>
		///  The suggest feature suggests similar looking terms based on a provided text by using a suggester
		/// </summary>
		public SearchDescriptor<T> Suggest(Func<SuggestContainerDescriptor<T>, IPromise<ISuggestContainer>> selector) =>
			Assign(a => a.Suggest = selector?.Invoke(new SuggestContainerDescriptor<T>())?.Value);

		/// <summary>
		/// Describe the query to perform using a query descriptor lambda
		/// </summary>
		public SearchDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> query) =>
			Assign(a => a.Query = query?.Invoke(new QueryContainerDescriptor<T>()));

		/// <summary>
		/// For scroll queries that return a lot of documents it is possible to split the scroll in multiple slices which can be
		/// consumed independently
		/// </summary>
		public SearchDescriptor<T> Slice(Func<SlicedScrollDescriptor<T>, ISlicedScroll> selector) =>
			Assign(a => a.Slice = selector?.Invoke(new SlicedScrollDescriptor<T>()));

		/// <summary>
		/// Shortcut to default to a match all query
		/// </summary>
		public SearchDescriptor<T> MatchAll(Func<MatchAllQueryDescriptor, IMatchAllQuery> selector = null) => Query(q => q.MatchAll(selector));

		/// <summary>
		/// Filter search using a filter descriptor lambda
		/// </summary>
		public SearchDescriptor<T> PostFilter(Func<QueryContainerDescriptor<T>, QueryContainer> filter) =>
			Assign(a => a.PostFilter = filter?.Invoke(new QueryContainerDescriptor<T>()));

		/// <summary>
		/// Allow to highlight search results on one or more fields. The implementation uses the either lucene
		/// fast-vector-highlighter or highlighter.
		/// </summary>
		public SearchDescriptor<T> Highlight(Func<HighlightDescriptor<T>, IHighlight> highlightSelector) =>
			Assign(a => a.Highlight = highlightSelector?.Invoke(new HighlightDescriptor<T>()));

		/// <summary>
		/// Allows to collapse search results based on field values.
		/// The collapsing is done by selecting only the top sorted document per collapse key.
		/// For instance the query below retrieves the best tweet for each user and sorts them by number of likes.
		/// <para>
		/// NOTE: The collapsing is applied to the top hits only and does not affect aggregations.
		/// You can only collapse to a depth of 2.
		/// </para>
		/// </summary>
		public SearchDescriptor<T> Collapse(Func<FieldCollapseDescriptor<T>, IFieldCollapse> collapseSelector) =>
			Assign(a => a.Collapse = collapseSelector?.Invoke(new FieldCollapseDescriptor<T>()));

		/// <summary>
		/// Allows you to specify one or more queries to use for rescoring
		/// </summary>
		public SearchDescriptor<T> Rescore(Func<RescoringDescriptor<T>, IPromise<IList<IRescore>>> rescoreSelector) =>
			Assign(a => a.Rescore = rescoreSelector?.Invoke(new RescoringDescriptor<T>()).Value);
	}
}
