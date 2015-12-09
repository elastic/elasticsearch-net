using Nest.QueryDsl.Visitor;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
	[JsonConverter(typeof(ReserializeJsonConverter<QueryContainer, IQueryContainer>))]
	public interface IQueryContainer : ICustomJson
	{
		[JsonProperty(PropertyName = "bool")]
		IBoolQuery Bool { get; set; }

		[JsonIgnore]
		bool IsConditionless { get; }

		[JsonIgnore]
		bool IsStrict { get; set; }

		[JsonIgnore]
		bool IsVerbatim { get; set; }
		
		[JsonIgnore]
		string RawQuery { get; set; }
		
		[JsonProperty(PropertyName = "match_all")]
		IMatchAllQuery MatchAllQuery { get; set; }

		[JsonProperty(PropertyName = "term")]
		ITermQuery Term { get; set; }

		[JsonProperty(PropertyName = "wildcard")]
		IWildcardQuery Wildcard { get; set; }

		[JsonProperty(PropertyName = "prefix")]
		IPrefixQuery Prefix { get; set; }

		[JsonProperty(PropertyName = "boosting")]
		IBoostingQuery Boosting { get; set; }

		[JsonProperty(PropertyName = "ids")]
		IIdsQuery Ids { get; set; }

		[JsonProperty(PropertyName = "limit")]
		ILimitQuery Limit { get; set; }

		[JsonProperty(PropertyName = "constant_score")]
		IConstantScoreQuery ConstantScore { get; set; }

		[JsonProperty(PropertyName = "dis_max")]
		IDisMaxQuery DisMax { get; set; }

		[JsonProperty(PropertyName = "multi_match")]
		IMultiMatchQuery MultiMatch { get; set; }

		[JsonProperty(PropertyName = "match")]
		IMatchQuery Match { get; set; }

		[JsonProperty(PropertyName = "fuzzy")]
		IFuzzyQuery Fuzzy { get; set; }

		[JsonProperty(PropertyName = "geo_shape")]
		IGeoShapeQuery GeoShape { get; set; }

		[JsonProperty(PropertyName = "common")]
		ICommonTermsQuery CommonTerms { get; set; }

		[JsonProperty(PropertyName = "terms")]
		ITermsQuery Terms { get; set; }

		[JsonProperty(PropertyName = "range")]
		IRangeQuery Range { get; set; }

		[JsonProperty(PropertyName = "regexp")]
		IRegexpQuery Regexp { get; set; }

		[JsonProperty(PropertyName = "has_child")]
		IHasChildQuery HasChild { get; set; }

		[JsonProperty(PropertyName = "has_parent")]
		IHasParentQuery HasParent { get; set; }
		
		[JsonProperty(PropertyName = "span_term")]
		ISpanTermQuery SpanTerm { get; set; }

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

		[JsonProperty(PropertyName = "span_containing")]
		ISpanContainingQuery SpanContaining { get; set; }

		[JsonProperty(PropertyName = "span_within")]
		ISpanWithinQuery SpanWithin { get; set; }

		[JsonProperty(PropertyName = "span_multi")]
		ISpanMultiTermQuery SpanMultiTerm { get; set; }

		[JsonProperty(PropertyName = "nested")]
		INestedQuery Nested { get; set; }

		[JsonProperty(PropertyName = "indices")]
		IIndicesQuery Indices { get; set; }

		[JsonProperty(PropertyName = "function_score")]
		IFunctionScoreQuery FunctionScore { get; set; }

		[JsonProperty(PropertyName = "template")]
		ITemplateQuery Template { get; set; }

		[JsonProperty("geo_bounding_box")]
		IGeoBoundingBoxQuery GeoBoundingBox { get; set; }

		[JsonProperty("geo_distance")]
		IGeoDistanceQuery GeoDistance { get; set; }

		[JsonProperty("geo_polygon")]
		IGeoPolygonQuery GeoPolygon { get; set; }

		[JsonProperty("geo_distance_range")]
		IGeoDistanceRangeQuery GeoDistanceRange { get; set; }

		[JsonProperty("geohash_cell")]
		IGeoHashCellQuery GeoHashCell { get; set; }

		[JsonProperty("script")]
		IScriptQuery Script { get; set; }

		[JsonProperty("exists")]
		IExistsQuery Exists { get; set; }

		[JsonProperty("missing")]
		IMissingQuery Missing { get; set; }

		[JsonProperty("type")]
		ITypeQuery Type { get; set; }

		[JsonProperty(PropertyName = "filtered")]
		IFilteredQuery Filtered { get; set; }

		[JsonProperty(PropertyName = "and")]
		IAndQuery And { get; set; }

		[JsonProperty(PropertyName = "or")]
		IOrQuery Or { get; set; }

		[JsonProperty(PropertyName = "not")]
		INotQuery Not { get; set; }

		void Accept(IQueryVisitor visitor);
	}
}