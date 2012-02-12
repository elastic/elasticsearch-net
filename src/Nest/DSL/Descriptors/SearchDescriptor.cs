using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;

namespace Nest.DSL
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class SearchDescriptor<T> where T : class
	{

		public SearchDescriptor()
		{
		}

		[JsonProperty(PropertyName="from")]
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
				if (this._RawFilter == null)
					return null;
				return new RawOrFilterDescriptor<T>
				{
					Raw = this._RawFilter,
				};
			}
		}

		internal string _RawFilter { get; set; }
		
		internal string _RawQuery { get; set; }
		internal QueryDescriptor<T> _Query { get; set; }

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
			Func<FluentDictionary<string, double>,FluentDictionary<string, double>> boost)
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
	
		public SearchDescriptor<T> FacetTerm(string name, Func<TermFacetDescriptor<T>, TermFacetDescriptor<T>> facet)
		{
			return this.FacetTerm(facet, Name: name);
		}

		public SearchDescriptor<T> FacetTerm(Func<TermFacetDescriptor<T>, TermFacetDescriptor<T>> facet, string Name = null)
		{
			if (this._Facets == null)
				this._Facets = new Dictionary<string, FacetDescriptorsBucket<T>>();

			
			var descriptor = new TermFacetDescriptor<T>();
			var f = facet(descriptor);
			var key = string.IsNullOrWhiteSpace(Name) ? f._Field : Name;
			if (string.IsNullOrWhiteSpace(key))
				throw new DslException("Could not figure out term facet name, when using multifield you have to specify a name!");
			this._Facets.Add(key, new FacetDescriptorsBucket<T> { Terms = f });

			return this;
		}

		public SearchDescriptor<T> FacetRange<K>(string name, Func<RangeFacetDescriptor<T, K>, RangeFacetDescriptor<T, K>> facet) where K : struct
		{
			return this.FacetRange<K>(facet, Name: name);
		}

		public SearchDescriptor<T> FacetRange<K>(Func<RangeFacetDescriptor<T, K>, RangeFacetDescriptor<T, K>> facet, string Name = null) where K : struct
		{
			if (this._Facets == null)
				this._Facets = new Dictionary<string, FacetDescriptorsBucket<T>>();

			var descriptor = new RangeFacetDescriptor<T, K>();
			var f = facet(descriptor);
			var key = string.IsNullOrWhiteSpace(Name) ? f._Field : Name;
			if (string.IsNullOrWhiteSpace(key))
				throw new DslException("Could not infer name for range facet");
			this._Facets.Add(key, new FacetDescriptorsBucket<T> { Range = f });

			return this;
		}

		public SearchDescriptor<T> FacetHistogram(string name, Func<HistogramFacetDescriptor<T>, HistogramFacetDescriptor<T>> facet)
		{
			return this.FacetHistogram(facet, Name: name);
		}

		public SearchDescriptor<T> FacetHistogram(Func<HistogramFacetDescriptor<T>, HistogramFacetDescriptor<T>> facet, string Name = null)
		{
			if (this._Facets == null)
				this._Facets = new Dictionary<string, FacetDescriptorsBucket<T>>();


			var descriptor = new HistogramFacetDescriptor<T>();
			var f = facet(descriptor);
			var key = string.IsNullOrWhiteSpace(Name) ? f._Field : Name;
			if (string.IsNullOrWhiteSpace(key))
				throw new DslException("Could not infer name for histogram facet");
			this._Facets.Add(key, new FacetDescriptorsBucket<T> { Histogram = f });

			return this;
		}

		public SearchDescriptor<T> FacetDateHistogram(string name, Func<DateHistogramFacetDescriptor<T>, DateHistogramFacetDescriptor<T>> facet)
		{
			return this.FacetDateHistogram(facet, Name: name);
		}

		public SearchDescriptor<T> FacetDateHistogram(Func<DateHistogramFacetDescriptor<T>, DateHistogramFacetDescriptor<T>> facet, string Name = null)
		{
			if (this._Facets == null)
				this._Facets = new Dictionary<string, FacetDescriptorsBucket<T>>();


			var descriptor = new DateHistogramFacetDescriptor<T>();
			var f = facet(descriptor);
			var key = string.IsNullOrWhiteSpace(Name) ? f._Field : Name;
			if (string.IsNullOrWhiteSpace(key))
				throw new DslException("Could not infer name for date histogram facet");
			this._Facets.Add(key, new FacetDescriptorsBucket<T> { DateHistogram = f });

			return this;
		}

		public SearchDescriptor<T> FacetStatistical(string name, Func<StatisticalFacetDescriptor<T>, StatisticalFacetDescriptor<T>> facet)
		{
			return this.FacetStatistical(facet, Name: name);
		}

		public SearchDescriptor<T> FacetStatistical(Func<StatisticalFacetDescriptor<T>, StatisticalFacetDescriptor<T>> facet, string Name = null)
		{
			if (this._Facets == null)
				this._Facets = new Dictionary<string, FacetDescriptorsBucket<T>>();

			var descriptor = new StatisticalFacetDescriptor<T>();
			var f = facet(descriptor);
			var key = string.IsNullOrWhiteSpace(Name) ? f._Field : Name;
			if (string.IsNullOrWhiteSpace(key))
				throw new DslException("Could not infer name for statistical facet");
			this._Facets.Add(key, new FacetDescriptorsBucket<T> { Statistical = f });

			return this;
		}

		public SearchDescriptor<T> FacetTermsStats(string name, Func<TermsStatsFacetDescriptor<T>, TermsStatsFacetDescriptor<T>> facet)
		{
			return this.FacetTermsStats(facet, Name: name);
		}

		public SearchDescriptor<T> FacetTermsStats(Func<TermsStatsFacetDescriptor<T>, TermsStatsFacetDescriptor<T>> facet, string Name = null)
		{
			if (this._Facets == null)
				this._Facets = new Dictionary<string, FacetDescriptorsBucket<T>>();

			var descriptor = new TermsStatsFacetDescriptor<T>();
			var f = facet(descriptor);
			var key = string.IsNullOrWhiteSpace(Name) ? f._KeyField : Name;
			if (string.IsNullOrWhiteSpace(key))
				throw new DslException("Could not infer name for terms_stats facet");
			this._Facets.Add(key, new FacetDescriptorsBucket<T> { TermsStats = f });

			return this;
		}
		public SearchDescriptor<T> FacetGeoDistance(string name, Func<GeoDistanceFacetDescriptor<T>, GeoDistanceFacetDescriptor<T>> facet)
		{
			return this.FacetGeoDistance(facet, Name: name);
		}

		public SearchDescriptor<T> FacetGeoDistance(Func<GeoDistanceFacetDescriptor<T>, GeoDistanceFacetDescriptor<T>> facet, string Name = null)
		{	
			if (this._Facets == null)
				this._Facets = new Dictionary<string, FacetDescriptorsBucket<T>>();

			var descriptor = new GeoDistanceFacetDescriptor<T>();
			var f = facet(descriptor);
			var key = string.IsNullOrWhiteSpace(Name) ? f._ValueField : Name;
			if (string.IsNullOrWhiteSpace(key))
				throw new DslException("Could not infer name for terms_stats facet");

			this._Facets.Add(key, new FacetDescriptorsBucket<T> { GeoDistance = f });

			return this;
		}


		public SearchDescriptor<T> FacetQuery(string name, Func<QueryDescriptor<T>,QueryDescriptor<T>> querySelector)
		{
			name.ThrowIfNullOrEmpty("name");
			querySelector.ThrowIfNull("query");
			if (this._Facets == null)
				this._Facets = new Dictionary<string, FacetDescriptorsBucket<T>>();

			var query = querySelector(new QueryDescriptor<T>());
			this._Facets.Add(name, new FacetDescriptorsBucket<T> { Query = query });

			return this;
		}



		public SearchDescriptor<T> Query(Func<QueryDescriptor<T>, QueryDescriptor<T>> query)
		{
			query.ThrowIfNull("query");
			this._Query = query(new QueryDescriptor<T>());
			return this;
		}
		public SearchDescriptor<T> Query(string rawQuery)
		{
			rawQuery.ThrowIfNull("rawQuery");
			this._RawQuery = rawQuery;
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
			this._Query = new QueryDescriptor<T>()
				.MatchAll();
			return this;
		}
	}

	public class FluentDictionary<K,V> : Dictionary<K,V>
	{
		public new FluentDictionary<K,V> Add(K k, V v)
		{
			base.Add(k, v);
			return this;
		}
	}
}
