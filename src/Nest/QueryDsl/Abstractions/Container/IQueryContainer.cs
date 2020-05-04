// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[JsonFormatter(typeof(QueryContainerInterfaceFormatter))]
	public interface IQueryContainer
	{
		[DataMember(Name ="bool")]
		IBoolQuery Bool { get; set; }

		[DataMember(Name ="boosting")]
		IBoostingQuery Boosting { get; set; }

		[DataMember(Name ="common")]
#pragma warning disable 618
		ICommonTermsQuery CommonTerms { get; set; }
#pragma warning restore 618

		[DataMember(Name ="constant_score")]
		IConstantScoreQuery ConstantScore { get; set; }

		[DataMember(Name ="dis_max")]
		IDisMaxQuery DisMax { get; set; }

		[DataMember(Name ="exists")]
		IExistsQuery Exists { get; set; }

		[DataMember(Name ="function_score")]
		IFunctionScoreQuery FunctionScore { get; set; }

		[DataMember(Name ="fuzzy")]
		IFuzzyQuery Fuzzy { get; set; }

		[DataMember(Name ="geo_bounding_box")]
		IGeoBoundingBoxQuery GeoBoundingBox { get; set; }

		[DataMember(Name ="geo_distance")]
		IGeoDistanceQuery GeoDistance { get; set; }

		[DataMember(Name ="geo_polygon")]
		IGeoPolygonQuery GeoPolygon { get; set; }

		[DataMember(Name ="geo_shape")]
		IGeoShapeQuery GeoShape { get; set; }

		[DataMember(Name ="shape")]
		IShapeQuery Shape { get; set; }

		[DataMember(Name ="has_child")]
		IHasChildQuery HasChild { get; set; }

		[DataMember(Name ="has_parent")]
		IHasParentQuery HasParent { get; set; }

		[DataMember(Name ="ids")]
		IIdsQuery Ids { get; set; }

		[DataMember(Name = "intervals")]
		IIntervalsQuery Intervals { get; set; }

		[IgnoreDataMember]
		bool IsConditionless { get; }

		[IgnoreDataMember]
		bool IsStrict { get; set; }

		[IgnoreDataMember]
		bool IsVerbatim { get; set; }

		[IgnoreDataMember]
		bool IsWritable { get; }

		[DataMember(Name ="match")]
		IMatchQuery Match { get; set; }

		[DataMember(Name ="match_all")]
		IMatchAllQuery MatchAll { get; set; }

		[DataMember(Name ="match_bool_prefix")]
		IMatchBoolPrefixQuery MatchBoolPrefix { get; set; }

		[DataMember(Name ="match_none")]
		IMatchNoneQuery MatchNone { get; set; }

		[DataMember(Name ="match_phrase")]
		IMatchPhraseQuery MatchPhrase { get; set; }

		[DataMember(Name ="match_phrase_prefix")]
		IMatchPhrasePrefixQuery MatchPhrasePrefix { get; set; }

		[DataMember(Name ="more_like_this")]
		IMoreLikeThisQuery MoreLikeThis { get; set; }

		[DataMember(Name ="multi_match")]
		IMultiMatchQuery MultiMatch { get; set; }

		[DataMember(Name ="nested")]
		INestedQuery Nested { get; set; }

		[DataMember(Name ="parent_id")]
		IParentIdQuery ParentId { get; set; }

		[DataMember(Name ="percolate")]
		IPercolateQuery Percolate { get; set; }

		[DataMember(Name ="prefix")]
		IPrefixQuery Prefix { get; set; }

		[DataMember(Name ="query_string")]
		IQueryStringQuery QueryString { get; set; }

		[DataMember(Name ="range")]
		IRangeQuery Range { get; set; }

		[IgnoreDataMember]
		IRawQuery RawQuery { get; set; }

		[DataMember(Name ="regexp")]
		IRegexpQuery Regexp { get; set; }

		[DataMember(Name ="script")]
		IScriptQuery Script { get; set; }

		/// <inheritdoc cref="IScriptScoreQuery"/>
		[DataMember(Name ="script_score")]
		IScriptScoreQuery ScriptScore { get; set; }

		[DataMember(Name ="simple_query_string")]
		ISimpleQueryStringQuery SimpleQueryString { get; set; }

		[DataMember(Name ="span_containing")]
		ISpanContainingQuery SpanContaining { get; set; }

		[DataMember(Name ="field_masking_span")]
		ISpanFieldMaskingQuery SpanFieldMasking { get; set; }

		[DataMember(Name ="span_first")]
		ISpanFirstQuery SpanFirst { get; set; }

		[DataMember(Name ="span_multi")]
		ISpanMultiTermQuery SpanMultiTerm { get; set; }

		[DataMember(Name ="span_near")]
		ISpanNearQuery SpanNear { get; set; }

		[DataMember(Name ="span_not")]
		ISpanNotQuery SpanNot { get; set; }

		[DataMember(Name ="span_or")]
		ISpanOrQuery SpanOr { get; set; }

		[DataMember(Name ="span_term")]
		ISpanTermQuery SpanTerm { get; set; }

		[DataMember(Name ="span_within")]
		ISpanWithinQuery SpanWithin { get; set; }

		[DataMember(Name ="term")]
		ITermQuery Term { get; set; }

		[DataMember(Name ="terms")]
		ITermsQuery Terms { get; set; }

		[DataMember(Name ="terms_set")]
		ITermsSetQuery TermsSet { get; set; }

		[DataMember(Name = "wildcard")]
		IWildcardQuery Wildcard { get; set; }

		/// <inheritdoc cref="IRankFeatureQuery"/>
		[DataMember(Name = "rank_feature")]
		IRankFeatureQuery RankFeature { get; set; }

		/// <inheritdoc cref="IDistanceFeatureQuery"/>
		[DataMember(Name = "distance_feature")]
		IDistanceFeatureQuery DistanceFeature { get; set; }

		/// <inheritdoc cref="IPinnedQuery"/>
		[DataMember(Name = "pinned")]
		IPinnedQuery Pinned { get; set; }


		void Accept(IQueryVisitor visitor);
	}
}
