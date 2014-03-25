using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Elasticsearch.Net;
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class MultiMatchQueryDescriptor<T> : IQuery where T : class
	{
		[JsonProperty(PropertyName = "type")]
		internal virtual string _Type { get; set; }

		[JsonProperty(PropertyName = "query")]
		internal string _Query { get; set; }

		[JsonProperty(PropertyName = "analyzer")]
		internal string _Analyzer { get; set; }

		[JsonProperty(PropertyName = "rewrite")]
		[JsonConverter(typeof(StringEnumConverter))]
		internal RewriteMultiTerm? _Rewrite { get; set; }

		[JsonProperty(PropertyName = "fuzziness")]
		internal double? _Fuzziness { get; set; }

		[JsonProperty(PropertyName = "cutoff_frequency")]
		internal double? _CutoffFrequency { get; set; }

		[JsonProperty(PropertyName = "prefix_length")]
		internal int? _PrefixLength { get; set; }

		[JsonProperty(PropertyName = "max_expansions")]
		internal int? _MaxExpansions { get; set; }

		[JsonProperty(PropertyName = "slop")]
		internal int? _Slop { get; set; }

		[JsonProperty(PropertyName = "boost")]
		internal double? _Boost { get; set; }

		[JsonProperty(PropertyName = "use_dis_max")]
		internal bool? _UseDisMax { get; set; }

		[JsonProperty(PropertyName = "tie_breaker")]
		internal double? _TieBreaker { get; set; }


		[JsonProperty(PropertyName = "operator")]
		[JsonConverter(typeof(StringEnumConverter))]
		internal Operator? _Operator { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return !this._Fields.HasAny() || this._Fields.All(f=>f.IsConditionless()) || this._Query.IsNullOrEmpty();
			}
		}

		[JsonProperty(PropertyName = "fields")]
		internal IEnumerable<PropertyPathMarker> _Fields { get; set; }

		public MultiMatchQueryDescriptor<T> OnFields(IEnumerable<string> fields)
		{
			this._Fields = fields.Select(f=>(PropertyPathMarker)f);
			return this;
		}
		public MultiMatchQueryDescriptor<T> OnFields(
			params Expression<Func<T, object>>[] objectPaths)
		{
			this._Fields = objectPaths.Select(e=>(PropertyPathMarker)e);
			return this;
		}
        public MultiMatchQueryDescriptor<T> OnFieldsWithBoost(Action<FluentDictionary<Expression<Func<T, object>>, double?>> boostableSelector)
        {
            var d = new FluentDictionary<Expression<Func<T, object>>, double?>();
            boostableSelector(d);
            this._Fields = d.Select(o => PropertyPathMarker.Create(o.Key, o.Value));
            return this;
        }

		public MultiMatchQueryDescriptor<T> Query(string query)
		{
			this._Query = query;
			return this;
		}
		public MultiMatchQueryDescriptor<T> Analyzer(string analyzer)
		{
			this._Analyzer = analyzer;
			return this;
		}
		public MultiMatchQueryDescriptor<T> Fuzziness(double fuzziness)
		{
			this._Fuzziness = fuzziness;
			return this;
		}
		public MultiMatchQueryDescriptor<T> CutoffFrequency(double cutoffFrequency)
		{
			this._CutoffFrequency = cutoffFrequency;
			return this;
		}

		public MultiMatchQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite)
		{
			this._Rewrite = rewrite;
			return this;
		}

		public MultiMatchQueryDescriptor<T> Boost(double boost)
		{
			this._Boost = boost;
			return this;
		}
		public MultiMatchQueryDescriptor<T> PrefixLength(int prefixLength)
		{
			this._PrefixLength = prefixLength;
			return this;
		}
		public MultiMatchQueryDescriptor<T> MaxExpansions(int maxExpansions)
		{
			this._MaxExpansions = maxExpansions;
			return this;
		}
		public MultiMatchQueryDescriptor<T> Slop(int slop)
		{
			this._Slop = slop;
			return this;
		}
		public MultiMatchQueryDescriptor<T> Operator(Operator op)
		{
			this._Operator = op;
			return this;
		}

		public MultiMatchQueryDescriptor<T> UseDisMax(bool useDismax)
		{
			this._UseDisMax = useDismax;
			return this;
		}

		public MultiMatchQueryDescriptor<T> TieBreaker(double tieBreaker)
		{
			this._TieBreaker = tieBreaker;
			return this;
		}

		public MultiMatchQueryDescriptor<T> Type(TextQueryType type)
		{
			this._Type = type.ToString().ToLower();
			return this;
		}
	}
}
