using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(MatchQueryJsonConverter))]
	public interface IMatchQuery : IFieldNameQuery
	{
		[JsonProperty("analyzer")]
		string Analyzer { get; set; }

		[JsonProperty("cutoff_frequency")]
		double? CutoffFrequency { get; set; }

		[JsonProperty("fuzziness")]
		IFuzziness Fuzziness { get; set; }

		[JsonProperty("fuzzy_rewrite")]
		MultiTermQueryRewrite FuzzyMultiTermQueryRewrite { get; set; }

		[JsonIgnore]
		[Obsolete("Use FuzzyMultiTermQueryRewrite")]
		RewriteMultiTerm? FuzzyRewrite { get; set; }

		[JsonProperty("fuzzy_transpositions")]
		bool? FuzzyTranspositions { get; set; }

		[JsonProperty("lenient")]
		bool? Lenient { get; set; }

		[JsonProperty("max_expansions")]
		int? MaxExpansions { get; set; }

		[JsonProperty("minimum_should_match")]
		MinimumShouldMatch MinimumShouldMatch { get; set; }

		[JsonProperty("operator")]
		Operator? Operator { get; set; }

		[JsonProperty("prefix_length")]
		int? PrefixLength { get; set; }

		[JsonProperty("query")]
		string Query { get; set; }

		[JsonProperty("slop")]
		int? Slop { get; set; }

		[JsonProperty("type")]
		string Type { get; }

		[JsonProperty("zero_terms_query")]
		ZeroTermsQuery? ZeroTermsQuery { get; set; }
	}

	public class MatchQuery : FieldNameQueryBase, IMatchQuery
	{
		public string Analyzer { get; set; }
		public double? CutoffFrequency { get; set; }
		public IFuzziness Fuzziness { get; set; }
		public MultiTermQueryRewrite FuzzyMultiTermQueryRewrite { get; set; }

		[Obsolete("Use FuzzyMultiTermQueryRewrite")]
		public RewriteMultiTerm? FuzzyRewrite
		{
			get => FuzzyMultiTermQueryRewrite?.Rewrite;
			set => FuzzyMultiTermQueryRewrite = value == null ? null : new MultiTermQueryRewrite(value.Value);
		}

		public bool? FuzzyTranspositions { get; set; }
		public bool? Lenient { get; set; }
		public int? MaxExpansions { get; set; }
		public MinimumShouldMatch MinimumShouldMatch { get; set; }
		public Operator? Operator { get; set; }
		public int? PrefixLength { get; set; }

		public string Query { get; set; }
		public int? Slop { get; set; }
		public ZeroTermsQuery? ZeroTermsQuery { get; set; }
		protected override bool Conditionless => IsConditionless(this);
		protected virtual string MatchQueryType => null;
		string IMatchQuery.Type => MatchQueryType;

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Match = this;

		internal static bool IsConditionless(IMatchQuery q) => q.Field.IsConditionless() || q.Query.IsNullOrEmpty();
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class MatchQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<MatchQueryDescriptor<T>, IMatchQuery, T>
			, IMatchQuery where T : class
	{
		protected override bool Conditionless => MatchQuery.IsConditionless(this);
		protected virtual string MatchQueryType => null;
		string IMatchQuery.Analyzer { get; set; }
		double? IMatchQuery.CutoffFrequency { get; set; }
		IFuzziness IMatchQuery.Fuzziness { get; set; }
		MultiTermQueryRewrite IMatchQuery.FuzzyMultiTermQueryRewrite { get; set; }

		RewriteMultiTerm? IMatchQuery.FuzzyRewrite
		{
			get => Self.FuzzyMultiTermQueryRewrite?.Rewrite;
			set => Self.FuzzyMultiTermQueryRewrite = value == null ? null : new MultiTermQueryRewrite(value.Value);
		}

		bool? IMatchQuery.FuzzyTranspositions { get; set; }
		bool? IMatchQuery.Lenient { get; set; }
		int? IMatchQuery.MaxExpansions { get; set; }
		MinimumShouldMatch IMatchQuery.MinimumShouldMatch { get; set; }
		Operator? IMatchQuery.Operator { get; set; }
		int? IMatchQuery.PrefixLength { get; set; }
		string IMatchQuery.Query { get; set; }
		int? IMatchQuery.Slop { get; set; }
		string IMatchQuery.Type => MatchQueryType;
		ZeroTermsQuery? IMatchQuery.ZeroTermsQuery { get; set; }

		public MatchQueryDescriptor<T> Query(string query) => Assign(a => a.Query = query);

		public MatchQueryDescriptor<T> Lenient(bool? lenient = true) => Assign(a => a.Lenient = lenient);

		public MatchQueryDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		public MatchQueryDescriptor<T> Fuzziness(Fuzziness fuzziness) => Assign(a => a.Fuzziness = fuzziness);

		public MatchQueryDescriptor<T> FuzzyTranspositions(bool? fuzzyTranspositions = true) =>
			Assign(a => a.FuzzyTranspositions = fuzzyTranspositions);

		public MatchQueryDescriptor<T> CutoffFrequency(double? cutoffFrequency) => Assign(a => a.CutoffFrequency = cutoffFrequency);

		[Obsolete("Use FuzzyRewrite(MultiTermQueryRewrite rewrite)")]
		public MatchQueryDescriptor<T> FuzzyRewrite(RewriteMultiTerm? rewrite) =>
			Assign(a =>
			{
				a.FuzzyMultiTermQueryRewrite = rewrite != null
					? new MultiTermQueryRewrite(rewrite.Value)
					: null;
			});

		public MatchQueryDescriptor<T> FuzzyRewrite(MultiTermQueryRewrite rewrite) => Assign(a => Self.FuzzyMultiTermQueryRewrite = rewrite);

		public MatchQueryDescriptor<T> PrefixLength(int? prefixLength) => Assign(a => a.PrefixLength = prefixLength);

		public MatchQueryDescriptor<T> MaxExpansions(int? maxExpansions) => Assign(a => a.MaxExpansions = maxExpansions);

		public MatchQueryDescriptor<T> Slop(int? slop) => Assign(a => a.Slop = slop);

		public MatchQueryDescriptor<T> MinimumShouldMatch(MinimumShouldMatch minimumShouldMatch) =>
			Assign(a => a.MinimumShouldMatch = minimumShouldMatch);

		public MatchQueryDescriptor<T> Operator(Operator? op) => Assign(a => a.Operator = op);

		public MatchQueryDescriptor<T> ZeroTermsQuery(ZeroTermsQuery? zeroTermsQuery) => Assign(a => a.ZeroTermsQuery = zeroTermsQuery);
	}
}
