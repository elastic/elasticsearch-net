using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<SearchRequest>))]
	public interface ISearchRequest : IQueryPath<SearchRequestParameters>
	{
		Type ClrType { get; }

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
		[JsonConverter(typeof (DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<IndexNameMarker, double> IndicesBoost { get; set; }

		[JsonProperty(PropertyName = "sort")]
		[JsonConverter(typeof(SortCollectionConverter))]
		IList<ISort> Sort { get; set; }

		[JsonProperty(PropertyName = "suggest")]
		IDictionary<string, ISuggestBucket> Suggest { get; set; }

		[JsonProperty(PropertyName = "highlight")]
		IHighlightRequest Highlight { get; set; }

		[JsonProperty(PropertyName = "rescore")]
		IRescore Rescore { get; set; }

		[JsonProperty(PropertyName = "fields")]
		IList<PropertyPathMarker> Fields { get; set; }

		[JsonProperty(PropertyName = "fielddata_fields")]
		IList<PropertyPathMarker> FielddataFields { get; set; }

		[JsonProperty(PropertyName = "script_fields")]
		[JsonConverter(typeof (DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<string, IScriptQuery> ScriptFields { get; set; }

		[JsonProperty(PropertyName = "_source")]
		[JsonConverter(typeof(ReadAsTypeConverter<SourceFilter>))]
		ISourceFilter Source { get; set; }

		[JsonProperty(PropertyName = "aggs")]
		[JsonConverter(typeof (DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<string, IAggregationContainer> Aggregations { get; set; }

		[JsonProperty(PropertyName = "query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryContainer>, CustomJsonConverter>))]
		IQueryContainer Query { get; set; }

		[JsonProperty(PropertyName = "post_filter")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryContainer>, CustomJsonConverter>))]
		IQueryContainer PostFilter { get; set; }

		[JsonProperty(PropertyName = "inner_hits")]
		[JsonConverter(typeof (DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<string, IInnerHitsContainer> InnerHits { get; set; }

		string Preference { get; }
		
		string Routing { get; }

		SearchType? SearchType { get;  }

		bool? IgnoreUnavalable { get; }

		Func<dynamic, Hit<dynamic>, Type> TypeSelector { get; set;}
		
		SearchRequestParameters QueryString { get; set; }
	}

	public interface ISearchRequest<T> : ISearchRequest {}

	internal static class SearchPathInfo
	{
		/// <summary>
		/// Based on the type information present in this descriptor create method that takes
		/// the returned _source and hit and returns the ClrType it should deserialize too.
		/// This is so that Documents[A] can contain actual instances of subclasses B, C as well.
		/// If you specify types using .Types(typeof(B), typeof(C)) then NEST can automagically
		/// create a TypeSelector based on the hits _type property.
		/// </summary>
		public static void CloseOverAutomagicCovariantResultSelector(ElasticInferrer infer, ISearchRequest self)
		{
			if (infer == null || self == null) return;
			var returnType = self.ClrType;

			if (returnType == null) return;

			var types = (self.Types ?? Enumerable.Empty<TypeNameMarker>()).Where(t => t.Type != null).ToList();
			if (self.TypeSelector != null || !types.HasAny(t => t.Type != returnType))
				return;
			
			var typeDictionary = types.ToDictionary(infer.TypeName, t => t.Type);
			self.TypeSelector = (o, h) =>
			{
				Type t;
				return !typeDictionary.TryGetValue(h.Type, out t) ? returnType : t;
			};
		}
		public static void Update(IConnectionSettingsValues settings, ElasticsearchPathInfo<SearchRequestParameters> pathInfo, ISearchRequest request)
		{
			pathInfo.HttpMethod = request.RequestParameters.ContainsKey("source") ? PathInfoHttpMethod.GET : PathInfoHttpMethod.POST;
		}
	}
	
	public partial class SearchRequest : QueryPathBase<SearchRequestParameters>, ISearchRequest
	{
		public SearchRequest() {}

		public SearchRequest(IndexNameMarker index, TypeNameMarker type = null) : base(index, type) { }

		public SearchRequest(IEnumerable<IndexNameMarker> indices, IEnumerable<TypeNameMarker> types = null) : base(indices, types) { }

		private Type _clrType { get; set; }
		Type ISearchRequest.ClrType { get { return _clrType; } }

		public string Timeout { get; set; }
		public int? From { get; set; }
		public int? Size { get; set; }
		public bool? Explain { get; set; }
		public bool? Version { get; set; }
		public bool? TrackScores { get; set; }
		public double? MinScore { get; set; }
		public long? TerminateAfter { get; set; }
		public IList<PropertyPathMarker> Fields { get; set; }
		public IList<PropertyPathMarker> FielddataFields { get; set; }
		public IDictionary<string, IScriptQuery> ScriptFields { get; set; }
		public ISourceFilter Source { get; set; }
		public IList<ISort> Sort { get; set; }
		public IDictionary<IndexNameMarker, double> IndicesBoost { get; set; }
		public IQueryContainer PostFilter { get; set; }
		public IDictionary<string, IInnerHitsContainer> InnerHits { get; set; }
		public IQueryContainer Query { get; set; }
		public IRescore Rescore { get; set; }
		public IDictionary<string, ISuggestBucket> Suggest { get; set; }
		public IHighlightRequest Highlight { get; set; }
		public IDictionary<string, IAggregationContainer> Aggregations { get; set; }

		SearchType? ISearchRequest.SearchType => this.QueryString?.GetQueryStringValue<SearchType?>("search_type");

		string ISearchRequest.Preference => this.QueryString?.GetQueryStringValue<string>("preference");

		string ISearchRequest.Routing => this.QueryString?.GetQueryStringValue<string[]>("routing") == null
			? null
			: string.Join(",", this.QueryString?.GetQueryStringValue<string[]>("routing"));

		bool? ISearchRequest.IgnoreUnavalable => this.QueryString?.GetQueryStringValue<bool?>("ignore_unavailable");

		public Func<dynamic, Hit<dynamic>, Type> TypeSelector { get; set; }

		public SearchRequestParameters QueryString { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SearchRequestParameters> pathInfo) =>
			SearchPathInfo.Update(settings, pathInfo, this);

	}

	public partial class SearchRequest<T> : QueryPathBase<SearchRequestParameters, T>, ISearchRequest
		where T : class
	{
		public SearchRequest() {}

		public SearchRequest(IndexNameMarker index, TypeNameMarker type = null) : base(index, type) { }

		public SearchRequest(IEnumerable<IndexNameMarker> indices, IEnumerable<TypeNameMarker> types = null) : base(indices, types) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SearchRequestParameters> pathInfo) =>
			SearchPathInfo.Update(settings,pathInfo, this);

		public Type ClrType => typeof(T);
		public string Timeout { get; set; }
		public int? From { get; set; }
		public int? Size { get; set; }
		public bool? Explain { get; set; }
		public bool? Version { get; set; }
		public bool? TrackScores { get; set; }
		public double? MinScore { get; set; }
		public long? TerminateAfter { get; set; }
		public IDictionary<IndexNameMarker, double> IndicesBoost { get; set; }
		public IList<ISort> Sort { get; set; }
		public IDictionary<string, ISuggestBucket> Suggest { get; set; }
		public IHighlightRequest Highlight { get; set; }
		public IRescore Rescore { get; set; }
		public IList<PropertyPathMarker> Fields { get; set; }
		public IList<PropertyPathMarker> FielddataFields { get; set; }
		public IDictionary<string, IScriptQuery> ScriptFields { get; set; }
		public ISourceFilter Source { get; set; }
		public IDictionary<string, IInnerHitsContainer> InnerHits { get; set; }
		public IDictionary<string, IAggregationContainer> Aggregations { get; set; }
		public IQueryContainer Query { get; set; }
		public IQueryContainer PostFilter { get; set; }
		SearchType? ISearchRequest.SearchType => this.QueryString?.GetQueryStringValue<SearchType?>("search_type");

		string ISearchRequest.Preference => this.QueryString?.GetQueryStringValue<string>("preference");

		string ISearchRequest.Routing => this.QueryString?.GetQueryStringValue<string[]>("routing") == null
			? null
			: string.Join(",", this.QueryString?.GetQueryStringValue<string[]>("routing"));

		bool? ISearchRequest.IgnoreUnavalable => this.QueryString?.GetQueryStringValue<bool?>("ignore_unavailable");

		public Func<dynamic, Hit<dynamic>, Type> TypeSelector { get; set; }
		public SearchRequestParameters QueryString { get; set; }
	}

	/// <summary>
	/// A descriptor wich describes a search operation for _search and _msearch
	/// </summary>
	public partial class SearchDescriptor<T> : QueryPathDescriptorBase<SearchDescriptor<T>, SearchRequestParameters, T>, ISearchRequest 
		where T : class
	{
		private ISearchRequest Self => this;

		private SearchDescriptor<T> _assign(Action<ISearchRequest> assigner) => Fluent.Assign(this, assigner);

		SearchType? ISearchRequest.SearchType => this.Request.RequestParameters.GetQueryStringValue<SearchType?>("search_type");

		SearchRequestParameters ISearchRequest.QueryString
		{
			get { return this.Request.RequestParameters;  }
			set { this.Request.RequestParameters = value;  }
		}

		string ISearchRequest.Preference => this.Request.RequestParameters.GetQueryStringValue<string>("preference");

		string ISearchRequest.Routing => this.Request.RequestParameters.GetQueryStringValue<string[]>("routing") == null
			? null : string.Join(",", this.Request.RequestParameters.GetQueryStringValue<string[]>("routing"));

		bool? ISearchRequest.IgnoreUnavalable => this.Request.RequestParameters.GetQueryStringValue<bool?>("ignore_unavailable");

		Type ISearchRequest.ClrType => typeof(T);

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

		IDictionary<IndexNameMarker, double> ISearchRequest.IndicesBoost { get; set; }

		IList<ISort> ISearchRequest.Sort { get; set; }

		IDictionary<string, ISuggestBucket> ISearchRequest.Suggest { get; set; }

		IHighlightRequest ISearchRequest.Highlight { get; set; }

		IRescore ISearchRequest.Rescore { get; set; }

		IQueryContainer ISearchRequest.Query { get; set; }

		IQueryContainer ISearchRequest.PostFilter { get; set; }

		IList<PropertyPathMarker> ISearchRequest.Fields { get; set; }

		IList<PropertyPathMarker> ISearchRequest.FielddataFields { get; set; }

		IDictionary<string, IScriptQuery> ISearchRequest.ScriptFields { get; set; }

		ISourceFilter ISearchRequest.Source { get; set; }

		IDictionary<string, IAggregationContainer> ISearchRequest.Aggregations { get; set; }

		IDictionary<string, IInnerHitsContainer> ISearchRequest.InnerHits { get; set; }

		Func<dynamic, Hit<dynamic>, Type> ISearchRequest.TypeSelector { get; set; }


		//TODO probably remove this when we normalize sorting
		private void AddSort(ISort sort)
		{
			Self.Sort = Self.Sort ?? new List<ISort>();
			Self.Sort.Add(sort);
		}

		/// <summary>
		/// When strict is set, conditionless queries are treated as an exception. 
		/// </summary>
		public SearchDescriptor<T> Strict(bool strict = true) => _assign(a => this._Strict = strict);

		public SearchDescriptor<T> Aggregations(Func<AggregationDescriptor<T>, IAggregationContainer> aggregationsSelector) =>
			_assign(a=>a.Aggregations = aggregationsSelector(new AggregationDescriptor<T>())?.Aggregations.NullIfNoKeys());
		
		//TODO refactor!
		public SearchDescriptor<T> InnerHits(
			Func<
				FluentDictionary<string, Func<InnerHitsContainerDescriptor<T>, IInnerHitsContainer>>, 
				FluentDictionary<string, Func<InnerHitsContainerDescriptor<T>, IInnerHitsContainer>>
			> innerHitsSelector)
		{
			if (innerHitsSelector == null)
			{
				Self.InnerHits = null;
				return this;
			}
			var containers = innerHitsSelector(new FluentDictionary<string, Func<InnerHitsContainerDescriptor<T>, IInnerHitsContainer>>())
				.Where(kv => kv.Value != null)
				.Select(kv => new {Key = kv.Key, Value = kv.Value(new InnerHitsContainerDescriptor<T>())})
				.Where(kv => kv.Value != null)
				.ToDictionary(kv => kv.Key, kv => kv.Value);
			if (containers == null || containers.Count == 0)
			{
				Self.InnerHits = null;
				return this;
			}
			Self.InnerHits = containers;
			return this;
		}

		public SearchDescriptor<T> Source(bool include = true)=> _assign(a => a.Source = !include ? SourceFilter.ExcludeAll : null);
		
		public SearchDescriptor<T> Source(Func<SearchSourceDescriptor<T>, SearchSourceDescriptor<T>> sourceSelector) =>
			_assign(a => a.Source = sourceSelector?.Invoke(new SearchSourceDescriptor<T>()));

		/// <summary>
		/// The number of hits to return. Defaults to 10. When using scroll search type 
		/// size is actually multiplied by the number of shards!
		/// </summary>
		public SearchDescriptor<T> Size(int size) => _assign(a => a.Size = size);

		/// <summary>
		/// The number of hits to return. Defaults to 10.
		/// </summary>
		public SearchDescriptor<T> Take(int take) => this.Size(take);

		/// <summary>
		/// The starting from index of the hits to return. Defaults to 0.
		/// </summary>
		public SearchDescriptor<T> From(int from) => _assign(a => a.From = from);

		/// <summary>
		/// The starting from index of the hits to return. Defaults to 0.
		/// </summary>
		public SearchDescriptor<T> Skip(int skip) => this.From(skip);

		/// <summary>
		/// A search timeout, bounding the search request to be executed within the 
		/// specified time value and bail with the hits accumulated up
		/// to that point when expired. Defaults to no timeout.
		/// </summary>
		public SearchDescriptor<T> Timeout(string timeout) => _assign(a => a.Timeout = timeout);

		/// <summary>
		/// Enables explanation for each hit on how its score was computed. 
		/// (Use .DocumentsWithMetaData on the return results)
		/// </summary>
		public SearchDescriptor<T> Explain(bool explain = true) => _assign(a => a.Explain = explain);

		/// <summary>
		/// Returns a version for each search hit. (Use .DocumentsWithMetaData on the return results)
		/// </summary>
		public SearchDescriptor<T> Version(bool version = true) => _assign(a => a.Version = version);

		/// <summary>
		/// Make sure we keep calculating score even if we are sorting on a field.
		/// </summary>
		public SearchDescriptor<T> TrackScores(bool trackscores = true) => _assign(a => a.TrackScores = trackscores);

		/// <summary>
		/// Allows to filter out documents based on a minimum score:
		/// </summary>
		public SearchDescriptor<T> MinScore(double minScore) => _assign(a => a.MinScore = minScore);

		/// <summary>
		/// The maximum number of documents to collect for each shard, upon reaching which the query execution will terminate early. 
		/// If set, the response will have a boolean field terminated_early to indicate whether the query execution has actually terminated_early. 
		/// </summary>
		public SearchDescriptor<T> TerminateAfter(long terminateAfter) => _assign(a => a.TerminateAfter = terminateAfter);

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
		public SearchDescriptor<T> IndicesBoost(Func<FluentDictionary<IndexNameMarker, double>, FluentDictionary<IndexNameMarker, double>> boost) =>
			_assign(a => a.IndicesBoost = boost?.Invoke(new FluentDictionary<IndexNameMarker, double>()));

		/// <summary>
		/// Allows to selectively load specific fields for each document 
		/// represented by a search hit. Defaults to load the internal _source field.
		/// </summary>
		public SearchDescriptor<T> Fields(params Expression<Func<T, object>>[] expressions) =>
			_assign(a => a.Fields = expressions?.Select(e => (PropertyPathMarker) e).ToListOrNullIfEmpty());

		/// <summary>
		/// Allows to selectively load specific fields for each document 
		/// represented by a search hit. Defaults to load the internal _source field.
		/// </summary>
		public SearchDescriptor<T> Fields(Func<FluentFieldList<T>, FluentFieldList<T>> properties) =>
			_assign(a => a.Fields = Self.Fields = properties?.Invoke(new FluentFieldList<T>()).ToListOrNullIfEmpty());

		/// <summary>
		/// Allows to selectively load specific fields for each document 
		/// represented by a search hit. Defaults to load the internal _source field.
		/// </summary>
		public SearchDescriptor<T> Fields(params string[] fields)
			=> _assign(a => a.Fields = fields?.Select(f => (PropertyPathMarker) f).ToListOrNullIfEmpty());

		///<summary>
		///A comma-separated list of fields to return as the field data representation of a field for each hit
		///</summary>
		public SearchDescriptor<T> FielddataFields(params string[] fielddataFields) =>
			_assign(a => a.FielddataFields = fielddataFields?.Select(f => (PropertyPathMarker) f).ToListOrNullIfEmpty());

		///<summary>
		///A comma-separated list of fields to return as the field data representation of a field for each hit
		///</summary>
		public SearchDescriptor<T> FielddataFields(params Expression<Func<T, object>>[] fielddataFields) =>
			_assign(a => a.FielddataFields = fielddataFields?.Select(f => (PropertyPathMarker) f).ToListOrNullIfEmpty());
		
		//TODO scriptfields needs a seperate encapsulation
		public SearchDescriptor<T> ScriptFields(
				Func<FluentDictionary<string, Func<ScriptQueryDescriptor<T>, ScriptQueryDescriptor<T>>>,
				 FluentDictionary<string, Func<ScriptQueryDescriptor<T>, ScriptQueryDescriptor<T>>>> scriptFields)
		{
			scriptFields.ThrowIfNull("scriptFields");
			var scriptFieldDescriptors = scriptFields(new FluentDictionary<string, Func<ScriptQueryDescriptor<T>, ScriptQueryDescriptor<T>>>());
			if (scriptFieldDescriptors == null || scriptFieldDescriptors.All(d => d.Value == null))
			{
				Self.ScriptFields = null;
				return this;
			}
			Self.ScriptFields = new FluentDictionary<string, IScriptQuery>();
			foreach (var d in scriptFieldDescriptors)
			{
				if (d.Value == null)
					continue;
				Self.ScriptFields.Add(d.Key, d.Value(new ScriptQueryDescriptor<T>()));
			}
			return this;
		}

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
		public SearchDescriptor<T> SuggestTerm(string name, Func<TermSuggestDescriptor<T>, TermSuggestDescriptor<T>> suggest)
		{
			name.ThrowIfNullOrEmpty("name");
			suggest.ThrowIfNull("suggest");
			if (Self.Suggest == null)
				Self.Suggest = new Dictionary<string, ISuggestBucket>();
			var desc = new TermSuggestDescriptor<T>();
			var item = suggest(desc);
			ITermSuggester i = item;
			var bucket = new SuggestBucket { Text = i.Text, Term = item };
			Self.Suggest.Add(name, bucket);
			return this;
		}

		/// <summary>
		/// The phrase suggester adds additional logic on top of the term suggester to select entire corrected phrases 
		/// instead of individual tokens weighted based on ngram-langugage models. 
		/// </summary>
		public SearchDescriptor<T> SuggestPhrase(string name, Func<PhraseSuggestDescriptor<T>, PhraseSuggestDescriptor<T>> suggest)
		{
			name.ThrowIfNullOrEmpty("name");
			suggest.ThrowIfNull("suggest");
			if (Self.Suggest == null)
				Self.Suggest = new Dictionary<string, ISuggestBucket>();

			var desc = new PhraseSuggestDescriptor<T>();
			var item = suggest(desc);
			IPhraseSuggester i = item;
			var bucket = new SuggestBucket { Text = i.Text, Phrase = item };
			Self.Suggest.Add(name, bucket);
			return this;
		}

		/// <summary>
		/// The completion suggester is a so-called prefix suggester. 
		/// It does not do spell correction like the term or phrase suggesters but allows basic auto-complete functionality.
		/// </summary>
		public SearchDescriptor<T> SuggestCompletion(string name, Func<CompletionSuggestDescriptor<T>, CompletionSuggestDescriptor<T>> suggest)
		{
			name.ThrowIfNullOrEmpty("name");
			suggest.ThrowIfNull("suggest");
			if (Self.Suggest == null)
				Self.Suggest = new Dictionary<string, ISuggestBucket>();

			var desc = new CompletionSuggestDescriptor<T>();
			var item = suggest(desc);
			ICompletionSuggester i = item;
			var bucket = new SuggestBucket { Text = i.Text, Completion = item };
			Self.Suggest.Add(name, bucket);
			return this;
		}

		/// <summary>
		/// Describe the query to perform using a query descriptor lambda
		/// </summary>
		public SearchDescriptor<T> Query(Func<QueryDescriptor<T>, QueryContainer> query)
		{
			query.ThrowIfNull("query");
			var q = new QueryDescriptor<T>();
			((IQueryContainer)q).IsStrict = this._Strict;
			var bq = query(q);
			return this.Query(bq);
		}

		public SearchDescriptor<T> Query(QueryContainer query)
		{
			return this.Query((IQueryContainer)query);
		}

		/// <summary>
		/// Describe the query to perform using the static Query class
		/// </summary>
		public SearchDescriptor<T> Query(IQueryContainer query)
		{
			if (query == null)
				return this;

			if (this._Strict && query.IsConditionless)
				throw new DslException("Query resulted in a conditionless query:\n{0}".F(JsonConvert.SerializeObject(query, Formatting.Indented)));

			else if (query.IsConditionless)
				return this;
			Self.Query = query;
			return this;

		}

		/// <summary>
		/// Shortcut to .Query(q=>q.QueryString(qs=>qs.Query("string"))
		/// Does a match_all if the userInput string is null or empty;
		/// </summary>
		public SearchDescriptor<T> QueryString(string userInput)
		{
			var q = new QueryDescriptor<T>();
			QueryContainer bq;
			if (userInput.IsNullOrEmpty())
				bq = q.MatchAll();
			else
				bq = q.QueryString(qs => qs.Query(userInput));
			Self.Query = bq;
			return this;
		}

		/// <summary>
		/// Describe the query to perform as a raw json string
		/// </summary>
		public SearchDescriptor<T> QueryRaw(string rawQuery)
		{
			Self.Query = new QueryDescriptor<T>().Raw(rawQuery);
			return this;
		}

		/// <summary>
		/// Filter search using a filter descriptor lambda
		/// </summary>
		public SearchDescriptor<T> PostFilter(Func<QueryDescriptor<T>, QueryContainer> filter)
		{
			filter.ThrowIfNull("filter");
			var f = new QueryDescriptor<T>().Strict(this._Strict);

			var bf = filter(f);
			if (bf == null)
				return this;
			if (this._Strict && bf.IsConditionless)
				throw new DslException("Filter resulted in a conditionless filter:\n{0}".F(JsonConvert.SerializeObject(bf, Formatting.Indented)));

			else if (bf.IsConditionless)
				return this;


			Self.PostFilter = bf;
			return this;
		}

		/// <summary>
		/// Filter search
		/// </summary>
		public SearchDescriptor<T> PostFilter(QueryContainer QueryDescriptor)
		{
			QueryDescriptor.ThrowIfNull("filter");
			Self.PostFilter = QueryDescriptor;
			return this;
		}

		/// <summary>
		/// Filter search using a raw json string
		/// </summary>
		public SearchDescriptor<T> FilterRaw(string rawFilter)
		{
			Self.PostFilter = new QueryDescriptor<T>().Raw(rawFilter);
			return this;
		}

		/// <summary>
		/// Allow to highlight search results on one or more fields. The implementation uses the either lucene fast-vector-highlighter or highlighter. 
		/// </summary>
		public SearchDescriptor<T> Highlight(Func<HighlightDescriptor<T>, IHighlightRequest> highlightSelector) =>
			_assign(a => a.Highlight = highlightSelector?.Invoke(new HighlightDescriptor<T>()));
		
		/// <summary>
		/// Allows you to specify a rescore query
		/// </summary>
		public SearchDescriptor<T> Rescore(Func<RescoreDescriptor<T>, IRescore> rescoreSelector) =>
			_assign(a => a.Rescore = rescoreSelector?.Invoke(new RescoreDescriptor<T>()));
		
		/// <summary>
		/// Shorthand for a match_all query without having to specify .Query(q=>q.MatchAll())
		/// </summary>
		public SearchDescriptor<T> MatchAll() => this.Query(q => q.MatchAll());

		public SearchDescriptor<T> ConcreteTypeSelector(Func<dynamic, Hit<dynamic>, Type> typeSelector) =>
			_assign(a => a.TypeSelector = typeSelector);

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SearchRequestParameters> pathInfo) =>
			SearchPathInfo.Update(settings,pathInfo, this);

	}
}
