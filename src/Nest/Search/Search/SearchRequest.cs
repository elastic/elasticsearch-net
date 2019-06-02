using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net;
using Elasticsearch.Net.Utf8Json;

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
		[JsonFormatter(typeof(IndicesBoostFormatter))]
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
	// ReSharper disable once UnusedTypeParameter
	public partial interface ISearchRequest<TInferDocument> : ISearchRequest { }

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

		[JsonFormatter(typeof(IndicesBoostFormatter))]
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
		public bool? Version { get; set; }

		protected override HttpMethod HttpMethod =>
			RequestState.RequestParameters?.ContainsQueryString("source") == true || RequestState.RequestParameters?.ContainsQueryString("q") == true
				? HttpMethod.GET
				: HttpMethod.POST;

		Type ICovariantSearchRequest.ClrType => null;

		protected sealed override void Initialize() => TypedKeys = true;
	}

	[DataContract]
	public partial class SearchRequest<TInferDocument> : ISearchRequest<TInferDocument>
	{
		Type ICovariantSearchRequest.ClrType => typeof(TInferDocument);
	}

	/// <summary>
	/// A descriptor which describes a search operation for _search and _msearch
	/// </summary>
	[DataContract]
	public partial class SearchDescriptor<TInferDocument> where TInferDocument : class
	{
		protected override HttpMethod HttpMethod =>
			RequestState.RequestParameters?.ContainsQueryString("source") == true || RequestState.RequestParameters?.ContainsQueryString("q") == true
				? HttpMethod.GET
				: HttpMethod.POST;

		AggregationDictionary ISearchRequest.Aggregations { get; set; }
		Type ICovariantSearchRequest.ClrType => typeof(TInferDocument);
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

		public SearchDescriptor<TInferDocument> Aggregations(Func<AggregationContainerDescriptor<TInferDocument>, IAggregationContainer> aggregationsSelector) =>
			Assign(aggregationsSelector(new AggregationContainerDescriptor<TInferDocument>())?.Aggregations, (a, v) => a.Aggregations = v);

		public SearchDescriptor<TInferDocument> Aggregations(AggregationDictionary aggregations) =>
			Assign(aggregations, (a, v) => a.Aggregations = v);

		public SearchDescriptor<TInferDocument> Source(bool enabled = true) => Assign(enabled, (a, v) => a.Source = v);

		public SearchDescriptor<TInferDocument> Source(Func<SourceFilterDescriptor<TInferDocument>, ISourceFilter> selector) =>
			Assign(selector, (a, v) => a.Source = new Union<bool, ISourceFilter>(v?.Invoke(new SourceFilterDescriptor<TInferDocument>())));

		/// <summary> The number of hits to return. Defaults to 10. </summary>
		public SearchDescriptor<TInferDocument> Size(int? size) => Assign(size, (a, v) => a.Size = v);

		/// <summary>
		/// The number of hits to return. Alias for <see cref="Size" />. Defaults to 10.
		/// </summary>
		public SearchDescriptor<TInferDocument> Take(int? take) => Size(take);

		/// <summary>
		/// The starting from index of the hits to return. Defaults to 0.
		/// </summary>
		public SearchDescriptor<TInferDocument> From(int? from) => Assign(from, (a, v) => a.From = v);

		/// <summary>
		/// The starting from index of the hits to return. Alias for <see cref="From" />. Defaults to 0.
		/// </summary>
		public SearchDescriptor<TInferDocument> Skip(int? skip) => From(skip);

		/// <summary>
		/// A search timeout, bounding the search request to be executed within the
		/// specified time value and bail with the hits accumulated up
		/// to that point when expired. Defaults to no timeout.
		/// </summary>
		public SearchDescriptor<TInferDocument> Timeout(string timeout) => Assign(timeout, (a, v) => a.Timeout = v);

		/// <summary>
		/// Enables explanation for each hit on how its score was computed.
		/// (Use .DocumentsWithMetadata on the return results)
		/// </summary>
		public SearchDescriptor<TInferDocument> Explain(bool? explain = true) => Assign(explain, (a, v) => a.Explain = v);

		/// <summary>
		/// Returns a version for each search hit. (Use .DocumentsWithMetadata on the return results)
		/// </summary>
		public SearchDescriptor<TInferDocument> Version(bool? version = true) => Assign(version, (a, v) => a.Version = v);

		/// <summary>
		/// Make sure we keep calculating score even if we are sorting on a field.
		/// </summary>
		public SearchDescriptor<TInferDocument> TrackScores(bool? trackscores = true) => Assign(trackscores, (a, v) => a.TrackScores = v);

		/// <summary>
		/// The Profile API provides detailed timing information about the execution of individual components in a query.
		/// It gives the user insight into how queries are executed at a low level so that the user can understand
		/// why certain queries are slow, and take steps to improve their slow queries.
		/// </summary>
		public SearchDescriptor<TInferDocument> Profile(bool? profile = true) => Assign(profile, (a, v) => a.Profile = v);

		/// <summary>
		/// Allows to filter out documents based on a minimum score:
		/// </summary>
		public SearchDescriptor<TInferDocument> MinScore(double? minScore) => Assign(minScore, (a, v) => a.MinScore = v);

		/// <summary>
		/// The maximum number of documents to collect for each shard, upon reaching which the query execution will terminate
		/// early.
		/// If set, the response will have a boolean field terminated_early to indicate whether the query execution has actually
		/// terminated_early.
		/// </summary>
		public SearchDescriptor<TInferDocument> TerminateAfter(long? terminateAfter) => Assign(terminateAfter, (a, v) => a.TerminateAfter = v);

		/// <summary>
		/// <para>
		/// Controls a preference of which shard replicas to execute the search request on.
		/// By default, the operation is randomized between the each shard replicas.
		/// </para>
		/// <para>
		/// The operation will go and be executed only on the primary shards.
		/// </para>
		/// </summary>
		public SearchDescriptor<TInferDocument> ExecuteOnPrimary() => Preference("_primary");

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
		public SearchDescriptor<TInferDocument> ExecuteOnPrimaryFirst() => Preference("_primary_first");

		/// <summary>
		/// <para>
		/// Controls a preference of which shard replicas to execute the search request on.
		/// By default, the operation is randomized between the each shard replicas.
		/// </para>
		/// <para>
		/// The operation will prefer to be executed on a local allocated shard is possible.
		/// </para>
		/// </summary>
		public SearchDescriptor<TInferDocument> ExecuteOnLocalShard() => Preference("_local");

		/// <summary>
		/// <para>
		/// Controls a preference of which shard replicas to execute the search request on.
		/// By default, the operation is randomized between the each shard replicas.
		/// </para>
		/// <para>
		/// Restricts the search to execute only on a node with the provided node id
		/// </para>
		/// </summary>
		public SearchDescriptor<TInferDocument> ExecuteOnNode(string node) => Preference($"_only_node:{node}");

		/// <summary>
		/// <para>
		/// Controls a preference of which shard replicas to execute the search request on.
		/// By default, the operation is randomized between the each shard replicas.
		/// </para>
		/// <para>
		/// Prefers execution on the node with the provided node id if applicable.
		/// </para>
		/// </summary>
		public SearchDescriptor<TInferDocument> ExecuteOnPreferredNode(string node) => Preference(node.IsNullOrEmpty() ? null : $"_prefer_node:{node}");

		/// <summary>
		/// Allows to configure different boost level per index when searching across
		/// more than one indices. This is very handy when hits coming from one index
		/// matter more than hits coming from another index (think social graph where each user has an index).
		/// </summary>
		public SearchDescriptor<TInferDocument> IndicesBoost(Func<FluentDictionary<IndexName, double>, FluentDictionary<IndexName, double>> boost) =>
			Assign(boost, (a, v) => a.IndicesBoost = v?.Invoke(new FluentDictionary<IndexName, double>()));

		/// <summary>
		/// Allows to selectively load specific fields for each document
		/// represented by a search hit. Defaults to load the internal _source field.
		/// </summary>
		public SearchDescriptor<TInferDocument> StoredFields(Func<FieldsDescriptor<TInferDocument>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.StoredFields = v?.Invoke(new FieldsDescriptor<TInferDocument>())?.Value);

		public SearchDescriptor<TInferDocument> StoredFields(Fields fields) => Assign(fields, (a, v) => a.StoredFields = v);

		public SearchDescriptor<TInferDocument> ScriptFields(Func<ScriptFieldsDescriptor, IPromise<IScriptFields>> selector) =>
			Assign(selector, (a, v) => a.ScriptFields = v?.Invoke(new ScriptFieldsDescriptor())?.Value);

		public SearchDescriptor<TInferDocument> DocValueFields(Func<FieldsDescriptor<TInferDocument>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.DocValueFields = v?.Invoke(new FieldsDescriptor<TInferDocument>())?.Value);

		public SearchDescriptor<TInferDocument> DocValueFields(Fields fields) => Assign(fields, (a, v) => a.DocValueFields = v);

		/// <summary>
		/// A comma-separated list of fields to return as the field data representation of a field for each hit
		/// </summary>
		public SearchDescriptor<TInferDocument> Sort(Func<SortDescriptor<TInferDocument>, IPromise<IList<ISort>>> selector) =>
			Assign(selector, (a, v) => a.Sort = v?.Invoke(new SortDescriptor<TInferDocument>())?.Value);

		/// <summary>
		///  Sort values that can be used to start returning results "after" any document in the result list.
		/// </summary>
		public SearchDescriptor<TInferDocument> SearchAfter(IList<object> searchAfter) => Assign(searchAfter, (a, v) => a.SearchAfter = v);

		/// <summary>
		///  Sort values that can be used to start returning results "after" any document in the result list.
		/// </summary>
		public SearchDescriptor<TInferDocument> SearchAfter(params object[] searchAfter) => Assign(searchAfter, (a, v) => a.SearchAfter = v);

		/// <summary>
		///  The suggest feature suggests similar looking terms based on a provided text by using a suggester
		/// </summary>
		public SearchDescriptor<TInferDocument> Suggest(Func<SuggestContainerDescriptor<TInferDocument>, IPromise<ISuggestContainer>> selector) =>
			Assign(selector, (a, v) => a.Suggest = v?.Invoke(new SuggestContainerDescriptor<TInferDocument>())?.Value);

		/// <summary>
		/// Describe the query to perform using a query descriptor lambda
		/// </summary>
		public SearchDescriptor<TInferDocument> Query(Func<QueryContainerDescriptor<TInferDocument>, QueryContainer> query) =>
			Assign(query, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<TInferDocument>()));

		/// <summary>
		/// For scroll queries that return a lot of documents it is possible to split the scroll in multiple slices which can be
		/// consumed independently
		/// </summary>
		public SearchDescriptor<TInferDocument> Slice(Func<SlicedScrollDescriptor<TInferDocument>, ISlicedScroll> selector) =>
			Assign(selector, (a, v) => a.Slice = v?.Invoke(new SlicedScrollDescriptor<TInferDocument>()));

		/// <summary>
		/// Shortcut to default to a match all query
		/// </summary>
		public SearchDescriptor<TInferDocument> MatchAll(Func<MatchAllQueryDescriptor, IMatchAllQuery> selector = null) => Query(q => q.MatchAll(selector));

		/// <summary>
		/// Filter search using a filter descriptor lambda
		/// </summary>
		public SearchDescriptor<TInferDocument> PostFilter(Func<QueryContainerDescriptor<TInferDocument>, QueryContainer> filter) =>
			Assign(filter, (a, v) => a.PostFilter = v?.Invoke(new QueryContainerDescriptor<TInferDocument>()));

		/// <summary>
		/// Allow to highlight search results on one or more fields. The implementation uses the either lucene
		/// fast-vector-highlighter or highlighter.
		/// </summary>
		public SearchDescriptor<TInferDocument> Highlight(Func<HighlightDescriptor<TInferDocument>, IHighlight> highlightSelector) =>
			Assign(highlightSelector, (a, v) => a.Highlight = v?.Invoke(new HighlightDescriptor<TInferDocument>()));

		/// <summary>
		/// Allows to collapse search results based on field values.
		/// The collapsing is done by selecting only the top sorted document per collapse key.
		/// For instance the query below retrieves the best tweet for each user and sorts them by number of likes.
		/// <para>
		/// NOTE: The collapsing is applied to the top hits only and does not affect aggregations.
		/// You can only collapse to a depth of 2.
		/// </para>
		/// </summary>
		public SearchDescriptor<TInferDocument> Collapse(Func<FieldCollapseDescriptor<TInferDocument>, IFieldCollapse> collapseSelector) =>
			Assign(collapseSelector, (a, v) => a.Collapse = v?.Invoke(new FieldCollapseDescriptor<TInferDocument>()));

		/// <summary>
		/// Allows you to specify one or more queries to use for rescoring
		/// </summary>
		public SearchDescriptor<TInferDocument> Rescore(Func<RescoringDescriptor<TInferDocument>, IPromise<IList<IRescore>>> rescoreSelector) =>
			Assign(rescoreSelector, (a, v) => a.Rescore = v?.Invoke(new RescoringDescriptor<TInferDocument>()).Value);
	}
}
