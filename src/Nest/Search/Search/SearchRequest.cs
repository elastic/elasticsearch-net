// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

 using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
 using Elastic.Transport;
 using Elasticsearch.Net;
using Nest.Utf8Json;

namespace Nest
{
	[ReadAs(typeof(SearchRequest))]
	[MapsApi("search.json")]
	public partial interface ISearchRequest : ITypedSearchRequest
	{
		/// <summary>
		/// Specifies the aggregations to perform
		/// </summary>
		[DataMember(Name = "aggs")]
		AggregationDictionary Aggregations { get; set; }

		/// <summary>
		/// Allows to collapse search results based on field values.
		/// The collapsing is done by selecting only the top sorted document per collapse key.
		/// For instance the query below retrieves the best tweet for each user and sorts them by number of likes.
		/// <para>
		/// NOTE: The collapsing is applied to the top hits only and does not affect aggregations.
		/// You can only collapse to a depth of 2.
		/// </para>
		/// </summary>
		[DataMember(Name = "collapse")]
		IFieldCollapse Collapse { get; set; }

		/// <summary>
		/// Enables explanation for each hit on how its score was computed
		/// </summary>
		[DataMember(Name = "explain")]
		bool? Explain { get; set; }

		/// <summary>
		/// The starting from index of the hits to return. Defaults to 0.
		/// </summary>
		[DataMember(Name = "from")]
		int? From { get; set; }

		/// <summary>
		/// Allow to highlight search results on one or more fields. The implementation uses the either lucene
		/// fast-vector-highlighter or highlighter.
		/// </summary>
		[DataMember(Name = "highlight")]
		IHighlight Highlight { get; set; }

		/// <summary>
		/// Allows to configure different boost level per index when searching across
		/// more than one indices. This is very handy when hits coming from one index
		/// matter more than hits coming from another index (think social graph where each user has an index).
		/// </summary>
		[DataMember(Name = "indices_boost")]
		[JsonFormatter(typeof(IndicesBoostFormatter))]
		IDictionary<IndexName, double> IndicesBoost { get; set; }

		/// <summary>
		/// Allows to filter out documents based on a minimum score
		/// </summary>
		[DataMember(Name = "min_score")]
		double? MinScore { get; set; }

		/// <summary>
		/// Specify a query to apply to the search hits at the very end of a search request,
		/// after aggregations have already been calculated. Useful when both search hits and aggregations
		/// will be returned in the response, and a filter should only be applied to the search hits.
		/// </summary>
		[DataMember(Name = "post_filter")]
		QueryContainer PostFilter { get; set; }

		/// <summary>
		/// The Profile API provides detailed timing information about the execution of individual components in a query.
		/// It gives the user insight into how queries are executed at a low level so that the user can understand
		/// why certain queries are slow, and take steps to improve their slow queries.
		/// </summary>
		[DataMember(Name = "profile")]
		bool? Profile { get; set; }

		/// <summary>
		/// Specify the search query to perform
		/// </summary>
		[DataMember(Name = "query")]
		QueryContainer Query { get; set; }

		/// <summary>
		/// Specify one or more queries to use for rescoring
		/// </summary>
		[DataMember(Name = "rescore")]
		IList<IRescore> Rescore { get; set; }

		/// <summary>
		/// Allows to return a script evaluation (based on different fields) for each hit
		/// </summary>
		[DataMember(Name = "script_fields")]
		IScriptFields ScriptFields { get; set; }

		/// <summary>
		///  Sort values that can be used to start returning results "after" any document in the result list.
		/// </summary>
		[DataMember(Name = "search_after")]
		IList<object> SearchAfter { get; set; }

		/// <summary> The number of hits to return. Defaults to 10. </summary>
		[DataMember(Name = "size")]
		int? Size { get; set; }

		/// <summary>
		/// For scroll queries that return a lot of documents it is possible to split the scroll in multiple slices which can be
		/// consumed independently
		/// </summary>
		[DataMember(Name = "slice")]
		ISlicedScroll Slice { get; set; }

