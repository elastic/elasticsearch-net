using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.DSL.Query;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using System.Linq.Expressions;

namespace Nest
{

	public static class Query<T> where T : class
	{
		public static QueryDescriptor<T> Strict(bool strict = true)
		{
			return new QueryDescriptor<T>().Strict(strict);
		}

		public static BaseQuery Bool(Action<BoolQueryDescriptor<T>> booleanQuery)
		{
			return new QueryDescriptor<T>().Bool(booleanQuery);
		}

		public static BaseQuery Boosting(Action<BoostingQueryDescriptor<T>> boostingQuery)
		{
			return new QueryDescriptor<T>().Boosting(boostingQuery);
		}

		public static BaseQuery ConstantScore(Action<ConstantScoreQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().ConstantScore(selector);
		}

		[Obsolete("Custom boost factor has been removed in 1.1")]
		public static BaseQuery CustomBoostFactor(Action<CustomBoostFactorQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().CustomBoostFactor(selector);
		}

		[Obsolete("Custom score has been removed in 1.1")]
		public static BaseQuery CustomScore(Action<CustomScoreQueryDescriptor<T>> customScoreQuery)
		{
			return new QueryDescriptor<T>().CustomScore(customScoreQuery);
		}

		public static BaseQuery Conditionless(Action<ConditionlessQueryDescriptor<T>> conditionlessQuery)
		{
			return new QueryDescriptor<T>().Conditionless(conditionlessQuery);
		}

		public static BaseQuery Dismax(Action<DismaxQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().Dismax(selector);
		}

		public static BaseQuery Filtered(Action<FilteredQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().Filtered(selector);
		}

		public static BaseQuery Fuzzy(Action<FuzzyQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().Fuzzy(selector);
		}

		public static BaseQuery FuzzyDate(Action<FuzzyDateQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().FuzzyDate(selector);
		}

		public static BaseQuery FuzzyLikeThis(Action<FuzzyLikeThisDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().FuzzyLikeThis(selector);
		}

		public static BaseQuery FuzzyNumeric(Action<FuzzyNumericQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().FuzzyNumeric(selector);
		}

		public static BaseQuery HasChild<K>(Action<HasChildQueryDescriptor<K>> selector) where K : class
		{
			return new QueryDescriptor<T>().HasChild<K>(selector);
		}

		public static BaseQuery Ids(IEnumerable<string> types, IEnumerable<string> values)
		{
			return new QueryDescriptor<T>().Ids(types, values);
		}

		public static BaseQuery Ids(IEnumerable<string> values)
		{
			return new QueryDescriptor<T>().Ids(values);
		}

		public static BaseQuery Ids(string type, IEnumerable<string> values)
		{
			return new QueryDescriptor<T>().Ids(type, values);
		}

		public static BaseQuery Indices(Action<IndicesQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().Indices(selector);
		}

		public static BaseQuery MatchAll(double? Boost = null, string NormField = null)
		{
			return new QueryDescriptor<T>().MatchAll(Boost, NormField);
		}

		public static BaseQuery MoreLikeThis(Action<MoreLikeThisQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().MoreLikeThis(selector);
		}

		public static BaseQuery Nested(Action<NestedQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().Nested(selector);
		}

		public static BaseQuery Prefix(Expression<Func<T, object>> fieldDescriptor, string value, double? Boost = null)
		{
			return new QueryDescriptor<T>().Prefix(fieldDescriptor, value, Boost);
		}

		public static BaseQuery Prefix(string field, string value, double? Boost = null)
		{
			return new QueryDescriptor<T>().Prefix(field, value, Boost);
		}

		public static BaseQuery SimpleQueryString(Action<SimpleQueryStringQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().SimpleQueryString(selector);
		}
		public static BaseQuery CommonTerms(Action<CommonTermsQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().CommonTerms(selector);
		}
		public static BaseQuery GeoShape(Action<GeoShapeQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().GeoShape(selector);
		}
		public static BaseQuery QueryString(Action<QueryStringDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().QueryString(selector);
		}

		public static BaseQuery Range(Action<RangeQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().Range(selector);
		}

		public static BaseQuery SpanFirst(Action<SpanFirstQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().SpanFirst(selector);
		}

		public static BaseQuery SpanNear(Action<SpanNearQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().SpanNear(selector);
		}

		public static BaseQuery SpanNot(Action<SpanNotQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().SpanNot(selector);
		}

		public static BaseQuery SpanOr(Action<SpanOrQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().SpanOr(selector);
		}

		public static BaseQuery SpanTerm(Expression<Func<T, object>> fieldDescriptor, string value, double? Boost = null)
		{
			return new QueryDescriptor<T>().SpanTerm(fieldDescriptor, value, Boost);
		}

		public static BaseQuery SpanTerm(string field, string value, double? Boost = null)
		{
			return new QueryDescriptor<T>().SpanTerm(field, value, Boost);
		}

		public static BaseQuery Term<K>(Expression<Func<T, K>> fieldDescriptor, K value, double? Boost = null)
		{
			return new QueryDescriptor<T>().Term(fieldDescriptor, value, Boost);
		}

		public static BaseQuery Term<K>(string field, K value, double? Boost = null)
		{
			return new QueryDescriptor<T>().Term(field, value, Boost);
		}
		public static BaseQuery Terms<K>(Expression<Func<T, K>> objectPath, params K[] terms)
		{
			return new QueryDescriptor<T>().Terms<K>(objectPath, terms);
		}
		public static BaseQuery Terms(Expression<Func<T, object>> objectPath, params string[] terms)
		{
			return new QueryDescriptor<T>().Terms(objectPath, terms);
		}

		public static BaseQuery Terms(string field, params string[] terms)
		{
			return new QueryDescriptor<T>().Terms(field, terms);
		}

		public static BaseQuery TermsDescriptor(Action<TermsQueryDescriptor<T, object>> selector)
		{
			return new QueryDescriptor<T>().TermsDescriptor(selector);
		}
		
		public static BaseQuery TermsDescriptor<K>(Action<TermsQueryDescriptor<T, K>> selector)
		{
			return new QueryDescriptor<T>().TermsDescriptor(selector);
		}


		public static BaseQuery Match(Action<MatchQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().Match(selector);
		}

		public static BaseQuery MatchPhrase(Action<MatchPhraseQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().MatchPhrase(selector);
		}

		public static BaseQuery MatchPhrasePrefix(Action<MatchPhrasePrefixQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().MatchPhrasePrefix(selector);
		}

		public static BaseQuery MultiMatch(Action<MultiMatchQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().MultiMatch(selector);
		}


		public static BaseQuery TopChildren<K>(Action<TopChildrenQueryDescriptor<K>> selector) where K : class
		{
			return new QueryDescriptor<T>().TopChildren<K>(selector);
		}

		public static BaseQuery Wildcard(Expression<Func<T, object>> fieldDescriptor, string value, double? Boost = null)
		{
			return new QueryDescriptor<T>().Wildcard(fieldDescriptor, value, Boost);
		}

		public static BaseQuery Wildcard(string field, string value, double? Boost = null)
		{
			return new QueryDescriptor<T>().Wildcard(field, value, Boost);
		}

	  public static BaseQuery FunctionScore(Action<FunctionScoreQueryDescriptor<T>> functionScoreQuery)
	  {
    	return new QueryDescriptor<T>().FunctionScore(functionScoreQuery);
    } 
    public static BaseQuery Regexp(Action<RegexpQueryDescriptor<T>> regexpSelector) 
		{
			return new QueryDescriptor<T>().Regexp(regexpSelector);
		}
	}
}
