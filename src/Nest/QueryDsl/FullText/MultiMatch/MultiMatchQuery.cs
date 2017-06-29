using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<MultiMatchQueryDescriptor<object>>))]
	public interface IMultiMatchQuery : IQuery
	{
		[JsonProperty("type")]
		TextQueryType? Type { get; set; }

		[JsonProperty("query")]
		string Query { get; set; }

		[JsonProperty("analyzer")]
		string Analyzer { get; set; }

		[JsonIgnore]
		[Obsolete("Use FuzzyMultiTermQueryRewrite")]
		RewriteMultiTerm? FuzzyRewrite { get; set; }

		[JsonProperty("fuzzy_rewrite")]
		MultiTermQueryRewrite FuzzyMultiTermQueryRewrite { get; set; }

		/// <summary>
		/// Allows fuzzy matching based on the type of field being queried.
		/// Cannot be used with the
		/// <see cref="TextQueryType.CrossFields"/>,
		/// <see cref="TextQueryType.Phrase"/> or
		/// <see cref="TextQueryType.PhrasePrefix"/> types.
		/// </summary>
		[JsonProperty("fuzziness")]
		Fuzziness Fuzziness { get; set; }

		[JsonProperty("cutoff_frequency")]
		double? CutoffFrequency { get; set; }

		[JsonProperty("prefix_length")]
		int? PrefixLength { get; set; }

		[JsonProperty("max_expansions")]
		int? MaxExpansions { get; set; }

		[JsonProperty("slop")]
		int? Slop { get; set; }

		[JsonProperty("lenient")]
		bool? Lenient { get; set; }

		[JsonProperty("use_dis_max")]
		bool? UseDisMax { get; set; }

		[JsonProperty("tie_breaker")]
		double? TieBreaker { get; set; }

		[JsonProperty("minimum_should_match")]
		MinimumShouldMatch MinimumShouldMatch { get; set; }

		[JsonProperty("operator")]
		Operator? Operator { get; set; }

		[JsonProperty("fields")]
		Fields Fields { get; set; }

		[JsonProperty("zero_terms_query")]
		ZeroTermsQuery? ZeroTermsQuery { get; set; }
	}

	public class MultiMatchQuery : QueryBase, IMultiMatchQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public TextQueryType? Type { get; set; }
		public string Query { get; set; }
		public string Analyzer { get; set; }
		[Obsolete("Use FuzzyMultiTermQueryRewrite")]
		public RewriteMultiTerm? FuzzyRewrite
		{
			get => FuzzyMultiTermQueryRewrite?.Rewrite;
			set => FuzzyMultiTermQueryRewrite = value == null ? null : new MultiTermQueryRewrite(value.Value);
		}
		public MultiTermQueryRewrite FuzzyMultiTermQueryRewrite { get; set; }

		/// <summary>
		/// Allows fuzzy matching based on the type of field being queried.
		/// Cannot be used with the
		/// <see cref="TextQueryType.CrossFields"/>,
		/// <see cref="TextQueryType.Phrase"/> or
		/// <see cref="TextQueryType.PhrasePrefix"/> types.
		/// </summary>
		public Fuzziness Fuzziness { get; set; }
		public double? CutoffFrequency { get; set; }
		public int? PrefixLength { get; set; }
		public int? MaxExpansions { get; set; }
		public int? Slop { get; set; }
		public bool? Lenient { get; set; }
		public bool? UseDisMax { get; set; }
		public double? TieBreaker { get; set; }
		public MinimumShouldMatch MinimumShouldMatch { get; set; }
		public Operator? Operator { get; set; }
		public Fields Fields { get; set; }
		public ZeroTermsQuery? ZeroTermsQuery { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.MultiMatch = this;

		internal static bool IsConditionless(IMultiMatchQuery q) => q.Fields.IsConditionless() || q.Query.IsNullOrEmpty();
	}

	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class MultiMatchQueryDescriptor<T>
		: QueryDescriptorBase<MultiMatchQueryDescriptor<T>, IMultiMatchQuery>
		, IMultiMatchQuery where T : class
	{
		protected override bool Conditionless => MultiMatchQuery.IsConditionless(this);
		TextQueryType? IMultiMatchQuery.Type { get; set; }
		string IMultiMatchQuery.Query { get; set; }
		string IMultiMatchQuery.Analyzer { get; set; }
		[Obsolete("Use FuzzyMultiTermQueryRewrite")]
		RewriteMultiTerm? IMultiMatchQuery.FuzzyRewrite
		{
			get => Self.FuzzyMultiTermQueryRewrite?.Rewrite;
			set => Self.FuzzyMultiTermQueryRewrite = value == null ? null : new MultiTermQueryRewrite(value.Value);
		}
		MultiTermQueryRewrite IMultiMatchQuery.FuzzyMultiTermQueryRewrite { get; set; }
		Fuzziness IMultiMatchQuery.Fuzziness { get; set; }
		double? IMultiMatchQuery.CutoffFrequency { get; set; }
		int? IMultiMatchQuery.PrefixLength { get; set; }
		int? IMultiMatchQuery.MaxExpansions { get; set; }
		int? IMultiMatchQuery.Slop { get; set; }
		bool? IMultiMatchQuery.Lenient { get; set; }
		bool? IMultiMatchQuery.UseDisMax { get; set; }
		double? IMultiMatchQuery.TieBreaker { get; set; }
		MinimumShouldMatch IMultiMatchQuery.MinimumShouldMatch { get; set; }
		Operator? IMultiMatchQuery.Operator { get; set; }
		Fields IMultiMatchQuery.Fields { get; set; }
		ZeroTermsQuery? IMultiMatchQuery.ZeroTermsQuery { get; set; }

		public MultiMatchQueryDescriptor<T> Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.Fields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public MultiMatchQueryDescriptor<T> Fields(Fields fields) => Assign(a => a.Fields = fields);

		public MultiMatchQueryDescriptor<T> Query(string query) => Assign(a => a.Query = query);

		public MultiMatchQueryDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		/// <summary>
		/// Allows fuzzy matching based on the type of field being queried.
		/// Cannot be used with the
		/// <see cref="TextQueryType.CrossFields"/>,
		/// <see cref="TextQueryType.Phrase"/> or
		/// <see cref="TextQueryType.PhrasePrefix"/> types.
		/// </summary>
		public MultiMatchQueryDescriptor<T> Fuzziness(Fuzziness fuzziness) => Assign(a => a.Fuzziness = fuzziness);

		public MultiMatchQueryDescriptor<T> CutoffFrequency(double cutoffFrequency)
			=> Assign(a => a.CutoffFrequency = cutoffFrequency);

		public MultiMatchQueryDescriptor<T> MinimumShouldMatch(MinimumShouldMatch minimumShouldMatch)
			=> Assign(a => a.MinimumShouldMatch = minimumShouldMatch);

		[Obsolete("Use FuzzyRewrite(MultiTermQueryRewrite rewrite)")]
		public MultiMatchQueryDescriptor<T> FuzzyRewrite(RewriteMultiTerm? rewrite) =>
			Assign(a =>
			{
				a.FuzzyMultiTermQueryRewrite = rewrite != null
					? new MultiTermQueryRewrite(rewrite.Value)
					: null;
			});

		public MultiMatchQueryDescriptor<T> FuzzyRewrite(MultiTermQueryRewrite rewrite) => Assign(a => Self.FuzzyMultiTermQueryRewrite = rewrite);

		public MultiMatchQueryDescriptor<T> Lenient(bool? lenient = true) => Assign(a => a.Lenient = lenient);

		public MultiMatchQueryDescriptor<T> PrefixLength(int? prefixLength) => Assign(a => a.PrefixLength = prefixLength);

		public MultiMatchQueryDescriptor<T> MaxExpansions(int? maxExpansions) => Assign(a => a.MaxExpansions = maxExpansions);

		public MultiMatchQueryDescriptor<T> Slop(int? slop) => Assign(a => a.Slop = slop);

		public MultiMatchQueryDescriptor<T> Operator(Operator? op) => Assign(a => a.Operator = op);

		public MultiMatchQueryDescriptor<T> TieBreaker(double? tieBreaker) => Assign(a => a.TieBreaker = tieBreaker);

		public MultiMatchQueryDescriptor<T> Type(TextQueryType? type) => Assign(a => a.Type = type);

		public MultiMatchQueryDescriptor<T> UseDisMax(bool? useDisMax = true) => Assign(a => a.UseDisMax = useDisMax);

		public MultiMatchQueryDescriptor<T> ZeroTermsQuery(ZeroTermsQuery? zeroTermsQuery) => Assign(a => a.ZeroTermsQuery = zeroTermsQuery);
	}
}
