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
		string Type { get; set; }

		[JsonProperty(PropertyName = "query")]
		string Query { get; set; }

		[JsonProperty(PropertyName = "analyzer")]
		string Analyzer { get; set; }

		[JsonProperty(PropertyName = "rewrite")]
		[JsonConverter(typeof(StringEnumConverter))]
		RewriteMultiTerm? Rewrite { get; set; }

		[JsonProperty(PropertyName = "fuzziness")]
		double? Fuzziness { get; set; }

		[JsonProperty(PropertyName = "cutoff_frequency")]
		double? CutoffFrequency { get; set; }

		[JsonProperty(PropertyName = "prefix_length")]
		int? PrefixLength { get; set; }

		[JsonProperty(PropertyName = "max_expansions")]
		int? MaxExpansions { get; set; }

		[JsonProperty(PropertyName = "slop")]
		int? Slop { get; set; }

		[JsonProperty(PropertyName = "boost")]
		double? Boost { get; set; }

		[JsonProperty(PropertyName = "use_dis_max")]
		bool? UseDisMax { get; set; }

		[JsonProperty(PropertyName = "tie_breaker")]
		double? TieBreaker { get; set; }

		[JsonProperty(PropertyName = "operator")]
		[JsonConverter(typeof(StringEnumConverter))]
		Operator? Operator { get; set; }

		[JsonProperty(PropertyName = "fields")]
		IEnumerable<PropertyPathMarker> Fields { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class MultiMatchQueryDescriptor<T> : IQuery, IMultiMatchQuery where T : class
	{
		string IMultiMatchQuery.Type { get; set; }

		string IMultiMatchQuery.Query { get; set; }

		string IMultiMatchQuery.Analyzer { get; set; }

		RewriteMultiTerm? IMultiMatchQuery.Rewrite { get; set; }

		double? IMultiMatchQuery.Fuzziness { get; set; }

		double? IMultiMatchQuery.CutoffFrequency { get; set; }

		int? IMultiMatchQuery.PrefixLength { get; set; }

		int? IMultiMatchQuery.MaxExpansions { get; set; }

		int? IMultiMatchQuery.Slop { get; set; }

		double? IMultiMatchQuery.Boost { get; set; }

		bool? IMultiMatchQuery.UseDisMax { get; set; }

		double? IMultiMatchQuery.TieBreaker { get; set; }

		Operator? IMultiMatchQuery.Operator { get; set; }

		IEnumerable<PropertyPathMarker> IMultiMatchQuery.Fields { get; set; }

		bool IQuery.IsConditionless
		{
			get
			{
				return !((IMultiMatchQuery)this).Fields.HasAny() || ((IMultiMatchQuery)this).Fields.All(f => f.IsConditionless()) || ((IMultiMatchQuery)this).Query.IsNullOrEmpty();
			}
		}


		public MultiMatchQueryDescriptor<T> OnFields(IEnumerable<string> fields)
		{
			((IMultiMatchQuery)this).Fields = fields.Select(f => (PropertyPathMarker)f);
			return this;
		}
		public MultiMatchQueryDescriptor<T> OnFields(
			params Expression<Func<T, object>>[] objectPaths)
		{
			((IMultiMatchQuery)this).Fields = objectPaths.Select(e => (PropertyPathMarker)e);
			return this;
		}
		public MultiMatchQueryDescriptor<T> OnFieldsWithBoost(Action<FluentDictionary<Expression<Func<T, object>>, double?>> boostableSelector)
		{
			var d = new FluentDictionary<Expression<Func<T, object>>, double?>();
			boostableSelector(d);
			((IMultiMatchQuery)this).Fields = d.Select(o => PropertyPathMarker.Create(o.Key, o.Value));
			return this;
		}

		public MultiMatchQueryDescriptor<T> Query(string query)
		{
			((IMultiMatchQuery)this).Query = query;
			return this;
		}
		public MultiMatchQueryDescriptor<T> Analyzer(string analyzer)
		{
			((IMultiMatchQuery)this).Analyzer = analyzer;
			return this;
		}
		public MultiMatchQueryDescriptor<T> Fuzziness(double fuzziness)
		{
			((IMultiMatchQuery)this).Fuzziness = fuzziness;
			return this;
		}
		public MultiMatchQueryDescriptor<T> CutoffFrequency(double cutoffFrequency)
		{
			((IMultiMatchQuery)this).CutoffFrequency = cutoffFrequency;
			return this;
		}

		public MultiMatchQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite)
		{
			((IMultiMatchQuery)this).Rewrite = rewrite;
			return this;
		}

		public MultiMatchQueryDescriptor<T> Boost(double boost)
		{
			((IMultiMatchQuery)this).Boost = boost;
			return this;
		}
		public MultiMatchQueryDescriptor<T> PrefixLength(int prefixLength)
		{
			((IMultiMatchQuery)this).PrefixLength = prefixLength;
			return this;
		}
		public MultiMatchQueryDescriptor<T> MaxExpansions(int maxExpansions)
		{
			((IMultiMatchQuery)this).MaxExpansions = maxExpansions;
			return this;
		}
		public MultiMatchQueryDescriptor<T> Slop(int slop)
		{
			((IMultiMatchQuery)this).Slop = slop;
			return this;
		}
		public MultiMatchQueryDescriptor<T> Operator(Operator op)
		{
			((IMultiMatchQuery)this).Operator = op;
			return this;
		}

		[Obsolete("Use type:best_fields or type:most_fields instead")]
		public MultiMatchQueryDescriptor<T> UseDisMax(bool useDismax)
		{
			((IMultiMatchQuery)this).UseDisMax = useDismax;
			return this;
		}

		public MultiMatchQueryDescriptor<T> TieBreaker(double tieBreaker)
		{
			((IMultiMatchQuery)this).TieBreaker = tieBreaker;
			return this;
		}

		public MultiMatchQueryDescriptor<T> Type(TextQueryType type)
		{
			((IMultiMatchQuery)this).Type = type.ToString().ToLower();
			return this;
		}
	}
}
