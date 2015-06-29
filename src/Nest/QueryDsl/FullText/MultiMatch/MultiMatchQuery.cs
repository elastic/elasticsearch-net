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
		public string Name { get; set; }
		bool IQuery.Conditionless => IsConditionless(this);
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

		protected override void WrapInContainer(IQueryContainer c) => c.MultiMatch = this;

		internal static bool IsConditionless(IMultiMatchQuery q)
		{
			return !q.Fields.HasAny() || q.Fields.All(f => f.IsConditionless()) || q.Query.IsNullOrEmpty();
		}
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class MultiMatchQueryDescriptor<T> : IMultiMatchQuery where T : class
	{
		private IMultiMatchQuery Self { get { return this; }}
		string IQuery.Name { get; set; }
		bool IQuery.Conditionless => MultiMatchQuery.IsConditionless(this);
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

		public MultiMatchQueryDescriptor<T> Name(string name)
		{
			Self.Name = name;
			return this;
		}

		public MultiMatchQueryDescriptor<T> OnFields(IEnumerable<string> fields)
		{
			Self.Fields = fields.Select(f => (PropertyPathMarker)f);
			return this;
		}

		public MultiMatchQueryDescriptor<T> OnFields(
			params Expression<Func<T, object>>[] objectPaths)
		{
			Self.Fields = objectPaths.Select(e => (PropertyPathMarker)e);
			return this;
		}

		public MultiMatchQueryDescriptor<T> OnFieldsWithBoost(Action<FluentDictionary<Expression<Func<T, object>>, double?>> boostableSelector)
		{
			var d = new FluentDictionary<Expression<Func<T, object>>, double?>();
			boostableSelector(d);
			Self.Fields = d.Select(o => PropertyPathMarker.Create(o.Key, o.Value));
			return this;
		}

		public MultiMatchQueryDescriptor<T> OnFieldsWithBoost(Action<FluentDictionary<string, double?>> boostableSelector)
		{
			var d = new FluentDictionary<string, double?>();
			boostableSelector(d);
			Self.Fields = d.Select(o => PropertyPathMarker.Create(o.Key, o.Value));
			return this;
		}

		public MultiMatchQueryDescriptor<T> Query(string query)
		{
			Self.Query = query;
			return this;
		}

		public MultiMatchQueryDescriptor<T> Analyzer(string analyzer)
		{
			Self.Analyzer = analyzer;
			return this;
		}

		public MultiMatchQueryDescriptor<T> Fuzziness(double fuzziness)
		{
			Self.Fuzziness = fuzziness;
			return this;
		}

		public MultiMatchQueryDescriptor<T> CutoffFrequency(double cutoffFrequency)
		{
			Self.CutoffFrequency = cutoffFrequency;
			return this;
		}

		public MultiMatchQueryDescriptor<T> MinimumShouldMatch(string minimumShouldMatch)
		{
			Self.MinimumShouldMatch = minimumShouldMatch;
			return this;
		}

		public MultiMatchQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite)
		{
			Self.Rewrite = rewrite;
			return this;
		}

		public MultiMatchQueryDescriptor<T> Boost(double boost)
		{
			Self.Boost = boost;
			return this;
		}

		public MultiMatchQueryDescriptor<T> Lenient(bool lenient = true) {
			Self.Lenient = lenient;
			return this;
		}

		public MultiMatchQueryDescriptor<T> PrefixLength(int prefixLength)
		{
			Self.PrefixLength = prefixLength;
			return this;
		}

		public MultiMatchQueryDescriptor<T> MaxExpansions(int maxExpansions)
		{
			Self.MaxExpansions = maxExpansions;
			return this;
		}

		public MultiMatchQueryDescriptor<T> Slop(int slop)
		{
			Self.Slop = slop;
			return this;
		}

		public MultiMatchQueryDescriptor<T> Operator(Operator op)
		{
			Self.Operator = op;
			return this;
		}

		public MultiMatchQueryDescriptor<T> TieBreaker(double tieBreaker)
		{
			Self.TieBreaker = tieBreaker;
			return this;
		}

		public MultiMatchQueryDescriptor<T> Type(TextQueryType type)
		{
			Self.Type = type;
			return this;
		}
	}
}
