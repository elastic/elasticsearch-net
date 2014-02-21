using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;
using Nest.Resolvers;
using Elasticsearch.Net;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class MatchQueryDescriptor<T> : IQuery where T : class
	{
		[JsonProperty(PropertyName = "type")]
		internal virtual string _Type { get { return null; } }

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

		[JsonProperty(PropertyName = "operator")]
		[JsonConverter(typeof(StringEnumConverter))]
		internal Operator? _Operator { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return this._Field.IsConditionless() || this._Query.IsNullOrEmpty();
			}
		}


		internal PropertyPathMarker _Field { get; set; }
		public MatchQueryDescriptor<T> OnField(string field)
		{
			this._Field = field;
			return this;
		}
		public MatchQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			this._Field = objectPath;
			return this;
		}

		public MatchQueryDescriptor<T> Query(string query)
		{
			this._Query = query;
			return this;
		}
		public MatchQueryDescriptor<T> Analyzer(string analyzer)
		{
			this._Analyzer = analyzer;
			return this;
		}
		public MatchQueryDescriptor<T> Fuzziness(double fuzziness)
		{
			this._Fuzziness = fuzziness;
			return this;
		}
		public MatchQueryDescriptor<T> CutoffFrequency(double cutoffFrequency)
		{
			this._CutoffFrequency = cutoffFrequency;
			return this;
		}

		public MatchQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite)
		{
			this._Rewrite = rewrite;
			return this;
		}

		public MatchQueryDescriptor<T> Boost(double boost)
		{
			this._Boost = boost;
			return this;
		}
		public MatchQueryDescriptor<T> PrefixLength(int prefixLength)
		{
			this._PrefixLength = prefixLength;
			return this;
		}
		public MatchQueryDescriptor<T> MaxExpansions(int maxExpansions)
		{
			this._MaxExpansions = maxExpansions;
			return this;
		}
		public MatchQueryDescriptor<T> Slop(int slop)
		{
			this._Slop = slop;
			return this;
		}
		public MatchQueryDescriptor<T> Operator(Operator op)
		{
			this._Operator = op;
			return this;
		}
	}
}
