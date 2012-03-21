using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class SearchDescriptor<T> where T : class
	{
		internal IEnumerable<string> _Indices { get; set; }
		internal IEnumerable<string> _Types { get; set; }
		internal string _Routing { get; set; }
		internal bool _AllIndices { get; set; }
		internal bool _AllTypes { get; set; }

		public SearchDescriptor<T> Indices(IEnumerable<string> indices)
		{
			indices.ThrowIfEmpty("indices");
			this._Indices = indices;
			return this;
		}
		public SearchDescriptor<T> Index(string index)
		{
			index.ThrowIfNullOrEmpty("indices");
			this._Indices = new[] { index };
			return this;
		}
		public SearchDescriptor<T> Types(IEnumerable<string> types)
		{
			types.ThrowIfEmpty("types");
			this._Types = types;
			return this;
		}
		public SearchDescriptor<T> Types(params string[] types)
		{
			return this.Types((IEnumerable<string>)types);
		}
		public SearchDescriptor<T> Types(IEnumerable<Type> types)
		{
			types.ThrowIfEmpty("types");
			return this.Types((IEnumerable<string>)types.Select(t => ElasticClient.GetTypeNameFor(t)).ToArray());
		}
		public SearchDescriptor<T> Types(params Type[] types)
		{
			return this.Types((IEnumerable<Type>)types);
		}
		public SearchDescriptor<T> Type(string type)
		{
			type.ThrowIfNullOrEmpty("type");
			this._Types = new[] { type };
			return this;
		}
		public SearchDescriptor<T> Type(Type type)
		{
			type.ThrowIfNull("type");
			return this.Type(ElasticClient.GetTypeNameFor(type));
		}
		public SearchDescriptor<T> AllIndices()
		{
			this._AllIndices = true;
			return this;
		}
		public SearchDescriptor<T> AllTypes()
		{
			this._AllTypes = true;
			return this;
		}
		public SearchDescriptor<T> Routing(string routing)
		{
			routing.ThrowIfNullOrEmpty("routing");
			this._Routing = routing;
			return this;
		}
		public SearchDescriptor()
		{
		}

		[JsonProperty(PropertyName = "from")]
		internal int? _From { get; set; }
		[JsonProperty(PropertyName = "size")]
		internal int? _Size { get; set; }
		[JsonProperty(PropertyName = "explain")]
		internal bool? _Explain { get; set; }
		[JsonProperty(PropertyName = "version")]
		internal bool? _Version { get; set; }
		[JsonProperty(PropertyName = "min_score")]
		internal double? _MinScore { get; set; }

		[JsonProperty(PropertyName = "preference")]
		internal string _Preference { get; set; }

		[JsonProperty(PropertyName = "indices_boost")]
		internal IDictionary<string, double> _IndicesBoost { get; set; }

		[JsonProperty(PropertyName = "sort")]
		internal IDictionary<string, string> _Sort { get; set; }

		[JsonProperty(PropertyName = "facets")]
		internal IDictionary<string, FacetDescriptorsBucket<T>> _Facets { get; set; }

		[JsonProperty(PropertyName = "query")]
		internal RawOrQueryDescriptor<T> _QueryOrRaw
		{
			get
			{
				if (this._RawQuery == null && this._Query == null)
					return null;
				return new RawOrQueryDescriptor<T>
				{
					Raw = this._RawQuery,
					Descriptor = this._Query
				};
			}
		}

		[JsonProperty(PropertyName = "filter")]
		internal RawOrFilterDescriptor<T> _FilterOrRaw
		{
			get
			{
				if (this._RawFilter == null && this._Filter == null)
					return null;
				return new RawOrFilterDescriptor<T>
				{
					Raw = this._RawFilter,
					Descriptor = this._Filter
				};
			}
		}


		internal string _RawQuery { get; set; }
		internal QueryDescriptor<T> _Query { get; set; }

		internal string _RawFilter { get; set; }
		internal FilterDescriptor<T> _Filter { get; set; }

		[JsonProperty(PropertyName = "fields")]
		internal IList<string> _Fields { get; set; }

		public SearchDescriptor<T> Size(int size)
		{
			this._Size = size;
			return this;
		}
		public SearchDescriptor<T> Take(int take)
		{
			return this.Size(take);
		}
		public SearchDescriptor<T> From(int from)
		{
			this._From = from;
			return this;
		}
		public SearchDescriptor<T> Skip(int skip)
		{
			return this.From(skip);
		}


		public SearchDescriptor<T> Explain(bool explain = true)
		{
			this._Explain = explain;
			return this;
		}
		public SearchDescriptor<T> Version(bool version = true)
		{
			this._Version = version;
			return this;
		}
		public SearchDescriptor<T> MinScore(double minScore)
		{
			this._MinScore = minScore;
			return this;
		}
		public SearchDescriptor<T> Preference(string preference)
		{
			this._Preference = preference;
			return this;
		}
		public SearchDescriptor<T> ExecuteOnPrimary()
		{
			this._Preference = "_primary";
			return this;
		}
		public SearchDescriptor<T> ExecuteOnLocalShard()
		{
			this._Preference = "_local";
			return this;
		}
		public SearchDescriptor<T> ExecuteOnNode(string node)
		{
			node.ThrowIfNull("node");
			this._Preference = "_only_node:" + node;
			return this;
		}

		public SearchDescriptor<T> IndicesBoost(
			Func<FluentDictionary<string, double>, FluentDictionary<string, double>> boost)
		{
			boost.ThrowIfNull("boost");
			this._IndicesBoost = boost(new FluentDictionary<string, double>());
			return this;
		}
		public SearchDescriptor<T> Fields(params Expression<Func<T, object>>[] expressions)
		{
			if (this._Fields == null)
				this._Fields = new List<string>();
			foreach (var e in expressions)
				this._Fields.Add(ElasticClient.PropertyNameResolver.Resolve(e));
			return this;
		}
		public SearchDescriptor<T> SortAscending(Expression<Func<T, object>> objectPath)
		{
			if (this._Sort == null)
				this._Sort = new Dictionary<string, string>();
			this._Sort.Add(ElasticClient.PropertyNameResolver.ResolveToSort(objectPath), "asc");
			return this;
		}
		public SearchDescriptor<T> SortDescending(Expression<Func<T, object>> objectPath)
		{
			if (this._Sort == null)
				this._Sort = new Dictionary<string, string>();

			var key = ElasticClient.PropertyNameResolver.ResolveToSort(objectPath);
			this._Sort.Add(key, "desc");
			return this;
		}


		private SearchDescriptor<T> _Facet<F>(
			string name,
			Func<F, F> facet,
			Func<F, string> inferedFieldNameSelector,
			Action<FacetDescriptorsBucket<T>, F> fillBucket
			)
			where F : BaseFacetDescriptor<T>, new()
		{
			facet.ThrowIfNull("facet");
			inferedFieldNameSelector.ThrowIfNull("inferedFieldNameSelector");
			fillBucket.ThrowIfNull("fillBucket");

			if (this._Facets == null)
				this._Facets = new Dictionary<string, FacetDescriptorsBucket<T>>();

			var descriptor = new F();
			var f = facet(descriptor);
			var key = string.IsNullOrWhiteSpace(name) ? inferedFieldNameSelector(descriptor) : name;
			if (string.IsNullOrWhiteSpace(key))
			{
				throw new DslException(
					"Couldn't infer name for facet of type {0}".F(typeof(F).Name)
				);
			}

			var bucket = new FacetDescriptorsBucket<T>
			{
				Global = f._IsGlobal,
				FacetFilter = f._FacetFilter,
				Nested = f._Nested,
				Scope = f._Scope
			};
			fillBucket(bucket, descriptor);
			this._Facets.Add(key, bucket);

			return this;
		}



		public SearchDescriptor<T> FacetTerm(string name, Func<TermFacetDescriptor<T>, TermFacetDescriptor<T>> facet)
		{
			return this.FacetTerm(facet, Name: name);
		}

		public SearchDescriptor<T> FacetTerm(Func<TermFacetDescriptor<T>, TermFacetDescriptor<T>> facet, string Name = null)
		{
			return this._Facet<TermFacetDescriptor<T>>(
				Name,
				facet,
				(d) => d._Field,
				(b, d) => b.Terms = d
			);
		}

		public SearchDescriptor<T> FacetRange<K>(string name, Func<RangeFacetDescriptor<T, K>, RangeFacetDescriptor<T, K>> facet) where K : struct
		{
			return this.FacetRange<K>(facet, Name: name);
		}

		public SearchDescriptor<T> FacetRange<K>(Func<RangeFacetDescriptor<T, K>, RangeFacetDescriptor<T, K>> facet, string Name = null) where K : struct
		{
			return this._Facet<RangeFacetDescriptor<T, K>>(
				Name,
				facet,
				(d) => d._Field,
				(b, d) => b.Range = d
			);
		}

		public SearchDescriptor<T> FacetHistogram(string name, Func<HistogramFacetDescriptor<T>, HistogramFacetDescriptor<T>> facet)
		{
			return this.FacetHistogram(facet, Name: name);
		}

		public SearchDescriptor<T> FacetHistogram(Func<HistogramFacetDescriptor<T>, HistogramFacetDescriptor<T>> facet, string Name = null)
		{
			return this._Facet<HistogramFacetDescriptor<T>>(
				Name,
				facet,
				(d) => d._Field,
				(b, d) => b.Histogram = d
			);
		}

		public SearchDescriptor<T> FacetDateHistogram(string name, Func<DateHistogramFacetDescriptor<T>, DateHistogramFacetDescriptor<T>> facet)
		{
			return this.FacetDateHistogram(facet, Name: name);
		}

		public SearchDescriptor<T> FacetDateHistogram(Func<DateHistogramFacetDescriptor<T>, DateHistogramFacetDescriptor<T>> facet, string Name = null)
		{
			return this._Facet<DateHistogramFacetDescriptor<T>>(
				Name,
				facet,
				(d) => d._Field,
				(b, d) => b.DateHistogram = d
			);
		}

		public SearchDescriptor<T> FacetStatistical(string name, Func<StatisticalFacetDescriptor<T>, StatisticalFacetDescriptor<T>> facet)
		{
			return this.FacetStatistical(facet, Name: name);
		}

		public SearchDescriptor<T> FacetStatistical(Func<StatisticalFacetDescriptor<T>, StatisticalFacetDescriptor<T>> facet, string Name = null)
		{
			return this._Facet<StatisticalFacetDescriptor<T>>(
				Name,
				facet,
				(d) => d._Field,
				(b, d) => b.Statistical = d
			);
		}

		public SearchDescriptor<T> FacetTermsStats(string name, Func<TermsStatsFacetDescriptor<T>, TermsStatsFacetDescriptor<T>> facet)
		{
			return this.FacetTermsStats(facet, Name: name);
		}

		public SearchDescriptor<T> FacetTermsStats(Func<TermsStatsFacetDescriptor<T>, TermsStatsFacetDescriptor<T>> facet, string Name = null)
		{
			return this._Facet<TermsStatsFacetDescriptor<T>>(
				Name,
				facet,
				(d) => d._KeyField,
				(b, d) => b.TermsStats = d
			);
		}
		public SearchDescriptor<T> FacetGeoDistance(string name, Func<GeoDistanceFacetDescriptor<T>, GeoDistanceFacetDescriptor<T>> facet)
		{
			return this.FacetGeoDistance(facet, Name: name);
		}

		public SearchDescriptor<T> FacetGeoDistance(Func<GeoDistanceFacetDescriptor<T>, GeoDistanceFacetDescriptor<T>> facet, string Name = null)
		{
			return this._Facet<GeoDistanceFacetDescriptor<T>>(
					Name,
					facet,
					(d) => d._ValueField,
					(b, d) => b.GeoDistance = d
				);
		}


		public SearchDescriptor<T> FacetQuery(string name, Action<QueryDescriptor<T>> querySelector, bool? Global = null)
		{
			name.ThrowIfNullOrEmpty("name");
			querySelector.ThrowIfNull("query");
			if (this._Facets == null)
				this._Facets = new Dictionary<string, FacetDescriptorsBucket<T>>();

			var query = new QueryDescriptor<T>();
			querySelector(query);
			this._Facets.Add(name, new FacetDescriptorsBucket<T> { Query = query });

			return this;
		}
		public SearchDescriptor<T> FacetFilter(string name, Action<FilterDescriptor<T>> querySelector)
		{
			name.ThrowIfNullOrEmpty("name");
			querySelector.ThrowIfNull("query");
			if (this._Facets == null)
				this._Facets = new Dictionary<string, FacetDescriptorsBucket<T>>();

			var filter = new FilterDescriptor<T>();
			querySelector(filter);
			this._Facets.Add(name, new FacetDescriptorsBucket<T> { Filter = filter });

			return this;
		}


		public SearchDescriptor<T> Query(Action<QueryDescriptor<T>> query)
		{
			query.ThrowIfNull("query");
			this._Query = new QueryDescriptor<T>();
			query(this._Query);
			return this;
		}
		public SearchDescriptor<T> Query(string rawQuery)
		{
			rawQuery.ThrowIfNull("rawQuery");
			this._RawQuery = rawQuery;
			return this;
		}
		public SearchDescriptor<T> Filter(Action<FilterDescriptor<T>> filter)
		{
			filter.ThrowIfNull("filter");
			this._Filter = new FilterDescriptor<T>();
			filter(this._Filter);
			return this;
		}
		public SearchDescriptor<T> Filter(string rawFilter)
		{
			rawFilter.ThrowIfNull("rawFilter");
			this._RawFilter = rawFilter;
			return this;
		}


		public SearchDescriptor<T> MatchAll()
		{
			this._Query = new QueryDescriptor<T>();
			this._Query.MatchAll();
			return this;
		}
	}

	public class FluentDictionary<K, V> : Dictionary<K, V>
	{
		public new FluentDictionary<K, V> Add(K k, V v)
		{
			base.Add(k, v);
			return this;
		}
	}
}
