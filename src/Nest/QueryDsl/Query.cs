using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Nest
{
	// TODO: Write a unit test for these using reflection to make sure all queries are covered
	public static class Query<T> where T : class
	{
		public static QueryContainerDescriptor<T> Strict(bool strict = true) => new QueryContainerDescriptor<T>().Strict(strict);

		public static QueryContainer Bool(Func<BoolQueryDescriptor<T>, IBoolQuery> selector) => 
			new QueryContainerDescriptor<T>().Bool(selector);

		public static QueryContainer Boosting(Func<BoostingQueryDescriptor<T>, IBoostingQuery> selector) => 
			new QueryContainerDescriptor<T>().Boosting(selector);

		public static QueryContainer ConstantScore(Func<ConstantScoreQueryDescriptor<T>, IConstantScoreQuery> selector) => 
			new QueryContainerDescriptor<T>().ConstantScore(selector);

		public static QueryContainer Conditionless(Func<ConditionlessQueryDescriptor<T>, IConditionlessQuery> selector) => 
			new QueryContainerDescriptor<T>().Conditionless(selector);

		public static QueryContainer Dismax(Func<DisMaxQueryDescriptor<T>, IDisMaxQuery> selector) => 
			new QueryContainerDescriptor<T>().Dismax(selector);

		public static QueryContainer Filtered(Func<FilteredQueryDescriptor<T>, IFilteredQuery> selector) => 
			new QueryContainerDescriptor<T>().Filtered(selector);

		public static QueryContainer Fuzzy(Func<FuzzyQueryDescriptor<T>, IFuzzyQuery> selector) => 
			new QueryContainerDescriptor<T>().Fuzzy(selector);

		public static QueryContainer HasChild<K>(Func<HasChildQueryDescriptor<K>, IHasChildQuery> selector) where K : class => 
			new QueryContainerDescriptor<T>().HasChild<K>(selector);

		public static QueryContainer Ids(Func<IdsQueryDescriptor, IIdsQuery> selector) => 
			new QueryContainerDescriptor<T>().Ids(selector);

		public static QueryContainer Indices(Func<IndicesQueryDescriptor<T>, IIndicesQuery> selector) => 
			new QueryContainerDescriptor<T>().Indices(selector);

		public static QueryContainer MatchAll(Func<MatchAllQueryDescriptor, IMatchAllQuery> selector = null) => 
			new QueryContainerDescriptor<T>().MatchAll(selector);

		public static QueryContainer MoreLikeThis(Func<MoreLikeThisQueryDescriptor<T>, IMoreLikeThisQuery> selector) => 
			new QueryContainerDescriptor<T>().MoreLikeThis(selector);

		public static QueryContainer Nested(Func<NestedQueryDescriptor<T>, INestedQuery> selector) => 
			new QueryContainerDescriptor<T>().Nested(selector);

		public static QueryContainer Prefix(Expression<Func<T, object>> fieldDescriptor, string value, double? Boost = null) => 
			new QueryContainerDescriptor<T>().Prefix(fieldDescriptor, value, Boost);

		public static QueryContainer Prefix(string field, string value, double? Boost = null) => 
			new QueryContainerDescriptor<T>().Prefix(field, value, Boost);

		public static QueryContainer SimpleQueryString(Func<SimpleQueryStringQueryDescriptor<T>, ISimpleQueryStringQuery> selector) => 
			new QueryContainerDescriptor<T>().SimpleQueryString(selector);

		public static QueryContainer CommonTerms(Func<CommonTermsQueryDescriptor<T>, ICommonTermsQuery> selector) =>
			new QueryContainerDescriptor<T>().CommonTerms(selector);

		public static QueryContainer GeoShapeCircle(Func<GeoShapeCircleQueryDescriptor<T>, IGeoShapeCircleQuery> selector) => 
			new QueryContainerDescriptor<T>().GeoShapeCircle(selector);

		public static QueryContainer GeoShapeEnvelope(Func<GeoShapeEnvelopeQueryDescriptor<T>, IGeoShapeEnvelopeQuery> selector) => 
			new QueryContainerDescriptor<T>().GeoShapeEnvelope(selector);

		public static QueryContainer GeoShapeLineString(Func<GeoShapeLineStringQueryDescriptor<T>, IGeoShapeLineStringQuery> selector) => 
			new QueryContainerDescriptor<T>().GeoShapeLineString(selector);

		public static QueryContainer GeoShapeMultiLineString(Func<GeoShapeMultiLineStringQueryDescriptor<T>, IGeoShapeMultiLineStringQuery> selector) => 
			new QueryContainerDescriptor<T>().GeoShapeMultiLineString(selector);

		public static QueryContainer GeoShapePoint(Func<GeoShapePointQueryDescriptor<T>, IGeoShapePointQuery> selector) => 
			new QueryContainerDescriptor<T>().GeoShapePoint(selector);

		public static QueryContainer GeoShapeMultiPoint(Func<GeoShapeMultiPointQueryDescriptor<T>, IGeoShapeMultiPointQuery> selector) => 
			new QueryContainerDescriptor<T>().GeoShapeMultiPoint(selector);

		public static QueryContainer GeoShapePolygon(Func<GeoShapePolygonQueryDescriptor<T>, IGeoShapePolygonQuery> selector) => 
			new QueryContainerDescriptor<T>().GeoShapePolygon(selector);

		public static QueryContainer GeoShapeMultiPolygon(Func<GeoShapeMultiPolygonQueryDescriptor<T>, IGeoShapeMultiPolygonQuery> selector) => 
			new QueryContainerDescriptor<T>().GeoShapeMultiPolygon(selector);

		public static QueryContainer QueryString(Func<QueryStringQueryDescriptor<T>, IQueryStringQuery> selector) => 
			new QueryContainerDescriptor<T>().QueryString(selector);

		public static QueryContainer Range(Func<RangeQueryDescriptor<T>, IRangeQuery> selector) => 
			new QueryContainerDescriptor<T>().Range(selector);

		public static QueryContainer SpanFirst(Func<SpanFirstQueryDescriptor<T>, ISpanFirstQuery> selector) => 
			new QueryContainerDescriptor<T>().SpanFirst(selector);

		public static QueryContainer SpanNear(Func<SpanNearQueryDescriptor<T>, ISpanNearQuery> selector) => 
			new QueryContainerDescriptor<T>().SpanNear(selector);

		public static QueryContainer SpanNot(Func<SpanNotQueryDescriptor<T>, ISpanNotQuery> selector) => 
			new QueryContainerDescriptor<T>().SpanNot(selector);

		public static QueryContainer SpanOr(Func<SpanOrQueryDescriptor<T>, ISpanOrQuery> selector) => 
			new QueryContainerDescriptor<T>().SpanOr(selector);

		public static QueryContainer SpanTerm(Expression<Func<T, object>> fieldDescriptor, string value, double? Boost = null) => 
			new QueryContainerDescriptor<T>().SpanTerm(fieldDescriptor, value, Boost);

		public static QueryContainer SpanTerm(string field, string value, double? Boost = null) => 
			new QueryContainerDescriptor<T>().SpanTerm(field, value, Boost);

		public static QueryContainer SpanMultiTerm(Func<SpanMultiTermQueryDescriptor<T>, ISpanMultiTermQuery> selector) => 
			new QueryContainerDescriptor<T>().SpanMultiTerm(selector);

		public static QueryContainer Term<K>(Expression<Func<T, object>> fieldDescriptor, K value, double? Boost = null) => 
			new QueryContainerDescriptor<T>().Term(fieldDescriptor, value, Boost);

		public static QueryContainer Term<K>(string field, K value, double? Boost = null) => 
			new QueryContainerDescriptor<T>().Term(field, value, Boost);

		public static QueryContainer Terms(string field, params string[] terms) => 
			new QueryContainerDescriptor<T>().Terms(field, terms);

		public static QueryContainer TermsDescriptor(Func<TermsQueryDescriptor<T, object>, ITermsQuery> selector) => 
			new QueryContainerDescriptor<T>().Terms(selector);

		public static QueryContainer TermsDescriptor<K>(Func<TermsQueryDescriptor<T, K>, ITermsQuery> selector) => 
			new QueryContainerDescriptor<T>().Terms(selector);

		public static QueryContainer Match(Func<MatchQueryDescriptor<T>, IMatchQuery> selector) => 
			new QueryContainerDescriptor<T>().Match(selector);

		public static QueryContainer MatchPhrase(Func<MatchPhraseQueryDescriptor<T>, IMatchQuery> selector) => 
			new QueryContainerDescriptor<T>().MatchPhrase(selector);

		public static QueryContainer MatchPhrasePrefix(Func<MatchPhrasePrefixQueryDescriptor<T>, IMatchQuery> selector) => 
			new QueryContainerDescriptor<T>().MatchPhrasePrefix(selector);

		public static QueryContainer MultiMatch(Func<MultiMatchQueryDescriptor<T>, IMultiMatchQuery> selector) => 
			new QueryContainerDescriptor<T>().MultiMatch(selector);

		public static QueryContainer Wildcard(Expression<Func<T, object>> fieldDescriptor, string value, double? Boost = null) => 
			new QueryContainerDescriptor<T>().Wildcard(fieldDescriptor, value, Boost);

		public static QueryContainer Wildcard(Func<WildcardQueryDescriptor<T>, IWildcardQuery> selector) => 
			new QueryContainerDescriptor<T>().Wildcard(selector);

		public static QueryContainer Wildcard(string field, string value, double? Boost = null) => 
			new QueryContainerDescriptor<T>().Wildcard(field, value, Boost);

		public static QueryContainer FunctionScore(Func<FunctionScoreQueryDescriptor<T>, IFunctionScoreQuery> selector) => 
			new QueryContainerDescriptor<T>().FunctionScore(selector);

		public static QueryContainer Regexp(Func<RegexpQueryDescriptor<T>, IRegexpQuery> selector) => 
			new QueryContainerDescriptor<T>().Regexp(selector);

	}
}
