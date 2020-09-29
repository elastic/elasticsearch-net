// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A match_bool_prefix query analyzes its input and constructs a bool query from the terms.
	/// Each term except the last is used in a term query. The last term is used in a prefix query.
	/// </summary>
	/// <remarks>
	/// Supported in Elasticsearch 7.2.0+
	/// </remarks>
	[InterfaceDataContract]
	[JsonFormatter(typeof(FieldNameQueryFormatter<MatchBoolPrefixQuery, IMatchBoolPrefixQuery>))]
	public interface IMatchBoolPrefixQuery : IFieldNameQuery
	{
		/// <summary>
		/// The analyzer to use for the query
		/// </summary>
		[DataMember(Name = "analyzer")]
		string Analyzer { get; set; }

		/// <summary>
		/// Allows fuzzy matching based on the type of field being queried.
		/// Applies only to the term sub queries constructed, and not the prefix query for the final term.
		/// </summary>
		[DataMember(Name = "fuzziness")]
		IFuzziness Fuzziness { get; set; }

		/// <summary>
		/// Controls how the query is rewritten if <see cref="Fuzziness" /> is set.
		/// In this scenario, the default is <see cref="MultiTermQueryRewrite.TopTermsBlendedFreqs" />.
		/// Applies only to the term sub queries constructed, and not the prefix query for the final term.
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
		/// Applies only to the term sub queries constructed, and not the prefix query for the final term.
		/// </summary>
		[DataMember(Name = "fuzzy_transpositions")]
		bool? FuzzyTranspositions { get; set; }

		/// <summary>
		/// Controls the number of terms fuzzy queries will expand to. Defaults to <c>50</c>.
		/// Applies only to the term sub queries constructed, and not the prefix query for the final term.
		/// </summary>
		[DataMember(Name = "max_expansions")]
		int? MaxExpansions { get; set; }

		/// <summary>
		/// A value controlling how many "should" clauses in the resulting boolean query should match.
		/// It can be an absolute value, a percentage or a combination of both.
		/// Applies to the bool query constructed.
		/// </summary>
		[DataMember(Name = "minimum_should_match")]
		MinimumShouldMatch MinimumShouldMatch { get; set; }

		/// <summary>
		/// The operator used if no explicit operator is specified.
		/// The default operator is <see cref="Nest.Operator.Or" />.
		/// Applies to the bool query constructed.
		/// </summary>
		[DataMember(Name = "operator")]
		Operator? Operator { get; set; }

		/// <summary>
		/// Set the prefix length for fuzzy queries. Default is <c>0</c>.
		/// Applies only to the term sub queries constructed, and not the prefix query for the final term.
		/// </summary>
		[DataMember(Name = "prefix_length")]
		int? PrefixLength { get; set; }

		/// <summary>
		/// The query
		/// </summary>
		[DataMember(Name = "query")]
		string Query { get; set; }
	}

	/// <inheritdoc cref="IMatchBoolPrefixQuery" />
	public class MatchBoolPrefixQuery : FieldNameQueryBase, IMatchBoolPrefixQuery
	{
		/// <inheritdoc />
		public string Analyzer { get; set; }

		/// <inheritdoc />
		public IFuzziness Fuzziness { get; set; }

		/// <inheritdoc />
		public MultiTermQueryRewrite FuzzyRewrite { get; set; }

		/// <inheritdoc />
		public bool? FuzzyTranspositions { get; set; }

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

		protected override bool Conditionless => IsConditionless(this);

		internal override void InternalWrapInContainer(IQueryContainer c) => c.MatchBoolPrefix = this;

		internal static bool IsConditionless(IMatchBoolPrefixQuery q) => q.Field.IsConditionless() || q.Query.IsNullOrEmpty();
	}

	/// <inheritdoc cref="IMatchBoolPrefixQuery" />
	public class MatchBoolPrefixQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<MatchBoolPrefixQueryDescriptor<T>, IMatchBoolPrefixQuery, T>, IMatchBoolPrefixQuery
		where T : class
	{
		protected override bool Conditionless => MatchBoolPrefixQuery.IsConditionless(this);
		string IMatchBoolPrefixQuery.Analyzer { get; set; }
		IFuzziness IMatchBoolPrefixQuery.Fuzziness { get; set; }
		MultiTermQueryRewrite IMatchBoolPrefixQuery.FuzzyRewrite { get; set; }
		bool? IMatchBoolPrefixQuery.FuzzyTranspositions { get; set; }
		int? IMatchBoolPrefixQuery.MaxExpansions { get; set; }
		MinimumShouldMatch IMatchBoolPrefixQuery.MinimumShouldMatch { get; set; }
		Operator? IMatchBoolPrefixQuery.Operator { get; set; }
		int? IMatchBoolPrefixQuery.PrefixLength { get; set; }
		string IMatchBoolPrefixQuery.Query { get; set; }

		/// <inheritdoc cref="IMatchBoolPrefixQuery.Analyzer" />
		public MatchBoolPrefixQueryDescriptor<T> Analyzer(string analyzer) => Assign(analyzer, (a, v) => a.Analyzer = v);

		/// <inheritdoc cref="IMatchBoolPrefixQuery.Fuzziness" />
		public MatchBoolPrefixQueryDescriptor<T> Fuzziness(Fuzziness fuzziness) => Assign(fuzziness, (a, v) => a.Fuzziness = v);

		/// <inheritdoc cref="IMatchBoolPrefixQuery.FuzzyTranspositions" />
		public MatchBoolPrefixQueryDescriptor<T> FuzzyTranspositions(bool? fuzzyTranspositions = true) =>
			Assign(fuzzyTranspositions, (a, v) => a.FuzzyTranspositions = v);

		/// <inheritdoc cref="IMatchBoolPrefixQuery.FuzzyRewrite" />
		public MatchBoolPrefixQueryDescriptor<T> FuzzyRewrite(MultiTermQueryRewrite rewrite) => Assign(rewrite, (a, v) => a.FuzzyRewrite = v);

		/// <inheritdoc cref="IMatchBoolPrefixQuery.MaxExpansions" />
		public MatchBoolPrefixQueryDescriptor<T> MaxExpansions(int? maxExpansions) => Assign(maxExpansions, (a, v) => a.MaxExpansions = v);

		/// <inheritdoc cref="IMatchBoolPrefixQuery.MinimumShouldMatch" />
		public MatchBoolPrefixQueryDescriptor<T> MinimumShouldMatch(MinimumShouldMatch minimumShouldMatch) =>
			Assign(minimumShouldMatch, (a, v) => a.MinimumShouldMatch = v);

		/// <inheritdoc cref="IMatchBoolPrefixQuery.Operator" />
		public MatchBoolPrefixQueryDescriptor<T> Operator(Operator? op) => Assign(op, (a, v) => a.Operator = v);

		/// <inheritdoc cref="IMatchBoolPrefixQuery.PrefixLength" />
		public MatchBoolPrefixQueryDescriptor<T> PrefixLength(int? prefixLength) => Assign(prefixLength, (a, v) => a.PrefixLength = v);

		/// <inheritdoc cref="IMatchBoolPrefixQuery.Query" />
		public MatchBoolPrefixQueryDescriptor<T> Query(string query) => Assign(query, (a, v) => a.Query = v);
	}
}
