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
		MultiMatchQueryDescriptor<T> _assign(Action<IMultiMatchQuery> assigner) => Fluent.Assign(this, assigner);

		private IMultiMatchQuery Self { get { return this; } }
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

		public MultiMatchQueryDescriptor<T> Name(string name) => _assign(a => a.Name = name);

		public MultiMatchQueryDescriptor<T> OnFields(IEnumerable<string> fields) =>
			_assign(a => a.Fields = fields?.Select(f => (PropertyPathMarker)f).ToListOrNullIfEmpty());

		public MultiMatchQueryDescriptor<T> OnFields(params Expression<Func<T, object>>[] objectPaths) =>
			_assign(a => a.Fields = objectPaths?.Select(f => (PropertyPathMarker)f).ToListOrNullIfEmpty());

		public MultiMatchQueryDescriptor<T> OnFieldsWithBoost(Func<
			FluentDictionary<Expression<Func<T, object>>, double?>, IDictionary<Expression<Func<T, object>>, double?>> boostableSelector) =>
				_assign(a => a.Fields = boostableSelector?
					.Invoke(new FluentDictionary<Expression<Func<T, object>>, double?>())
					.Select(o => PropertyPathMarker.Create(o.Key, o.Value))
					.ToListOrNullIfEmpty()
				);

		public MultiMatchQueryDescriptor<T> OnFieldsWithBoost(
			Func<FluentDictionary<string, double?>, IDictionary<Expression<Func<T, object>>, double?>> boostableSelector) =>
				_assign(a => a.Fields = boostableSelector?
					.Invoke(new FluentDictionary<string, double?>())
					.Select(o => PropertyPathMarker.Create(o.Key, o.Value))
					.ToListOrNullIfEmpty()
				);

		public MultiMatchQueryDescriptor<T> Query(string query) => _assign(a => a.Query = query);

		public MultiMatchQueryDescriptor<T> Analyzer(string analyzer) => _assign(a => a.Analyzer = analyzer);

		public MultiMatchQueryDescriptor<T> Fuzziness(double fuzziness) => _assign(a => a.Fuzziness = fuzziness);

		public MultiMatchQueryDescriptor<T> CutoffFrequency(double cutoffFrequency)
			=> _assign(a => a.CutoffFrequency = cutoffFrequency);

		public MultiMatchQueryDescriptor<T> MinimumShouldMatch(string minimumShouldMatch)
			=> _assign(a => a.MinimumShouldMatch = minimumShouldMatch);

		public MultiMatchQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite) => _assign(a => a.Rewrite = rewrite);

		public MultiMatchQueryDescriptor<T> Boost(double boost) => _assign(a => a.Boost = boost);

		public MultiMatchQueryDescriptor<T> Lenient(bool lenient = true) => _assign(a => a.Lenient = lenient);

		public MultiMatchQueryDescriptor<T> PrefixLength(int prefixLength) => _assign(a => a.PrefixLength = prefixLength);

		public MultiMatchQueryDescriptor<T> MaxExpansions(int maxExpansions) => _assign(a => a.MaxExpansions = maxExpansions);

		public MultiMatchQueryDescriptor<T> Slop(int slop) => _assign(a => a.Slop = slop);

		public MultiMatchQueryDescriptor<T> Operator(Operator op) => _assign(a => a.Operator = op);

		public MultiMatchQueryDescriptor<T> TieBreaker(double tieBreaker) => _assign(a => a.TieBreaker = tieBreaker);

		public MultiMatchQueryDescriptor<T> Type(TextQueryType type) => _assign(a => a.Type = type);

	}
}
