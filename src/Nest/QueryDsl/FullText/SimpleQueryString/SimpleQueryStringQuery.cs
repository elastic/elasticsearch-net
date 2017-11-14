using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeJsonConverter<SimpleQueryStringQueryDescriptor<object>>))]
	public interface ISimpleQueryStringQuery : IQuery
	{
		[JsonProperty("fields")]
		Fields Fields { get; set; }

		[JsonProperty("query")]
		string Query { get; set; }

		[JsonProperty("analyzer")]
		string Analyzer { get; set; }

		[JsonProperty("default_operator")]
		Operator? DefaultOperator { get; set; }

		[JsonProperty("flags")]
		SimpleQueryStringFlags? Flags { get; set; }

		[JsonProperty("locale")]
		[Obsolete("Deprecated in Elasticsearch 5.1.1. Can be performed by the analyzer applied")]
		string Locale { get; set; }

		[JsonProperty("lowercase_expanded_terms")]
		[Obsolete("Deprecated in Elasticsearch 5.1.1. Can be performed by the analyzer applied")]
		bool? LowercaseExpandedTerms { get; set; }

		[JsonProperty("lenient")]
		bool? Lenient { get; set; }

		[JsonProperty("analyze_wildcard")]
		bool? AnalyzeWildcard { get; set; }

		[JsonProperty("minimum_should_match")]
		MinimumShouldMatch MinimumShouldMatch { get; set; }

		[JsonProperty("quote_field_suffix")]
		string QuoteFieldSuffix { get; set; }

		[JsonProperty("all_fields")]
		bool? AllFields { get; set; }
	}

	public class SimpleQueryStringQuery : QueryBase, ISimpleQueryStringQuery
	{
		protected override bool Conditionless => IsConditionless(this);
		public Fields Fields { get; set; }
		public string Query { get; set; }
		public string Analyzer { get; set; }
		public Operator? DefaultOperator { get; set; }
		public SimpleQueryStringFlags? Flags { get; set; }
		[Obsolete("Deprecated in Elasticsearch 5.1.1. Can be performed by the analyzer applied")]
		public string Locale { get; set; }
		[Obsolete("Deprecated in Elasticsearch 5.1.1. Can be performed by the analyzer applied")]
		public bool? LowercaseExpandedTerms { get; set; }
		public bool? Lenient { get; set; }
		public bool? AnalyzeWildcard { get; set; }
		public MinimumShouldMatch MinimumShouldMatch { get; set; }
		public string QuoteFieldSuffix { get; set; }
		public bool? AllFields { get; set; }

		internal override void InternalWrapInContainer(IQueryContainer c) => c.SimpleQueryString = this;
		internal static bool IsConditionless(ISimpleQueryStringQuery q) => q.Query.IsNullOrEmpty();
	}

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
		[Obsolete("Deprecated in Elasticsearch 5.1.1. Can be performed by the analyzer applied")]
		string ISimpleQueryStringQuery.Locale { get; set; }
		[Obsolete("Deprecated in Elasticsearch 5.1.1. Can be performed by the analyzer applied")]
		bool? ISimpleQueryStringQuery.LowercaseExpandedTerms { get; set; }
		bool? ISimpleQueryStringQuery.AnalyzeWildcard { get; set; }
		bool? ISimpleQueryStringQuery.Lenient { get; set; }
		MinimumShouldMatch ISimpleQueryStringQuery.MinimumShouldMatch { get; set; }
		string ISimpleQueryStringQuery.QuoteFieldSuffix { get; set; }
		bool? ISimpleQueryStringQuery.AllFields { get; set; }

		public SimpleQueryStringQueryDescriptor<T> Fields(Func<FieldsDescriptor<T>, IPromise<Fields>> fields) =>
			Assign(a => a.Fields = fields?.Invoke(new FieldsDescriptor<T>())?.Value);

		public SimpleQueryStringQueryDescriptor<T> Fields(Fields fields) => Assign(a => a.Fields = fields);

		public SimpleQueryStringQueryDescriptor<T> Query(string query) => Assign(a => a.Query = query);

		public SimpleQueryStringQueryDescriptor<T> Analyzer(string analyzer) => Assign(a => a.Analyzer = analyzer);

		public SimpleQueryStringQueryDescriptor<T> DefaultOperator(Operator? op) => Assign(a => a.DefaultOperator = op);

		public SimpleQueryStringQueryDescriptor<T> Flags(SimpleQueryStringFlags? flags) => Assign(a => a.Flags = flags);

		[Obsolete("Deprecated in Elasticsearch 5.1.1. Can be performed by the analyzer applied")]
		public SimpleQueryStringQueryDescriptor<T> Locale(string locale) => Assign(a => a.Locale = locale);

		[Obsolete("Deprecated in Elasticsearch 5.1.1. Can be performed by the analyzer applied")]
		public SimpleQueryStringQueryDescriptor<T> LowercaseExpandedTerms(bool? lowercaseExpandedTerms = true) =>
			Assign(a => a.LowercaseExpandedTerms = lowercaseExpandedTerms);

		public SimpleQueryStringQueryDescriptor<T> AnalyzeWildcard(bool? analyzeWildcard = true) =>
			Assign(a => a.AnalyzeWildcard = analyzeWildcard);

		public SimpleQueryStringQueryDescriptor<T> Lenient(bool? lenient = true) => Assign(a => a.Lenient = lenient);

		public SimpleQueryStringQueryDescriptor<T> MinimumShouldMatch(MinimumShouldMatch minimumShouldMatch) =>
			Assign(a => a.MinimumShouldMatch = minimumShouldMatch);

		public SimpleQueryStringQueryDescriptor<T> QuoteFieldSuffix(string quoteFieldSuffix) =>
			Assign(a => a.QuoteFieldSuffix = quoteFieldSuffix);

		public SimpleQueryStringQueryDescriptor<T> AllFields(bool? allFields = true) =>
			Assign(a => a.AllFields = allFields);
	}
}
