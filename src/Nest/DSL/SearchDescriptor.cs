using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;
using Nest.DSL.Descriptors;

namespace Nest
{

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
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
		IList<KeyValuePair<PropertyPathMarker, ISort>> Sort { get; set; }

		[JsonProperty(PropertyName = "facets")]
		[JsonConverter(typeof (DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<PropertyPathMarker, IFacetContainer> Facets { get; set; }

		[JsonProperty(PropertyName = "suggest")]
		IDictionary<string, ISuggestBucket> Suggest { get; set; }

		[JsonProperty(PropertyName = "highlight")]
		IHighlightRequest Highlight { get; set; }

		[JsonProperty(PropertyName = "rescore")]
		IRescore Rescore { get; set; }

		[JsonProperty(PropertyName = "fields")]
		IList<PropertyPathMarker> Fields { get; set; }

		[JsonProperty(PropertyName = "script_fields")]
		[JsonConverter(typeof (DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<string, IScriptFilter> ScriptFields { get; set; }

		[JsonProperty(PropertyName = "_source")]
		ISourceFilter Source { get; set; }

		[JsonProperty(PropertyName = "aggs")]
		[JsonConverter(typeof (DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<string, IAggregationContainer> Aggregations { get; set; }

		[JsonProperty(PropertyName = "query")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<QueryContainer>, CustomJsonConverter>))]
		IQueryContainer Query { get; set; }

		[JsonProperty(PropertyName = "filter")]
		[JsonConverter(typeof(CompositeJsonConverter<ReadAsTypeConverter<FilterContainer>, CustomJsonConverter>))]
		IFilterContainer Filter { get; set; }

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
		public IDictionary<string, IScriptFilter> ScriptFields { get; set; }
		public ISourceFilter Source { get; set; }
		public IList<KeyValuePair<PropertyPathMarker, ISort>> Sort { get; set; }
		public IDictionary<IndexNameMarker, double> IndicesBoost { get; set; }
		public IFilterContainer Filter { get; set; }
		public IQueryContainer Query { get; set; }
		public IRescore Rescore { get; set; }
		public IDictionary<PropertyPathMarker, IFacetContainer> Facets { get; set; }
		public IDictionary<string, ISuggestBucket> Suggest { get; set; }
		public IHighlightRequest Highlight { get; set; }
		public IDictionary<string, IAggregationContainer> Aggregations { get; set; }

		SearchType? ISearchRequest.SearchType
		{
			get { return  this.QueryString == null ? null : this.QueryString.GetQueryStringValue<SearchType?>("search_type");  }
		}

		string ISearchRequest.Preference
		{
			get { return this.QueryString == null ? null : this.QueryString.GetQueryStringValue<string>("preference"); }
		}

		string ISearchRequest.Routing
		{
			get
			{
				if (this.QueryString == null)
					return null;
				var routing = this.QueryString.GetQueryStringValue<string[]>("routing");
				return routing == null
					? null
					: string.Join(",", routing);
			}
		}

		bool? ISearchRequest.IgnoreUnavalable
		{
			get { return this.QueryString == null ? null : this.QueryString.GetQueryStringValue<bool?>("ignore_unavailable"); }
		}

		public Func<dynamic, Hit<dynamic>, Type> TypeSelector { get; set; }

		public SearchRequestParameters QueryString { get; set; }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SearchRequestParameters> pathInfo)
		{
			SearchPathInfo.Update(settings, pathInfo, this);
		}

	}

	public partial class SearchRequest<T> : QueryPathBase<SearchRequestParameters, T>, ISearchRequest
		where T : class
	{
		public SearchRequest() {}

		public SearchRequest(IndexNameMarker index, TypeNameMarker type = null) : base(index, type) { }

		public SearchRequest(IEnumerable<IndexNameMarker> indices, IEnumerable<TypeNameMarker> types = null) : base(indices, types) { }

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SearchRequestParameters> pathInfo)
		{
			SearchPathInfo.Update(settings,pathInfo, this);
		}

		public Type ClrType { get { return typeof(T);  } }
		public string Timeout { get; set; }
		public int? From { get; set; }
		public int? Size { get; set; }
		public bool? Explain { get; set; }
		public bool? Version { get; set; }
		public bool? TrackScores { get; set; }
		public double? MinScore { get; set; }
		public long? TerminateAfter { get; set; }
		public IDictionary<IndexNameMarker, double> IndicesBoost { get; set; }
		public IList<KeyValuePair<PropertyPathMarker, ISort>> Sort { get; set; }
		public IDictionary<PropertyPathMarker, IFacetContainer> Facets { get; set; }
		public IDictionary<string, ISuggestBucket> Suggest { get; set; }
		public IHighlightRequest Highlight { get; set; }
		public IRescore Rescore { get; set; }
		public IList<PropertyPathMarker> Fields { get; set; }
		public IDictionary<string, IScriptFilter> ScriptFields { get; set; }
		public ISourceFilter Source { get; set; }
		public IDictionary<string, IAggregationContainer> Aggregations { get; set; }
		public IQueryContainer Query { get; set; }
		public IFilterContainer Filter { get; set; }
		SearchType? ISearchRequest.SearchType
		{
			get { return  this.QueryString == null ? null : this.QueryString.GetQueryStringValue<SearchType?>("search_type");  }
		}

		string ISearchRequest.Preference
		{
			get { return this.QueryString == null ? null : this.QueryString.GetQueryStringValue<string>("preference"); }
		}

		string ISearchRequest.Routing
		{
			get
			{
				if (this.QueryString == null)
					return null;
				var routing = this.QueryString.GetQueryStringValue<string[]>("routing");
				return routing == null
					? null
					: string.Join(",", routing);
			}
		}

		bool? ISearchRequest.IgnoreUnavalable
		{
			get { return this.QueryString == null ? null : this.QueryString.GetQueryStringValue<bool?>("ignore_unavailable");  }
		}

		public Func<dynamic, Hit<dynamic>, Type> TypeSelector { get; set; }
		public SearchRequestParameters QueryString { get; set; }
	}


	/// <summary>
	/// A descriptor wich describes a search operation for _search and _msearch
	/// </summary>
	public partial class SearchDescriptor<T> : QueryPathDescriptorBase<SearchDescriptor<T>, SearchRequestParameters, T>, ISearchRequest 
		where T : class
	{
		private ISearchRequest Self { get { return this; } }

		SearchType? ISearchRequest.SearchType
		{
			get { return this.Request.RequestParameters.GetQueryStringValue<SearchType?>("search_type");  }
		}

		SearchRequestParameters ISearchRequest.QueryString
		{
			get { return this.Request.RequestParameters;  }
			set { this.Request.RequestParameters = value;  }
		}

		string ISearchRequest.Preference
		{
			get { return this.Request.RequestParameters.GetQueryStringValue<string>("preference"); }
		}

		string ISearchRequest.Routing
		{
			get
			{
				var routing = this.Request.RequestParameters.GetQueryStringValue<string[]>("routing");
				return routing == null
					? null
					: string.Join(",", routing);
			}
		}

		bool? ISearchRequest.IgnoreUnavalable
		{
			get { return this.Request.RequestParameters.GetQueryStringValue<bool?>("ignore_unavailable"); }
		}

		Type ISearchRequest.ClrType { get { return typeof(T); } }

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

		IList<KeyValuePair<PropertyPathMarker, ISort>> ISearchRequest.Sort { get; set; }

		IDictionary<PropertyPathMarker, IFacetContainer> ISearchRequest.Facets { get; set; }

		IDictionary<string, ISuggestBucket> ISearchRequest.Suggest { get; set; }

		IHighlightRequest ISearchRequest.Highlight { get; set; }

		IRescore ISearchRequest.Rescore { get; set; }

		IQueryContainer ISearchRequest.Query { get; set; }

		IFilterContainer ISearchRequest.Filter { get; set; }

		IList<PropertyPathMarker> ISearchRequest.Fields { get; set; }

		IDictionary<string, IScriptFilter> ISearchRequest.ScriptFields { get; set; }

		ISourceFilter ISearchRequest.Source { get; set; }

		IDictionary<string, IAggregationContainer> ISearchRequest.Aggregations { get; set; }

		Func<dynamic, Hit<dynamic>, Type> ISearchRequest.TypeSelector { get; set; }

		/// <summary>
		/// When strict is set, conditionless queries are treated as an exception. 
		/// </summary>
		public SearchDescriptor<T> Strict(bool strict = true)
		{
			this._Strict = strict;
			return this;
		}


		public SearchDescriptor<T> Aggregations(Func<AggregationDescriptor<T>, AggregationDescriptor<T>> aggregationsSelector)
		{
			var aggs = aggregationsSelector(new AggregationDescriptor<T>());
			if (aggs == null) return this;
			Self.Aggregations = ((IAggregationContainer)aggs).Aggregations;
			return this;
		}


		public SearchDescriptor<T> Source(bool include = true)
		{
			if (!include)
			{
				Self.Source = new SourceFilter
				{
					Exclude = new PropertyPathMarker[] {"*"}
				};
			}
			else Self.Source = null;
			return this;
		}

		public SearchDescriptor<T> Source(Func<SearchSourceDescriptor<T>, SearchSourceDescriptor<T>> sourceSelector)
		{
			Self.Source = sourceSelector(new SearchSourceDescriptor<T>());
			return this;
		}
		/// <summary>
		/// The number of hits to return. Defaults to 10. When using scroll search type 
		/// size is actually multiplied by the number of shards!
		/// </summary>
		public SearchDescriptor<T> Size(int size)
		{
			Self.Size = size;
			return this;
		}
		/// <summary>
		/// The number of hits to return. Defaults to 10.
		/// </summary>
		public SearchDescriptor<T> Take(int take)
		{
			return this.Size(take);
		}
		/// <summary>
		/// The starting from index of the hits to return. Defaults to 0.
		/// </summary>
		public SearchDescriptor<T> From(int from)
		{
			Self.From = from;
			return this;
		}
		/// <summary>
		/// The starting from index of the hits to return. Defaults to 0.
		/// </summary>
		public SearchDescriptor<T> Skip(int skip)
		{
			return this.From(skip);
		}
		/// <summary>
		/// A search timeout, bounding the search request to be executed within the 
		/// specified time value and bail with the hits accumulated up
		/// to that point when expired. Defaults to no timeout.
		/// </summary>
		public SearchDescriptor<T> Timeout(string timeout)
		{
			Self.Timeout = timeout;
			return this;
		}
		/// <summary>
		/// Enables explanation for each hit on how its score was computed. 
		/// (Use .DocumentsWithMetaData on the return results)
		/// </summary>
		public SearchDescriptor<T> Explain(bool explain = true)
		{
			Self.Explain = explain;
			return this;
		}
		/// <summary>
		/// Returns a version for each search hit. (Use .DocumentsWithMetaData on the return results)
		/// </summary>
		public SearchDescriptor<T> Version(bool version = true)
		{
			Self.Version = version;
			return this;
		}
		/// <summary>
		/// Make sure we keep calculating score even if we are sorting on a field.
		/// </summary>
		public SearchDescriptor<T> TrackScores(bool trackscores = true)
		{
			Self.TrackScores = trackscores;
			return this;
		}
		/// <summary>
		/// Allows to filter out documents based on a minimum score:
		/// </summary>
		public SearchDescriptor<T> MinScore(double minScore)
		{
			Self.MinScore = minScore;
			return this;
		}

		/// <summary>
		/// The maximum number of documents to collect for each shard, upon reaching which the query execution will terminate early. 
		/// If set, the response will have a boolean field terminated_early to indicate whether the query execution has actually terminated_early. 
		/// </summary>
		public SearchDescriptor<T> TerminateAfter(long terminateAfter)
		{
			Self.TerminateAfter = terminateAfter;
			return this;
		}

		/// <summary>
		/// <para>
		/// Controls a preference of which shard replicas to execute the search request on. 
		/// By default, the operation is randomized between the each shard replicas.
		/// </para>
		/// <para>
		/// The operation will go and be executed only on the primary shards.
		/// </para>
		/// </summary>
		public SearchDescriptor<T> ExecuteOnPrimary()
		{
			return this.Preference("_primary");
		}
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
		public SearchDescriptor<T> ExecuteOnPrimaryFirst()
		{
			return this.Preference("_primary_first");
		}
		/// <summary>
		/// <para>
		/// Controls a preference of which shard replicas to execute the search request on. 
		/// By default, the operation is randomized between the each shard replicas.
		/// </para>
		/// <para>
		/// The operation will prefer to be executed on a local allocated shard is possible.
		/// </para>
		/// </summary>
		public SearchDescriptor<T> ExecuteOnLocalShard()
		{
			return this.Preference("_local");
		}
		/// <summary>
		/// <para>
		/// Controls a preference of which shard replicas to execute the search request on. 
		/// By default, the operation is randomized between the each shard replicas.
		/// </para>
		/// <para>
		/// Restricts the search to execute only on a node with the provided node id
		/// </para>
		/// </summary>
		public SearchDescriptor<T> ExecuteOnNode(string node)
		{
			node.ThrowIfNull("node");
			return this.Preference("_only_node:" + node);
		}
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
			this.Preference(string.Format("_prefer_node:{0}", node));
			return this;
		}
		/// <summary>
		/// Allows to configure different boost level per index when searching across 
		/// more than one indices. This is very handy when hits coming from one index
		/// matter more than hits coming from another index (think social graph where each user has an index).
		/// </summary>
		public SearchDescriptor<T> IndicesBoost(
			Func<FluentDictionary<IndexNameMarker, double>, FluentDictionary<IndexNameMarker, double>> boost)
		{
			boost.ThrowIfNull("boost");
			Self.IndicesBoost = boost(new FluentDictionary<IndexNameMarker, double>());
			return this;
		}
		/// <summary>
		/// Allows to selectively load specific fields for each document 
		/// represented by a search hit. Defaults to load the internal _source field.
		/// </summary>
		public SearchDescriptor<T> Fields(params Expression<Func<T, object>>[] expressions)
		{
			Self.Fields = expressions.Select(e => (PropertyPathMarker)e).ToList();
			return this;
		}
		
		/// <summary>
		/// Allows to selectively load specific fields for each document 
		/// represented by a search hit. Defaults to load the internal _source field.
		/// </summary>
		public SearchDescriptor<T> Fields(Func<FluentFieldList<T>, FluentFieldList<T>> properties)
		{
			Self.Fields = properties(new FluentFieldList<T>()).ToList();
			return this;
		}
		/// <summary>
		/// Allows to selectively load specific fields for each document 
		/// represented by a search hit. Defaults to load the internal _source field.
		/// </summary>
		public SearchDescriptor<T> Fields(params string[] fields)
		{
			Self.Fields = fields.Select(f => (PropertyPathMarker)f).ToList();
			return this;
		}

		public SearchDescriptor<T> ScriptFields(
				Func<FluentDictionary<string, Func<ScriptFilterDescriptor, ScriptFilterDescriptor>>,
				 FluentDictionary<string, Func<ScriptFilterDescriptor, ScriptFilterDescriptor>>> scriptFields)
		{
			scriptFields.ThrowIfNull("scriptFields");
			var scriptFieldDescriptors = scriptFields(new FluentDictionary<string, Func<ScriptFilterDescriptor, ScriptFilterDescriptor>>());
			if (scriptFieldDescriptors == null || scriptFieldDescriptors.All(d => d.Value == null))
			{
				Self.ScriptFields = null;
				return this;
			}
			Self.ScriptFields = new FluentDictionary<string, IScriptFilter>();
			foreach (var d in scriptFieldDescriptors)
			{
				if (d.Value == null)
					continue;
				Self.ScriptFields.Add(d.Key, d.Value(new ScriptFilterDescriptor()));
			}
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
			if (Self.Sort == null) Self.Sort = new List<KeyValuePair<PropertyPathMarker, ISort>>();

			Self.Sort.Add(new KeyValuePair<PropertyPathMarker, ISort>(objectPath, new Sort() { Order = SortOrder.Ascending}));
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
			if (Self.Sort == null) Self.Sort = new List<KeyValuePair<PropertyPathMarker, ISort>>();

			Self.Sort.Add(new KeyValuePair<PropertyPathMarker, ISort>(objectPath, new Sort() { Order = SortOrder.Descending }));
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
			if (Self.Sort == null) Self.Sort = new List<KeyValuePair<PropertyPathMarker, ISort>>();
			Self.Sort.Add(new KeyValuePair<PropertyPathMarker, ISort>(field, new Sort() { Order = SortOrder.Ascending }));
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
			if (Self.Sort == null)
				Self.Sort = new List<KeyValuePair<PropertyPathMarker, ISort>>();

			Self.Sort.Add(new KeyValuePair<PropertyPathMarker, ISort>(field, new Sort() { Order = SortOrder.Descending}));
			return this;
		}

		/// <summary>
		/// <para>Sort() allows you to fully describe your sort unlike the SortAscending and SortDescending aliases.
		/// </para>
		/// </summary>
		public SearchDescriptor<T> Sort(Func<SortFieldDescriptor<T>, IFieldSort> sortSelector)
		{
			if (Self.Sort == null)
				Self.Sort = new List<KeyValuePair<PropertyPathMarker, ISort>>();

			sortSelector.ThrowIfNull("sortSelector");
			var descriptor = sortSelector(new SortFieldDescriptor<T>());
			Self.Sort.Add(new KeyValuePair<PropertyPathMarker, ISort>(descriptor.Field, descriptor));
			return this;
		}

		/// <summary>
		/// <para>SortGeoDistance() allows you to sort by a distance from a geo point.
		/// </para>
		/// </summary>
		public SearchDescriptor<T> SortGeoDistance(Func<SortGeoDistanceDescriptor<T>, IGeoDistanceSort> sortSelector)
		{
			if (Self.Sort == null)
				Self.Sort = new List<KeyValuePair<PropertyPathMarker, ISort>>();

			sortSelector.ThrowIfNull("sortSelector");
			var descriptor = sortSelector(new SortGeoDistanceDescriptor<T>());
			Self.Sort.Add(new KeyValuePair<PropertyPathMarker, ISort>("_geo_distance", descriptor));
			return this;
		}

		/// <summary>
		/// <para>SortScript() allows you to sort by a distance from a geo point.
		/// </para>
		/// </summary>
		public SearchDescriptor<T> SortScript(Func<SortScriptDescriptor<T>, IScriptSort> sortSelector)
		{
			if (Self.Sort == null)
				Self.Sort = new List<KeyValuePair<PropertyPathMarker, ISort>>();

			sortSelector.ThrowIfNull("sortSelector");
			var descriptor = sortSelector(new SortScriptDescriptor<T>());
			Self.Sort.Add(new KeyValuePair<PropertyPathMarker, ISort>("_script", descriptor));
			return this;
		}

		private SearchDescriptor<T> _Facet<F, FI>(
			string name,
			Func<F, F> facet,
			Func<FI, PropertyPathMarker> inferedFieldNameSelector,
			Action<FacetContainer, F> fillBucket
			)
			where F : IFacetRequest, FI, new()
			
		{
			facet.ThrowIfNull("facet");
			inferedFieldNameSelector.ThrowIfNull("inferedFieldNameSelector");
			fillBucket.ThrowIfNull("fillBucket");

			if (Self.Facets == null)
				Self.Facets = new Dictionary<PropertyPathMarker, IFacetContainer>();

			var descriptor = new F();
			var f = facet(descriptor);
			var key = string.IsNullOrWhiteSpace(name) ? inferedFieldNameSelector(descriptor) : name;
			if (key.IsConditionless())
			{
				throw new DslException(
					"Couldn't infer name for facet of type {0}".F(typeof(F).Name)
				);
			}
			var bucket = new FacetContainer();
			bucket.Global = f.Global;
			bucket.FacetFilter = f.FacetFilter;
			f.FacetFilter = null;
			bucket.Nested = f.Nested;
			fillBucket(bucket, descriptor);
			Self.Facets.Add(key, bucket);

			return this;
		}


		/// <summary>
		/// Allow to specify field facets that return the N most frequent terms.
		/// </summary>
		public SearchDescriptor<T> FacetTerm(string name, Func<TermFacetDescriptor<T>, TermFacetDescriptor<T>> facet)
		{
			return this.FacetTerm(facet, Name: name);
		}

		/// <summary>
		/// Allow to specify field facets that return the N most frequent terms.
		/// </summary>
		public SearchDescriptor<T> FacetTerm(Func<TermFacetDescriptor<T>, TermFacetDescriptor<T>> facet, string Name = null)
		{
			return this._Facet<TermFacetDescriptor<T>, ITermFacetRequest>(
				Name,
				facet,
				(d) => d.Field,
				(b, d) => b.Terms = d
			);
		}

		/// <summary>
		/// range facet allow to specify a set of ranges and get both the number of docs (count) 
		/// that fall within each range, and aggregated data either based on the field, or using another field
		/// </summary>
		/// <typeparam name="K">struct, (int, double, string, DateTime)</typeparam>
		public SearchDescriptor<T> FacetRange<K>(string name, Func<RangeFacetDescriptor<T, K>, RangeFacetDescriptor<T, K>> facet) where K : struct
		{
			return this.FacetRange<K>(facet, Name: name);
		}
		/// <summary>
		/// range facet allow to specify a set of ranges and get both the number of docs (count) 
		/// that fall within each range, and aggregated data either based on the field, or using another field
		/// </summary>
		/// <typeparam name="K">struct, (int, double, string, DateTime)</typeparam>
		public SearchDescriptor<T> FacetRange<K>(Func<RangeFacetDescriptor<T, K>, RangeFacetDescriptor<T, K>> facet, string Name = null) where K : struct
		{
			return this._Facet<RangeFacetDescriptor<T, K>, IRangeFacetRequest<K>>(
				Name,
				facet,
				(d) => d.Field,
				(b, d) => b.Range = d
			);
		}
		/// <summary>
		/// The histogram facet works with numeric data by building a histogram across intervals 
		/// of the field values. Each value is “rounded” into an interval (or placed in a bucket), 
		/// and statistics are provided per interval/bucket (count and total). 
		/// </summary>
		public SearchDescriptor<T> FacetHistogram(string name, Func<HistogramFacetDescriptor<T>, HistogramFacetDescriptor<T>> facet)
		{
			return this.FacetHistogram(facet, Name: name);
		}
		/// <summary>
		/// The histogram facet works with numeric data by building a histogram across intervals 
		/// of the field values. Each value is “rounded” into an interval (or placed in a bucket), 
		/// and statistics are provided per interval/bucket (count and total). 
		/// </summary>
		public SearchDescriptor<T> FacetHistogram(Func<HistogramFacetDescriptor<T>, HistogramFacetDescriptor<T>> facet, string Name = null)
		{
			return this._Facet<HistogramFacetDescriptor<T>, IHistogramFacetRequest>(
				Name,
				facet,
				(d) => d.Field,
				(b, d) => b.Histogram = d
			);
		}
		/// <summary>
		/// A specific histogram facet that can work with date field types enhancing it over the regular histogram facet.
		/// </summary>
		public SearchDescriptor<T> FacetDateHistogram(string name, Func<DateHistogramFacetDescriptor<T>, DateHistogramFacetDescriptor<T>> facet)
		{
			return this.FacetDateHistogram(facet, Name: name);
		}
		/// <summary>
		/// A specific histogram facet that can work with date field types enhancing it over the regular histogram facet.
		/// </summary>
		public SearchDescriptor<T> FacetDateHistogram(Func<DateHistogramFacetDescriptor<T>, DateHistogramFacetDescriptor<T>> facet, string Name = null)
		{
			return this._Facet<DateHistogramFacetDescriptor<T>, IDateHistogramFacetRequest>(
				Name,
				facet,
				(d) => d.Field,
				(b, d) => b.DateHistogram = d
			);
		}

		/// <summary>
		/// Statistical facet allows to compute statistical data on a numeric fields. 
		/// The statistical data include count, total, sum of squares, 
		/// mean (average), minimum, maximum, variance, and standard deviation. 
		/// </summary>
		public SearchDescriptor<T> FacetStatistical(string name, Func<StatisticalFacetDescriptor<T>, StatisticalFacetDescriptor<T>> facet)
		{
			return this.FacetStatistical(facet, Name: name);
		}

		/// <summary>
		/// Statistical facet allows to compute statistical data on a numeric fields. 
		/// The statistical data include count, total, sum of squares, 
		/// mean (average), minimum, maximum, variance, and standard deviation. 
		/// </summary>
		public SearchDescriptor<T> FacetStatistical(Func<StatisticalFacetDescriptor<T>, StatisticalFacetDescriptor<T>> facet, string Name = null)
		{
			return this._Facet<StatisticalFacetDescriptor<T>, IStatisticalFacetRequest>(
				Name,
				facet,
				(d) => d.Field,
				(b, d) => b.Statistical = d
			);
		}

		/// <summary>
		/// The terms_stats facet combines both the terms and statistical allowing 
		/// to compute stats computed on a field, per term value driven by another field.
		/// </summary>
		public SearchDescriptor<T> FacetTermsStats(string name, Func<TermsStatsFacetDescriptor<T>, TermsStatsFacetDescriptor<T>> facet)
		{
			return this.FacetTermsStats(facet, Name: name);
		}

		/// <summary>
		/// The terms_stats facet combines both the terms and statistical allowing 
		/// to compute stats computed on a field, per term value driven by another field.
		/// </summary>
		public SearchDescriptor<T> FacetTermsStats(Func<TermsStatsFacetDescriptor<T>, TermsStatsFacetDescriptor<T>> facet, string Name = null)
		{
			return this._Facet<TermsStatsFacetDescriptor<T>, ITermsStatsFacetRequest>(
				Name,
				facet,
				(d) => d.KeyField,
				(b, d) => b.TermsStats = d
			);
		}
		/// <summary>
		/// The geo_distance facet is a facet providing information for ranges of distances
		/// from a provided geo_point including count of the number of hits that fall 
		/// within each range, and aggregation information (like total).
		/// </summary>
		public SearchDescriptor<T> FacetGeoDistance(string name, Func<GeoDistanceFacetDescriptor<T>, GeoDistanceFacetDescriptor<T>> facet)
		{
			return this.FacetGeoDistance(facet, Name: name);
		}

		/// <summary>
		/// The geo_distance facet is a facet providing information for ranges of distances
		/// from a provided geo_point including count of the number of hits that fall 
		/// within each range, and aggregation information (like total).
		/// </summary>
		public SearchDescriptor<T> FacetGeoDistance(Func<GeoDistanceFacetDescriptor<T>, GeoDistanceFacetDescriptor<T>> facet, string Name = null)
		{
			return this._Facet<GeoDistanceFacetDescriptor<T>, IGeoDistanceFacetRequest>(
					Name,
					facet,
					(d) => d.ValueField ?? d.Field,
					(b, d) => b.GeoDistance = d
				);
		}

		/// <summary>
		/// A facet query allows to return a count of the hits matching 
		/// the facet query. The query itself can be expressed using the Query DSL.
		/// </summary>
		public SearchDescriptor<T> FacetQuery(string name, Func<QueryDescriptor<T>, QueryContainer> querySelector, bool? Global = null)
		{
			name.ThrowIfNullOrEmpty("name");
			querySelector.ThrowIfNull("query");
			if (Self.Facets == null)
				Self.Facets = new Dictionary<PropertyPathMarker, IFacetContainer>();

			var query = new QueryDescriptor<T>();
			var q = querySelector(query);
			Self.Facets.Add(name, new FacetContainer { Query = q });

			return this;
		}
		/// <summary>
		/// A filter facet (not to be confused with a facet filter) allows you to return a count of the h
		/// its matching the filter. The filter itself can be expressed using the Query DSL.
		/// Note, filter facet filters are faster than query facet when using native filters (non query wrapper ones).
		/// </summary>
		public SearchDescriptor<T> FacetFilter(string name, Func<FilterDescriptor<T>, FilterContainer> filterSelector)
		{
			name.ThrowIfNullOrEmpty("name");
			filterSelector.ThrowIfNull("filterSelector");

			if (Self.Facets == null)
				Self.Facets = new Dictionary<PropertyPathMarker, IFacetContainer>();

			var filter = new FilterDescriptor<T>();
			var f = filterSelector(filter);
			Self.Facets.Add(name, new FacetContainer { Filter = f });

			return this;
		}

		///// <summary>
		///// To avoid repetition of the suggest text, it is possible to define a global text.
		///// </summary>
		//public SearchDescriptor<T> SuggestGlobalText(string globalSuggestText)
		//{
		//	if (Self.Suggest == null)
		//		Self.Suggest = new Dictionary<string, ISuggester>();
		//	Self.Suggest.Add("text", globalSuggestText);
		//	return this;
		//}

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
		public SearchDescriptor<T> Filter(Func<FilterDescriptor<T>, FilterContainer> filter)
		{
			filter.ThrowIfNull("filter");
			var f = new FilterDescriptor<T>().Strict(this._Strict);

			var bf = filter(f);
			if (bf == null)
				return this;
			if (this._Strict && bf.IsConditionless)
				throw new DslException("Filter resulted in a conditionless filter:\n{0}".F(JsonConvert.SerializeObject(bf, Formatting.Indented)));

			else if (bf.IsConditionless)
				return this;


			Self.Filter = bf;
			return this;
		}
		/// <summary>
		/// Filter search
		/// </summary>
		public SearchDescriptor<T> Filter(FilterContainer filterDescriptor)
		{
			filterDescriptor.ThrowIfNull("filter");
			Self.Filter = filterDescriptor;
			return this;
		}

		/// <summary>
		/// Filter search using a raw json string
		/// </summary>
		public SearchDescriptor<T> FilterRaw(string rawFilter)
		{
			Self.Filter = new FilterDescriptor<T>().Raw(rawFilter);
			return this;
		}


		/// <summary>
		/// Allow to highlight search results on one or more fields. The implementation uses the either lucene fast-vector-highlighter or highlighter. 
		/// </summary>
		public SearchDescriptor<T> Highlight(Action<HighlightDescriptor<T>> highlightDescriptor)
		{
			highlightDescriptor.ThrowIfNull("highlightDescriptor");
			var d = new HighlightDescriptor<T>();
			highlightDescriptor(d);
			Self.Highlight = d;
			return this;
		}

		/// <summary>
		/// Allows you to specify a rescore query
		/// </summary>
		public SearchDescriptor<T> Rescore(Action<RescoreDescriptor<T>> rescoreSelector)
		{
			rescoreSelector.ThrowIfNull("rescoreSelector");
			var d = new RescoreDescriptor<T>();
			rescoreSelector(d);
			Self.Rescore = d;
			return this;
		}

		/// <summary>
		/// Shorthand for a match_all query without having to specify .Query(q=>q.MatchAll())
		/// </summary>
		public SearchDescriptor<T> MatchAll()
		{
			return this.Query(q => q.MatchAll());
		}

		public SearchDescriptor<T> ConcreteTypeSelector(Func<dynamic, Hit<dynamic>, Type> typeSelector)
		{
			Self.TypeSelector = typeSelector;
			return this;
		}

		

		protected override void UpdatePathInfo(IConnectionSettingsValues settings, ElasticsearchPathInfo<SearchRequestParameters> pathInfo)
		{
			SearchPathInfo.Update(settings,pathInfo, this);
		}

	}
}