		/// <summary>
		/// Specifies how to sort the search hits
		/// </summary>
		[DataMember(Name = "sort")]
		IList<ISort> Sort { get; set; }

		/// <summary>
		/// Specify how the _source field is returned for each search hit.
		/// <para>When <c>true</c>, _source retrieval is enabled (default)</para>
		/// <para>When <c>false</c>, _source retrieval is disabled, and no _source will be returned for each hit</para>
		/// <para>When <see cref="ISourceFilter"/> is specified, fields to include/exclude can be controlled</para>
		/// </summary>
		[DataMember(Name = "_source")]
		Union<bool, ISourceFilter> Source { get; set; }

		/// <summary>
		///  The suggest feature suggests similar looking terms based on a provided text by using a suggester
		/// </summary>
		[DataMember(Name = "suggest")]
		ISuggestContainer Suggest { get; set; }

		/// <summary>
		/// The maximum number of documents to collect for each shard, upon reaching which the query execution will terminate
		/// early.
		/// If set, the response will have a boolean field terminated_early to indicate whether the query execution has actually
		/// terminated_early.
		/// </summary>
		[DataMember(Name = "terminate_after")]
		long? TerminateAfter { get; set; }

		/// <summary>
		/// A search timeout, bounding the search request to be executed within the
		/// specified time value and bail with the hits accumulated up
		/// to that point, when expired. Defaults to no timeout.
		/// </summary>
		[DataMember(Name = "timeout")]
		string Timeout { get; set; }

		/// <summary>
		/// Make sure we keep calculating score even if we are sorting on a field.
		/// </summary>
		[DataMember(Name = "track_scores")]
		bool? TrackScores { get; set; }

		/// <summary>
		/// Return a version for each search hit
		/// </summary>
		[DataMember(Name = "version")]
		bool? Version { get; set; }

		[DataMember(Name = "pit")]
		IPointInTime PointInTime { get; set; }
	}

	[ReadAs(typeof(SearchRequest<>))]
	[InterfaceDataContract]
	// ReSharper disable once UnusedTypeParameter
	public partial interface ISearchRequest<TInferDocument> : ISearchRequest { }

	[DataContract]
	public partial class SearchRequest
	{
		/// <inheritdoc />
		public AggregationDictionary Aggregations { get; set; }
		/// <inheritdoc />
		public IFieldCollapse Collapse { get; set; }
		/// <inheritdoc />
		public Fields DocValueFields { get; set; }
		/// <inheritdoc />
		public bool? Explain { get; set; }
		/// <inheritdoc />
		public int? From { get; set; }
		/// <inheritdoc />
		public IHighlight Highlight { get; set; }
		/// <inheritdoc />
		[JsonFormatter(typeof(IndicesBoostFormatter))]
		public IDictionary<IndexName, double> IndicesBoost { get; set; }
		/// <inheritdoc />
		public double? MinScore { get; set; }
		/// <inheritdoc />
		public QueryContainer PostFilter { get; set; }
		/// <inheritdoc />
		public bool? Profile { get; set; }
		/// <inheritdoc />
		public QueryContainer Query { get; set; }
		/// <inheritdoc />
		public IList<IRescore> Rescore { get; set; }
		/// <inheritdoc />
		public IScriptFields ScriptFields { get; set; }
		/// <inheritdoc />
		public IList<object> SearchAfter { get; set; }
		/// <inheritdoc />
		public int? Size { get; set; }
		/// <inheritdoc />
		public ISlicedScroll Slice { get; set; }
		/// <inheritdoc />
		public IList<ISort> Sort { get; set; }
		/// <inheritdoc />
		public Union<bool, ISourceFilter> Source { get; set; }
		/// <inheritdoc />
		public Fields StoredFields { get; set; }
		/// <inheritdoc />
		public ISuggestContainer Suggest { get; set; }
		/// <inheritdoc />
		public long? TerminateAfter { get; set; }
		/// <inheritdoc />
		public string Timeout { get; set; }
		/// <inheritdoc />
		public bool? TrackScores { get; set; }
		/// <inheritdoc />
		public bool? TrackTotalHits { get; set; }
		/// <inheritdoc />
		public bool? Version { get; set; }
		/// <inheritdoc />
		public IPointInTime PointInTime { get; set; }

