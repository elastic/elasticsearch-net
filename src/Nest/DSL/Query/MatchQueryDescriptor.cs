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
	public interface IMatchQuery
	{
		[JsonProperty(PropertyName = "type")]
		string _Type { get; }

		[JsonProperty(PropertyName = "query")]
		string _Query { get; set; }

		[JsonProperty(PropertyName = "analyzer")]
		string _Analyzer { get; set; }

		[JsonProperty(PropertyName = "rewrite")]
		[JsonConverter(typeof (StringEnumConverter))]
		RewriteMultiTerm? _Rewrite { get; set; }

		[JsonProperty(PropertyName = "fuzziness")]
		double? _Fuzziness { get; set; }

		[JsonProperty(PropertyName = "cutoff_frequency")]
		double? _CutoffFrequency { get; set; }

		[JsonProperty(PropertyName = "prefix_length")]
		int? _PrefixLength { get; set; }

		[JsonProperty(PropertyName = "max_expansions")]
		int? _MaxExpansions { get; set; }

		[JsonProperty(PropertyName = "slop")]
		int? _Slop { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? _Boost { get; set; }

		[JsonProperty(PropertyName = "lenient")]
		bool? _Lenient { get; set; }
		
		[JsonProperty(PropertyName = "operator")]
		[JsonConverter(typeof (StringEnumConverter))]
		Operator? _Operator { get; set; }

		PropertyPathMarker _Field { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class MatchQueryDescriptor<T> : IQuery, IMatchQuery where T : class
	{
		protected virtual string _type { get { return null; } }

		string IMatchQuery._Type { get { return _type; } }

		string IMatchQuery._Query { get; set; }

		string IMatchQuery._Analyzer { get; set; }

		RewriteMultiTerm? IMatchQuery._Rewrite { get; set; }

		double? IMatchQuery._Fuzziness { get; set; }

		double? IMatchQuery._CutoffFrequency { get; set; }

		int? IMatchQuery._PrefixLength { get; set; }

		int? IMatchQuery._MaxExpansions { get; set; }

		int? IMatchQuery._Slop { get; set; }

		double? IMatchQuery._Boost { get; set; }

		bool? IMatchQuery._Lenient { get; set; }
		
		[JsonProperty(PropertyName = "operator")]
		[JsonConverter(typeof(StringEnumConverter))]
		Operator? IMatchQuery._Operator { get; set; }

		PropertyPathMarker IMatchQuery._Field { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return ((IMatchQuery)this)._Field.IsConditionless() || ((IMatchQuery)this)._Query.IsNullOrEmpty();
			}
		}


		public MatchQueryDescriptor<T> OnField(string field)
		{
			((IMatchQuery)this)._Field = field;
			return this;
		}
		public MatchQueryDescriptor<T> OnField(Expression<Func<T, object>> objectPath)
		{
			((IMatchQuery)this)._Field = objectPath;
			return this;
		}

		public MatchQueryDescriptor<T> Query(string query)
		{
			((IMatchQuery)this)._Query = query;
			return this;
		}
		public MatchQueryDescriptor<T> Lenient(bool lenient = true)
		{
			((IMatchQuery)this)._Lenient = lenient;
			return this;
		}
		public MatchQueryDescriptor<T> Analyzer(string analyzer)
		{
			((IMatchQuery)this)._Analyzer = analyzer;
			return this;
		}
		public MatchQueryDescriptor<T> Fuzziness(double fuzziness)
		{
			((IMatchQuery)this)._Fuzziness = fuzziness;
			return this;
		}
		public MatchQueryDescriptor<T> CutoffFrequency(double cutoffFrequency)
		{
			((IMatchQuery)this)._CutoffFrequency = cutoffFrequency;
			return this;
		}

		public MatchQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite)
		{
			((IMatchQuery)this)._Rewrite = rewrite;
			return this;
		}

		public MatchQueryDescriptor<T> Boost(double boost)
		{
			((IMatchQuery)this)._Boost = boost;
			return this;
		}
		public MatchQueryDescriptor<T> PrefixLength(int prefixLength)
		{
			((IMatchQuery)this)._PrefixLength = prefixLength;
			return this;
		}
		public MatchQueryDescriptor<T> MaxExpansions(int maxExpansions)
		{
			((IMatchQuery)this)._MaxExpansions = maxExpansions;
			return this;
		}
		public MatchQueryDescriptor<T> Slop(int slop)
		{
			((IMatchQuery)this)._Slop = slop;
			return this;
		}
		public MatchQueryDescriptor<T> Operator(Operator op)
		{
			((IMatchQuery)this)._Operator = op;
			return this;
		}
	}
}
