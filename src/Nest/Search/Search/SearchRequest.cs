using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Nest
{

	[JsonConverter(typeof(ReadAsTypeJsonConverter<SearchRequest>))]
	public partial interface ISearchRequest : ICovariantSearchRequest
	{
		[JsonProperty(PropertyName = "timeout")]
		string Timeout { get; set; }

		[JsonProperty(PropertyName = "from")]
		int? From { get; set; }

		[JsonProperty(PropertyName = "size")]
		int? Size { get; set; }

		[JsonProperty(PropertyName = "explain")]
		bool? Explain { get; set; }

		[JsonProperty(PropertyName = "version")]
		bool? Version { get; set; }

		[JsonProperty(PropertyName = "track_scores")]
		bool? TrackScores { get; set; }

		[JsonProperty(PropertyName = "min_score")]
		double? MinScore { get; set; }

		[JsonProperty(PropertyName = "terminate_after")]
		long? TerminateAfter { get; set; }

		[JsonProperty(PropertyName = "indices_boost")]
		[JsonConverter(typeof (VerbatimDictionaryKeysJsonConverter))]
		IDictionary<IndexName, double> IndicesBoost { get; set; }

		[JsonProperty(PropertyName = "sort")]
		[JsonConverter(typeof(SortCollectionJsonConverter))]
		IList<ISort> Sort { get; set; }

		[JsonProperty(PropertyName = "suggest")]
		IDictionary<string, ISuggestBucket> Suggest { get; set; }

		[JsonProperty(PropertyName = "highlight")]
		IHighlight Highlight { get; set; }

		[JsonProperty(PropertyName = "rescore")]
		IRescore Rescore { get; set; }

		[JsonProperty(PropertyName = "fields")]
		IList<Field> Fields { get; set; }

		[JsonProperty(PropertyName = "fielddata_fields")]
		IList<Field> FielddataFields { get; set; }

		[JsonProperty(PropertyName = "script_fields")]
		[JsonConverter(typeof (VerbatimDictionaryKeysJsonConverter))]
		IDictionary<string, IScriptQuery> ScriptFields { get; set; }

		[JsonProperty(PropertyName = "_source")]
		[JsonConverter(typeof(ReadAsTypeJsonConverter<SourceFilter>))]
		ISourceFilter Source { get; set; }

		[JsonProperty(PropertyName = "aggs")]
		AggregationDictionary Aggregations { get; set; }

		[JsonProperty(PropertyName = "query")]
		QueryContainer Query { get; set; }

		[JsonProperty(PropertyName = "post_filter")]
		QueryContainer PostFilter { get; set; }

		[JsonProperty(PropertyName = "inner_hits")]
		[JsonConverter(typeof (VerbatimDictionaryKeysJsonConverter))]
		IDictionary<string, IInnerHitsContainer> InnerHits { get; set; }

		string Preference { get; }
		
		string Routing { get; }

		SearchType? SearchType { get;  }

		bool? IgnoreUnavalable { get; }
	}
	public partial interface ISearchRequest<T> : ISearchRequest { }

	public partial class SearchRequest 
	{
		private Type _clrType { get; set; }
		Type ICovariantSearchRequest.ClrType => this._clrType;
		Types ICovariantSearchRequest.ElasticsearchTypes => ((ISearchRequest)this).Type;
		protected override HttpMethod HttpMethod =>
			RequestState.RequestParameters?.ContainsKey("_source") == true || RequestState.RequestParameters?.ContainsKey("q")  == true? HttpMethod.GET : HttpMethod.POST;

		public string Timeout { get; set; }
		public int? From { get; set; }
		public int? Size { get; set; }
		public bool? Explain { get; set; }
		public bool? Version { get; set; }
		public bool? TrackScores { get; set; }
		public double? MinScore { get; set; }
		public long? TerminateAfter { get; set; }
		public IList<Field> Fields { get; set; }
		public IList<Field> FielddataFields { get; set; }
		public IDictionary<string, IScriptQuery> ScriptFields { get; set; }
		public ISourceFilter Source { get; set; }
		public IList<ISort> Sort { get; set; }
		public IDictionary<IndexName, double> IndicesBoost { get; set; }
		public QueryContainer PostFilter { get; set; }
		public IDictionary<string, IInnerHitsContainer> InnerHits { get; set; }
		public QueryContainer Query { get; set; }
		public IRescore Rescore { get; set; }
		public IDictionary<string, ISuggestBucket> Suggest { get; set; }
		public IHighlight Highlight { get; set; }
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
		private Type _clrType { get; set; }
		Type ICovariantSearchRequest.ClrType => this._clrType;
		Types ICovariantSearchRequest.ElasticsearchTypes => ((ISearchRequest)this).Type;
		protected override HttpMethod HttpMethod =>
			RequestState.RequestParameters?.ContainsKey("_source") == true || RequestState.RequestParameters?.ContainsKey("q")  == true? HttpMethod.GET : HttpMethod.POST;
		
		public string Timeout { get; set; }
		public int? From { get; set; }
		public int? Size { get; set; }
		public bool? Explain { get; set; }
		public bool? Version { get; set; }
		public bool? TrackScores { get; set; }
		public double? MinScore { get; set; }
		public long? TerminateAfter { get; set; }
		public IList<Field> Fields { get; set; }
		public IList<Field> FielddataFields { get; set; }
		public IDictionary<string, IScriptQuery> ScriptFields { get; set; }
		public ISourceFilter Source { get; set; }
		public IList<ISort> Sort { get; set; }
		public IDictionary<IndexName, double> IndicesBoost { get; set; }
		public QueryContainer PostFilter { get; set; }
		public IDictionary<string, IInnerHitsContainer> InnerHits { get; set; }
		public QueryContainer Query { get; set; }
		public IRescore Rescore { get; set; }
		public IDictionary<string, ISuggestBucket> Suggest { get; set; }
		public IHighlight Highlight { get; set; }
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
			RequestState.RequestParameters?.ContainsKey("_source") == true || RequestState.RequestParameters?.ContainsKey("q")  == true? HttpMethod.GET : HttpMethod.POST;

		SearchType? ISearchRequest.SearchType => RequestState.RequestParameters.GetQueryStringValue<SearchType?>("search_type");

		string ISearchRequest.Preference => RequestState.RequestParameters.GetQueryStringValue<string>("preference");

		string ISearchRequest.Routing => RequestState.RequestParameters.GetQueryStringValue<string[]>("routing") == null
			? null : string.Join(",", RequestState.RequestParameters.GetQueryStringValue<string[]>("routing"));

		bool? ISearchRequest.IgnoreUnavalable => RequestState.RequestParameters.GetQueryStringValue<bool?>("ignore_unavailable");

		/// <summary>
		/// Whether conditionless queries are allowed or not
		/// </summary>
		internal bool _Strict { get; set; }

		string ISearchRequest.Timeout { get; set; }
		int? ISearchRequest.From { get; set; }
		int? ISearchRequest.Size { get; set; }
		bool? ISearchRequest.Explain { get; set; }
		bool? ISearchRequest.Version { get; set; }
		bool? ISearchRequest.TrackScores { get; set; }
		double? ISearchRequest.MinScore { get; set; }
		long? ISearchRequest.TerminateAfter { get; set; }

		IDictionary<IndexName, double> ISearchRequest.IndicesBoost { get; set; }

		IList<ISort> ISearchRequest.Sort { get; set; }

		IDictionary<string, ISuggestBucket> ISearchRequest.Suggest { get; set; }

		IHighlight ISearchRequest.Highlight { get; set; }

		IRescore ISearchRequest.Rescore { get; set; }

		QueryContainer ISearchRequest.Query { get; set; }

		QueryContainer ISearchRequest.PostFilter { get; set; }

		IList<Field> ISearchRequest.Fields { get; set; }

		IList<Field> ISearchRequest.FielddataFields { get; set; }

		IDictionary<string, IScriptQuery> ISearchRequest.ScriptFields { get; set; }
		ISourceFilter ISearchRequest.Source { get; set; }

		AggregationDictionary ISearchRequest.Aggregations { get; set; }

		IDictionary<string, IInnerHitsContainer> ISearchRequest.InnerHits { get; set; }

		//TODO probably remove this when we normalize sorting
		private void AddSort(ISort sort) => Assign(a =>
		{
			a.Sort = a.Sort ?? new List<ISort>();
			a.Sort.Add(sort);
		});

		/// <summary>
		/// When strict is set, conditionless queries are treated as an exception. 
		/// </summary>
		public SearchDescriptor<T> Strict(bool strict = true) => Assign(a => this._Strict = strict);

		public SearchDescriptor<T> Aggregations(Func<AggregationContainerDescriptor<T>, IAggregationContainer> aggregationsSelector) =>
			Assign(a=>a.Aggregations = aggregationsSelector(new AggregationContainerDescriptor<T>())?.Aggregations);

		//TODO refactor!
		public SearchDescriptor<T> InnerHits(
			Func<
				FluentDictionary<string, Func<InnerHitsContainerDescriptor<T>, IInnerHitsContainer>>,
				FluentDictionary<string, Func<InnerHitsContainerDescriptor<T>, IInnerHitsContainer>>
			> innerHitsSelector) => Assign(a => 
			{
				if (innerHitsSelector == null)
				{
					a.InnerHits = null;
					return;
				}
				var containers = innerHitsSelector(new FluentDictionary<string, Func<InnerHitsContainerDescriptor<T>, IInnerHitsContainer>>())
					.Where(kv => kv.Value != null)
					.Select(kv => new { Key = kv.Key, Value = kv.Value(new InnerHitsContainerDescriptor<T>()) })
					.Where(kv => kv.Value != null)
					.ToDictionary(kv => kv.Key, kv => kv.Value);
				if (containers == null || containers.Count == 0)
				{
					a.InnerHits = null;
					return;
				}
				a.InnerHits = containers;
			});

		public SearchDescriptor<T> Source(bool include = true)=> Assign(a => a.Source = !include ? SourceFilter.ExcludeAll : null);
		
		public SearchDescriptor<T> Source(Func<SourceFilterDescriptor<T>, SourceFilterDescriptor<T>> sourceSelector) =>
			Assign(a => a.Source = sourceSelector?.Invoke(new SourceFilterDescriptor<T>()));

		/// <summary>
		/// The number of hits to return. Defaults to 10. When using scroll search type 
		/// size is actually multiplied by the number of shards!
		/// </summary>
		public SearchDescriptor<T> Size(int size) => Assign(a => a.Size = size);

		/// <summary>
		/// The number of hits to return. Defaults to 10.
		/// </summary>
		public SearchDescriptor<T> Take(int take) => this.Size(take);

		/// <summary>
		/// The starting from index of the hits to return. Defaults to 0.
		/// </summary>
		public SearchDescriptor<T> From(int from) => Assign(a => a.From = from);

		/// <summary>
		/// The starting from index of the hits to return. Defaults to 0.
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
		public SearchDescriptor<T> ExecuteOnPreferredNode(string node)
		{
			node.ThrowIfNull("node");
			return this.Preference(string.Format("_prefer_node:{0}", node));
		}

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
		public SearchDescriptor<T> Fields(params Expression<Func<T, object>>[] expressions) =>
			Assign(a => a.Fields = expressions?.Select(e => (Field) e).ToListOrNullIfEmpty());

		/// <summary>
		/// Allows to selectively load specific fields for each document 
		/// represented by a search hit. Defaults to load the internal _source field.
		/// </summary>
		public SearchDescriptor<T> Fields(Func<FluentFieldList<T>, FluentFieldList<T>> properties) =>
			Assign(a => a.Fields  = properties?.Invoke(new FluentFieldList<T>()).ToListOrNullIfEmpty());

		/// <summary>
		/// Allows to selectively load specific fields for each document 
		/// represented by a search hit. Defaults to load the internal _source field.
		/// </summary>
		public SearchDescriptor<T> Fields(params string[] fields)
			=> Assign(a => a.Fields = fields?.Select(f => (Field) f).ToListOrNullIfEmpty());

		///<summary>
		///A comma-separated list of fields to return as the field data representation of a field for each hit
		///</summary>
		public SearchDescriptor<T> FielddataFields(params string[] fielddataFields) =>
			Assign(a => a.FielddataFields = fielddataFields?.Select(f => (Field) f).ToListOrNullIfEmpty());

		///<summary>
		///A comma-separated list of fields to return as the field data representation of a field for each hit
		///</summary>
		public SearchDescriptor<T> FielddataFields(params Expression<Func<T, object>>[] fielddataFields) =>
			Assign(a => a.FielddataFields = fielddataFields?.Select(f => (Field) f).ToListOrNullIfEmpty());

		//TODO scriptfields needs a seperate encapsulation
		public SearchDescriptor<T> ScriptFields(
			Func<FluentDictionary<string, Func<ScriptQueryDescriptor<T>, ScriptQueryDescriptor<T>>>,
			 FluentDictionary<string, Func<ScriptQueryDescriptor<T>, ScriptQueryDescriptor<T>>>> scriptFields) => Assign(a =>
			 {
				 scriptFields.ThrowIfNull("scriptFields");
				 var scriptFieldDescriptors = scriptFields(new FluentDictionary<string, Func<ScriptQueryDescriptor<T>, ScriptQueryDescriptor<T>>>());
				 if (scriptFieldDescriptors == null || scriptFieldDescriptors.All(d => d.Value == null))
				 {
					 a.ScriptFields = null;
					 return;
				 }
				 a.ScriptFields = new FluentDictionary<string, IScriptQuery>();
				 foreach (var d in scriptFieldDescriptors)
				 {
					 if (d.Value == null)
						 continue;
					 a.ScriptFields.Add(d.Key, d.Value(new ScriptQueryDescriptor<T>()));
				 }
			 });

		/// <summary>
		/// <para>Allows adding a prebuilt sort of any type.
		/// </para>
		/// </summary>
		public SearchDescriptor<T> Sort(ISort sort)
		{
			AddSort(sort);
			return this;
		}

		/// <summary>
		/// <para>Allows to add one or more sort on specific fields. Each sort can be reversed as well.
		/// The sort is defined on a per field level, with special field name for _score to sort by score.
		/// </para>
		/// <para>
		/// Sort ascending.
		/// </para>
		/// </summary>
		public SearchDescriptor<T> SortAscending(Expression<Func<T, object>> objectPath)
		{
			AddSort(new Sort() { Field = objectPath, Order = SortOrder.Ascending });
			return this;
		}

		/// <summary>
		/// <para>Allows to add one or more sort on specific fields. Each sort can be reversed as well.
		/// The sort is defined on a per field level, with special field name for _score to sort by score.
		/// </para>
		/// <para>
		/// Sort descending.
		/// </para>
		/// </summary>
		public SearchDescriptor<T> SortDescending(Expression<Func<T, object>> objectPath)
		{
			AddSort(new Sort() { Field = objectPath, Order = SortOrder.Descending });
			return this;
		}

		/// <summary>
		/// <para>Allows to add one or more sort on specific fields. Each sort can be reversed as well.
		/// The sort is defined on a per field level, with special field name for _score to sort by score.
		/// </para>
		/// <para>
		/// Sort ascending.
		/// </para>
		/// </summary>
		public SearchDescriptor<T> SortAscending(string field)
		{
			AddSort(new Sort() { Field = field, Order = SortOrder.Ascending });
			return this;
		}

		/// <summary>
		/// <para>Allows to add one or more sort on specific fields. Each sort can be reversed as well.
		/// The sort is defined on a per field level, with special field name for _score to sort by score.
		/// </para>
		/// <para>
		/// Sort descending.
		/// </para>
		/// </summary>
		public SearchDescriptor<T> SortDescending(string field)
		{
			AddSort(new Sort() { Field = field, Order = SortOrder.Descending});
			return this;
		}

		/// <summary>
		/// <para>Sort() allows you to fully describe your sort unlike the SortAscending and SortDescending aliases.
		/// </para>
		/// </summary>
		public SearchDescriptor<T> Sort(Func<SortFieldDescriptor<T>, IFieldSort> sortSelector)
		{
			sortSelector.ThrowIfNull("sortSelector");
			var descriptor = sortSelector(new SortFieldDescriptor<T>());
			AddSort(descriptor);
			return this;
		}

		/// <summary>
		/// <para>SortMulti allows multiple sorts to be provided on one search descriptor
		/// </para>
		/// </summary>
		public SearchDescriptor<T> SortMulti(params Func<SortFieldDescriptor<T>, IFieldSort>[] sorts)
		{
			foreach (var sort in sorts)
			{
				this.Sort(sort);
			}

			return this;
		}

		/// <summary>
		/// <para>SortMulti allows multiple sorts to be provided on one search descriptor
		/// </para>
		/// </summary>
		public SearchDescriptor<T> SortMulti(IEnumerable<SortFieldDescriptor<T>> sorts)
		{
			foreach (var sort in sorts)
			{
				var copy = sort;
				this.Sort(s => copy);
			}

			return this;
		}

		/// <summary>
		/// <para>SortGeoDistance() allows you to sort by a distance from a geo point.
		/// </para>
		/// </summary>
		public SearchDescriptor<T> SortGeoDistance(Func<SortGeoDistanceDescriptor<T>, IGeoDistanceSort> sortSelector)
		{
			sortSelector.ThrowIfNull("sortSelector");
			var descriptor = sortSelector(new SortGeoDistanceDescriptor<T>());
			AddSort(descriptor);
			return this;
		}

		/// <summary>
		/// <para>SortScript() allows you to sort by a distance from a geo point.
		/// </para>
		/// </summary>
		public SearchDescriptor<T> SortScript(Func<SortScriptDescriptor<T>, IScriptSort> sortSelector)
		{
			sortSelector.ThrowIfNull("sortSelector");
			var descriptor = sortSelector(new SortScriptDescriptor<T>());
			AddSort(descriptor);
			return this;
		}

		/// <summary>
		/// The term suggester suggests terms based on edit distance. The provided suggest text is analyzed before terms are suggested. 
		/// The suggested terms are provided per analyzed suggest text token. The term suggester doesn’t take the query into account that is part of request.
		/// </summary>
		public SearchDescriptor<T> SuggestTerm(string name, Func<TermSuggestDescriptor<T>, TermSuggestDescriptor<T>> suggest) => Assign(a =>
		{
			name.ThrowIfNullOrEmpty("name");
			suggest.ThrowIfNull("suggest");
			if (a.Suggest == null) a.Suggest = new Dictionary<string, ISuggestBucket>();
			var desc = new TermSuggestDescriptor<T>();
			var item = suggest(desc);
			ITermSuggester i = item;
			var bucket = new SuggestBucket { Text = i.Text, Term = item };
			a.Suggest.Add(name, bucket);
		});

		/// <summary>
		/// The phrase suggester adds additional logic on top of the term suggester to select entire corrected phrases 
		/// instead of individual tokens weighted based on ngram-langugage models. 
		/// </summary>
		public SearchDescriptor<T> SuggestPhrase(string name, Func<PhraseSuggestDescriptor<T>, PhraseSuggestDescriptor<T>> suggest) => Assign(a =>
		{
			name.ThrowIfNullOrEmpty("name");
			suggest.ThrowIfNull("suggest");
			if (a.Suggest == null)
				a.Suggest = new Dictionary<string, ISuggestBucket>();

			var desc = new PhraseSuggestDescriptor<T>();
			var item = suggest(desc);
			IPhraseSuggester i = item;
			var bucket = new SuggestBucket { Text = i.Text, Phrase = item };
			a.Suggest.Add(name, bucket);
		});

		/// <summary>
		/// The completion suggester is a so-called prefix suggester. 
		/// It does not do spell correction like the term or phrase suggesters but allows basic auto-complete functionality.
		/// </summary>
		public SearchDescriptor<T> SuggestCompletion(string name, Func<CompletionSuggestDescriptor<T>, CompletionSuggestDescriptor<T>> suggest) => Assign(a => {
			name.ThrowIfNullOrEmpty("name");
			suggest.ThrowIfNull("suggest");
			if (a.Suggest == null)
				a.Suggest = new Dictionary<string, ISuggestBucket>();

			var desc = new CompletionSuggestDescriptor<T>();
			var item = suggest(desc);
			ICompletionSuggester i = item;
			var bucket = new SuggestBucket { Text = i.Text, Completion = item };
			a.Suggest.Add(name, bucket);
		});

		/// <summary>
		/// Describe the query to perform using a query descriptor lambda
		/// </summary>
		public SearchDescriptor<T> Query(Func<QueryContainerDescriptor<T>, QueryContainer> query)
		{
			query.ThrowIfNull("query");
			var q = new QueryContainerDescriptor<T>();
			((IQueryContainer)q).IsStrict = this._Strict;
			var bq = query(q);
			return this.Query(bq);
		}

		/// <summary>
		/// Describe the query to perform using the static Query class
		/// </summary>
		public SearchDescriptor<T> Query(QueryContainer query) => Assign(a =>
		{
			if (query == null) return ;

			if (this._Strict && query.IsConditionless)
				throw new DslException("Query resulted in a conditionless query:\n{0}".F(JsonConvert.SerializeObject(query, Formatting.Indented)));

			else if (query.IsConditionless) return ;
			a.Query = query;
		});

		/// <summary>
		/// Describe the query to perform as a raw json string
		/// </summary>
		public SearchDescriptor<T> QueryRaw(string rawQuery) => Assign(a => a.Query = new QueryContainerDescriptor<T>().Raw(rawQuery));

		/// <summary>
		/// Filter search using a filter descriptor lambda
		/// </summary>
		public SearchDescriptor<T> PostFilter(Func<QueryContainerDescriptor<T>, QueryContainer> filter) => Assign(a =>
		{
			filter.ThrowIfNull("filter");
			var f = new QueryContainerDescriptor<T>().Strict(this._Strict);

			var bf = filter(f);
			if (bf == null) return;
			if (this._Strict && bf.IsConditionless)
				throw new DslException("Filter resulted in a conditionless filter:\n{0}".F(JsonConvert.SerializeObject(bf, Formatting.Indented)));

			else if (bf.IsConditionless) return ;
			a.PostFilter = bf;
		});

		/// <summary>
		/// Filter search
		/// </summary>
		public SearchDescriptor<T> PostFilter(QueryContainer QueryDescriptor) => Assign(a => {
			QueryDescriptor.ThrowIfNull("filter");
			a.PostFilter = QueryDescriptor;
		});

		/// <summary>
		/// Filter search using a raw json string
		/// </summary>
		public SearchDescriptor<T> FilterRaw(string rawFilter) => Assign(a => a.PostFilter = new QueryContainerDescriptor<T>().Raw(rawFilter));

		/// <summary>
		/// Allow to highlight search results on one or more fields. The implementation uses the either lucene fast-vector-highlighter or highlighter. 
		/// </summary>
		public SearchDescriptor<T> Highlight(Func<HighlightDescriptor<T>, IHighlight> highlightSelector) =>
			Assign(a => a.Highlight = highlightSelector?.Invoke(new HighlightDescriptor<T>()));
		
		/// <summary>
		/// Allows you to specify a rescore query
		/// </summary>
		public SearchDescriptor<T> Rescore(Func<RescoreDescriptor<T>, IRescore> rescoreSelector) =>
			Assign(a => a.Rescore = rescoreSelector?.Invoke(new RescoreDescriptor<T>()));
		
		/// <summary>
		/// Shorthand for a match_all query without having to specify .Query(q=>q.MatchAll())
		/// </summary>
		public SearchDescriptor<T> MatchAll() => this.Query(q => q.MatchAll());

		public SearchDescriptor<T> ConcreteTypeSelector(Func<dynamic, Hit<dynamic>, Type> typeSelector) =>
			Assign(a => a.TypeSelector = typeSelector);

	}
}