		protected override HttpMethod HttpMethod =>
			RequestState.RequestParameters?.ContainsQueryString("source") == true
				? HttpMethod.GET
				: HttpMethod.POST;

		Type ITypedSearchRequest.ClrType => null;

		protected sealed override void RequestDefaults(SearchRequestParameters parameters) => TypedKeys = true;

		protected override string ResolveUrl(RouteValues routeValues, IConnectionSettingsValues settings)
		{
			if (Self.PointInTime is object && !string.IsNullOrEmpty(Self.PointInTime.Id) && routeValues.ContainsKey("index"))
			{
				routeValues.Remove("index");
			}

			return base.ResolveUrl(routeValues, settings);
		}
	}

	[DataContract]
	public partial class SearchRequest<TInferDocument> : ISearchRequest<TInferDocument>
	{
		Type ITypedSearchRequest.ClrType => typeof(TInferDocument);
	}

	/// <summary>
	/// A descriptor which describes a search operation for _search and _msearch
	/// </summary>
	[DataContract]
	public partial class SearchDescriptor<TInferDocument> where TInferDocument : class
	{
		protected override HttpMethod HttpMethod =>
			RequestState.RequestParameters?.ContainsQueryString("source") == true
				? HttpMethod.GET
				: HttpMethod.POST;

		AggregationDictionary ISearchRequest.Aggregations { get; set; }
		Type ITypedSearchRequest.ClrType => typeof(TInferDocument);
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
		bool? ISearchRequest.TrackTotalHits { get; set; }
		bool? ISearchRequest.Version { get; set; }
		IPointInTime ISearchRequest.PointInTime { get; set; }

		protected sealed override void RequestDefaults(SearchRequestParameters parameters) => TypedKeys();

		/// <inheritdoc cref="ISearchRequest.Aggregations" />
		public SearchDescriptor<TInferDocument> Aggregations(
			Func<AggregationContainerDescriptor<TInferDocument>, IAggregationContainer> aggregationsSelector
		) =>
			Assign(aggregationsSelector(new AggregationContainerDescriptor<TInferDocument>())?.Aggregations, (a, v) => a.Aggregations = v);

		/// <inheritdoc cref="ISearchRequest.Aggregations" />
		public SearchDescriptor<TInferDocument> Aggregations(AggregationDictionary aggregations) =>
			Assign(aggregations, (a, v) => a.Aggregations = v);

		/// <inheritdoc cref="ISearchRequest.Source" />
		public SearchDescriptor<TInferDocument> Source(bool enabled = true) => Assign(enabled, (a, v) => a.Source = v);

		/// <inheritdoc cref="ISearchRequest.Source" />
		public SearchDescriptor<TInferDocument> Source(Func<SourceFilterDescriptor<TInferDocument>, ISourceFilter> selector) =>
			Assign(selector?.Invoke(new SourceFilterDescriptor<TInferDocument>()), (a, v) => a.Source = v is null
				? null
				: new Union<bool,ISourceFilter>(v));

		/// <inheritdoc cref="ISearchRequest.Size" />
		public SearchDescriptor<TInferDocument> Size(int? size) => Assign(size, (a, v) => a.Size = v);

		/// <inheritdoc cref="ISearchRequest.Size" />
		public SearchDescriptor<TInferDocument> Take(int? take) => Size(take);

		/// <inheritdoc cref="ISearchRequest.From" />
		public SearchDescriptor<TInferDocument> From(int? from) => Assign(from, (a, v) => a.From = v);

		/// <inheritdoc cref="ISearchRequest.From" />
		public SearchDescriptor<TInferDocument> Skip(int? skip) => From(skip);

		/// <inheritdoc cref="ISearchRequest.Timeout" />
		public SearchDescriptor<TInferDocument> Timeout(string timeout) => Assign(timeout, (a, v) => a.Timeout = v);

