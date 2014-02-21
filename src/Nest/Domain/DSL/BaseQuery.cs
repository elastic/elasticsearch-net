using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using Elasticsearch.Net;
using System.Linq.Expressions;

namespace Nest
{
	public class BaseQuery
	{
		private static readonly IEnumerable<BaseQuery> Empty = Enumerable.Empty<BaseQuery>();

		[JsonProperty(PropertyName = "bool")]
		internal BoolBaseQueryDescriptor BoolQueryDescriptor { get; set; }

		internal bool IsConditionless { get; set; }

		internal bool IsStrict { get; set; }

		internal bool IsVerbatim { get; set; }

		/// <summary>
		/// AND's two BaseQueries
		/// </summary>
		/// <returns>A new basequery that represents the AND of the two</returns>
		public static BaseQuery operator &(BaseQuery leftQuery, BaseQuery rightQuery)
		{
			var defaultQuery = new BaseQuery() { IsConditionless = true };
			leftQuery = leftQuery ?? defaultQuery;
			rightQuery = rightQuery ?? defaultQuery;
			var combined = new[] { leftQuery, rightQuery };

			//if any of the queries is conditionless return the first one that is not
			//or return the defaultQuery
			if (combined.Any(bf => bf.IsConditionless))
				return combined.FirstOrDefault(bf => !bf.IsConditionless) ?? defaultQuery;

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
			var defaultQuery = new BaseQuery() { IsConditionless = true };
			leftQuery = leftQuery ?? defaultQuery;
			rightQuery = rightQuery ?? defaultQuery;
			var combined = new[] { leftQuery, rightQuery };

			if (combined.Any(bf => bf.IsConditionless))
				return combined.FirstOrDefault(bf => !bf.IsConditionless) ?? defaultQuery;

			var leftBoolQuery = leftQuery.BoolQueryDescriptor;
			var rightBoolQuery = rightQuery.BoolQueryDescriptor;


			var f = new BaseQuery();
			var fq = new BoolBaseQueryDescriptor();
			fq._ShouldQueries = new[] { leftQuery, rightQuery };
			f.BoolQueryDescriptor = fq;

			var mergeLeft = !leftQuery.IsStrict && (leftBoolQuery == null || leftBoolQuery._MinimumNumberShouldMatches == null);
			var mergeRight = !rightQuery.IsStrict && (rightBoolQuery == null || rightBoolQuery._MinimumNumberShouldMatches == null);

			//if neither the left nor the right side represent a bool query join them
			if (leftQuery.BoolQueryDescriptor == null && rightQuery.BoolQueryDescriptor == null)
			{
				fq._ShouldQueries = leftQuery.MergeShouldQueries(rightQuery);
				return f;
			}
			//if the left or right sight already is a bool query join the non bool query side of the 
			//of the operation onto the other.
			if (leftQuery.BoolQueryDescriptor != null 
				&& rightQuery.BoolQueryDescriptor == null
				&& mergeLeft)
			{
				JoinShouldOnSide(leftQuery, rightQuery, fq);
			}
			else if (rightQuery.BoolQueryDescriptor != null 
				&& leftQuery.BoolQueryDescriptor == null
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
			if (lbq == null || lbq.IsConditionless)
				return new BaseQuery { IsConditionless = true };

			var f = new BaseQuery();
			var fq = new BoolBaseQueryDescriptor();
			fq._MustNotQueries = new[] { lbq };

			f.BoolQueryDescriptor = fq;
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
			var leftBoolQuery = leftQuery.BoolQueryDescriptor;
			var rightBoolQuery = rightQuery.BoolQueryDescriptor;
			//if neither side is already a boolquery 
			//  or if all boolqueries are strict.
			//  or if one side is strict and the other is null

			var mergeLeft = !leftQuery.IsStrict && (leftBoolQuery == null || leftBoolQuery._MinimumNumberShouldMatches == null);
			var mergeRight = !rightQuery.IsStrict && (rightBoolQuery == null || rightBoolQuery._MinimumNumberShouldMatches == null);

			//no merging is needed just return the combination
			if (
				(leftBoolQuery == null && rightBoolQuery == null)
				|| (!mergeLeft && !mergeRight)
				|| (leftQuery.IsStrict && rightBoolQuery == null)
				|| (rightQuery.IsStrict && leftBoolQuery == null)
				)
			{
				return CreateReturnQuery((returnQuery, returnBoolQuery) => returnBoolQuery._MustQueries = combined);
			}
			return null;
		}

		private static BaseQuery StrictSingleSideAndMerge(BaseQuery targetQuery, BaseQuery mergeQuery)
		{
			//if the target is not strict return
			if (!targetQuery.IsStrict) return null;

			var mergeBoolQuery = mergeQuery.BoolQueryDescriptor;

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
			var targetBoolQuery = targetQuery.BoolQueryDescriptor;
			var mergeBoolQuery = mergeQuery.BoolQueryDescriptor;

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
			returnQuery.BoolQueryDescriptor = returnBoolQuery;
			if (modify != null)
			{
				modify(returnQuery, returnBoolQuery);
			}
			return returnQuery;
		}
	}
}
