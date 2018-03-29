using System;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A query that uses the SimpleQueryParser to parse its context.
	/// Unlike the regular <see cref="IQueryStringQuery"/>, the <see cref="ISimpleQueryStringQuery"/> query will
	/// never throw an exception, and discards invalid parts of the query.
	/// </summary>
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SimpleQueryStringQueryDescriptor<object>>))]
	public interface ISimpleQueryStringQuery : IQuery
	{
		/// <summary>
		/// The fields to perform the parsed query against.
		/// Defaults to the <c>index.query.default_field</c> index settings, which in turn defaults to <c>*</c>.
		/// <c>*</c> extracts all fields in the mapping that are eligible to term queries and filters the metadata fields.
		/// </summary>
		[JsonProperty("fields")]
		Fields Fields { get; set; }

		/// <summary>
		/// The query to be parsed
		/// </summary>
		[JsonProperty("query")]
		string Query { get; set; }

		/// <summary>
		/// The analyzer name used to analyze the query
		/// </summary>
		[JsonProperty("analyzer")]
		string Analyzer { get; set; }

		/// <summary>
		/// The default operator used if no explicit operator is specified
		/// The default operator is <see cref="Operator.Or"/>
		/// </summary>
		[JsonProperty("default_operator")]
		Operator? DefaultOperator { get; set; }

		/// <summary>
		/// Flags specifying which features to enable.
		/// Defaults to <see cref="SimpleQueryStringFlags.All"/>.
		/// </summary>
		[JsonProperty("flags")]
		SimpleQueryStringFlags? Flags { get; set; }

		/// <summary>
		/// If set to <c>true</c> will cause format based failures (like providing text to a numeric field)
		/// to be ignored
		/// </summary>
		[JsonProperty("lenient")]
		bool? Lenient { get; set; }

		/// <summary>
		/// By default, wildcards terms in a query are not analyzed.
		/// By setting this value to <c>true</c>, a best effort will be made to analyze those as well.
		/// </summary>
		[JsonProperty("analyze_wildcard")]
		bool? AnalyzeWildcard { get; set; }

		/// <summary>
		/// A value controlling how many "should" clauses in the resulting boolean query should match.
		/// It can be an absolute value, a percentage or a combination of both
		/// </summary>
		[JsonProperty("minimum_should_match")]
		MinimumShouldMatch MinimumShouldMatch { get; set; }

		/// <summary>
		/// A suffix to append to fields for quoted parts of the query string.
		/// This allows to use a field that has a different analysis chain for exact matching.
		/// </summary>
		[JsonProperty("quote_field_suffix")]
		string QuoteFieldSuffix { get; set; }

		/// <summary>
		/// Set the prefix length for fuzzy queries. Default is <c>0</c>
		/// </summary>
		[JsonProperty("fuzzy_prefix_length")]
		int? FuzzyPrefixLength { get; set; }

		/// <summary>
		/// Controls the number of terms fuzzy queries will expand to. Defaults to <c>50</c>
		/// </summary>
		[JsonProperty("fuzzy_max_expansions")]
		int? FuzzyMaxExpansions { get; set; }

		/// <summary>
		/// Sets whether transpositions are supported in fuzzy queries. Default is <c>true</c>.
		/// <para />
		/// The default metric used by fuzzy queries to determine a match is the Damerau-Levenshtein
		/// distance formula which supports transpositions. Setting transposition to false will
		/// switch to classic Levenshtein distance.
		/// If not set, Damerau-Levenshtein distance metric will be used.
		/// </summary>
		[JsonProperty("fuzzy_transpositions")]
		bool? FuzzyTranspositions { get; set; }

    /// <summary></summary>
		[JsonProperty("auto_generate_synonyms_phrase_query")]
		bool? AutoGenerateSynonymsPhraseQuery { get; set; }
	}

	/// <inheritdoc cref="ISimpleQueryStringQuery"/>
	public class SimpleQueryStringQuery : QueryBase, ISimpleQueryStringQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		/// <inheritdoc />
		public Fields Fields { get; set; }
		/// <inheritdoc />
		public string Query { get; set; }
		/// <inheritdoc />
		public string Analyzer { get; set; }
		/// <inheritdoc />
		public Operator? DefaultOperator { get; set; }
		/// <inheritdoc />
		public SimpleQueryStringFlags? Flags { get; set; }
		/// <inheritdoc />
		public bool? Lenient { get; set; }
		/// <inheritdoc />
		public bool? AnalyzeWildcard { get; set; }
		/// <inheritdoc />
		public MinimumShouldMatch MinimumShouldMatch { get; set; }
		/// <inheritdoc />
		public string QuoteFieldSuffix { get; set; }
		/// <inheritdoc />
		public int? FuzzyPrefixLength { get; set; }
		/// <inheritdoc />
		public int? FuzzyMaxExpansions { get; set; }
		/// <inheritdoc />
		public bool? FuzzyTranspositions { get; set; }
    /// <inheritdoc />
		public bool? AutoGenerateSynonymsPhraseQuery { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.SimpleQueryString = this;
		internal static bool IsConditionless(ISimpleQueryStringQuery q) => q.Query.IsNullOrEmpty();
	}

	/// <inheritdoc cref="ISimpleQueryStringQuery"/>
	public class SimpleQueryStringQueryDescriptor<T>
		: QueryDescriptorBase<SimpleQueryStringQueryDescriptor<T>, ISimpleQueryStringQuery>
		, ISimpleQueryStringQuery where T : class
	{
		protected override bool Conditionless => SimpleQueryStringQuery.IsConditionless(this);
		Fields ISimpleQueryStringQuery.Fields { get; set; }
		string ISimpleQueryStringQuery.Query { get; set; }
		string ISimpleQueryStringQuery.Analyzer { get; set; }
		Operator? ISimpleQueryStringQuery.DefaultOperator { get; set; }
		SimpleQueryStringFlags? ISimpleQueryStringQuery.Flags { get; set; }
		bool? ISimpleQueryStringQuery.AnalyzeWildcard { get; set; }
		bool? ISimpleQueryStringQuery.Lenient { get; set; }
		MinimumShouldMatch ISimpleQueryStringQuery.MinimumShouldMatch { get; set; }
		string ISimpleQueryStringQuery.QuoteFieldSuffix { get; set; }
		int? ISimpleQueryStringQuery.FuzzyPrefixLength { get; set; }
		int? ISimpleQueryStringQuery.FuzzyMaxExpansions { get; set; }
		bool? ISimpleQueryStringQuery.FuzzyTranspositions { get; set; }
		bool? ISimpleQueryStringQuery.AutoGenerateSynonymsPhraseQuery { get; set; }

		/// <inheritdoc cref="ISimpleQueryStringQuery.Fields"/>
		public SimpleQueryStringQueryDescriptor<T> Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.Fields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		/// <inheritdoc cref="ISimpleQueryStringQuery.Fields"/>
		public SimpleQueryStringQueryDescriptor<T> Fields(Fields fields) => Assign(a => a.Fields = fields);

		/// <inheritdoc cref="ISimpleQueryStringQuery.Query"/>
		public SimpleQueryStringQueryDescriptor<T> Query(string query) => Assign(a => a.Query = query);

		/// <inheritdoc cref="ISimpleQueryStringQuery.Analyzer"/>
		public SimpleQueryStringQueryDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		/// <inheritdoc cref="ISimpleQueryStringQuery.DefaultOperator"/>
		public SimpleQueryStringQueryDescriptor<T> DefaultOperator(Operator? op) => Assign(a => a.DefaultOperator = op);

		/// <inheritdoc cref="ISimpleQueryStringQuery.Flags"/>
		public SimpleQueryStringQueryDescriptor<T> Flags(SimpleQueryStringFlags? flags) => Assign(a => a.Flags = flags);

		/// <inheritdoc cref="ISimpleQueryStringQuery.AnalyzeWildcard"/>
		public SimpleQueryStringQueryDescriptor<T> AnalyzeWildcard(bool? analyzeWildcard = true) =>
			Assign(a => a.AnalyzeWildcard = analyzeWildcard);

		/// <inheritdoc cref="ISimpleQueryStringQuery.Lenient"/>
		public SimpleQueryStringQueryDescriptor<T> Lenient(bool? lenient = true) => Assign(a => a.Lenient = lenient);

		/// <inheritdoc cref="ISimpleQueryStringQuery.MinimumShouldMatch"/>
		public SimpleQueryStringQueryDescriptor<T> MinimumShouldMatch(MinimumShouldMatch minimumShouldMatch) =>
			Assign(a => a.MinimumShouldMatch = minimumShouldMatch);

		/// <inheritdoc cref="ISimpleQueryStringQuery.QuoteFieldSuffix"/>
		public SimpleQueryStringQueryDescriptor<T> QuoteFieldSuffix(string quoteFieldSuffix) =>
			Assign(a => a.QuoteFieldSuffix = quoteFieldSuffix);

		/// <inheritdoc cref="ISimpleQueryStringQuery.FuzzyPrefixLength"/>
		public SimpleQueryStringQueryDescriptor<T> FuzzyPrefixLength(int? fuzzyPrefixLength) => Assign(a => a.FuzzyPrefixLength = fuzzyPrefixLength);

		/// <inheritdoc cref="ISimpleQueryStringQuery.FuzzyMaxExpansions"/>
		public SimpleQueryStringQueryDescriptor<T> FuzzyMaxExpansions(int? fuzzyMaxExpansions) => Assign(a => a.FuzzyMaxExpansions = fuzzyMaxExpansions);

		/// <inheritdoc cref="ISimpleQueryStringQuery.FuzzyTranspositions"/>
		public SimpleQueryStringQueryDescriptor<T> FuzzyTranspositions(bool? fuzzyTranspositions = true) =>
			Assign(a => a.FuzzyTranspositions = fuzzyTranspositions);

    /// <inheritdoc cref="ISimpleQueryStringQuery.AutoGenerateSynonymsPhraseQuery"/>
		public SimpleQueryStringQueryDescriptor<T> AutoGenerateSynonymsPhraseQuery(bool? autoGenerateSynonymsPhraseQuery = true) =>
			Assign(a => a.AutoGenerateSynonymsPhraseQuery = autoGenerateSynonymsPhraseQuery);
	}
}
