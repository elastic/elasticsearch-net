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

		[JsonProperty(PropertyName = "query")]
		internal RawOrQueryDescriptor<T> _QueryOrRaw
		{
			get
			{
				if (this._RawQuery == null && this._Query == null)
					return null;
				return new RawOrQueryDescriptor<T> 
				{
					RawQuery = this._RawQuery,
					Descriptor = this._Query
				};
			}
		}

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
			this._Sort.Add(ElasticClient.PropertyNameResolver.ResolveToSort(objectPath), "desc");
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
