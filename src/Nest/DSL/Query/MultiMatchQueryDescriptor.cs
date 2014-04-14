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
	public interface IMultiMatchQuery
	{
		[JsonProperty(PropertyName = "type")]
		string _Type { get; set; }

		[JsonProperty(PropertyName = "query")]
		string _Query { get; set; }

		[JsonProperty(PropertyName = "analyzer")]
		string _Analyzer { get; set; }

		[JsonProperty(PropertyName = "rewrite")]
		[JsonConverter(typeof(StringEnumConverter))]
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

		[JsonProperty(PropertyName = "use_dis_max")]
		bool? _UseDisMax { get; set; }

		[JsonProperty(PropertyName = "tie_breaker")]
		double? _TieBreaker { get; set; }

		[JsonProperty(PropertyName = "operator")]
		[JsonConverter(typeof(StringEnumConverter))]
		Operator? _Operator { get; set; }

		[JsonProperty(PropertyName = "fields")]
		IEnumerable<PropertyPathMarker> _Fields { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class MultiMatchQueryDescriptor<T> : IQuery, IMultiMatchQuery where T : class
	{
		string IMultiMatchQuery._Type { get; set; }

		string IMultiMatchQuery._Query { get; set; }

		string IMultiMatchQuery._Analyzer { get; set; }

		RewriteMultiTerm? IMultiMatchQuery._Rewrite { get; set; }

		double? IMultiMatchQuery._Fuzziness { get; set; }

		double? IMultiMatchQuery._CutoffFrequency { get; set; }

		int? IMultiMatchQuery._PrefixLength { get; set; }

		int? IMultiMatchQuery._MaxExpansions { get; set; }

		int? IMultiMatchQuery._Slop { get; set; }

		double? IMultiMatchQuery._Boost { get; set; }

		bool? IMultiMatchQuery._UseDisMax { get; set; }

		double? IMultiMatchQuery._TieBreaker { get; set; }

		Operator? IMultiMatchQuery._Operator { get; set; }

		IEnumerable<PropertyPathMarker> IMultiMatchQuery._Fields { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return !((IMultiMatchQuery)this)._Fields.HasAny() || ((IMultiMatchQuery)this)._Fields.All(f => f.IsConditionless()) || ((IMultiMatchQuery)this)._Query.IsNullOrEmpty();
			}
		}


		public MultiMatchQueryDescriptor<T> OnFields(IEnumerable<string> fields)
		{
			((IMultiMatchQuery)this)._Fields = fields.Select(f => (PropertyPathMarker)f);
			return this;
		}
		public MultiMatchQueryDescriptor<T> OnFields(
			params Expression<Func<T, object>>[] objectPaths)
		{
			((IMultiMatchQuery)this)._Fields = objectPaths.Select(e => (PropertyPathMarker)e);
			return this;
		}
		public MultiMatchQueryDescriptor<T> OnFieldsWithBoost(Action<FluentDictionary<Expression<Func<T, object>>, double?>> boostableSelector)
		{
			var d = new FluentDictionary<Expression<Func<T, object>>, double?>();
			boostableSelector(d);
			((IMultiMatchQuery)this)._Fields = d.Select(o => PropertyPathMarker.Create(o.Key, o.Value));
			return this;
		}

		public MultiMatchQueryDescriptor<T> Query(string query)
		{
			((IMultiMatchQuery)this)._Query = query;
			return this;
		}
		public MultiMatchQueryDescriptor<T> Analyzer(string analyzer)
		{
			((IMultiMatchQuery)this)._Analyzer = analyzer;
			return this;
		}
		public MultiMatchQueryDescriptor<T> Fuzziness(double fuzziness)
		{
			((IMultiMatchQuery)this)._Fuzziness = fuzziness;
			return this;
		}
		public MultiMatchQueryDescriptor<T> CutoffFrequency(double cutoffFrequency)
		{
			((IMultiMatchQuery)this)._CutoffFrequency = cutoffFrequency;
			return this;
		}

		public MultiMatchQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite)
		{
			((IMultiMatchQuery)this)._Rewrite = rewrite;
			return this;
		}

		public MultiMatchQueryDescriptor<T> Boost(double boost)
		{
			((IMultiMatchQuery)this)._Boost = boost;
			return this;
		}
		public MultiMatchQueryDescriptor<T> PrefixLength(int prefixLength)
		{
			((IMultiMatchQuery)this)._PrefixLength = prefixLength;
			return this;
		}
		public MultiMatchQueryDescriptor<T> MaxExpansions(int maxExpansions)
		{
			((IMultiMatchQuery)this)._MaxExpansions = maxExpansions;
			return this;
		}
		public MultiMatchQueryDescriptor<T> Slop(int slop)
		{
			((IMultiMatchQuery)this)._Slop = slop;
			return this;
		}
		public MultiMatchQueryDescriptor<T> Operator(Operator op)
		{
			((IMultiMatchQuery)this)._Operator = op;
			return this;
		}

		[Obsolete("Use type:best_fields or type:most_fields instead")]
		public MultiMatchQueryDescriptor<T> UseDisMax(bool useDismax)
		{
			((IMultiMatchQuery)this)._UseDisMax = useDismax;
			return this;
		}

		public MultiMatchQueryDescriptor<T> TieBreaker(double tieBreaker)
		{
			((IMultiMatchQuery)this)._TieBreaker = tieBreaker;
			return this;
		}

		public MultiMatchQueryDescriptor<T> Type(TextQueryType type)
		{
			((IMultiMatchQuery)this)._Type = type.ToString().ToLower();
			return this;
		}
	}
}
