using Newtonsoft.Json;
using System;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(FieldNameQueryJsonConverter<MatchPhrasePrefixQuery>))]
	public interface IMatchPhrasePrefixQuery : IFieldNameQuery
	{
		[JsonProperty("query")]
		string Query { get; set; }

		[JsonProperty("analyzer")]
		string Analyzer { get; set; }

		[JsonProperty("max_expansions")]
		int? MaxExpansions { get; set; }

		[JsonProperty("slop")]
		int? Slop { get; set; }

		/// <summary>
		/// If the analyzer used removes all tokens in a query like a stop filter does, the default behavior is
		/// to match no documents at all. In order to change that, <see cref="ZeroTermsQuery"/> can be used,
		/// which accepts <see cref="ZeroTermsQuery.None"/> (default) and <see cref="ZeroTermsQuery.All"/>
		/// which corresponds to a match_all query.
		/// </summary>
		[JsonProperty("zero_terms_query")]
		ZeroTermsQuery? ZeroTermsQuery { get; set; }
	}

	public class MatchPhrasePrefixQuery : FieldNameQueryBase, IMatchPhrasePrefixQuery
	{
		protected override bool Conditionless => IsConditionless(this);

		public string Analyzer { get; set; }
		public int? MaxExpansions { get; set; }
		public string Query { get; set; }
		public int? Slop { get; set; }
		/// <inheritdoc />
		public ZeroTermsQuery? ZeroTermsQuery { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.MatchPhrasePrefix = this;

		internal static bool IsConditionless(IMatchPhrasePrefixQuery q) => q.Field.IsConditionless() || q.Query.IsNullOrEmpty();
	}

	public class MatchPhrasePrefixQueryDescriptor<T>
		: FieldNameQueryDescriptorBase<MatchPhrasePrefixQueryDescriptor<T>, IMatchPhrasePrefixQuery, T>, IMatchPhrasePrefixQuery
		where T : class
	{
		protected override bool Conditionless => MatchPhrasePrefixQuery.IsConditionless(this);

		string IMatchPhrasePrefixQuery.Query { get; set; }
		string IMatchPhrasePrefixQuery.Analyzer { get; set; }
		int? IMatchPhrasePrefixQuery.MaxExpansions { get; set; }
		int? IMatchPhrasePrefixQuery.Slop { get; set; }
		ZeroTermsQuery? IMatchPhrasePrefixQuery.ZeroTermsQuery { get; set; }

		public MatchPhrasePrefixQueryDescriptor<T> Query(string query) => Assign(a => a.Query = query);

		public MatchPhrasePrefixQueryDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		public MatchPhrasePrefixQueryDescriptor<T> MaxExpansions(int? maxExpansions) => Assign(a => a.MaxExpansions = maxExpansions);

		public MatchPhrasePrefixQueryDescriptor<T> Slop(int? slop) => Assign(a => a.Slop = slop);

		/// <inheritdoc cref="IMatchQuery.ZeroTermsQuery" />
		public MatchPhrasePrefixQueryDescriptor<T> ZeroTermsQuery(ZeroTermsQuery? zeroTermsQuery) => Assign(a => a.ZeroTermsQuery = zeroTermsQuery);

	}
}
