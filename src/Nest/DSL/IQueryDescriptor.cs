using System;
using System.Collections.Generic;
namespace Nest
{
  interface IQueryDescriptor<T>
   where T : class
  {
    BaseQuery Bool(Action<BoolQueryDescriptor<T>> booleanQuery);
    BaseQuery Boosting(Action<BoostingQueryDescriptor<T>> boostingQuery);
    BaseQuery ConstantScore(Action<ConstantScoreQueryDescriptor<T>> selector);
    BaseQuery CustomBoostFactor(Action<CustomBoostFactorQueryDescriptor<T>> selector);
    BaseQuery CustomScore(Action<CustomScoreQueryDescriptor<T>> customScoreQuery);
    BaseQuery Dismax(Action<DismaxQueryDescriptor<T>> selector);
    BaseQuery Filtered(Action<FilteredQueryDescriptor<T>> selector);
    BaseQuery Fuzzy(Action<FuzzyQueryDescriptor<T>> selector);
    BaseQuery FuzzyDate(Action<FuzzyDateQueryDescriptor<T>> selector);
    BaseQuery FuzzyLikeThis(Action<FuzzyLikeThisDescriptor<T>> selector);
    BaseQuery FuzzyNumeric(Action<FuzzyNumericQueryDescriptor<T>> selector);
    BaseQuery HasChild<K>(Action<HasChildQueryDescriptor<K>> selector) where K : class;
    BaseQuery Ids(IEnumerable<string> types, IEnumerable<string> values);
    BaseQuery Ids(IEnumerable<string> values);
    BaseQuery Ids(string type, IEnumerable<string> values);
    BaseQuery Indices(Action<IndicesQueryDescriptor<T>> selector);
    BaseQuery MatchAll(double? Boost = null, string NormField = null);
    BaseQuery MoreLikeThis(Action<MoreLikeThisQueryDescriptor<T>> selector);
    BaseQuery Nested(Action<NestedQueryDescriptor<T>> selector);
	BaseQuery Prefix(System.Linq.Expressions.Expression<Func<T, object>> fieldDescriptor, string value, double? Boost = null, RewriteMultiTerm? Rewrite = null);
	BaseQuery Prefix(string field, string value, double? Boost = null, RewriteMultiTerm? Rewrite = null);
    BaseQuery QueryString(Action<QueryStringDescriptor<T>> selector);
    BaseQuery Range(Action<RangeQueryDescriptor<T>> selector);
    BaseQuery SpanFirst(Action<SpanFirstQueryDescriptor<T>> selector);
    BaseQuery SpanNear(Action<SpanNearQueryDescriptor<T>> selector);
    BaseQuery SpanNot(Action<SpanNotQueryDescriptor<T>> selector);
    BaseQuery SpanOr(Action<SpanOrQueryDescriptor<T>> selector);
    BaseQuery SpanTerm(System.Linq.Expressions.Expression<Func<T, object>> fieldDescriptor, string value, double? Boost = null);
    BaseQuery SpanTerm(string field, string value, double? Boost = null);
    BaseQuery Term(System.Linq.Expressions.Expression<Func<T, object>> fieldDescriptor, string value, double? Boost = null);
    BaseQuery Term(string field, string value, double? Boost = null);
    BaseQuery Terms(System.Linq.Expressions.Expression<Func<T, object>> objectPath, params string[] terms);
    BaseQuery Terms(string field, params string[] terms);
    BaseQuery TermsDescriptor(Action<TermsQueryDescriptor<T>> selector);
    BaseQuery Text(Action<TextQueryDescriptor<T>> selector);
    BaseQuery TextPhrase(Action<TextPhraseQueryDescriptor<T>> selector);
    BaseQuery TextPhrasePrefix(Action<TextPhrasePrefixQueryDescriptor<T>> selector);
    BaseQuery TopChildren<K>(Action<TopChildrenQueryDescriptor<K>> selector) where K : class;
	BaseQuery Wildcard(System.Linq.Expressions.Expression<Func<T, object>> fieldDescriptor, string value, double? Boost = null, RewriteMultiTerm? Rewrite = null);
    BaseQuery Wildcard(string field, string value, double? Boost = null, RewriteMultiTerm? Rewrite = null);
  }
}
