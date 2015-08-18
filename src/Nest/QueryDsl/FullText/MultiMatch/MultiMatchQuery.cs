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
	[JsonConverter(typeof(ReadAsTypeJsonConverter<MultiMatchQueryDescriptor<object>>))]
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
		IEnumerable<FieldName> Fields { get; set; }
	}

	public class MultiMatchQuery : QueryBase, IMultiMatchQuery
	{
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
		public bool? Lenient { get; set; }
		public bool? UseDisMax { get; set; }
		public double? TieBreaker { get; set; }
		public string MinimumShouldMatch { get; set; }
		public Operator? Operator { get; set; }
		public IEnumerable<FieldName> Fields { get; set; }

		protected override void WrapInContainer(IQueryContainer c) => c.MultiMatch = this;

		internal static bool IsConditionless(IMultiMatchQuery q) 
			=> !q.Fields.HasAny() || q.Fields.All(f => f.IsConditionless()) || q.Query.IsNullOrEmpty();
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class MultiMatchQueryDescriptor<T> 
		: QueryDescriptorBase<MultiMatchQueryDescriptor<T>, IMultiMatchQuery> 
		, IMultiMatchQuery where T : class
	{
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
		bool? IMultiMatchQuery.Lenient { get; set; }
		bool? IMultiMatchQuery.UseDisMax { get; set; }
		double? IMultiMatchQuery.TieBreaker { get; set; }
		string IMultiMatchQuery.MinimumShouldMatch { get; set; }
		Operator? IMultiMatchQuery.Operator { get; set; }
		IEnumerable<FieldName> IMultiMatchQuery.Fields { get; set; }

		public MultiMatchQueryDescriptor<T> OnFields(IEnumerable<string> fields) =>
			Assign(a => a.Fields = fields?.Select(f => (FieldName)f).ToListOrNullIfEmpty());

		public MultiMatchQueryDescriptor<T> OnFields(params Expression<Func<T, object>>[] objectPaths) =>
			Assign(a => a.Fields = objectPaths?.Select(f => (FieldName)f).ToListOrNullIfEmpty());

		public MultiMatchQueryDescriptor<T> OnFieldsWithBoost(Func<
			FluentDictionary<Expression<Func<T, object>>, double?>, IDictionary<Expression<Func<T, object>>, double?>> boostableSelector) =>
				Assign(a => a.Fields = boostableSelector?
					.Invoke(new FluentDictionary<Expression<Func<T, object>>, double?>())
					.Select(o => FieldName.Create(o.Key, o.Value))
					.ToListOrNullIfEmpty()
				);

		public MultiMatchQueryDescriptor<T> OnFieldsWithBoost(
			Func<FluentDictionary<string, double?>, IDictionary<Expression<Func<T, object>>, double?>> boostableSelector) =>
				Assign(a => a.Fields = boostableSelector?
					.Invoke(new FluentDictionary<string, double?>())
					.Select(o => FieldName.Create(o.Key, o.Value))
					.ToListOrNullIfEmpty()
				);

		public MultiMatchQueryDescriptor<T> Query(string query) => Assign(a => a.Query = query);

		public MultiMatchQueryDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		public MultiMatchQueryDescriptor<T> Fuzziness(double fuzziness) => Assign(a => a.Fuzziness = fuzziness);

		public MultiMatchQueryDescriptor<T> CutoffFrequency(double cutoffFrequency)
			=> Assign(a => a.CutoffFrequency = cutoffFrequency);

		public MultiMatchQueryDescriptor<T> MinimumShouldMatch(string minimumShouldMatch)
			=> Assign(a => a.MinimumShouldMatch = minimumShouldMatch);

		public MultiMatchQueryDescriptor<T> Rewrite(RewriteMultiTerm rewrite) => Assign(a => a.Rewrite = rewrite);

		public MultiMatchQueryDescriptor<T> Lenient(bool lenient = true) => Assign(a => a.Lenient = lenient);

		public MultiMatchQueryDescriptor<T> PrefixLength(int prefixLength) => Assign(a => a.PrefixLength = prefixLength);

		public MultiMatchQueryDescriptor<T> MaxExpansions(int maxExpansions) => Assign(a => a.MaxExpansions = maxExpansions);

		public MultiMatchQueryDescriptor<T> Slop(int slop) => Assign(a => a.Slop = slop);

		public MultiMatchQueryDescriptor<T> Operator(Operator op) => Assign(a => a.Operator = op);

		public MultiMatchQueryDescriptor<T> TieBreaker(double tieBreaker) => Assign(a => a.TieBreaker = tieBreaker);

		public MultiMatchQueryDescriptor<T> Type(TextQueryType type) => Assign(a => a.Type = type);
	}
}
