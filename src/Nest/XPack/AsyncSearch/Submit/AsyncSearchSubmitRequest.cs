using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Elasticsearch.Net.Specification.AsyncSearchApi;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[MapsApi("async_search.submit.json")]
	[ReadAs(typeof(AsyncSearchSubmitRequest))]
	public partial interface IAsyncSearchSubmitRequest : ITypedSearchRequest
	{
		/// <inheritdoc cref="ISearchRequest.Aggregations"/>
		[DataMember(Name = "aggs")]
		AggregationDictionary Aggregations { get; set; }

		/// <inheritdoc cref="ISearchRequest.Collapse"/>
		[DataMember(Name = "collapse")]
		IFieldCollapse Collapse { get; set; }

		/// <inheritdoc cref="ISearchRequest.Explain"/>
		[DataMember(Name = "explain")]
		bool? Explain { get; set; }

		/// <inheritdoc cref="ISearchRequest.From"/>
		[DataMember(Name = "from")]
		int? From { get; set; }

		/// <inheritdoc cref="ISearchRequest.Highlight"/>
		[DataMember(Name = "highlight")]
		IHighlight Highlight { get; set; }

		/// <inheritdoc cref="ISearchRequest.IndicesBoost"/>
		[DataMember(Name = "indices_boost")]
		[JsonFormatter(typeof(IndicesBoostFormatter))]
		IDictionary<IndexName, double> IndicesBoost { get; set; }

		/// <inheritdoc cref="ISearchRequest.MinScore"/>
		[DataMember(Name = "min_score")]
		double? MinScore { get; set; }

		/// <inheritdoc cref="ISearchRequest.PostFilter"/>
		[DataMember(Name = "post_filter")]
		QueryContainer PostFilter { get; set; }

		/// <inheritdoc cref="ISearchRequest.Profile"/>
		[DataMember(Name = "profile")]
		bool? Profile { get; set; }

		/// <inheritdoc cref="ISearchRequest.Query"/>
		[DataMember(Name = "query")]
		QueryContainer Query { get; set; }

		/// <inheritdoc cref="ISearchRequest.Rescore"/>
		[DataMember(Name = "rescore")]
		IList<IRescore> Rescore { get; set; }

		/// <inheritdoc cref="ISearchRequest.ScriptFields"/>
		[DataMember(Name = "script_fields")]
		IScriptFields ScriptFields { get; set; }

		/// <inheritdoc cref="ISearchRequest.SearchAfter"/>
		[DataMember(Name = "search_after")]
		IList<object> SearchAfter { get; set; }

		/// <inheritdoc cref="ISearchRequest.Size"/>
		[DataMember(Name = "size")]
		int? Size { get; set; }

		/// <inheritdoc cref="ISearchRequest.Sort"/>
		[DataMember(Name = "sort")]
		IList<ISort> Sort { get; set; }

		/// <inheritdoc cref="ISearchRequest.Source"/>
		[DataMember(Name = "_source")]
		Union<bool, ISourceFilter> Source { get; set; }

		/// <inheritdoc cref="ISearchRequest.Suggest"/>
		[DataMember(Name = "suggest")]
		ISuggestContainer Suggest { get; set; }

		/// <inheritdoc cref="ISearchRequest.TerminateAfter"/>
		[DataMember(Name = "terminate_after")]
		long? TerminateAfter { get; set; }

		/// <inheritdoc cref="ISearchRequest.Timeout"/>
		[DataMember(Name = "timeout")]
		string Timeout { get; set; }

		/// <inheritdoc cref="ISearchRequest.TrackScores"/>
		[DataMember(Name = "track_scores")]
		bool? TrackScores { get; set; }

		/// <inheritdoc cref="ISearchRequest.Version"/>
		[DataMember(Name = "version")]
		bool? Version { get; set; }
	}

	[ReadAs(typeof(AsyncSearchSubmitRequest<>))]
	[InterfaceDataContract]
	// ReSharper disable once UnusedTypeParameter
	public partial interface IAsyncSearchSubmitRequest<TInferDocument> : IAsyncSearchSubmitRequest { }

	/// <inheritdoc cref="IAsyncSearchSubmitRequest"/>
	[DataContract]
	public partial class AsyncSearchSubmitRequest
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

		Type ITypedSearchRequest.ClrType => null;

		protected sealed override void RequestDefaults(AsyncSearchSubmitRequestParameters parameters) => TypedKeys = true;
	}

	/// <inheritdoc cref="IAsyncSearchSubmitRequest"/>
	[DataContract]
	public partial class AsyncSearchSubmitRequest<TInferDocument> : IAsyncSearchSubmitRequest<TInferDocument>
	{
		Type ITypedSearchRequest.ClrType => typeof(TInferDocument);
	}

	/// <inheritdoc cref="IAsyncSearchSubmitRequest"/>
	public partial class AsyncSearchSubmitDescriptor<TInferDocument> where TInferDocument : class
	{
		AggregationDictionary IAsyncSearchSubmitRequest.Aggregations { get; set; }
		Type ITypedSearchRequest.ClrType => typeof(TInferDocument);
		IFieldCollapse IAsyncSearchSubmitRequest.Collapse { get; set; }
		Fields IAsyncSearchSubmitRequest.DocValueFields { get; set; }
		bool? IAsyncSearchSubmitRequest.Explain { get; set; }
		int? IAsyncSearchSubmitRequest.From { get; set; }
		IHighlight IAsyncSearchSubmitRequest.Highlight { get; set; }
		IDictionary<IndexName, double> IAsyncSearchSubmitRequest.IndicesBoost { get; set; }
		double? IAsyncSearchSubmitRequest.MinScore { get; set; }
		QueryContainer IAsyncSearchSubmitRequest.PostFilter { get; set; }
		bool? IAsyncSearchSubmitRequest.Profile { get; set; }
		QueryContainer IAsyncSearchSubmitRequest.Query { get; set; }
		IList<IRescore> IAsyncSearchSubmitRequest.Rescore { get; set; }
		IScriptFields IAsyncSearchSubmitRequest.ScriptFields { get; set; }
		IList<object> IAsyncSearchSubmitRequest.SearchAfter { get; set; }
		int? IAsyncSearchSubmitRequest.Size { get; set; }
		IList<ISort> IAsyncSearchSubmitRequest.Sort { get; set; }
		Union<bool, ISourceFilter> IAsyncSearchSubmitRequest.Source { get; set; }
		Fields IAsyncSearchSubmitRequest.StoredFields { get; set; }
		ISuggestContainer IAsyncSearchSubmitRequest.Suggest { get; set; }
		long? IAsyncSearchSubmitRequest.TerminateAfter { get; set; }
		string IAsyncSearchSubmitRequest.Timeout { get; set; }
		bool? IAsyncSearchSubmitRequest.TrackScores { get; set; }
		bool? IAsyncSearchSubmitRequest.TrackTotalHits { get; set; }
		bool? IAsyncSearchSubmitRequest.Version { get; set; }

		protected sealed override void RequestDefaults(AsyncSearchSubmitRequestParameters parameters) => TypedKeys();

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.Aggregations" />
		public AsyncSearchSubmitDescriptor<TInferDocument> Aggregations(
			Func<AggregationContainerDescriptor<TInferDocument>, IAggregationContainer> aggregationsSelector
		) =>
			Assign(aggregationsSelector(new AggregationContainerDescriptor<TInferDocument>())?.Aggregations, (a, v) => a.Aggregations = v);

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.Aggregations" />
		public AsyncSearchSubmitDescriptor<TInferDocument> Aggregations(AggregationDictionary aggregations) =>
			Assign(aggregations, (a, v) => a.Aggregations = v);

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.Source" />
		public AsyncSearchSubmitDescriptor<TInferDocument> Source(bool? enabled = true) => Assign(enabled, (a, v) => a.Source = v);

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.Source" />
		public AsyncSearchSubmitDescriptor<TInferDocument> Source(Func<SourceFilterDescriptor<TInferDocument>, ISourceFilter> selector) =>
			Assign(selector, (a, v) => a.Source = new Union<bool, ISourceFilter>(v?.Invoke(new SourceFilterDescriptor<TInferDocument>())));

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.Size" />
		public AsyncSearchSubmitDescriptor<TInferDocument> Size(int? size) => Assign(size, (a, v) => a.Size = v);

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.Size" />
		public AsyncSearchSubmitDescriptor<TInferDocument> Take(int? take) => Size(take);

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.From" />
		public AsyncSearchSubmitDescriptor<TInferDocument> From(int? from) => Assign(from, (a, v) => a.From = v);

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.From" />
		public AsyncSearchSubmitDescriptor<TInferDocument> Skip(int? skip) => From(skip);

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.Timeout" />
		public AsyncSearchSubmitDescriptor<TInferDocument> Timeout(string timeout) => Assign(timeout, (a, v) => a.Timeout = v);

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.Explain" />
		public AsyncSearchSubmitDescriptor<TInferDocument> Explain(bool? explain = true) => Assign(explain, (a, v) => a.Explain = v);

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.Version" />
		public AsyncSearchSubmitDescriptor<TInferDocument> Version(bool? version = true) => Assign(version, (a, v) => a.Version = v);

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.TrackScores" />
		public AsyncSearchSubmitDescriptor<TInferDocument> TrackScores(bool? trackscores = true) => Assign(trackscores, (a, v) => a.TrackScores = v);

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.Profile" />
		public AsyncSearchSubmitDescriptor<TInferDocument> Profile(bool? profile = true) => Assign(profile, (a, v) => a.Profile = v);

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.MinScore" />
		public AsyncSearchSubmitDescriptor<TInferDocument> MinScore(double? minScore) => Assign(minScore, (a, v) => a.MinScore = v);

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.TerminateAfter" />
		public AsyncSearchSubmitDescriptor<TInferDocument> TerminateAfter(long? terminateAfter) => Assign(terminateAfter, (a, v) => a.TerminateAfter = v);

		/// <inheritdoc cref="SearchDescriptor{TInferDocument}.ExecuteOnLocalShard" />
		public AsyncSearchSubmitDescriptor<TInferDocument> ExecuteOnLocalShard() => Preference("_local");

		/// <inheritdoc cref="SearchDescriptor{TInferDocument}.ExecuteOnNode" />
		public AsyncSearchSubmitDescriptor<TInferDocument> ExecuteOnNode(string node) => Preference($"_only_node:{node}");

		/// <inheritdoc cref="SearchDescriptor{TInferDocument}.ExecuteOnPreferredNode" />
		public AsyncSearchSubmitDescriptor<TInferDocument> ExecuteOnPreferredNode(string node) =>
			Preference(node.IsNullOrEmpty() ? null : $"_prefer_node:{node}");

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.IndicesBoost" />
		public AsyncSearchSubmitDescriptor<TInferDocument> IndicesBoost(Func<FluentDictionary<IndexName, double>, FluentDictionary<IndexName, double>> boost) =>
			Assign(boost, (a, v) => a.IndicesBoost = v?.Invoke(new FluentDictionary<IndexName, double>()));

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.StoredFields" />
		public AsyncSearchSubmitDescriptor<TInferDocument> StoredFields(Func<FieldsDescriptor<TInferDocument>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.StoredFields = v?.Invoke(new FieldsDescriptor<TInferDocument>())?.Value);

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.StoredFields" />
		public AsyncSearchSubmitDescriptor<TInferDocument> StoredFields(Fields fields) => Assign(fields, (a, v) => a.StoredFields = v);

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.ScriptFields" />
		public AsyncSearchSubmitDescriptor<TInferDocument> ScriptFields(Func<ScriptFieldsDescriptor, IPromise<IScriptFields>> selector) =>
			Assign(selector, (a, v) => a.ScriptFields = v?.Invoke(new ScriptFieldsDescriptor())?.Value);

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.ScriptFields" />
		public AsyncSearchSubmitDescriptor<TInferDocument> DocValueFields(Func<FieldsDescriptor<TInferDocument>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.DocValueFields = v?.Invoke(new FieldsDescriptor<TInferDocument>())?.Value);

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.DocValueFields" />
		public AsyncSearchSubmitDescriptor<TInferDocument> DocValueFields(Fields fields) => Assign(fields, (a, v) => a.DocValueFields = v);

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.Sort" />
		public AsyncSearchSubmitDescriptor<TInferDocument> Sort(Func<SortDescriptor<TInferDocument>, IPromise<IList<ISort>>> selector) =>
			Assign(selector, (a, v) => a.Sort = v?.Invoke(new SortDescriptor<TInferDocument>())?.Value);

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.SearchAfter" />
		public AsyncSearchSubmitDescriptor<TInferDocument> SearchAfter(IList<object> searchAfter) => Assign(searchAfter, (a, v) => a.SearchAfter = v);

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.SearchAfter" />
		public AsyncSearchSubmitDescriptor<TInferDocument> SearchAfter(params object[] searchAfter) => Assign(searchAfter, (a, v) => a.SearchAfter = v);

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.Suggest" />
		public AsyncSearchSubmitDescriptor<TInferDocument> Suggest(Func<SuggestContainerDescriptor<TInferDocument>, IPromise<ISuggestContainer>> selector) =>
			Assign(selector, (a, v) => a.Suggest = v?.Invoke(new SuggestContainerDescriptor<TInferDocument>())?.Value);

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.Query" />
		public AsyncSearchSubmitDescriptor<TInferDocument> Query(Func<QueryContainerDescriptor<TInferDocument>, QueryContainer> query) =>
			Assign(query, (a, v) => a.Query = v?.Invoke(new QueryContainerDescriptor<TInferDocument>()));

		/// <inheritdoc cref="SearchDescriptor{TInferDocument}.MatchAll" />
		public AsyncSearchSubmitDescriptor<TInferDocument> MatchAll(Func<MatchAllQueryDescriptor, IMatchAllQuery> selector = null) =>
			Query(q => q.MatchAll(selector));

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.PostFilter" />
		public AsyncSearchSubmitDescriptor<TInferDocument> PostFilter(Func<QueryContainerDescriptor<TInferDocument>, QueryContainer> filter) =>
			Assign(filter, (a, v) => a.PostFilter = v?.Invoke(new QueryContainerDescriptor<TInferDocument>()));

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.Highlight" />
		public AsyncSearchSubmitDescriptor<TInferDocument> Highlight(Func<HighlightDescriptor<TInferDocument>, IHighlight> highlightSelector) =>
			Assign(highlightSelector, (a, v) => a.Highlight = v?.Invoke(new HighlightDescriptor<TInferDocument>()));

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.Collapse" />
		public AsyncSearchSubmitDescriptor<TInferDocument> Collapse(Func<FieldCollapseDescriptor<TInferDocument>, IFieldCollapse> collapseSelector) =>
			Assign(collapseSelector, (a, v) => a.Collapse = v?.Invoke(new FieldCollapseDescriptor<TInferDocument>()));

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.Rescore" />
		public AsyncSearchSubmitDescriptor<TInferDocument> Rescore(Func<RescoringDescriptor<TInferDocument>, IPromise<IList<IRescore>>> rescoreSelector) =>
			Assign(rescoreSelector, (a, v) => a.Rescore = v?.Invoke(new RescoringDescriptor<TInferDocument>()).Value);

		/// <inheritdoc cref="IAsyncSearchSubmitRequest.TrackTotalHits" />
		public AsyncSearchSubmitDescriptor<TInferDocument> TrackTotalHits(bool? trackTotalHits = true) => Assign(trackTotalHits, (a, v) => a.TrackTotalHits = v);
	}
}
