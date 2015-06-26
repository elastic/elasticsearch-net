using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Nest
{

	public static class Query<T> where T : class
	{
		public static QueryDescriptor<T> Strict(bool strict = true)
		{
			return new QueryDescriptor<T>().Strict(strict);
		}

		public static QueryContainer Bool(Action<BoolQueryDescriptor<T>> booleanQuery)
		{
			return new QueryDescriptor<T>().Bool(booleanQuery);
		}

		public static QueryContainer Boosting(Action<BoostingQueryDescriptor<T>> boostingQuery)
		{
			return new QueryDescriptor<T>().Boosting(boostingQuery);
		}

		public static QueryContainer ConstantScore(Action<ConstantScoreQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().ConstantScore(selector);
		}

		public static QueryContainer Conditionless(Action<ConditionlessQueryDescriptor<T>> conditionlessQuery)
		{
			return new QueryDescriptor<T>().Conditionless(conditionlessQuery);
		}

		public static QueryContainer Dismax(Action<DisMaxQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().Dismax(selector);
		}

		public static QueryContainer Filtered(Action<FilteredQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().Filtered(selector);
		}

		public static QueryContainer Fuzzy(Action<FuzzyQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().Fuzzy(selector);
		}

		public static QueryContainer FuzzyDate(Action<FuzzyDateQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().FuzzyDate(selector);
		}

		public static QueryContainer FuzzyNumeric(Action<FuzzyNumericQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().FuzzyNumeric(selector);
		}

		public static QueryContainer HasChild<K>(Action<HasChildQueryDescriptor<K>> selector) where K : class
		{
			return new QueryDescriptor<T>().HasChild<K>(selector);
		}

		public static QueryContainer Ids(IEnumerable<string> types, IEnumerable<string> values)
		{
			return new QueryDescriptor<T>().Ids(types, values);
		}

		public static QueryContainer Ids(IEnumerable<string> values)
		{
			return new QueryDescriptor<T>().Ids(values);
		}

		public static QueryContainer Ids(string type, IEnumerable<string> values)
		{
			return new QueryDescriptor<T>().Ids(type, values);
		}

		public static QueryContainer Indices(Action<IndicesQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().Indices(selector);
		}

		public static QueryContainer MatchAll(double? Boost = null, string NormField = null)
		{
			return new QueryDescriptor<T>().MatchAll(Boost, NormField);
		}

		public static QueryContainer MoreLikeThis(Action<MoreLikeThisQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().MoreLikeThis(selector);
		}

		public static QueryContainer Nested(Action<NestedQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().Nested(selector);
		}

		public static QueryContainer Prefix(Expression<Func<T, object>> fieldDescriptor, string value, double? Boost = null)
		{
			return new QueryDescriptor<T>().Prefix(fieldDescriptor, value, Boost);
		}

		public static QueryContainer Prefix(string field, string value, double? Boost = null)
		{
			return new QueryDescriptor<T>().Prefix(field, value, Boost);
		}

		public static QueryContainer SimpleQueryString(Action<SimpleQueryStringQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().SimpleQueryString(selector);
		}
		public static QueryContainer CommonTerms(Action<CommonTermsQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().CommonTerms(selector);
		}
		public static QueryContainer GeoShapeCircle(Action<GeoShapeCircleQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().GeoShapeCircle(selector);
		}
		public static QueryContainer GeoShapeEnvelope(Action<GeoShapeEnvelopeQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().GeoShapeEnvelope(selector);
		}
		public static QueryContainer GeoShapeLineString(Action<GeoShapeLineStringQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().GeoShapeLineString(selector);
		}
		public static QueryContainer GeoShapeMultiLineString(Action<GeoShapeMultiLineStringQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().GeoShapeMultiLineString(selector);
		}
		public static QueryContainer GeoShapePoint(Action<GeoShapePointQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().GeoShapePoint(selector);
		}
		public static QueryContainer GeoShapeMultiPoint(Action<GeoShapeMultiPointQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().GeoShapeMultiPoint(selector);
		}
		public static QueryContainer GeoShapePolygon(Action<GeoShapePolygonQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().GeoShapePolygon(selector);
		}
		public static QueryContainer GeoShapeMultiPolygon(Action<GeoShapeMultiPolygonQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().GeoShapeMultiPolygon(selector);
		}
		public static QueryContainer QueryString(Action<QueryStringQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().QueryString(selector);
		}

		public static QueryContainer Range(Action<RangeQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().Range(selector);
		}

		public static QueryContainer SpanFirst(Action<SpanFirstQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().SpanFirst(selector);
		}

		public static QueryContainer SpanNear(Action<SpanNearQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().SpanNear(selector);
		}

		public static QueryContainer SpanNot(Action<SpanNotQuery<T>> selector)
		{
			return new QueryDescriptor<T>().SpanNot(selector);
		}

		public static QueryContainer SpanOr(Action<SpanOrQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().SpanOr(selector);
		}

		public static QueryContainer SpanTerm(Expression<Func<T, object>> fieldDescriptor, string value, double? Boost = null)
		{
			return new QueryDescriptor<T>().SpanTerm(fieldDescriptor, value, Boost);
		}

		public static QueryContainer SpanTerm(string field, string value, double? Boost = null)
		{
			return new QueryDescriptor<T>().SpanTerm(field, value, Boost);
		}

		public static QueryContainer SpanMultiTerm(Action<SpanMultiTermQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().SpanMultiTerm(selector);
		}

		public static QueryContainer Term<K>(Expression<Func<T, K>> fieldDescriptor, K value, double? Boost = null)
		{
			return new QueryDescriptor<T>().Term(fieldDescriptor, value, Boost);
		}

		public static QueryContainer Term<K>(string field, K value, double? Boost = null)
		{
			return new QueryDescriptor<T>().Term(field, value, Boost);
		}
		public static QueryContainer Terms<K>(Expression<Func<T, K>> objectPath, params K[] terms)
		{
			return new QueryDescriptor<T>().Terms<K>(objectPath, terms);
		}
		public static QueryContainer Terms(Expression<Func<T, object>> objectPath, params string[] terms)
		{
			return new QueryDescriptor<T>().Terms(objectPath, terms);
		}

		public static QueryContainer Terms(string field, params string[] terms)
		{
			return new QueryDescriptor<T>().Terms(field, terms);
		}

		public static QueryContainer TermsDescriptor(Action<TermsQueryDescriptor<T, object>> selector)
		{
			return new QueryDescriptor<T>().TermsDescriptor(selector);
		}
		
		public static QueryContainer TermsDescriptor<K>(Action<TermsQueryDescriptor<T, K>> selector)
		{
			return new QueryDescriptor<T>().TermsDescriptor(selector);
		}


		public static QueryContainer Match(Action<MatchQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().Match(selector);
		}

		public static QueryContainer MatchPhrase(Action<MatchPhraseQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().MatchPhrase(selector);
		}

		public static QueryContainer MatchPhrasePrefix(Action<MatchPhrasePrefixQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().MatchPhrasePrefix(selector);
		}

		public static QueryContainer MultiMatch(Action<MultiMatchQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().MultiMatch(selector);
		}

		public static QueryContainer Wildcard(Expression<Func<T, object>> fieldDescriptor, string value, double? Boost = null)
		{
			return new QueryDescriptor<T>().Wildcard(fieldDescriptor, value, Boost);
		}

		public static QueryContainer Wildcard(Action<WildcardQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().Wildcard(selector);
		}

		public static QueryContainer Wildcard(string field, string value, double? Boost = null)
		{
			return new QueryDescriptor<T>().Wildcard(field, value, Boost);
		}

	  public static QueryContainer FunctionScore(Action<FunctionScoreQueryDescriptor<T>> functionScoreQuery)
	  {
    	return new QueryDescriptor<T>().FunctionScore(functionScoreQuery);
    } 
    public static QueryContainer Regexp(Action<RegexpQueryDescriptor<T>> regexpSelector) 
		{
			return new QueryDescriptor<T>().Regexp(regexpSelector);
		}
	}
}
