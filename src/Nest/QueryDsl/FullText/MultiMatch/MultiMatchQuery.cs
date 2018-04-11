using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Nest
{
	/// <summary>
	/// A match query across multiple fields.
	/// </summary>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<MultiMatchQueryDescriptor<object>>))]
	public interface IMultiMatchQuery : IQuery
	{
		/// <summary>
		/// How the fields should be combined to build the text query.
		/// Default is <see cref="TextQueryType.BestFields"/>
		/// </summary>
		[JsonProperty("type")]
		TextQueryType? Type { get; set; }

		/// <summary>
		/// The query to execute
		/// </summary>
		[JsonProperty("query")]
		string Query { get; set; }

		/// <summary>
		/// The analyzer name used to analyze the query
		/// </summary>
		[JsonProperty("analyzer")]
		string Analyzer { get; set; }

		/// <summary>
		/// Controls how the query is rewritten if <see cref="Fuzziness"/> is set.
		/// In this scenario, the default is <see cref="MultiTermQueryRewrite.TopTermsBlendedFreqs"/>.
		/// </summary>
		[JsonProperty("fuzzy_rewrite")]
		MultiTermQueryRewrite FuzzyRewrite { get; set; }

		/// <summary>
		/// Allows fuzzy matching based on the type of field being queried.
		/// Cannot be used with the
		/// <see cref="TextQueryType.CrossFields"/>,
		/// <see cref="TextQueryType.Phrase"/> or
		/// <see cref="TextQueryType.PhrasePrefix"/> types.
		/// </summary>
		[JsonProperty("fuzziness")]
		Fuzziness Fuzziness { get; set; }

		/// <summary>
		/// Sets whether transpositions are supported in fuzzy queries.
		/// <para />
		/// The default metric used by fuzzy queries to determine a match is the Damerau-Levenshtein
		/// distance formula which supports transpositions. Setting transposition to false will
		/// switch to classic Levenshtein distance.
		/// If not set, Damerau-Levenshtein distance metric will be used.
		/// </summary>
		[JsonProperty("fuzzy_transpositions")]
		bool? FuzzyTranspositions { get; set; }

		/// <summary>
		/// A cutoff frequency that allows specifying an absolute or relative document frequency where
		/// high frequency terms are moved into an optional subquery and are only scored if one of the low frequency
		/// (below the cutoff) terms in the case of <see cref="Operator.Or"/>,
		/// or all of the low frequency terms in the case of an <see cref="Operator.And"/> match.
		/// </summary>
		[JsonProperty("cutoff_frequency")]
		double? CutoffFrequency { get; set; }

		/// <summary>
		/// Set the prefix length for fuzzy queries. Default is <c>0</c>.
		/// </summary>
		[JsonProperty("prefix_length")]
		int? PrefixLength { get; set; }

		/// <summary>
		/// Controls the number of terms fuzzy queries will expand to. Defaults to <c>50</c>
		/// </summary>
		[JsonProperty("max_expansions")]
		int? MaxExpansions { get; set; }

		/// <summary>
		/// How far apart terms are allowed to be while still considering the document to be a match.
		/// </summary>
		[JsonProperty("slop")]
		int? Slop { get; set; }

		/// <summary>
		/// If set to <c>true</c> will cause format based failures (like providing text to a numeric field)
		/// to be ignored
		/// </summary>
		[JsonProperty("lenient")]
		bool? Lenient { get; set; }

		/// <summary>
		/// By default, a <see cref="IMultiMatchQuery"/> generates a match clause per field, then wraps them
		/// in a <see cref="IDisMaxQuery"/>. By setting <see cref="UseDisMax"/> to <c>false</c>,
		/// they will be wrapped in a <see cref="IBoolQuery"/> instead.
		/// </summary>
		[JsonProperty("use_dis_max")]
		bool? UseDisMax { get; set; }

		/// <summary>
		/// Used to influence how the score is calculated for <see cref="TextQueryType.BestFields"/>. If specified,
		/// score is calculated using
		/// </summary>
		[JsonProperty("tie_breaker")]
		double? TieBreaker { get; set; }

		/// <summary>
		/// A value controlling how many "should" clauses in the resulting boolean query should match.
		/// It can be an absolute value, a percentage or a combination of both.
		/// </summary>
		[JsonProperty("minimum_should_match")]
		MinimumShouldMatch MinimumShouldMatch { get; set; }

		/// <summary>
		/// The operator used if no explicit operator is specified.
		/// The default operator is <see cref="Operator.Or"/>
		/// </summary>
		/// <remarks>
		/// <see cref="TextQueryType.BestFields"/> and <see cref="TextQueryType.MostFields"/> types are field-centric ;
		/// they generate a match query per field. This means that <see cref="Operator"/> and <see cref="MinimumShouldMatch"/>
		/// are applied to each field individually, which is probably not what you want.
		/// Consider using <see cref="TextQueryType.CrossFields"/>.
		/// </remarks>
		[JsonProperty("operator")]
		Operator? Operator { get; set; }

		/// <summary>
		/// The fields to perform the query against.
		/// </summary>
		[JsonProperty("fields")]
		Fields Fields { get; set; }

		/// <summary>
		/// If the analyzer used removes all tokens in a query like a stop filter does, the default behavior is
		/// to match no documents at all. In order to change that, <see cref="ZeroTermsQuery"/> can be used,
		/// which accepts <see cref="ZeroTermsQuery.None"/> (default) and <see cref="ZeroTermsQuery.All"/>
		/// which corresponds to a match_all query.
		/// </summary>
		[JsonProperty("zero_terms_query")]
		ZeroTermsQuery? ZeroTermsQuery { get; set; }

    	/// <summary></summary>
		[JsonProperty("auto_generate_synonyms_phrase_query")]
		bool? AutoGenerateSynonymsPhraseQuery { get; set; }
	}

	/// <inheritdoc cref="IMultiMatchQuery" />
	public class MultiMatchQuery : QueryBase, IMultiMatchQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		/// <inheritdoc />
		public TextQueryType? Type { get; set; }
		/// <inheritdoc />
		public string Query { get; set; }
		/// <inheritdoc />
		public string Analyzer { get; set; }
		/// <inheritdoc />
		public MultiTermQueryRewrite FuzzyRewrite { get; set; }
		/// <inheritdoc />
		public Fuzziness Fuzziness { get; set; }
		/// <inheritdoc />
		public double? CutoffFrequency { get; set; }
		/// <inheritdoc />
		public int? PrefixLength { get; set; }
		/// <inheritdoc />
		public int? MaxExpansions { get; set; }
		/// <inheritdoc />
		public int? Slop { get; set; }
		/// <inheritdoc />
		public bool? Lenient { get; set; }
		/// <inheritdoc />
		public bool? UseDisMax { get; set; }
		/// <inheritdoc />
		public double? TieBreaker { get; set; }
		/// <inheritdoc />
		public MinimumShouldMatch MinimumShouldMatch { get; set; }
		/// <inheritdoc />
		public Operator? Operator { get; set; }
		/// <inheritdoc />
		public Fields Fields { get; set; }
		/// <inheritdoc />
		public ZeroTermsQuery? ZeroTermsQuery { get; set; }
		/// <inheritdoc />
		public bool? FuzzyTranspositions { get; set; }
    	/// <inheritdoc />
		public bool? AutoGenerateSynonymsPhraseQuery { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.MultiMatch = this;
		internal static bool IsConditionless(IMultiMatchQuery q) => q.Query.IsNullOrEmpty();
	}

	/// <inheritdoc cref="IMultiMatchQuery"/>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	public class MultiMatchQueryDescriptor<T>
		: QueryDescriptorBase<MultiMatchQueryDescriptor<T>, IMultiMatchQuery>
		, IMultiMatchQuery where T : class
	{
		protected override bool Conditionless => MultiMatchQuery.IsConditionless(this);
		TextQueryType? IMultiMatchQuery.Type { get; set; }
		string IMultiMatchQuery.Query { get; set; }
		string IMultiMatchQuery.Analyzer { get; set; }
		MultiTermQueryRewrite IMultiMatchQuery.FuzzyRewrite { get; set; }
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
		bool? IMultiMatchQuery.FuzzyTranspositions { get; set; }
		bool? IMultiMatchQuery.AutoGenerateSynonymsPhraseQuery { get; set; }

		/// <inheritdoc cref="IMultiMatchQuery.Fields"/>
		public MultiMatchQueryDescriptor<T> Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.Fields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		/// <inheritdoc cref="IMultiMatchQuery.Fields"/>
		public MultiMatchQueryDescriptor<T> Fields(Fields fields) => Assign(a => a.Fields = fields);

		/// <inheritdoc cref="IMultiMatchQuery.Query"/>
		public MultiMatchQueryDescriptor<T> Query(string query) => Assign(a => a.Query = query);

		/// <inheritdoc cref="IMultiMatchQuery.Analyzer"/>
		public MultiMatchQueryDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		/// <inheritdoc cref="IMultiMatchQuery.Fuzziness"/>
		public MultiMatchQueryDescriptor<T> Fuzziness(Fuzziness fuzziness) => Assign(a => a.Fuzziness = fuzziness);

		/// <inheritdoc cref="IMultiMatchQuery.CutoffFrequency"/>
		public MultiMatchQueryDescriptor<T> CutoffFrequency(double? cutoffFrequency)
			=> Assign(a => a.CutoffFrequency = cutoffFrequency);

		/// <inheritdoc cref="IMultiMatchQuery.MinimumShouldMatch"/>
		public MultiMatchQueryDescriptor<T> MinimumShouldMatch(MinimumShouldMatch minimumShouldMatch)
			=> Assign(a => a.MinimumShouldMatch = minimumShouldMatch);

		/// <inheritdoc cref="IMultiMatchQuery.FuzzyRewrite"/>
		public MultiMatchQueryDescriptor<T> FuzzyRewrite(MultiTermQueryRewrite rewrite) => Assign(a => Self.FuzzyRewrite = rewrite);

		/// <inheritdoc cref="IMultiMatchQuery.FuzzyTranspositions"/>
		public MultiMatchQueryDescriptor<T> FuzzyTranspositions(bool? fuzzyTranpositions = true) =>
			Assign(a => a.FuzzyTranspositions = fuzzyTranpositions);

		/// <inheritdoc cref="IMultiMatchQuery.Lenient"/>
		public MultiMatchQueryDescriptor<T> Lenient(bool? lenient = true) => Assign(a => a.Lenient = lenient);

		/// <inheritdoc cref="IMultiMatchQuery.PrefixLength"/>
		public MultiMatchQueryDescriptor<T> PrefixLength(int? prefixLength) => Assign(a => a.PrefixLength = prefixLength);

		/// <inheritdoc cref="IMultiMatchQuery.MaxExpansions"/>
		public MultiMatchQueryDescriptor<T> MaxExpansions(int? maxExpansions) => Assign(a => a.MaxExpansions = maxExpansions);

		/// <inheritdoc cref="IMultiMatchQuery.Slop"/>
		public MultiMatchQueryDescriptor<T> Slop(int? slop) => Assign(a => a.Slop = slop);

		/// <inheritdoc cref="IMultiMatchQuery.Operator"/>
		public MultiMatchQueryDescriptor<T> Operator(Operator? op) => Assign(a => a.Operator = op);

		/// <inheritdoc cref="IMultiMatchQuery.TieBreaker"/>
		public MultiMatchQueryDescriptor<T> TieBreaker(double? tieBreaker) => Assign(a => a.TieBreaker = tieBreaker);

		/// <inheritdoc cref="IMultiMatchQuery.Type"/>
		public MultiMatchQueryDescriptor<T> Type(TextQueryType? type) => Assign(a => a.Type = type);

		/// <inheritdoc cref="IMultiMatchQuery.UseDisMax"/>
		public MultiMatchQueryDescriptor<T> UseDisMax(bool? useDisMax = true) => Assign(a => a.UseDisMax = useDisMax);

		/// <inheritdoc cref="IMultiMatchQuery.ZeroTermsQuery"/>
		public MultiMatchQueryDescriptor<T> ZeroTermsQuery(ZeroTermsQuery? zeroTermsQuery) => Assign(a => a.ZeroTermsQuery = zeroTermsQuery);

    	/// <inheritdoc cref="IMultiMatchQuery.AutoGenerateSynonymsPhraseQuery"/>
		public MultiMatchQueryDescriptor<T> AutoGenerateSynonymsPhraseQuery(bool? autoGenerateSynonymsPhraseQuery = true) =>
			Assign(a => a.AutoGenerateSynonymsPhraseQuery = autoGenerateSynonymsPhraseQuery);
	}
}
