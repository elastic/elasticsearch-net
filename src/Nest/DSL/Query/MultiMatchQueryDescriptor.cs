using System;
using System.Collections.Generic;
using System.Linq;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<MultiMatchQueryDescriptor<object>>))]
	public interface IMultiMatchQuery : IQuery
	{
		[JsonProperty(PropertyName = "type")]
		[JsonConverter(typeof(StringEnumConverter))]
		TextQueryType? Type { get; set; }

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

		[JsonProperty(PropertyName = "lenient")]
		bool? Lenient { get; set; }

		[JsonProperty(PropertyName = "use_dis_max")]
		bool? UseDisMax { get; set; }

		[JsonProperty(PropertyName = "tie_breaker")]
		double? TieBreaker { get; set; }

		[JsonProperty(PropertyName = "minimum_should_match")]
		string MinimumShouldMatch { get; set; }

		[JsonProperty(PropertyName = "operator")]
		[JsonConverter(typeof(StringEnumConverter))]
		Operator? Operator { get; set; }

		[JsonProperty(PropertyName = "fields")]
		IEnumerable<PropertyPathMarker> Fields { get; set; }
	}

	public class MultiMatchQuery : PlainQuery, IMultiMatchQuery
	{
		protected override void WrapInContainer(IQueryContainer container)
		{
			container.MultiMatch = this;
		}

		bool IQuery.IsConditionless { get { return false; } }
		public TextQueryType? Type { get; set; }
		public string Query { get; set; }
		public string Analyzer { get; set; }
		public RewriteMultiTerm? Rewrite { get; set; }
		public double? Fuzziness { get; set; }
		public double? CutoffFrequency { get; set; }
		public int? PrefixLength { get; set; }
		public int? MaxExpansions { get; set; }
		public int? Slop { get; set; }
		public double? Boost { get; set; }
		public bool? Lenient { get; set; }
		public bool? UseDisMax { get; set; }
		public double? TieBreaker { get; set; }
		public string MinimumShouldMatch { get; set; }
		public Operator? Operator { get; set; }
		public IEnumerable<PropertyPathMarker> Fields { get; set; }
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class MultiMatchQueryDescriptor<T> : IMultiMatchQuery where T : class
	{
		TextQueryType? IMultiMatchQuery.Type { get; set; }

		string IMultiMatchQuery.Query { get; set; }

		string IMultiMatchQuery.Analyzer { get; set; }

		RewriteMultiTerm? IMultiMatchQuery.Rewrite { get; set; }

		double? IMultiMatchQuery.Fuzziness { get; set; }

		double? IMultiMatchQuery.CutoffFrequency { get; set; }

		int? IMultiMatchQuery.PrefixLength { get; set; }

		int? IMultiMatchQuery.MaxExpansions { get; set; }

		int? IMultiMatchQuery.Slop { get; set; }

		double? IMultiMatchQuery.Boost { get; set; }

		bool? IMultiMatchQuery.Lenient { get; set; }

		bool? IMultiMatchQuery.UseDisMax { get; set; }

		double? IMultiMatchQuery.TieBreaker { get; set; }

		string IMultiMatchQuery.MinimumShouldMatch { get; set; }

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
		public MultiMatchQueryDescriptor<T> OnFieldsWithBoost(Action<FluentDictionary<string, double?>> boostableSelector)
		{
			var d = new FluentDictionary<string, double?>();
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

		public MultiMatchQueryDescriptor<T> MinimumShouldMatch(string minimumShouldMatch)
		{
			((IMultiMatchQuery)this).MinimumShouldMatch = minimumShouldMatch;
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

		public MultiMatchQueryDescriptor<T> Lenient(bool lenient = true) {
			((IMultiMatchQuery)this).Lenient = lenient;
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
			((IMultiMatchQuery)this).Type = type;
			return this;
		}
	}
}
