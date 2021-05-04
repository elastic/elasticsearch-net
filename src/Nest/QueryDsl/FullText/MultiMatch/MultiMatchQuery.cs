// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A match query across multiple fields.
	/// </summary>
	[InterfaceDataContract]
	[ReadAs(typeof(MultiMatchQuery))]
	public interface IMultiMatchQuery : IQuery
	{
		/// <summary>
		/// The analyzer name used to analyze the query
		/// </summary>
		[DataMember(Name = "analyzer")]
		string Analyzer { get; set; }

		/// <summary></summary>
		[DataMember(Name = "auto_generate_synonyms_phrase_query")]
		bool? AutoGenerateSynonymsPhraseQuery { get; set; }

		/// <summary>
		/// A cutoff frequency that allows specifying an absolute or relative document frequency where
		/// high frequency terms are moved into an optional subquery and are only scored if one of the low frequency
		/// (below the cutoff) terms in the case of <see cref="Nest.Operator.Or" />,
		/// or all of the low frequency terms in the case of an <see cref="Nest.Operator.And" /> match.
		/// </summary>
		[DataMember(Name = "cutoff_frequency")]
		double? CutoffFrequency { get; set; }

		/// <summary>
		/// The fields to perform the query against.
		/// </summary>
		[DataMember(Name = "fields")]
		Fields Fields { get; set; }

		/// <summary>
		/// Allows fuzzy matching based on the type of field being queried.
		/// Cannot be used with the
		/// <see cref="TextQueryType.CrossFields" />,
		/// <see cref="TextQueryType.Phrase" /> or
		/// <see cref="TextQueryType.PhrasePrefix" /> types.
		/// </summary>
		[DataMember(Name = "fuzziness")]
		Fuzziness Fuzziness { get; set; }

		/// <summary>
		/// Controls how the query is rewritten if <see cref="Fuzziness" /> is set.
		/// In this scenario, the default is <see cref="MultiTermQueryRewrite.TopTermsBlendedFreqs" />.
		/// </summary>
		[DataMember(Name = "fuzzy_rewrite")]
		MultiTermQueryRewrite FuzzyRewrite { get; set; }

		/// <summary>
		/// Sets whether transpositions are supported in fuzzy queries.
		/// <para />
		/// The default metric used by fuzzy queries to determine a match is the Damerau-Levenshtein
		/// distance formula which supports transpositions. Setting transposition to false will
		/// switch to classic Levenshtein distance.
		/// If not set, Damerau-Levenshtein distance metric will be used.
		/// </summary>
		[DataMember(Name = "fuzzy_transpositions")]
		bool? FuzzyTranspositions { get; set; }

		/// <summary>
		/// If set to <c>true</c> will cause format based failures (like providing text to a numeric field)
		/// to be ignored
		/// </summary>
		[DataMember(Name = "lenient")]
		bool? Lenient { get; set; }

		/// <summary>
		/// Controls the number of terms fuzzy queries will expand to. Defaults to <c>50</c>
		/// </summary>
		[DataMember(Name = "max_expansions")]
		int? MaxExpansions { get; set; }

		/// <summary>
		/// A value controlling how many "should" clauses in the resulting boolean query should match.
		/// It can be an absolute value, a percentage or a combination of both.
		/// </summary>
		[DataMember(Name = "minimum_should_match")]
		MinimumShouldMatch MinimumShouldMatch { get; set; }

		/// <summary>
		/// The operator used if no explicit operator is specified.
		/// The default operator is <see cref="Nest.Operator.Or" />
		/// </summary>
		/// <remarks>
		/// <see cref="TextQueryType.BestFields" /> and <see cref="TextQueryType.MostFields" /> types are field-centric ;
		/// they generate a match query per field. This means that <see cref="Operator" /> and <see cref="MinimumShouldMatch" />
		/// are applied to each field individually, which is probably not what you want.
		/// Consider using <see cref="TextQueryType.CrossFields" />.
		/// </remarks>
		[DataMember(Name = "operator")]
		Operator? Operator { get; set; }

		/// <summary>
		/// Set the prefix length for fuzzy queries. Default is <c>0</c>.
		/// </summary>
		[DataMember(Name = "prefix_length")]
		int? PrefixLength { get; set; }

		/// <summary>
		/// The query to execute
		/// </summary>
		[DataMember(Name = "query")]
		string Query { get; set; }

		/// <summary>
		/// How far apart terms are allowed to be while still considering the document to be a match.
		/// </summary>
		[DataMember(Name = "slop")]
		int? Slop { get; set; }

		/// <summary>
		/// Used to influence how the score is calculated for <see cref="TextQueryType.BestFields" />. If specified,
		/// score is calculated using
		/// </summary>
		[DataMember(Name = "tie_breaker")]
		double? TieBreaker { get; set; }

		/// <summary>
		/// How the fields should be combined to build the text query.
		/// Default is <see cref="TextQueryType.BestFields" />
		/// </summary>
		[DataMember(Name = "type")]
		TextQueryType? Type { get; set; }

		/// <summary>
		/// By default, a <see cref="IMultiMatchQuery" /> generates a match clause per field, then wraps them
		/// in a <see cref="IDisMaxQuery" />. By setting <see cref="UseDisMax" /> to <c>false</c>,
		/// they will be wrapped in a <see cref="IBoolQuery" /> instead.
		/// </summary>
		[DataMember(Name = "use_dis_max")]
		bool? UseDisMax { get; set; }

		/// <summary>
		/// If the analyzer used removes all tokens in a query like a stop filter does, the default behavior is
		/// to match no documents at all. In order to change that, <see cref="Nest.ZeroTermsQuery" /> can be used,
		/// which accepts <see cref="Nest.ZeroTermsQuery.None" /> (default) and <see cref="Nest.ZeroTermsQuery.All" />
		/// which corresponds to a match_all query.
		/// </summary>
		[DataMember(Name = "zero_terms_query")]
		ZeroTermsQuery? ZeroTermsQuery { get; set; }
	}

	/// <inheritdoc cref="IMultiMatchQuery" />
	public class MultiMatchQuery : QueryBase, IMultiMatchQuery
	{
		/// <inheritdoc />
		public string Analyzer { get; set; }

		/// <inheritdoc />
		public bool? AutoGenerateSynonymsPhraseQuery { get; set; }

		/// <inheritdoc />
		public double? CutoffFrequency { get; set; }

		/// <inheritdoc />
		public Fields Fields { get; set; }

		/// <inheritdoc />
		public Fuzziness Fuzziness { get; set; }

		/// <inheritdoc />
		public MultiTermQueryRewrite FuzzyRewrite { get; set; }

		/// <inheritdoc />
		public bool? FuzzyTranspositions { get; set; }

		/// <inheritdoc />
		public bool? Lenient { get; set; }

		/// <inheritdoc />
		public int? MaxExpansions { get; set; }

		/// <inheritdoc />
		public MinimumShouldMatch MinimumShouldMatch { get; set; }

		/// <inheritdoc />
		public Operator? Operator { get; set; }

		/// <inheritdoc />
		public int? PrefixLength { get; set; }

		/// <inheritdoc />
		public string Query { get; set; }

		/// <inheritdoc />
		public int? Slop { get; set; }

		/// <inheritdoc />
		public double? TieBreaker { get; set; }

		/// <inheritdoc />
		public TextQueryType? Type { get; set; }

		/// <inheritdoc />
		public bool? UseDisMax { get; set; }

		/// <inheritdoc />
		public ZeroTermsQuery? ZeroTermsQuery { get; set; }

		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.MultiMatch = this;

		internal static bool IsConditionless(IMultiMatchQuery q) => q.Query.IsNullOrEmpty();
	}

	/// <inheritdoc cref="IMultiMatchQuery" />
	[DataContract]
	public class MultiMatchQueryDescriptor<T>
		: QueryDescriptorBase<MultiMatchQueryDescriptor<T>, IMultiMatchQuery>
			, IMultiMatchQuery where T : class
	{
		protected override bool Conditionless => MultiMatchQuery.IsConditionless(this);
		string IMultiMatchQuery.Analyzer { get; set; }
		bool? IMultiMatchQuery.AutoGenerateSynonymsPhraseQuery { get; set; }
		double? IMultiMatchQuery.CutoffFrequency { get; set; }
		Fields IMultiMatchQuery.Fields { get; set; }
		Fuzziness IMultiMatchQuery.Fuzziness { get; set; }
		MultiTermQueryRewrite IMultiMatchQuery.FuzzyRewrite { get; set; }
		bool? IMultiMatchQuery.FuzzyTranspositions { get; set; }
		bool? IMultiMatchQuery.Lenient { get; set; }
		int? IMultiMatchQuery.MaxExpansions { get; set; }
		MinimumShouldMatch IMultiMatchQuery.MinimumShouldMatch { get; set; }
		Operator? IMultiMatchQuery.Operator { get; set; }
		int? IMultiMatchQuery.PrefixLength { get; set; }
		string IMultiMatchQuery.Query { get; set; }
		int? IMultiMatchQuery.Slop { get; set; }
		double? IMultiMatchQuery.TieBreaker { get; set; }
		TextQueryType? IMultiMatchQuery.Type { get; set; }
		bool? IMultiMatchQuery.UseDisMax { get; set; }
		ZeroTermsQuery? IMultiMatchQuery.ZeroTermsQuery { get; set; }

		/// <inheritdoc cref="IMultiMatchQuery.Fields" />
		public MultiMatchQueryDescriptor<T> Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(fields, (a, v) => a.Fields = v?.Invoke(new FieldsDescriptor<T>())?.Value);

		/// <inheritdoc cref="IMultiMatchQuery.Fields" />
		public MultiMatchQueryDescriptor<T> Fields(Fields fields) => Assign(fields, (a, v) => a.Fields = v);

		/// <inheritdoc cref="IMultiMatchQuery.Query" />
		public MultiMatchQueryDescriptor<T> Query(string query) => Assign(query, (a, v) => a.Query = v);

		/// <inheritdoc cref="IMultiMatchQuery.Analyzer" />
		public MultiMatchQueryDescriptor<T> Analyzer(string analyzer) => Assign(analyzer, (a, v) => a.Analyzer = v);

		/// <inheritdoc cref="IMultiMatchQuery.Fuzziness" />
		public MultiMatchQueryDescriptor<T> Fuzziness(Fuzziness fuzziness) => Assign(fuzziness, (a, v) => a.Fuzziness = v);

		/// <inheritdoc cref="IMultiMatchQuery.CutoffFrequency" />
		public MultiMatchQueryDescriptor<T> CutoffFrequency(double? cutoffFrequency)
			=> Assign(cutoffFrequency, (a, v) => a.CutoffFrequency = v);

		/// <inheritdoc cref="IMultiMatchQuery.MinimumShouldMatch" />
		public MultiMatchQueryDescriptor<T> MinimumShouldMatch(MinimumShouldMatch minimumShouldMatch)
			=> Assign(minimumShouldMatch, (a, v) => a.MinimumShouldMatch = v);

		/// <inheritdoc cref="IMultiMatchQuery.FuzzyRewrite" />
		public MultiMatchQueryDescriptor<T> FuzzyRewrite(MultiTermQueryRewrite rewrite) => Assign(rewrite, (a, v) => a.FuzzyRewrite = v);

		/// <inheritdoc cref="IMultiMatchQuery.FuzzyTranspositions" />
		public MultiMatchQueryDescriptor<T> FuzzyTranspositions(bool? fuzzyTranpositions = true) =>
			Assign(fuzzyTranpositions, (a, v) => a.FuzzyTranspositions = v);

		/// <inheritdoc cref="IMultiMatchQuery.Lenient" />
		public MultiMatchQueryDescriptor<T> Lenient(bool? lenient = true) => Assign(lenient, (a, v) => a.Lenient = v);

		/// <inheritdoc cref="IMultiMatchQuery.PrefixLength" />
		public MultiMatchQueryDescriptor<T> PrefixLength(int? prefixLength) => Assign(prefixLength, (a, v) => a.PrefixLength = v);

		/// <inheritdoc cref="IMultiMatchQuery.MaxExpansions" />
		public MultiMatchQueryDescriptor<T> MaxExpansions(int? maxExpansions) => Assign(maxExpansions, (a, v) => a.MaxExpansions = v);

		/// <inheritdoc cref="IMultiMatchQuery.Slop" />
		public MultiMatchQueryDescriptor<T> Slop(int? slop) => Assign(slop, (a, v) => a.Slop = v);

		/// <inheritdoc cref="IMultiMatchQuery.Operator" />
		public MultiMatchQueryDescriptor<T> Operator(Operator? op) => Assign(op, (a, v) => a.Operator = v);

		/// <inheritdoc cref="IMultiMatchQuery.TieBreaker" />
		public MultiMatchQueryDescriptor<T> TieBreaker(double? tieBreaker) => Assign(tieBreaker, (a, v) => a.TieBreaker = v);

		/// <inheritdoc cref="IMultiMatchQuery.Type" />
		public MultiMatchQueryDescriptor<T> Type(TextQueryType? type) => Assign(type, (a, v) => a.Type = v);

		/// <inheritdoc cref="IMultiMatchQuery.UseDisMax" />
		public MultiMatchQueryDescriptor<T> UseDisMax(bool? useDisMax = true) => Assign(useDisMax, (a, v) => a.UseDisMax = v);

		/// <inheritdoc cref="IMultiMatchQuery.ZeroTermsQuery" />
		public MultiMatchQueryDescriptor<T> ZeroTermsQuery(ZeroTermsQuery? zeroTermsQuery) => Assign(zeroTermsQuery, (a, v) => a.ZeroTermsQuery = v);

		/// <inheritdoc cref="IMultiMatchQuery.AutoGenerateSynonymsPhraseQuery" />
		public MultiMatchQueryDescriptor<T> AutoGenerateSynonymsPhraseQuery(bool? autoGenerateSynonymsPhraseQuery = true) =>
			Assign(autoGenerateSynonymsPhraseQuery, (a, v) => a.AutoGenerateSynonymsPhraseQuery = v);
	}
}
