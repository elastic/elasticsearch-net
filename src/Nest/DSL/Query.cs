using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

		public static BaseQuery CustomBoostFactor(Action<CustomBoostFactorQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().CustomBoostFactor(selector);
		}

		public static BaseQuery CustomScore(Action<CustomScoreQueryDescriptor<T>> customScoreQuery)
		{
			return new QueryDescriptor<T>().CustomScore(customScoreQuery);
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

		public static BaseQuery Terms(Expression<Func<T, object>> objectPath, params string[] terms)
		{
			return new QueryDescriptor<T>().Terms(objectPath, terms);
		}

		public static BaseQuery Terms(string field, params string[] terms)
		{
			return new QueryDescriptor<T>().Terms(field, terms);
		}

		public static BaseQuery TermsDescriptor(Action<TermsQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().TermsDescriptor(selector);
		}

		public static BaseQuery Text(Action<TextQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().Text(selector);
		}

		public static BaseQuery TextPhrase(Action<TextPhraseQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().TextPhrase(selector);
		}

		public static BaseQuery TextPhrasePrefix(Action<TextPhrasePrefixQueryDescriptor<T>> selector)
		{
			return new QueryDescriptor<T>().TextPhrasePrefix(selector);
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
	}


	public static class Query
	{
		public static BaseQuery Bool(Action<BoolQueryDescriptor<dynamic>> booleanQuery)
		{
			return new QueryDescriptor<dynamic>().Bool(booleanQuery);
		}

		public static BaseQuery Boosting(Action<BoostingQueryDescriptor<dynamic>> boostingQuery)
		{
			return new QueryDescriptor<dynamic>().Boosting(boostingQuery);
		}

		public static BaseQuery ConstantScore(Action<ConstantScoreQueryDescriptor<dynamic>> selector)
		{
			return new QueryDescriptor<dynamic>().ConstantScore(selector);
		}

		public static BaseQuery CustomBoostFactor(Action<CustomBoostFactorQueryDescriptor<dynamic>> selector)
		{
			return new QueryDescriptor<dynamic>().CustomBoostFactor(selector);
		}

		public static BaseQuery CustomScore(Action<CustomScoreQueryDescriptor<dynamic>> customScoreQuery)
		{
			return new QueryDescriptor<dynamic>().CustomScore(customScoreQuery);
		}

		public static BaseQuery Dismax(Action<DismaxQueryDescriptor<dynamic>> selector)
		{
			return new QueryDescriptor<dynamic>().Dismax(selector);
		}

		public static BaseQuery Filtered(Action<FilteredQueryDescriptor<dynamic>> selector)
		{
			return new QueryDescriptor<dynamic>().Filtered(selector);
		}

		public static BaseQuery Fuzzy(Action<FuzzyQueryDescriptor<dynamic>> selector)
		{
			return new QueryDescriptor<dynamic>().Fuzzy(selector);
		}

		public static BaseQuery FuzzyDate(Action<FuzzyDateQueryDescriptor<dynamic>> selector)
		{
			return new QueryDescriptor<dynamic>().FuzzyDate(selector);
		}

		public static BaseQuery FuzzyLikeThis(Action<FuzzyLikeThisDescriptor<dynamic>> selector)
		{
			return new QueryDescriptor<dynamic>().FuzzyLikeThis(selector);
		}

		public static BaseQuery FuzzyNumeric(Action<FuzzyNumericQueryDescriptor<dynamic>> selector)
		{
			return new QueryDescriptor<dynamic>().FuzzyNumeric(selector);
		}

		public static BaseQuery HasChild<K>(Action<HasChildQueryDescriptor<K>> selector) where K : class
		{
			return new QueryDescriptor<dynamic>().HasChild<K>(selector);
		}

		public static BaseQuery Ids(IEnumerable<string> types, IEnumerable<string> values)
		{
			return new QueryDescriptor<dynamic>().Ids(types, values);
		}

		public static BaseQuery Ids(IEnumerable<string> values)
		{
			return new QueryDescriptor<dynamic>().Ids(values);
		}

		public static BaseQuery Ids(string type, IEnumerable<string> values)
		{
			return new QueryDescriptor<dynamic>().Ids(type, values);
		}

		public static BaseQuery Indices(Action<IndicesQueryDescriptor<dynamic>> selector)
		{
			return new QueryDescriptor<dynamic>().Indices(selector);
		}

		public static BaseQuery MatchAll(double? Boost = null, string NormField = null)
		{
			return new QueryDescriptor<dynamic>().MatchAll(Boost, NormField);
		}

		public static BaseQuery MoreLikeThis(Action<MoreLikeThisQueryDescriptor<dynamic>> selector)
		{
			return new QueryDescriptor<dynamic>().MoreLikeThis(selector);
		}

		public static BaseQuery Nested(Action<NestedQueryDescriptor<dynamic>> selector)
		{
			return new QueryDescriptor<dynamic>().Nested(selector);
		}

		public static BaseQuery Prefix(Expression<Func<dynamic, object>> fieldDescriptor, string value, double? Boost = null)
		{
			return new QueryDescriptor<dynamic>().Prefix(fieldDescriptor, value, Boost);
		}

		public static BaseQuery Prefix(string field, string value, double? Boost = null)
		{
			return new QueryDescriptor<dynamic>().Prefix(field, value, Boost);
		}

		public static BaseQuery QueryString(Action<QueryStringDescriptor<dynamic>> selector)
		{
			return new QueryDescriptor<dynamic>().QueryString(selector);
		}

		public static BaseQuery Range(Action<RangeQueryDescriptor<dynamic>> selector)
		{
			return new QueryDescriptor<dynamic>().Range(selector);
		}

		public static BaseQuery SpanFirst(Action<SpanFirstQueryDescriptor<dynamic>> selector)
		{
			return new QueryDescriptor<dynamic>().SpanFirst(selector);
		}

		public static BaseQuery SpanNear(Action<SpanNearQueryDescriptor<dynamic>> selector)
		{
			return new QueryDescriptor<dynamic>().SpanNear(selector);
		}

		public static BaseQuery SpanNot(Action<SpanNotQueryDescriptor<dynamic>> selector)
		{
			return new QueryDescriptor<dynamic>().SpanNot(selector);
		}

		public static BaseQuery SpanOr(Action<SpanOrQueryDescriptor<dynamic>> selector)
		{
			return new QueryDescriptor<dynamic>().SpanOr(selector);
		}

		public static BaseQuery SpanTerm(Expression<Func<dynamic, object>> fieldDescriptor, string value, double? Boost = null)
		{
			return new QueryDescriptor<dynamic>().SpanTerm(fieldDescriptor, value, Boost);
		}

		public static BaseQuery SpanTerm(string field, string value, double? Boost = null)
		{
			return new QueryDescriptor<dynamic>().SpanTerm(field, value, Boost);
		}

		public static BaseQuery Term(Expression<Func<dynamic, object>> fieldDescriptor, string value, double? Boost = null)
		{
			return new QueryDescriptor<dynamic>().Term(fieldDescriptor, value, Boost);
		}

		public static BaseQuery Term(string field, string value, double? Boost = null)
		{
			return new QueryDescriptor<dynamic>().Term(field, value, Boost);
		}

		public static BaseQuery Terms(Expression<Func<dynamic, object>> objectPath, params string[] terms)
		{
			return new QueryDescriptor<dynamic>().Terms(objectPath, terms);
		}

		public static BaseQuery Terms(string field, params string[] terms)
		{
			return new QueryDescriptor<dynamic>().Terms(field, terms);
		}

		public static BaseQuery TermsDescriptor(Action<TermsQueryDescriptor<dynamic>> selector)
		{
			return new QueryDescriptor<dynamic>().TermsDescriptor(selector);
		}

		public static BaseQuery Text(Action<TextQueryDescriptor<dynamic>> selector)
		{
			return new QueryDescriptor<dynamic>().Text(selector);
		}

		public static BaseQuery TextPhrase(Action<TextPhraseQueryDescriptor<dynamic>> selector)
		{
			return new QueryDescriptor<dynamic>().TextPhrase(selector);
		}

		public static BaseQuery TextPhrasePrefix(Action<TextPhrasePrefixQueryDescriptor<dynamic>> selector)
		{
			return new QueryDescriptor<dynamic>().TextPhrasePrefix(selector);
		}

		public static BaseQuery TopChildren<K>(Action<TopChildrenQueryDescriptor<K>> selector) where K : class
		{
			return new QueryDescriptor<dynamic>().TopChildren<K>(selector);
		}

		public static BaseQuery Wildcard(Expression<Func<dynamic, object>> fieldDescriptor, string value, double? Boost = null)
		{
			return new QueryDescriptor<dynamic>().Wildcard(fieldDescriptor, value, Boost);
		}

		public static BaseQuery Wildcard(string field, string value, double? Boost = null)
		{
			return new QueryDescriptor<dynamic>().Wildcard(field, value, Boost);
		}
	}
}
