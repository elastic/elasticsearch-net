using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Resolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using Elasticsearch.Net;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class BaseQuery : IQueryDescriptor
	{
		private static readonly IEnumerable<BaseQuery> Empty = Enumerable.Empty<BaseQuery>();

		[JsonProperty(PropertyName = "bool")]
		BoolBaseQueryDescriptor IQueryDescriptor.BoolQueryDescriptor { get; set; }
		
		[JsonProperty(PropertyName = "match_all")]
		MatchAll IQueryDescriptor.MatchAllQuery { get; set; }
		
		[JsonProperty(PropertyName = "term")]
		Term IQueryDescriptor.TermQuery { get; set; }
		
		[JsonProperty(PropertyName = "wildcard")]
		Wildcard IQueryDescriptor.WildcardQuery { get; set; }
		
		[JsonProperty(PropertyName = "prefix")]
		Prefix IQueryDescriptor.PrefixQuery { get; set; }

		[JsonProperty(PropertyName = "boosting")]
		IBoostingQuery IQueryDescriptor.BoostingQueryDescriptor { get; set; }
		
		[JsonProperty(PropertyName = "ids")]
		IdsQuery IQueryDescriptor.IdsQuery { get; set; }
		
		[JsonProperty(PropertyName = "custom_score")]
		ICustomScoreQuery IQueryDescriptor.CustomScoreQueryDescriptor { get; set; }
		
		[JsonProperty(PropertyName = "custom_filters_score")]
		ICustomFiltersScoreQuery IQueryDescriptor.CustomFiltersScoreQueryDescriptor { get; set; }

		[JsonProperty(PropertyName = "custom_boost_factor")]
		ICustomBoostFactorQuery IQueryDescriptor.CustomBoostFactorQueryDescriptor { get; set; }
		
		[JsonProperty(PropertyName = "constant_score")]
		IConstantScoreQuery IQueryDescriptor.ConstantScoreQueryDescriptor { get; set; }
		
		[JsonProperty(PropertyName = "dis_max")]
		IDismaxQuery IQueryDescriptor.DismaxQueryDescriptor { get; set; }
		
		[JsonProperty(PropertyName = "filtered")]
		IFilteredQuery IQueryDescriptor.FilteredQueryDescriptor { get; set; }

		[JsonProperty(PropertyName = "multi_match")]
		IMultiMatchQuery IQueryDescriptor.MultiMatchQueryDescriptor { get; set; }
		
		[JsonProperty(PropertyName = "match")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<PropertyPathMarker, object> IQueryDescriptor.MatchQueryDescriptor { get; set; }
		
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		[JsonProperty(PropertyName = "fuzzy")]
		IDictionary<PropertyPathMarker, object> IQueryDescriptor.FuzzyQueryDescriptor { get; set; }
		
		[JsonProperty(PropertyName = "geo_shape")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<PropertyPathMarker, object> IQueryDescriptor.GeoShapeQueryDescriptor { get; set; }
		
		[JsonProperty(PropertyName = "common_terms")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<PropertyPathMarker, object> IQueryDescriptor.CommonTermsQueryDescriptor { get; set; }
		
		[JsonProperty(PropertyName = "terms")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<PropertyPathMarker, object> IQueryDescriptor.TermsQueryDescriptor { get; set; }
		
		[JsonProperty(PropertyName = "simple_query_string")]
		ISimpleQueryStringQuery IQueryDescriptor.SimpleQueryStringDescriptor { get; set; }
		
		[JsonProperty(PropertyName = "query_string")]
		IQueryStringQuery IQueryDescriptor.QueryStringDescriptor { get; set; }
		
		[JsonProperty(PropertyName = "regexp")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<PropertyPathMarker, object> IQueryDescriptor.RegexpQueryDescriptor { get; set; }

		[JsonProperty(PropertyName = "flt")]
		IFuzzyLikeThisQuery IQueryDescriptor.FuzzyLikeThisDescriptor { get; set; }
		
		[JsonProperty(PropertyName = "has_child")]
		IHasChildQuery IQueryDescriptor.HasChildQueryDescriptor { get; set; }
		
		[JsonProperty(PropertyName = "has_parent")]
		IHasParentQuery IQueryDescriptor.HasParentQueryDescriptor { get; set; }
		
		[JsonProperty(PropertyName = "mlt")]
		IMoreLikeThisQuery IQueryDescriptor.MoreLikeThisDescriptor { get; set; }
		
		[JsonProperty(PropertyName = "range")]
		[JsonConverter(typeof(DictionaryKeysAreNotPropertyNamesJsonConverter))]
		IDictionary<PropertyPathMarker, object> IQueryDescriptor.RangeQueryDescriptor { get; set; }

		[JsonProperty(PropertyName = "span_term")]
		SpanTerm IQueryDescriptor.SpanTermQuery { get; set; }
		
		[JsonProperty(PropertyName = "span_first")]
		ISpanFirstQuery IQueryDescriptor.SpanFirstQueryDescriptor { get; set; }
		
		[JsonProperty(PropertyName = "span_or")]
		ISpanOrQuery IQueryDescriptor.SpanOrQueryDescriptor { get; set; }
		
		[JsonProperty(PropertyName = "span_near")]
		ISpanNearQuery IQueryDescriptor.SpanNearQuery { get; set; }
		
		[JsonProperty(PropertyName = "span_not")]
		ISpanNotQuery IQueryDescriptor.SpanNotQuery { get; set; }

		[JsonProperty(PropertyName = "top_children")]
		ITopChildrenQuery IQueryDescriptor.TopChildrenQueryDescriptor { get; set; }
		
		[JsonProperty(PropertyName = "nested")]
		INestedQuery IQueryDescriptor.NestedQueryDescriptor { get; set; }
		
		[JsonProperty(PropertyName = "indices")]
		IIndicesQuery IQueryDescriptor.IndicesQueryDescriptor { get; set; }

		[JsonProperty(PropertyName = "function_score")]
		IFunctionScoreQuery IQueryDescriptor.FunctionScoreQueryDescriptor { get; set; }
		
		bool IQueryDescriptor.IsConditionless { get; set; }

		public bool IsConditionless { get { return ((IQueryDescriptor)this).IsConditionless; } }

		bool IQueryDescriptor.IsStrict { get; set; }

		public bool IsStrict { get { return ((IQueryDescriptor)this).IsStrict; } }
		
		bool IQueryDescriptor.IsVerbatim { get; set; }

		public bool IsVerbatim { get { return ((IQueryDescriptor)this).IsVerbatim; } }

		/// <summary>
		/// AND's two BaseQueries
		/// </summary>
		/// <returns>A new basequery that represents the AND of the two</returns>
		public static BaseQuery operator &(BaseQuery leftQuery, BaseQuery rightQuery)
		{
			var defaultQuery = new BaseQuery();
			((IQueryDescriptor)defaultQuery).IsConditionless = true;
			leftQuery = leftQuery ?? defaultQuery;
			rightQuery = rightQuery ?? defaultQuery;
			var combined = new[] { leftQuery, rightQuery };

			//if any of the queries is conditionless return the first one that is not
			//or return the defaultQuery
			if (combined.Any(bf => ((IQueryDescriptor)bf).IsConditionless))
				return combined.FirstOrDefault(bf => !((IQueryDescriptor)bf).IsConditionless) ?? defaultQuery;

			//return simple combination of the two if merging is not possible/necessary
			var noMergeQuery = CombineIfNoMergeIsNecessary(leftQuery, rightQuery, combined);
			if (noMergeQuery != null)
				return noMergeQuery;

			//if the left is a strict bool try to merge right on left first 
			var joinStrictLeft = StrictSingleSideAndMerge(leftQuery, rightQuery);
			if (joinStrictLeft != null)
				return joinStrictLeft;

			// if the right side is a strict bool try to merge left on right
			var joinStrictRight = StrictSingleSideAndMerge(rightQuery, leftQuery);
			if (joinStrictRight != null)
				return joinStrictRight;

			// if the left side is a normal bool try to merge right on left
			var joinLeft = SingleSideAndMerge(leftQuery, rightQuery);
			if (joinLeft != null)
				return joinLeft;

			// if the right side is a normal bool try to merge lefft on right
			var joinRight = SingleSideAndMerge(rightQuery, leftQuery);
			return joinRight ?? defaultQuery;
		}

		public static BaseQuery operator |(BaseQuery leftQuery, BaseQuery rightQuery)
		{
			var defaultQuery = new BaseQuery();
			((IQueryDescriptor)defaultQuery).IsConditionless = true;
			leftQuery = leftQuery ?? defaultQuery;
			rightQuery = rightQuery ?? defaultQuery;
			var combined = new[] { leftQuery, rightQuery };

			if (combined.Any(bf => ((IQueryDescriptor)bf).IsConditionless))
				return combined.FirstOrDefault(bf => !((IQueryDescriptor)bf).IsConditionless) ?? defaultQuery;

			var leftBoolQuery = ((IQueryDescriptor)leftQuery).BoolQueryDescriptor;
			var rightBoolQuery = ((IQueryDescriptor)rightQuery).BoolQueryDescriptor;


			var f = new BaseQuery();
			var fq = new BoolBaseQueryDescriptor();
			fq._ShouldQueries = new[] { leftQuery, rightQuery };
			((IQueryDescriptor)f).BoolQueryDescriptor = fq;

			var mergeLeft = !((IQueryDescriptor)leftQuery).IsStrict && (leftBoolQuery == null || leftBoolQuery._MinimumNumberShouldMatches == null);
			var mergeRight = !((IQueryDescriptor)rightQuery).IsStrict && (rightBoolQuery == null || rightBoolQuery._MinimumNumberShouldMatches == null);

			//if neither the left nor the right side represent a bool query join them
			if (((IQueryDescriptor)leftQuery).BoolQueryDescriptor == null && ((IQueryDescriptor)rightQuery).BoolQueryDescriptor == null)
			{
				fq._ShouldQueries = leftQuery.MergeShouldQueries(rightQuery);
				return f;
			}
			//if the left or right sight already is a bool query join the non bool query side of the 
			//of the operation onto the other.
			if (((IQueryDescriptor)leftQuery).BoolQueryDescriptor != null 
				&& ((IQueryDescriptor)rightQuery).BoolQueryDescriptor == null
				&& mergeLeft)
			{
				JoinShouldOnSide(leftQuery, rightQuery, fq);
			}
			else if (((IQueryDescriptor)rightQuery).BoolQueryDescriptor != null 
				&& ((IQueryDescriptor)leftQuery).BoolQueryDescriptor == null
				&& mergeRight)
			{
				JoinShouldOnSide(rightQuery, leftQuery, fq);
			}
			//both sides already represent a bool query
			else
			{
				//both sides report that we may merge the shoulds
				if (mergeLeft && mergeRight
					&& leftBoolQuery.CanJoinShould()
					&& rightBoolQuery.CanJoinShould())
					fq._ShouldQueries = leftQuery.MergeShouldQueries(rightQuery);
				else
					//create a new nested bool with two separate should bool sections
					fq._ShouldQueries = new[] { leftQuery, rightQuery };
			}
			return f;
		}

		public static BaseQuery operator !(BaseQuery lbq)
		{
			if (lbq == null || ((IQueryDescriptor)lbq).IsConditionless)
			{
				var newConditionless = new BaseQuery();
				((IQueryDescriptor)newConditionless).IsConditionless = true;
				return newConditionless;
			}

			var f = new BaseQuery();
			var fq = new BoolBaseQueryDescriptor();
			fq._MustNotQueries = new[] { lbq };

			((IQueryDescriptor)f).BoolQueryDescriptor = fq;
			return f;
		}

		public static bool operator false(BaseQuery a)
		{
			return false;
		}

		public static bool operator true(BaseQuery a)
		{
			return false;
		}

		private static void JoinShouldOnSide(BaseQuery lbq, BaseQuery rbq, BoolBaseQueryDescriptor bq)
		{
			bq._ShouldQueries = lbq.MergeShouldQueries(rbq);
		}

		private static BaseQuery CombineIfNoMergeIsNecessary(
			BaseQuery leftQuery,
			BaseQuery rightQuery,
			BaseQuery[] combined)
		{
			var leftBoolQuery = ((IQueryDescriptor)leftQuery).BoolQueryDescriptor;
			var rightBoolQuery = ((IQueryDescriptor)rightQuery).BoolQueryDescriptor;
			//if neither side is already a boolquery 
			//  or if all boolqueries are strict.
			//  or if one side is strict and the other is null

			var mergeLeft = !((IQueryDescriptor)leftQuery).IsStrict && (leftBoolQuery == null || leftBoolQuery._MinimumNumberShouldMatches == null);
			var mergeRight = !((IQueryDescriptor)rightQuery).IsStrict && (rightBoolQuery == null || rightBoolQuery._MinimumNumberShouldMatches == null);

			//no merging is needed just return the combination
			if (
				(leftBoolQuery == null && rightBoolQuery == null)
				|| (!mergeLeft && !mergeRight)
				|| (((IQueryDescriptor)leftQuery).IsStrict && rightBoolQuery == null)
				|| (((IQueryDescriptor)rightQuery).IsStrict && leftBoolQuery == null)
				)
			{
				return CreateReturnQuery((returnQuery, returnBoolQuery) => returnBoolQuery._MustQueries = combined);
			}
			return null;
		}

		private static BaseQuery StrictSingleSideAndMerge(BaseQuery targetQuery, BaseQuery mergeQuery)
		{
			//if the target is not strict return
			if (!((IQueryDescriptor)targetQuery).IsStrict) return null;

			var mergeBoolQuery = ((IQueryDescriptor)mergeQuery).BoolQueryDescriptor;

			return CreateReturnQuery((returnQuery, returnBoolQuery) =>
			{
				if (mergeBoolQuery._MustNotQueries.HasAny())
				{
					returnBoolQuery._MustNotQueries = mergeBoolQuery._MustNotQueries;
					mergeBoolQuery._MustNotQueries = null;
				}

				returnBoolQuery._MustQueries = new[] { targetQuery }.Concat(mergeBoolQuery._MustQueries ?? Empty);
			});
		}

		private static BaseQuery SingleSideAndMerge(BaseQuery targetQuery, BaseQuery mergeQuery)
		{
			var targetBoolQuery = ((IQueryDescriptor)targetQuery).BoolQueryDescriptor;
			var mergeBoolQuery = ((IQueryDescriptor)mergeQuery).BoolQueryDescriptor;

			if (targetBoolQuery == null) return null;

			var combined = new[] { targetQuery, mergeQuery };
			return CreateReturnQuery((returnQuery, returnBoolQuery) =>
			{
				if (!targetBoolQuery.CanMergeMustAndMustNots() || !mergeBoolQuery.CanMergeMustAndMustNots())
				{
					returnBoolQuery._MustQueries = combined;
					return;
				}

				returnBoolQuery._MustQueries = (targetBoolQuery._MustQueries ?? Empty)
					.Concat(mergeBoolQuery != null
						? (mergeBoolQuery._MustQueries ?? Empty)
						: new[] { mergeQuery })
					.NullIfEmpty();
				returnBoolQuery._MustNotQueries = (targetBoolQuery._MustNotQueries ?? Empty)
					.Concat(mergeBoolQuery != null
						? (mergeBoolQuery._MustNotQueries ?? Empty)
						: Empty
					).NullIfEmpty();

			});
		}

		public static BaseQuery CreateReturnQuery(Action<BaseQuery, BoolBaseQueryDescriptor> modify = null)
		{
			var returnQuery = new BaseQuery();
			var returnBoolQuery = new BoolBaseQueryDescriptor() { };
			((IQueryDescriptor)returnQuery).BoolQueryDescriptor = returnBoolQuery;
			if (modify != null)
			{
				modify(returnQuery, returnBoolQuery);
			}
			return returnQuery;
		}
	}
}
