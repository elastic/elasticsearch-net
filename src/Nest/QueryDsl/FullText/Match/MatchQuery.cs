// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A match query for a single field
	/// </summary>
	[InterfaceDataContract]
	[JsonFormatter(typeof(FieldNameQueryFormatter<MatchQuery, IMatchQuery>))]
	public interface IMatchQuery : IFieldNameQuery
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
		[Obsolete("Deprecated in 7.3.0. This option can be omitted since MatchQuery can skip blocks of documents efficiently if the total number of hits is not tracked.")]
		double? CutoffFrequency { get; set; }

		/// <summary>
		/// Allows fuzzy matching based on the type of field being queried.
		/// </summary>
		[DataMember(Name = "fuzziness")]
		IFuzziness Fuzziness { get; set; }

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
		/// If the analyzer used removes all tokens in a query like a stop filter does, the default behavior is
		/// to match no documents at all. In order to change that, <see cref="Nest.ZeroTermsQuery" /> can be used,
		/// which accepts <see cref="Nest.ZeroTermsQuery.None" /> (default) and <see cref="Nest.ZeroTermsQuery.All" />
		/// which corresponds to a match_all query.
		/// </summary>
		[DataMember(Name = "zero_terms_query")]
		ZeroTermsQuery? ZeroTermsQuery { get; set; }
	}

	/// <inheritdoc cref="IMatchQuery" />
	public class MatchQuery : FieldNameQueryBase, IMatchQuery
	{
		/// <inheritdoc />
		public string Analyzer { get; set; }

		/// <inheritdoc />
		public bool? AutoGenerateSynonymsPhraseQuery { get; set; }

		/// <inheritdoc />
		[Obsolete("Deprecated in 7.3.0. This option can be omitted since MatchQuery can skip blocks of documents efficiently if the total number of hits is not tracked.")]
		public double? CutoffFrequency { get; set; }

		/// <inheritdoc />
		public IFuzziness Fuzziness { get; set; }

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
		public ZeroTermsQuery? ZeroTermsQuery { get; set; }

		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.Match = this;

		internal static bool IsConditionless(IMatchQuery q) => q.Field.IsConditionless() || q.Query.IsNullOrEmpty();
	}

	/// <inheritdoc cref="IMatchQuery" />
	[DataContract]
	public class MatchQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<MatchQueryDescriptor<T>, IMatchQuery, T>
			, IMatchQuery where T : class
	{
		protected override bool Conditionless => MatchQuery.IsConditionless(this);
		protected virtual string MatchQueryType => null;
		string IMatchQuery.Analyzer { get; set; }
		bool? IMatchQuery.AutoGenerateSynonymsPhraseQuery { get; set; }
		[Obsolete("Deprecated in 7.3.0. This option can be omitted since MatchQuery can skip blocks of documents efficiently if the total number of hits is not tracked.")]
		double? IMatchQuery.CutoffFrequency { get; set; }
		IFuzziness IMatchQuery.Fuzziness { get; set; }
		MultiTermQueryRewrite IMatchQuery.FuzzyRewrite { get; set; }
		bool? IMatchQuery.FuzzyTranspositions { get; set; }
		bool? IMatchQuery.Lenient { get; set; }
		int? IMatchQuery.MaxExpansions { get; set; }
		MinimumShouldMatch IMatchQuery.MinimumShouldMatch { get; set; }
		Operator? IMatchQuery.Operator { get; set; }
		int? IMatchQuery.PrefixLength { get; set; }
		string IMatchQuery.Query { get; set; }
		ZeroTermsQuery? IMatchQuery.ZeroTermsQuery { get; set; }

		/// <inheritdoc cref="IMatchQuery.Query" />
		public MatchQueryDescriptor<T> Query(string query) => Assign(query, (a, v) => a.Query = v);

		/// <inheritdoc cref="IMatchQuery.Lenient" />
		public MatchQueryDescriptor<T> Lenient(bool? lenient = true) => Assign(lenient, (a, v) => a.Lenient = v);

		/// <inheritdoc cref="IMatchQuery.Analyzer" />
		public MatchQueryDescriptor<T> Analyzer(string analyzer) => Assign(analyzer, (a, v) => a.Analyzer = v);

		/// <inheritdoc cref="IMatchQuery.Fuzziness" />
		public MatchQueryDescriptor<T> Fuzziness(Fuzziness fuzziness) => Assign(fuzziness, (a, v) => a.Fuzziness = v);

		/// <inheritdoc cref="IMatchQuery.FuzzyTranspositions" />
		public MatchQueryDescriptor<T> FuzzyTranspositions(bool? fuzzyTranspositions = true) =>
			Assign(fuzzyTranspositions, (a, v) => a.FuzzyTranspositions = v);

		/// <inheritdoc cref="IMatchQuery.CutoffFrequency" />
		[Obsolete("Deprecated in 7.3.0. This option can be omitted since MatchQuery can skip blocks of documents efficiently if the total number of hits is not tracked.")]
		public MatchQueryDescriptor<T> CutoffFrequency(double? cutoffFrequency) => Assign(cutoffFrequency, (a, v) => a.CutoffFrequency = v);

		/// <inheritdoc cref="IMatchQuery.FuzzyRewrite" />
		public MatchQueryDescriptor<T> FuzzyRewrite(MultiTermQueryRewrite rewrite) => Assign(rewrite, (a, v) => a.FuzzyRewrite = v);

		/// <inheritdoc cref="IMatchQuery.MinimumShouldMatch" />
		public MatchQueryDescriptor<T> MinimumShouldMatch(MinimumShouldMatch minimumShouldMatch) =>
			Assign(minimumShouldMatch, (a, v) => a.MinimumShouldMatch = v);

		/// <inheritdoc cref="IMatchQuery.Operator" />
		public MatchQueryDescriptor<T> Operator(Operator? op) => Assign(op, (a, v) => a.Operator = v);

		/// <inheritdoc cref="IMatchQuery.ZeroTermsQuery" />
		public MatchQueryDescriptor<T> ZeroTermsQuery(ZeroTermsQuery? zeroTermsQuery) => Assign(zeroTermsQuery, (a, v) => a.ZeroTermsQuery = v);

		/// <inheritdoc cref="IMatchQuery.PrefixLength" />
		public MatchQueryDescriptor<T> PrefixLength(int? prefixLength) => Assign(prefixLength, (a, v) => a.PrefixLength = v);

		/// <inheritdoc cref="IMatchQuery.MaxExpansions" />
		public MatchQueryDescriptor<T> MaxExpansions(int? maxExpansions) => Assign(maxExpansions, (a, v) => a.MaxExpansions = v);

		/// <inheritdoc cref="IMatchQuery.AutoGenerateSynonymsPhraseQuery" />
		public MatchQueryDescriptor<T> AutoGenerateSynonymsPhraseQuery(bool? autoGenerateSynonymsPhraseQuery = true) =>
			Assign(autoGenerateSynonymsPhraseQuery, (a, v) => a.AutoGenerateSynonymsPhraseQuery = v);
	}
}
