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
	public class BaseQuery : IQueryDescriptor, ICustomJson
	{
		private static readonly IEnumerable<BaseQuery> Empty = Enumerable.Empty<BaseQuery>();

		IBoolQuery IQueryDescriptor.Bool { get; set; }

		string IQueryDescriptor.RawQuery { get; set; }

		MatchAll IQueryDescriptor.MatchAll { get; set; }
		
		ITermQuery IQueryDescriptor.Term { get; set; }
		
		IWildcardQuery IQueryDescriptor.Wildcard { get; set; }
		
		IPrefixQuery IQueryDescriptor.Prefix { get; set; }

		IBoostingQuery IQueryDescriptor.Boosting { get; set; }
		
		IdsQuery IQueryDescriptor.Ids { get; set; }
		
		ICustomScoreQuery IQueryDescriptor.CustomScore { get; set; }
		
		ICustomFiltersScoreQuery IQueryDescriptor.CustomFiltersScore { get; set; }

		ICustomBoostFactorQuery IQueryDescriptor.CustomBoostFactor { get; set; }
		
		IConstantScoreQuery IQueryDescriptor.ConstantScore { get; set; }
		
		IDisMaxQuery IQueryDescriptor.DisMax { get; set; }
		
		IFilteredQuery IQueryDescriptor.Filtered { get; set; }

		IMultiMatchQuery IQueryDescriptor.MultiMatch { get; set; }
		
		IMatchQuery IQueryDescriptor.Match { get; set; }
		
		IFuzzyQuery IQueryDescriptor.Fuzzy { get; set; }
		
		IGeoShapeQuery IQueryDescriptor.GeoShape { get; set; }
		
		ICommonTermsQuery IQueryDescriptor.CommonTerms { get; set; }
		
		ITermsQuery IQueryDescriptor.Terms { get; set; }
		
		IQueryStringQuery IQueryDescriptor.QueryString { get; set; }
		
		ISimpleQueryStringQuery IQueryDescriptor.SimpleQueryString { get; set; }
		
		IRegexpQuery IQueryDescriptor.Regexp { get; set; }

		IFuzzyLikeThisQuery IQueryDescriptor.FuzzyLikeThis { get; set; }
		
		IHasChildQuery IQueryDescriptor.HasChild { get; set; }
		
		IHasParentQuery IQueryDescriptor.HasParent { get; set; }
		
		IMoreLikeThisQuery IQueryDescriptor.MoreLikeThis { get; set; }
		
		IRangeQuery IQueryDescriptor.Range { get; set; }

		ITermQuery IQueryDescriptor.SpanTerm { get; set; }
		
		ISpanFirstQuery IQueryDescriptor.SpanFirst { get; set; }
		
		ISpanOrQuery IQueryDescriptor.SpanOr { get; set; }
		
		ISpanNotQuery IQueryDescriptor.SpanNot { get; set; }
		
		ISpanNearQuery IQueryDescriptor.SpanNear { get; set; }

		ITopChildrenQuery IQueryDescriptor.TopChildren { get; set; }
		
		INestedQuery IQueryDescriptor.Nested { get; set; }
		
		IIndicesQuery IQueryDescriptor.Indices { get; set; }

		IFunctionScoreQuery IQueryDescriptor.FunctionScore { get; set; }
		
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

			var leftBoolQuery = ((IQueryDescriptor)leftQuery).Bool;
			var rightBoolQuery = ((IQueryDescriptor)rightQuery).Bool;


			var f = new BaseQuery();
			var fq = new BoolBaseQueryDescriptor();
			((IBoolQuery)fq).Should = new[] { leftQuery, rightQuery };
			((IQueryDescriptor)f).Bool = fq;

			var mergeLeft = !((IQueryDescriptor)leftQuery).IsStrict && (leftBoolQuery == null || ((IBoolQuery)leftBoolQuery).MinimumNumberShouldMatches == null);
			var mergeRight = !((IQueryDescriptor)rightQuery).IsStrict && (rightBoolQuery == null || ((IBoolQuery)rightBoolQuery).MinimumNumberShouldMatches == null);

			//if neither the left nor the right side represent a bool query join them
			if (((IQueryDescriptor)leftQuery).Bool == null && ((IQueryDescriptor)rightQuery).Bool == null)
			{
				((IBoolQuery)fq).Should = leftQuery.MergeShouldQueries(rightQuery);
				return f;
			}
			//if the left or right sight already is a bool query join the non bool query side of the 
			//of the operation onto the other.
			if (((IQueryDescriptor)leftQuery).Bool != null 
				&& ((IQueryDescriptor)rightQuery).Bool == null
				&& mergeLeft)
			{
				JoinShouldOnSide(leftQuery, rightQuery, fq);
			}
			else if (((IQueryDescriptor)rightQuery).Bool != null 
				&& ((IQueryDescriptor)leftQuery).Bool == null
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
					((IBoolQuery)fq).Should = leftQuery.MergeShouldQueries(rightQuery);
				else
					//create a new nested bool with two separate should bool sections
					((IBoolQuery)fq).Should = new[] { leftQuery, rightQuery };
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
			((IBoolQuery)fq).MustNot = new[] { lbq };

			((IQueryDescriptor)f).Bool = fq;
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
			((IBoolQuery)bq).Should = lbq.MergeShouldQueries(rbq);
		}

		private static BaseQuery CombineIfNoMergeIsNecessary(
			BaseQuery leftQuery,
			BaseQuery rightQuery,
			BaseQuery[] combined)
		{
			var leftBoolQuery = ((IQueryDescriptor)leftQuery).Bool;
			var rightBoolQuery = ((IQueryDescriptor)rightQuery).Bool;
			//if neither side is already a boolquery 
			//  or if all boolqueries are strict.
			//  or if one side is strict and the other is null

			var mergeLeft = !((IQueryDescriptor)leftQuery).IsStrict && (leftBoolQuery == null || ((IBoolQuery)leftBoolQuery).MinimumNumberShouldMatches == null);
			var mergeRight = !((IQueryDescriptor)rightQuery).IsStrict && (rightBoolQuery == null || ((IBoolQuery)rightBoolQuery).MinimumNumberShouldMatches == null);

			//no merging is needed just return the combination
			if (
				(leftBoolQuery == null && rightBoolQuery == null)
				|| (!mergeLeft && !mergeRight)
				|| (((IQueryDescriptor)leftQuery).IsStrict && rightBoolQuery == null)
				|| (((IQueryDescriptor)rightQuery).IsStrict && leftBoolQuery == null)
				)
			{
				return CreateReturnQuery((returnQuery, returnBoolQuery) => ((IBoolQuery)returnBoolQuery).Must = combined);
			}
			return null;
		}

		private static BaseQuery StrictSingleSideAndMerge(BaseQuery targetQuery, BaseQuery mergeQuery)
		{
			//if the target is not strict return
			if (!((IQueryDescriptor)targetQuery).IsStrict) return null;

			var mergeBoolQuery = ((IQueryDescriptor)mergeQuery).Bool;

			return CreateReturnQuery((returnQuery, returnBoolQuery) =>
			{
				if (((IBoolQuery)mergeBoolQuery).MustNot.HasAny())
				{
					((IBoolQuery)returnBoolQuery).MustNot = ((IBoolQuery)mergeBoolQuery).MustNot;
					((IBoolQuery)mergeBoolQuery).MustNot = null;
				}

				((IBoolQuery)returnBoolQuery).Must = new[] { targetQuery }.Concat(((IBoolQuery)mergeBoolQuery).Must ?? Empty);
			});
		}

		private static BaseQuery SingleSideAndMerge(BaseQuery targetQuery, BaseQuery mergeQuery)
		{
			var targetBoolQuery = ((IQueryDescriptor)targetQuery).Bool;
			var mergeBoolQuery = ((IQueryDescriptor)mergeQuery).Bool;

			if (targetBoolQuery == null) return null;

			var combined = new[] { targetQuery, mergeQuery };
			return CreateReturnQuery((returnQuery, returnBoolQuery) =>
			{
				if (!targetBoolQuery.CanMergeMustAndMustNots() || !mergeBoolQuery.CanMergeMustAndMustNots())
				{
					((IBoolQuery)returnBoolQuery).Must = combined;
					return;
				}

				((IBoolQuery)returnBoolQuery).Must = (((IBoolQuery)targetBoolQuery).Must ?? Empty)
					.Concat(mergeBoolQuery != null
						? (((IBoolQuery)mergeBoolQuery).Must ?? Empty)
						: new[] { mergeQuery })
					.NullIfEmpty();
				((IBoolQuery)returnBoolQuery).MustNot = (((IBoolQuery)targetBoolQuery).MustNot ?? Empty)
					.Concat(mergeBoolQuery != null
						? (((IBoolQuery)mergeBoolQuery).MustNot ?? Empty)
						: Empty
					).NullIfEmpty();

			});
		}

		public static BaseQuery CreateReturnQuery(Action<BaseQuery, BoolBaseQueryDescriptor> modify = null)
		{
			var returnQuery = new BaseQuery();
			var returnBoolQuery = new BoolBaseQueryDescriptor() { };
			((IQueryDescriptor)returnQuery).Bool = returnBoolQuery;
			if (modify != null)
			{
				modify(returnQuery, returnBoolQuery);
			}
			return returnQuery;
		}

		public object GetCustomJson()
		{	
			var f = ((IQueryDescriptor)this);
			if (f.RawQuery.IsNullOrEmpty()) return f; 
			return new RawJson(f.RawQuery);
		}
	}
}