		/// <inheritdoc cref="ISearchRequest.Explain" />
		public SearchDescriptor<TInferDocument> Explain(bool? explain = true) => Assign(explain, (a, v) => a.Explain = v);

		/// <inheritdoc cref="ISearchRequest.Version" />
		public SearchDescriptor<TInferDocument> Version(bool? version = true) => Assign(version, (a, v) => a.Version = v);

		/// <inheritdoc cref="ISearchRequest.TrackScores" />
		public SearchDescriptor<TInferDocument> TrackScores(bool? trackscores = true) => Assign(trackscores, (a, v) => a.TrackScores = v);

		/// <inheritdoc cref="ISearchRequest.Profile" />
		public SearchDescriptor<TInferDocument> Profile(bool? profile = true) => Assign(profile, (a, v) => a.Profile = v);

		/// <inheritdoc cref="ISearchRequest.MinScore" />
		public SearchDescriptor<TInferDocument> MinScore(double? minScore) => Assign(minScore, (a, v) => a.MinScore = v);

		/// <inheritdoc cref="ISearchRequest.TerminateAfter" />
		public SearchDescriptor<TInferDocument> TerminateAfter(long? terminateAfter) => Assign(terminateAfter, (a, v) => a.TerminateAfter = v);

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
		public SearchDescriptor<TInferDocument> ExecuteOnPreferredNode(string node) =>
			Preference(node.IsNullOrEmpty() ? null : $"_prefer_node:{node}");

		/// <inheritdoc cref="ISearchRequest.IndicesBoost" />
		public SearchDescriptor<TInferDocument> IndicesBoost(Func<FluentDictionary<IndexName, double>, FluentDictionary<IndexName, double>> boost) =>
			Assign(boost, (a, v) => a.IndicesBoost = v?.Invoke(new FluentDictionary<IndexName, double>()));

		/// <inheritdoc cref="ISearchRequest.StoredFields" />
		public SearchDescriptor<TInferDocument> StoredFields(Func<FieldsDescriptor<TInferDocument>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.StoredFields = v?.Invoke(new FieldsDescriptor<TInferDocument>())?.Value);

		/// <inheritdoc cref="ISearchRequest.StoredFields" />
		public SearchDescriptor<TInferDocument> StoredFields(Fields fields) => Assign(fields, (a, v) => a.StoredFields = v);

		/// <inheritdoc cref="ISearchRequest.ScriptFields" />
		public SearchDescriptor<TInferDocument> ScriptFields(Func<ScriptFieldsDescriptor, IPromise<IScriptFields>> selector) =>
			Assign(selector, (a, v) => a.ScriptFields = v?.Invoke(new ScriptFieldsDescriptor())?.Value);

		/// <inheritdoc cref="ISearchRequest.ScriptFields" />
		public SearchDescriptor<TInferDocument> DocValueFields(Func<FieldsDescriptor<TInferDocument>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.DocValueFields = v?.Invoke(new FieldsDescriptor<TInferDocument>())?.Value);

		/// <inheritdoc cref="ISearchRequest.DocValueFields" />
		public SearchDescriptor<TInferDocument> DocValueFields(Fields fields) => Assign(fields, (a, v) => a.DocValueFields = v);

		/// <inheritdoc cref="ISearchRequest.Sort" />
		public SearchDescriptor<TInferDocument> Sort(Func<SortDescriptor<TInferDocument>, IPromise<IList<ISort>>> selector) =>
			Assign(selector, (a, v) => a.Sort = v?.Invoke(new SortDescriptor<TInferDocument>())?.Value);

		/// <inheritdoc cref="ISearchRequest.SearchAfter" />
		public SearchDescriptor<TInferDocument> SearchAfter(IList<object> searchAfter) => Assign(searchAfter, (a, v) => a.SearchAfter = v);

		/// <inheritdoc cref="ISearchRequest.SearchAfter" />
		public SearchDescriptor<TInferDocument> SearchAfter(params object[] searchAfter) => Assign(searchAfter, (a, v) => a.SearchAfter = v);

