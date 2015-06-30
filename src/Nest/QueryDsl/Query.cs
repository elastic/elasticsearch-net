using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Nest
{
	// Do we really need this class?
	// TODO: Write a unit test for these using reflection to make sure all queries are covered
	public static class Query<T> where T : class
	{
		public static QueryDescriptor<T> Strict(bool strict = true) => new QueryDescriptor<T>().Strict(strict);

		public static QueryContainer Bool(Func<BoolQueryDescriptor<T>, IBoolQuery> selector) => 
			new QueryDescriptor<T>().Bool(selector);

		public static QueryContainer Boosting(Func<BoostingQueryDescriptor<T>, IBoostingQuery> selector) => 
			new QueryDescriptor<T>().Boosting(selector);

		public static QueryContainer ConstantScore(Func<ConstantScoreQueryDescriptor<T>, IConstantScoreQuery> selector) => 
			new QueryDescriptor<T>().ConstantScore(selector);

		public static QueryContainer Conditionless(Func<ConditionlessQueryDescriptor<T>, IConditionlessQuery> selector) => 
			new QueryDescriptor<T>().Conditionless(selector);

		public static QueryContainer Dismax(Func<DisMaxQueryDescriptor<T>, IDisMaxQuery> selector) => 
			new QueryDescriptor<T>().Dismax(selector);

		public static QueryContainer Filtered(Func<FilteredQueryDescriptor<T>, IFilteredQuery> selector) => 
			new QueryDescriptor<T>().Filtered(selector);

		public static QueryContainer Fuzzy(Func<FuzzyQueryDescriptor<T>, IFuzzyQuery> selector) => 
			new QueryDescriptor<T>().Fuzzy(selector);

		public static QueryContainer HasChild<K>(Func<HasChildQueryDescriptor<K>, IHasChildQuery> selector) where K : class => 
			new QueryDescriptor<T>().HasChild<K>(selector);

		public static QueryContainer Ids(Func<IdsQueryDescriptor, IIdsQuery> selector) => 
			new QueryDescriptor<T>().Ids(selector);

		public static QueryContainer Indices(Func<IndicesQueryDescriptor<T>, IIndicesQuery> selector) => 
			new QueryDescriptor<T>().Indices(selector);

		public static QueryContainer MatchAll(double? Boost = null, string NormField = null) => 
			new QueryDescriptor<T>().MatchAll(Boost, NormField);

		public static QueryContainer MoreLikeThis(Func<MoreLikeThisQueryDescriptor<T>, IMoreLikeThisQuery> selector) => 
			new QueryDescriptor<T>().MoreLikeThis(selector);

		public static QueryContainer Nested(Func<NestedQueryDescriptor<T>, INestedQuery> selector) => 
			new QueryDescriptor<T>().Nested(selector);

		public static QueryContainer Prefix(Expression<Func<T, object>> fieldDescriptor, string value, double? Boost = null) => 
			new QueryDescriptor<T>().Prefix(fieldDescriptor, value, Boost);

		public static QueryContainer Prefix(string field, string value, double? Boost = null) => 
			new QueryDescriptor<T>().Prefix(field, value, Boost);

		public static QueryContainer SimpleQueryString(Func<SimpleQueryStringQueryDescriptor<T>, ISimpleQueryStringQuery> selector) => 
			new QueryDescriptor<T>().SimpleQueryString(selector);

		public static QueryContainer CommonTerms(Func<CommonTermsQueryDescriptor<T>, ICommonTermsQuery> selector) =>
			new QueryDescriptor<T>().CommonTerms(selector);

		public static QueryContainer GeoShapeCircle(Func<GeoShapeCircleQueryDescriptor<T>, IGeoShapeCircleQuery> selector) => 
			new QueryDescriptor<T>().GeoShapeCircle(selector);

		public static QueryContainer GeoShapeEnvelope(Func<GeoShapeEnvelopeQueryDescriptor<T>, IGeoShapeEnvelopeQuery> selector) => 
			new QueryDescriptor<T>().GeoShapeEnvelope(selector);

		public static QueryContainer GeoShapeLineString(Func<GeoShapeLineStringQueryDescriptor<T>, IGeoShapeLineStringQuery> selector) => 
			new QueryDescriptor<T>().GeoShapeLineString(selector);

		public static QueryContainer GeoShapeMultiLineString(Func<GeoShapeMultiLineStringQueryDescriptor<T>, IGeoShapeMultiLineStringQuery> selector) => 
			new QueryDescriptor<T>().GeoShapeMultiLineString(selector);

		public static QueryContainer GeoShapePoint(Func<GeoShapePointQueryDescriptor<T>, IGeoShapePointQuery> selector) => 
			new QueryDescriptor<T>().GeoShapePoint(selector);

		public static QueryContainer GeoShapeMultiPoint(Func<GeoShapeMultiPointQueryDescriptor<T>, IGeoShapeMultiPointQuery> selector) => 
			new QueryDescriptor<T>().GeoShapeMultiPoint(selector);

		public static QueryContainer GeoShapePolygon(Func<GeoShapePolygonQueryDescriptor<T>, IGeoShapePolygonQuery> selector) => 
			new QueryDescriptor<T>().GeoShapePolygon(selector);

		public static QueryContainer GeoShapeMultiPolygon(Func<GeoShapeMultiPolygonQueryDescriptor<T>, IGeoShapeMultiPolygonQuery> selector) => 
			new QueryDescriptor<T>().GeoShapeMultiPolygon(selector);

		public static QueryContainer QueryString(Func<QueryStringQueryDescriptor<T>, IQueryStringQuery> selector) => 
			new QueryDescriptor<T>().QueryString(selector);

		public static QueryContainer Range(Func<RangeQueryDescriptor<T>, IRangeQuery> selector) => 
			new QueryDescriptor<T>().Range(selector);

		public static QueryContainer SpanFirst(Func<SpanFirstQueryDescriptor<T>, ISpanFirstQuery> selector) => 
			new QueryDescriptor<T>().SpanFirst(selector);

		public static QueryContainer SpanNear(Func<SpanNearQueryDescriptor<T>, ISpanNearQuery> selector) => 
			new QueryDescriptor<T>().SpanNear(selector);

		public static QueryContainer SpanNot(Func<SpanNotQuery<T>, ISpanNotQuery> selector) => 
			new QueryDescriptor<T>().SpanNot(selector);

		public static QueryContainer SpanOr(Func<SpanOrQueryDescriptor<T>, ISpanOrQuery> selector) => 
			new QueryDescriptor<T>().SpanOr(selector);

		public static QueryContainer SpanTerm(Expression<Func<T, object>> fieldDescriptor, string value, double? Boost = null) => 
			new QueryDescriptor<T>().SpanTerm(fieldDescriptor, value, Boost);

		public static QueryContainer SpanTerm(string field, string value, double? Boost = null) => 
			new QueryDescriptor<T>().SpanTerm(field, value, Boost);

		public static QueryContainer SpanMultiTerm(Func<SpanMultiTermQueryDescriptor<T>, ISpanMultiTermQuery> selector) => 
			new QueryDescriptor<T>().SpanMultiTerm(selector);

		public static QueryContainer Term<K>(Expression<Func<T, K>> fieldDescriptor, K value, double? Boost = null) => 
			new QueryDescriptor<T>().Term(fieldDescriptor, value, Boost);

		public static QueryContainer Term<K>(string field, K value, double? Boost = null) => 
			new QueryDescriptor<T>().Term(field, value, Boost);

		public static QueryContainer Terms<K>(Expression<Func<T, K>> objectPath, params K[] terms) => 
			new QueryDescriptor<T>().Terms<K>(objectPath, terms);

		public static QueryContainer Terms(Expression<Func<T, object>> objectPath, params string[] terms) => 
			new QueryDescriptor<T>().Terms(objectPath, terms);

		public static QueryContainer Terms(string field, params string[] terms) => 
			new QueryDescriptor<T>().Terms(field, terms);

		public static QueryContainer TermsDescriptor(Func<TermsQueryDescriptor<T, object>, ITermsQuery> selector) => 
			new QueryDescriptor<T>().Terms(selector);

		public static QueryContainer TermsDescriptor<K>(Func<TermsQueryDescriptor<T, K>, ITermsQuery> selector) => 
			new QueryDescriptor<T>().Terms(selector);

		public static QueryContainer Match(Func<MatchQueryDescriptor<T>, IMatchQuery> selector) => 
			new QueryDescriptor<T>().Match(selector);

		public static QueryContainer MatchPhrase(Func<MatchPhraseQueryDescriptor<T>, IMatchQuery> selector) => 
			new QueryDescriptor<T>().MatchPhrase(selector);

		public static QueryContainer MatchPhrasePrefix(Func<MatchPhrasePrefixQueryDescriptor<T>, IMatchQuery> selector) => 
			new QueryDescriptor<T>().MatchPhrasePrefix(selector);

		public static QueryContainer MultiMatch(Func<MultiMatchQueryDescriptor<T>, IMultiMatchQuery> selector) => 
			new QueryDescriptor<T>().MultiMatch(selector);

		public static QueryContainer Wildcard(Expression<Func<T, object>> fieldDescriptor, string value, double? Boost = null) => 
			new QueryDescriptor<T>().Wildcard(fieldDescriptor, value, Boost);

		public static QueryContainer Wildcard(Func<WildcardQueryDescriptor<T>, IWildcardQuery> selector) => 
			new QueryDescriptor<T>().Wildcard(selector);

		public static QueryContainer Wildcard(string field, string value, double? Boost = null) => 
			new QueryDescriptor<T>().Wildcard(field, value, Boost);

		public static QueryContainer FunctionScore(Func<FunctionScoreQueryDescriptor<T>, IFunctionScoreQuery> selector) => 
			new QueryDescriptor<T>().FunctionScore(selector);

		public static QueryContainer Regexp(Func<RegexpQueryDescriptor<T>, IRegexpQuery> selector) => 
			new QueryDescriptor<T>().Regexp(selector);

	}
}
