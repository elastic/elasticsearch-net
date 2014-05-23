using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.DSL.Visitor;
using Nest.Resolvers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using Elasticsearch.Net;
using System.Linq.Expressions;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	public class QueryContainer : IQueryContainer
	{
		private static readonly IEnumerable<QueryContainer> Empty = Enumerable.Empty<QueryContainer>();

		IBoolQuery IQueryContainer.Bool { get; set; }

		string IQueryContainer.RawQuery { get; set; }

		IMatchAllQuery IQueryContainer.MatchAllQuery { get; set; }
		
		ITermQuery IQueryContainer.Term { get; set; }
		
		IWildcardQuery IQueryContainer.Wildcard { get; set; }
		
		IPrefixQuery IQueryContainer.Prefix { get; set; }

		IBoostingQuery IQueryContainer.Boosting { get; set; }
		
		IIdsQuery IQueryContainer.Ids { get; set; }
		
		ICustomScoreQuery IQueryContainer.CustomScore { get; set; }
		
		ICustomFiltersScoreQuery IQueryContainer.CustomFiltersScore { get; set; }

		ICustomBoostFactorQuery IQueryContainer.CustomBoostFactor { get; set; }
		
		IConstantScoreQuery IQueryContainer.ConstantScore { get; set; }
		
		IDisMaxQuery IQueryContainer.DisMax { get; set; }
		
		IFilteredQuery IQueryContainer.Filtered { get; set; }

		IMultiMatchQuery IQueryContainer.MultiMatch { get; set; }
		
		IMatchQuery IQueryContainer.Match { get; set; }
		
		IFuzzyQuery IQueryContainer.Fuzzy { get; set; }
		
		IGeoShapeQuery IQueryContainer.GeoShape { get; set; }
		
		ICommonTermsQuery IQueryContainer.CommonTerms { get; set; }
		
		ITermsQuery IQueryContainer.Terms { get; set; }
		
		IQueryStringQuery IQueryContainer.QueryString { get; set; }
		
		ISimpleQueryStringQuery IQueryContainer.SimpleQueryString { get; set; }
		
		IRegexpQuery IQueryContainer.Regexp { get; set; }

		IFuzzyLikeThisQuery IQueryContainer.FuzzyLikeThis { get; set; }
		
		IHasChildQuery IQueryContainer.HasChild { get; set; }
		
		IHasParentQuery IQueryContainer.HasParent { get; set; }
		
		IMoreLikeThisQuery IQueryContainer.MoreLikeThis { get; set; }
		
		IRangeQuery IQueryContainer.Range { get; set; }

		ISpanTermQuery IQueryContainer.SpanTerm { get; set; }
		
		ISpanFirstQuery IQueryContainer.SpanFirst { get; set; }
		
		ISpanOrQuery IQueryContainer.SpanOr { get; set; }
		
		ISpanNotQuery IQueryContainer.SpanNot { get; set; }
		
		ISpanNearQuery IQueryContainer.SpanNear { get; set; }

		ITopChildrenQuery IQueryContainer.TopChildren { get; set; }
		
		INestedQuery IQueryContainer.Nested { get; set; }
		
		IIndicesQuery IQueryContainer.Indices { get; set; }

		IFunctionScoreQuery IQueryContainer.FunctionScore { get; set; }
		
		public void Accept(IQueryVisitor visitor)
		{
			var walker = new QueryFilterWalker();
			if (visitor.Scope == VisitorScope.Unknown) visitor.Scope = VisitorScope.Query;
			walker.Walk(this, visitor);
		}

		bool IQueryContainer.IsConditionless { get; set; }

		public bool IsConditionless { get { return ((IQueryContainer)this).IsConditionless; } }

		bool IQueryContainer.IsStrict { get; set; }

		public bool IsStrict { get { return ((IQueryContainer)this).IsStrict; } }
		
		bool IQueryContainer.IsVerbatim { get; set; }

		public bool IsVerbatim { get { return ((IQueryContainer)this).IsVerbatim; } }

		/// <summary>
		/// AND's two BaseQueries
		/// </summary>
		/// <returns>A new basequery that represents the AND of the two</returns>
		public static QueryContainer operator &(QueryContainer leftQuery, QueryContainer rightQuery)
		{
			var defaultQuery = new QueryContainer();
			((IQueryContainer)defaultQuery).IsConditionless = true;
			leftQuery = leftQuery ?? defaultQuery;
			rightQuery = rightQuery ?? defaultQuery;
			var combined = new[] { leftQuery, rightQuery };

			//if any of the queries is conditionless return the first one that is not
			//or return the defaultQuery
			if (combined.Any(bf => ((IQueryContainer)bf).IsConditionless))
				return combined.FirstOrDefault(bf => !((IQueryContainer)bf).IsConditionless) ?? defaultQuery;

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

		public static QueryContainer operator |(QueryContainer leftQuery, QueryContainer rightQuery)
		{
			var defaultQuery = new QueryContainer();
			((IQueryContainer)defaultQuery).IsConditionless = true;
			leftQuery = leftQuery ?? defaultQuery;
			rightQuery = rightQuery ?? defaultQuery;
			var combined = new[] { leftQuery, rightQuery };

			if (combined.Any(bf => ((IQueryContainer)bf).IsConditionless))
				return combined.FirstOrDefault(bf => !((IQueryContainer)bf).IsConditionless) ?? defaultQuery;

			var leftBoolQuery = ((IQueryContainer)leftQuery).Bool;
			var rightBoolQuery = ((IQueryContainer)rightQuery).Bool;


			var f = new QueryContainer();
			var fq = new BoolBaseQueryDescriptor();
			((IBoolQuery)fq).Should = new[] { leftQuery, rightQuery };
			((IQueryContainer)f).Bool = fq;

			var mergeLeft = !((IQueryContainer)leftQuery).IsStrict && (leftBoolQuery == null || ((IBoolQuery)leftBoolQuery).MinimumShouldMatch == null);
			var mergeRight = !((IQueryContainer)rightQuery).IsStrict && (rightBoolQuery == null || ((IBoolQuery)rightBoolQuery).MinimumShouldMatch == null);

			//if neither the left nor the right side represent a bool query join them
			if (((IQueryContainer)leftQuery).Bool == null && ((IQueryContainer)rightQuery).Bool == null)
			{
				((IBoolQuery)fq).Should = leftQuery.MergeShouldQueries(rightQuery);
				return f;
			}
			//if the left or right sight already is a bool query join the non bool query side of the 
			//of the operation onto the other.
			if (((IQueryContainer)leftQuery).Bool != null 
				&& ((IQueryContainer)rightQuery).Bool == null
				&& mergeLeft)
			{
				JoinShouldOnSide(leftQuery, rightQuery, fq);
			}
			else if (((IQueryContainer)rightQuery).Bool != null 
				&& ((IQueryContainer)leftQuery).Bool == null
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

		public static QueryContainer operator !(QueryContainer lbq)
		{
			if (lbq == null || ((IQueryContainer)lbq).IsConditionless)
			{
				var newConditionless = new QueryContainer();
				((IQueryContainer)newConditionless).IsConditionless = true;
				return newConditionless;
			}

			var f = new QueryContainer();
			var fq = new BoolBaseQueryDescriptor();
			((IBoolQuery)fq).MustNot = new[] { lbq };

			((IQueryContainer)f).Bool = fq;
			return f;
		}

		public static bool operator false(QueryContainer a)
		{
			return false;
		}

		public static bool operator true(QueryContainer a)
		{
			return false;
		}

		private static void JoinShouldOnSide(QueryContainer lbq, QueryContainer rbq, BoolBaseQueryDescriptor bq)
		{
			((IBoolQuery)bq).Should = lbq.MergeShouldQueries(rbq);
		}

		private static QueryContainer CombineIfNoMergeIsNecessary(
			QueryContainer leftQuery,
			QueryContainer rightQuery,
			QueryContainer[] combined)
		{
			var leftBoolQuery = ((IQueryContainer)leftQuery).Bool;
			var rightBoolQuery = ((IQueryContainer)rightQuery).Bool;
			//if neither side is already a boolquery 
			//  or if all boolqueries are strict.
			//  or if one side is strict and the other is null

			var mergeLeft = !((IQueryContainer)leftQuery).IsStrict && (leftBoolQuery == null || ((IBoolQuery)leftBoolQuery).MinimumShouldMatch == null);
			var mergeRight = !((IQueryContainer)rightQuery).IsStrict && (rightBoolQuery == null || ((IBoolQuery)rightBoolQuery).MinimumShouldMatch == null);

			//no merging is needed just return the combination
			if (
				(leftBoolQuery == null && rightBoolQuery == null)
				|| (!mergeLeft && !mergeRight)
				|| (((IQueryContainer)leftQuery).IsStrict && rightBoolQuery == null)
				|| (((IQueryContainer)rightQuery).IsStrict && leftBoolQuery == null)
				)
			{
				return CreateReturnQuery((returnQuery, returnBoolQuery) => ((IBoolQuery)returnBoolQuery).Must = combined);
			}
			return null;
		}

		private static QueryContainer StrictSingleSideAndMerge(QueryContainer targetQuery, QueryContainer mergeQuery)
		{
			//if the target is not strict return
			if (!((IQueryContainer)targetQuery).IsStrict) return null;

			var mergeBoolQuery = ((IQueryContainer)mergeQuery).Bool;

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

		private static QueryContainer SingleSideAndMerge(QueryContainer targetQuery, QueryContainer mergeQuery)
		{
			var targetBoolQuery = ((IQueryContainer)targetQuery).Bool;
			var mergeBoolQuery = ((IQueryContainer)mergeQuery).Bool;

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

		public static QueryContainer CreateReturnQuery(Action<QueryContainer, BoolBaseQueryDescriptor> modify = null)
		{
			var returnQuery = new QueryContainer();
			var returnBoolQuery = new BoolBaseQueryDescriptor() { };
			((IQueryContainer)returnQuery).Bool = returnBoolQuery;
			if (modify != null)
			{
				modify(returnQuery, returnBoolQuery);
			}
			return returnQuery;
		}

		public object GetCustomJson()
		{	
			var f = ((IQueryContainer)this);
			if (f.RawQuery.IsNullOrEmpty()) return f; 
			return new RawJson(f.RawQuery);
		}
	}
}