		/// <inheritdoc cref="ISearchRequest.Suggest" />
		public SearchDescriptor<TInferDocument> Suggest(Func<SuggestContainerDescriptor<TInferDocument>, IPromise<ISuggestContainer>> selector) =>
			Assign(selector, (a, v) => a.Suggest = v?.Invoke(new SuggestContainerDescriptor<TInferDocument>())?.Value);

		/// <inheritdoc cref="ISearchRequest.Query" />
		public SearchDescriptor<TInferDocument> Query(Func<QueryContainerDescriptor<TInferDocument>, QueryContainer> query) =>
			Assign(query, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<TInferDocument>()));

		/// <inheritdoc cref="ISearchRequest.Slice" />
		public SearchDescriptor<TInferDocument> Slice(Func<SlicedScrollDescriptor<TInferDocument>, ISlicedScroll> selector) =>
			Assign(selector, (a, v) => a.Slice = v?.Invoke(new SlicedScrollDescriptor<TInferDocument>()));

		/// <summary>
		/// Shortcut to default to a match all query
		/// </summary>
		public SearchDescriptor<TInferDocument> MatchAll(Func<MatchAllQueryDescriptor, IMatchAllQuery> selector = null) =>
			Query(q => q.MatchAll(selector));

		/// <inheritdoc cref="ISearchRequest.PostFilter" />
		public SearchDescriptor<TInferDocument> PostFilter(Func<QueryContainerDescriptor<TInferDocument>, QueryContainer> filter) =>
			Assign(filter, (a, v) => a.PostFilter = v?.Invoke(new QueryContainerDescriptor<TInferDocument>()));

		/// <inheritdoc cref="ISearchRequest.Highlight" />
		public SearchDescriptor<TInferDocument> Highlight(Func<HighlightDescriptor<TInferDocument>, IHighlight> highlightSelector) =>
			Assign(highlightSelector, (a, v) => a.Highlight = v?.Invoke(new HighlightDescriptor<TInferDocument>()));

		/// <inheritdoc cref="ISearchRequest.Collapse" />
		public SearchDescriptor<TInferDocument> Collapse(Func<FieldCollapseDescriptor<TInferDocument>, IFieldCollapse> collapseSelector) =>
			Assign(collapseSelector, (a, v) => a.Collapse = v?.Invoke(new FieldCollapseDescriptor<TInferDocument>()));

		/// <inheritdoc cref="ISearchRequest.Rescore" />
		public SearchDescriptor<TInferDocument> Rescore(Func<RescoringDescriptor<TInferDocument>, IPromise<IList<IRescore>>> rescoreSelector) =>
			Assign(rescoreSelector, (a, v) => a.Rescore = v?.Invoke(new RescoringDescriptor<TInferDocument>()).Value);

		/// <inheritdoc cref="ISearchRequest.TrackTotalHits" />
		public SearchDescriptor<TInferDocument> TrackTotalHits(bool? trackTotalHits = true) => Assign(trackTotalHits, (a, v) => a.TrackTotalHits = v);

		/// <inheritdoc cref="ISearchRequest.PointInTime" />
		public SearchDescriptor<TInferDocument> PointInTime(string pitId)
		{
			Self.PointInTime = new PointInTime(pitId);
			return this;
		}

		/// <inheritdoc cref="ISearchRequest.PointInTime" />
		public SearchDescriptor<TInferDocument> PointInTime(string pitId, Func<PointInTimeDescriptor, IPointInTime> pit) =>
			Assign(pit, (a, v) => a.PointInTime = v?.Invoke(new PointInTimeDescriptor(pitId)));

		protected override string ResolveUrl(RouteValues routeValues, IConnectionSettingsValues settings)
		{
			if (Self.PointInTime is object && !string.IsNullOrEmpty(Self.PointInTime.Id) && routeValues.ContainsKey("index"))
			{
				routeValues.Remove("index");
			}

			return base.ResolveUrl(routeValues, settings);
		}
	}
}
