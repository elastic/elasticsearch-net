using System;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[ContractJsonConverter(typeof(QueryContainerJsonConverter))]
	public interface IQueryContainer
	{
		[JsonIgnore]
		bool IsConditionless { get; }

		[JsonIgnore]
		bool IsStrict { get; set; }

		[JsonIgnore]
		bool IsVerbatim { get; set; }

		[JsonIgnore]
		bool IsWritable { get; }

		[JsonIgnore]
		IRawQuery RawQuery { get; set; }

		[JsonProperty("bool")]
		IBoolQuery Bool { get; set; }

		[JsonProperty("match_all")]
		IMatchAllQuery MatchAll { get; set; }

		[JsonProperty("match_none")]
		IMatchNoneQuery MatchNone { get; set; }

		[JsonProperty("term")]
		ITermQuery Term { get; set; }

		[JsonProperty("wildcard")]
		IWildcardQuery Wildcard { get; set; }

		[JsonProperty("prefix")]
		IPrefixQuery Prefix { get; set; }

		[JsonProperty("boosting")]
		IBoostingQuery Boosting { get; set; }

		[JsonProperty("ids")]
		IIdsQuery Ids { get; set; }

		[JsonProperty("constant_score")]
		IConstantScoreQuery ConstantScore { get; set; }

		[JsonProperty("dis_max")]
		IDisMaxQuery DisMax { get; set; }

		[JsonProperty("multi_match")]
		IMultiMatchQuery MultiMatch { get; set; }

		[JsonProperty("match")]
		IMatchQuery Match { get; set; }

		[JsonProperty("match_phrase")]
		IMatchPhraseQuery MatchPhrase { get; set; }

		[JsonProperty("match_phrase_prefix")]
		IMatchPhrasePrefixQuery MatchPhrasePrefix { get; set; }

		[JsonProperty("fuzzy")]
		IFuzzyQuery Fuzzy { get; set; }

		[JsonProperty("geo_shape")]
		IGeoShapeQuery GeoShape { get; set; }

		[JsonProperty("common")]
		ICommonTermsQuery CommonTerms { get; set; }

		[JsonProperty("terms")]
		ITermsQuery Terms { get; set; }

		[JsonProperty("range")]
		IRangeQuery Range { get; set; }

		[JsonProperty("regexp")]
		IRegexpQuery Regexp { get; set; }

		[JsonProperty("has_child")]
		IHasChildQuery HasChild { get; set; }

		[JsonProperty("has_parent")]
		IHasParentQuery HasParent { get; set; }

		[JsonProperty("parent_id")]
		IParentIdQuery ParentId { get; set; }

		[JsonProperty("span_term")]
		ISpanTermQuery SpanTerm { get; set; }

		[JsonProperty("simple_query_string")]
		ISimpleQueryStringQuery SimpleQueryString { get; set; }

		[JsonProperty("query_string")]
		IQueryStringQuery QueryString { get; set; }

		[JsonProperty("more_like_this")]
		IMoreLikeThisQuery MoreLikeThis { get; set; }

		[JsonProperty("span_first")]
		ISpanFirstQuery SpanFirst { get; set; }

		[JsonProperty("span_or")]
		ISpanOrQuery SpanOr { get; set; }

		[JsonProperty("span_near")]
		ISpanNearQuery SpanNear { get; set; }

		[JsonProperty("span_not")]
		ISpanNotQuery SpanNot { get; set; }

		[JsonProperty("span_containing")]
		ISpanContainingQuery SpanContaining { get; set; }

		[JsonProperty("span_within")]
		ISpanWithinQuery SpanWithin { get; set; }

		[JsonProperty("span_multi")]
		ISpanMultiTermQuery SpanMultiTerm { get; set; }

		[JsonProperty("field_masking_span")]
		ISpanFieldMaskingQuery SpanFieldMasking { get; set; }

		[JsonProperty("nested")]
		INestedQuery Nested { get; set; }

		[JsonProperty("function_score")]
		IFunctionScoreQuery FunctionScore { get; set; }

		[JsonProperty("geo_bounding_box")]
		IGeoBoundingBoxQuery GeoBoundingBox { get; set; }

		[JsonProperty("geo_distance")]
		IGeoDistanceQuery GeoDistance { get; set; }

		[JsonProperty("geo_polygon")]
		IGeoPolygonQuery GeoPolygon { get; set; }

		[JsonProperty("script")]
		IScriptQuery Script { get; set; }

		[JsonProperty("exists")]
		IExistsQuery Exists { get; set; }

		[JsonProperty("type")]
		ITypeQuery Type { get; set; }

		[JsonProperty("percolate")]
		IPercolateQuery Percolate { get; set; }

		[JsonProperty("terms_set")]
		ITermsSetQuery TermsSet { get; set; }

		void Accept(IQueryVisitor visitor);
	}
}
