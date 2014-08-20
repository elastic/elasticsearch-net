using Nest.DSL.Visitor;
using Nest.Resolvers.Converters;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReadAsTypeConverter<QueryContainer>))]
	public interface IQueryContainer : ICustomJson
	{
		[JsonProperty(PropertyName = "bool")]
		IBoolQuery Bool { get; set; }

		[JsonIgnore]
		bool IsConditionless { get; set; }

		[JsonIgnore]
		bool IsStrict { get; set; }

		[JsonIgnore]
		bool IsVerbatim { get; set; }
		
		[JsonIgnore]
		string RawQuery { get; set; }
		
		[JsonProperty(PropertyName = "match_all")]
		IMatchAllQuery MatchAllQuery { get; set; }

		[JsonProperty(PropertyName = "term")]
		[JsonConverter(typeof (FieldNameQueryConverter<TermQuery>))]
		ITermQuery Term { get; set; }

		[JsonProperty(PropertyName = "wildcard")]
		[JsonConverter(typeof (FieldNameQueryConverter<WildcardQuery>))]
		IWildcardQuery Wildcard { get; set; }

		[JsonProperty(PropertyName = "prefix")]
		[JsonConverter(typeof (FieldNameQueryConverter<PrefixQuery>))]
		IPrefixQuery Prefix { get; set; }

		[JsonProperty(PropertyName = "boosting")]
		IBoostingQuery Boosting { get; set; }

		[JsonProperty(PropertyName = "ids")]
		IIdsQuery Ids { get; set; }

		[JsonProperty(PropertyName = "custom_score")]
		ICustomScoreQuery CustomScore { get; set; }

		[JsonProperty(PropertyName = "custom_filters_score")]
		ICustomFiltersScoreQuery CustomFiltersScore { get; set; }

		[JsonProperty(PropertyName = "custom_boost_factor")]
		ICustomBoostFactorQuery CustomBoostFactor { get; set; }

		[JsonProperty(PropertyName = "constant_score")]
		IConstantScoreQuery ConstantScore { get; set; }

		[JsonProperty(PropertyName = "dis_max")]
		IDisMaxQuery DisMax { get; set; }

		[JsonProperty(PropertyName = "filtered")]
		IFilteredQuery Filtered { get; set; }

		[JsonProperty(PropertyName = "multi_match")]
		IMultiMatchQuery MultiMatch { get; set; }

		[JsonProperty(PropertyName = "match")]
		[JsonConverter(typeof (MatchQueryJsonConverter))]
		IMatchQuery Match { get; set; }

		[JsonProperty(PropertyName = "fuzzy")]
		[JsonConverter(typeof(FuzzyQueryJsonConverter))]
		IFuzzyQuery Fuzzy { get; set; }

		[JsonProperty(PropertyName = "geo_shape")]
		[JsonConverter(typeof(CompositeJsonConverter<GeoShapeQueryJsonReader, FieldNameQueryConverter<EnvelopeGeoShape>>))]
		IGeoShapeQuery GeoShape { get; set; }

		[JsonProperty(PropertyName = "common_terms")]
		[JsonConverter(typeof (FieldNameQueryConverter<CommonTermsQuery>))]
		ICommonTermsQuery CommonTerms { get; set; }

		[JsonProperty(PropertyName = "terms")]
		ITermsQuery Terms { get; set; }

		[JsonProperty(PropertyName = "range")]
		[JsonConverter(typeof (FieldNameQueryConverter<RangeQuery>))]
		IRangeQuery Range { get; set; }

		[JsonProperty(PropertyName = "regexp")]
		[JsonConverter(typeof (FieldNameQueryConverter<RegexpQuery>))]
		IRegexpQuery Regexp { get; set; }

		[JsonProperty(PropertyName = "has_child")]
		IHasChildQuery HasChild { get; set; }

		[JsonProperty(PropertyName = "has_parent")]
		IHasParentQuery HasParent { get; set; }
		
		[JsonProperty(PropertyName = "span_term")]
		ISpanTermQuery SpanTerm { get; set; }

		[JsonProperty(PropertyName = "flt")]
		IFuzzyLikeThisQuery FuzzyLikeThis { get; set; }

		[JsonProperty(PropertyName = "simple_query_string")]
		ISimpleQueryStringQuery SimpleQueryString { get; set; }

		[JsonProperty(PropertyName = "query_string")]
		IQueryStringQuery QueryString { get; set; }

		[JsonProperty(PropertyName = "mlt")]
		IMoreLikeThisQuery MoreLikeThis { get; set; }

		[JsonProperty(PropertyName = "span_first")]
		ISpanFirstQuery SpanFirst { get; set; }

		[JsonProperty(PropertyName = "span_or")]
		ISpanOrQuery SpanOr { get; set; }

		[JsonProperty(PropertyName = "span_near")]
		ISpanNearQuery SpanNear { get; set; }

		[JsonProperty(PropertyName = "span_not")]
		ISpanNotQuery SpanNot { get; set; }

		[JsonProperty(PropertyName = "span_multi")]
		ISpanMultiTermQuery SpanMultiTerm { get; set; }

		[JsonProperty(PropertyName = "top_children")]
		ITopChildrenQuery TopChildren { get; set; }

		[JsonProperty(PropertyName = "nested")]
		INestedQuery Nested { get; set; }

		[JsonProperty(PropertyName = "indices")]
		IIndicesQuery Indices { get; set; }

		[JsonProperty(PropertyName = "function_score")]
		IFunctionScoreQuery FunctionScore { get; set; }

		void Accept(IQueryVisitor visitor);
	}
}