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

	public class BaseFilterDescriptor : IFilterDescriptor
	{
		public string _Name { get; set; }
		
		public string _CacheKey { get; set; }
		
		public bool? _Cache { get; set; }
		
		[JsonIgnore]
		public virtual bool IsConditionless { get; internal set; }
		
		BoolBaseFilterDescriptor IFilterDescriptor.BoolFilterDescriptor { get; set; }
		
		IExistsFilter IFilterDescriptor.ExistsFilter { get; set; }

		IMissingFilter IFilterDescriptor.MissingFilter { get; set; }

		IIdsFilter IFilterDescriptor.IdsFilter { get; set; }

		IGeoBoundingBoxFilter IFilterDescriptor.GeoBoundingBoxFilter { get; set; }

		IGeoDistanceFilter IFilterDescriptor.GeoDistanceFilter { get; set; }

		IGeoDistanceRangeFilter IFilterDescriptor.GeoDistanceRangeFilter { get; set; }

		IGeoPolygonFilter IFilterDescriptor.GeoPolygonFilter { get; set; }

		IGeoShapeBaseFilter IFilterDescriptor.GeoShapeFilter { get; set; }

		ILimitFilter IFilterDescriptor.LimitFilter { get; set; }

		ITypeFilter IFilterDescriptor.TypeFilter { get; set; }

		IMatchAllFilter IFilterDescriptor.MatchAllFilter { get; set; }

		IHasChildFilter IFilterDescriptor.HasChildFilter { get; set; }

		IHasParentFilter IFilterDescriptor.HasParentFilter { get; set; }

		INumericRangeFilter IFilterDescriptor.NumericRangeFilter { get; set; }

		IRangeFilter IFilterDescriptor.RangeFilter { get; set; }

		IPrefixFilter IFilterDescriptor.PrefixFilter { get; set; }

		ITermFilter IFilterDescriptor.TermFilter { get; set; }

		ITermsBaseFilter IFilterDescriptor.TermsFilter { get; set; }

		IQueryFilter IFilterDescriptor.QueryFilter { get; set; }

		IAndFilter IFilterDescriptor.AndFilter { get; set; }

		IOrFilter IFilterDescriptor.OrFilter { get; set; }

		INotFilter IFilterDescriptor.NotFilter{ get; set; }

		IScriptFilter IFilterDescriptor.ScriptFilter { get; set; }

		INestedFilterDescriptor IFilterDescriptor.NestedFilter { get; set; }

		IRegexpFilter IFilterDescriptor.RegexpFilter { get; set; }
		private static readonly IEnumerable<BaseFilterDescriptor> Empty = Enumerable.Empty<BaseFilterDescriptor>();

		
		[JsonIgnore]
		public bool IsStrict { get; internal set; }
		[JsonIgnore]
		public bool IsVerbatim { get; internal set; }
		
		/// <summary>
		/// AND's two BaseFilters
		/// </summary>
		/// <returns>A new basefilter that represents the AND of the two</returns>
		public static BaseFilterDescriptor operator &(BaseFilterDescriptor leftFilterDescriptor, BaseFilterDescriptor rightFilterDescriptor)
		{
			var defaultFilter = new BaseFilterDescriptor() { IsConditionless = true };
			leftFilterDescriptor = leftFilterDescriptor ?? defaultFilter;
			rightFilterDescriptor = rightFilterDescriptor ?? defaultFilter;
			var combined = new[] { leftFilterDescriptor, rightFilterDescriptor };

			//if any of the queries is conditionless return the first one that is not
			//or return the defaultQuery
			if (combined.Any(bf => bf.IsConditionless))
				return combined.FirstOrDefault(bf=>!bf.IsConditionless) ?? defaultFilter;

			//return simple combination of the two if merging is not possible/necessary
			var noMergeFilter = CombineIfNoMergeIsNecessary(leftFilterDescriptor, rightFilterDescriptor, combined);
			if (noMergeFilter != null)
				return noMergeFilter;

			//if the left is a strict bool try to merge right on left first 
			var joinStrictLeft = StrictSingleSideAndMerge(leftFilterDescriptor, rightFilterDescriptor);
			if (joinStrictLeft != null)
				return joinStrictLeft;

			// if the right side is a strict bool try to merge left on right
			var joinStrictRight = StrictSingleSideAndMerge(rightFilterDescriptor, leftFilterDescriptor);
			if (joinStrictRight != null)
				return joinStrictRight;

			// if the left side is a normal bool try to merge right on left
			var joinLeft = SingleSideAndMerge(leftFilterDescriptor, rightFilterDescriptor);
			if (joinLeft != null)
				return joinLeft;

			// if the right side is a normal bool try to merge lefft on right
			var joinRight = SingleSideAndMerge(rightFilterDescriptor, leftFilterDescriptor);
			return joinRight ?? defaultFilter;
		}

		public static BaseFilterDescriptor operator |(BaseFilterDescriptor leftFilterDescriptor, BaseFilterDescriptor rightFilterDescriptor)
		{
			var defaultFilter = new BaseFilterDescriptor() { IsConditionless = true };
			leftFilterDescriptor = leftFilterDescriptor ?? defaultFilter;
			rightFilterDescriptor = rightFilterDescriptor ?? defaultFilter;
			var combined = new[] { leftFilterDescriptor, rightFilterDescriptor };

			if (combined.Any(bf => bf.IsConditionless))
				return combined.FirstOrDefault(bf => !bf.IsConditionless) ?? defaultFilter;

			var leftBoolFilter = ((IFilterDescriptor)leftFilterDescriptor).BoolFilterDescriptor;
			var rightBoolFilter = ((IFilterDescriptor)rightFilterDescriptor).BoolFilterDescriptor;
		

			var f = new BaseFilterDescriptor();
			var fq = new BoolBaseFilterDescriptor();
			fq._ShouldFilters = new[] { leftFilterDescriptor, rightFilterDescriptor };
			((IFilterDescriptor)f).BoolFilterDescriptor = fq;

			//if neither the left nor the right side represent a bool filter join them
			if (((IFilterDescriptor)leftFilterDescriptor).BoolFilterDescriptor == null && ((IFilterDescriptor)rightFilterDescriptor).BoolFilterDescriptor == null)
			{
				fq._ShouldFilters = leftFilterDescriptor.MergeShouldFilters(rightFilterDescriptor);
				return f;
			}
			//if the left or right sight already is a bool filter determine join the non bool query side of the 
			//of the operation onto the other.
			if (((IFilterDescriptor)leftFilterDescriptor).BoolFilterDescriptor != null && ((IFilterDescriptor)rightFilterDescriptor).BoolFilterDescriptor == null && !leftFilterDescriptor.IsStrict)
			{
				JoinShouldOnSide(leftFilterDescriptor, rightFilterDescriptor, fq);
			}
			else if (((IFilterDescriptor)rightFilterDescriptor).BoolFilterDescriptor != null && ((IFilterDescriptor)leftFilterDescriptor).BoolFilterDescriptor == null && !rightFilterDescriptor.IsStrict)
			{
				JoinShouldOnSide(rightFilterDescriptor, leftFilterDescriptor, fq);
			}
			//both sides already represent a bool filter
			else
			{
				//both sides report that we may merge the shoulds
				if (!leftFilterDescriptor.IsStrict && !rightFilterDescriptor.IsStrict
					&& leftBoolFilter.CanJoinShould()
					&& rightBoolFilter.CanJoinShould())
					fq._ShouldFilters = leftFilterDescriptor.MergeShouldFilters(rightFilterDescriptor);
				else
					//create a new nested bool with two separate should bool sections
					fq._ShouldFilters = new[] { leftFilterDescriptor, rightFilterDescriptor };
			}
			return f;
		}

		public static BaseFilterDescriptor operator !(BaseFilterDescriptor lbq)
		{
			if (lbq == null || lbq.IsConditionless)
				return new BaseFilterDescriptor { IsConditionless = true };

			var f = new BaseFilterDescriptor();
			var fq = new BoolBaseFilterDescriptor();
			fq._MustNotFilters = new[] { lbq };

			((IFilterDescriptor)f).BoolFilterDescriptor = fq;
			return f;
		}

		public static bool operator false(BaseFilterDescriptor a)
		{
			return false;
		}

		public static bool operator true(BaseFilterDescriptor a)
		{
			return false;
		}

		private static void JoinShouldOnSide(BaseFilterDescriptor lbq, BaseFilterDescriptor rbq, BoolBaseFilterDescriptor bq)
		{
			bq._ShouldFilters = lbq.MergeShouldFilters(rbq);
		}

		private static BaseFilterDescriptor CombineIfNoMergeIsNecessary(
			BaseFilterDescriptor leftFilterDescriptor, 
			BaseFilterDescriptor rightFilterDescriptor, 
			BaseFilterDescriptor[] combined)
		{
			var leftBoolFilter = ((IFilterDescriptor)leftFilterDescriptor).BoolFilterDescriptor;
			var rightBoolFilter = ((IFilterDescriptor)rightFilterDescriptor).BoolFilterDescriptor;
			//if neither side is already a boolfilter 
			//  or if all boolfilters are strict.
			//  or if one side is strict and the other is null
			//no merging is needed just return the combination
			if (
				(leftBoolFilter == null && rightBoolFilter == null)
				|| (leftFilterDescriptor.IsStrict && rightFilterDescriptor.IsStrict)
				|| (leftFilterDescriptor.IsStrict && rightBoolFilter == null)
				|| (rightFilterDescriptor.IsStrict && leftBoolFilter == null))
			{
				return CreateReturnFilter((returnFilter, returnBoolFilter) => returnBoolFilter._MustFilters = combined);
			}
			return null;
		}

		private static BaseFilterDescriptor StrictSingleSideAndMerge(BaseFilterDescriptor targetFilterDescriptor, BaseFilterDescriptor mergeFilterDescriptor)
		{
			//if the target is not strict return
			if (!targetFilterDescriptor.IsStrict) return null;

			var mergeBoolFilter = ((IFilterDescriptor)mergeFilterDescriptor).BoolFilterDescriptor;

			return CreateReturnFilter((returnFilter, returnBoolFilter) =>
			{
				if (mergeBoolFilter._MustNotFilters.HasAny())
				{
					returnBoolFilter._MustNotFilters = mergeBoolFilter._MustNotFilters;
					mergeBoolFilter._MustNotFilters = null;
				}

				returnBoolFilter._MustFilters = new[] { targetFilterDescriptor }.Concat(mergeBoolFilter._MustFilters ?? Empty);
			});
		}

		private static BaseFilterDescriptor SingleSideAndMerge(BaseFilterDescriptor targetFilterDescriptor, BaseFilterDescriptor mergeFilterDescriptor)
		{
			var targetBoolFilter = ((IFilterDescriptor)targetFilterDescriptor).BoolFilterDescriptor;
			var mergeBoolFilter = ((IFilterDescriptor)mergeFilterDescriptor).BoolFilterDescriptor;

			if (targetBoolFilter == null) return null;

			var combined = new[] { targetFilterDescriptor, mergeFilterDescriptor };
			return CreateReturnFilter((returnFilter, returnBoolFilter) =>
			{
				if (!targetBoolFilter.CanMergeMustAndMustNots() || !mergeBoolFilter.CanMergeMustAndMustNots())
				{
					returnBoolFilter._MustFilters = combined;
					return;
				}

				returnBoolFilter._MustFilters = (targetBoolFilter._MustFilters ?? Empty)
					.Concat(mergeBoolFilter != null
						? (mergeBoolFilter._MustFilters ?? Empty)
						: new[] {mergeFilterDescriptor})
					.NullIfEmpty();
				returnBoolFilter._MustNotFilters = (targetBoolFilter._MustNotFilters ?? Empty)
					.Concat(mergeBoolFilter != null
						? (mergeBoolFilter._MustNotFilters ?? Empty)
						: Empty
					).NullIfEmpty();
					
			});
		}

		public static BaseFilterDescriptor CreateReturnFilter(Action<BaseFilterDescriptor, BoolBaseFilterDescriptor> modify = null)
		{
			var returnFilter = new BaseFilterDescriptor();
			var returnBoolFilter = new BoolBaseFilterDescriptor() { };
			((IFilterDescriptor)returnFilter).BoolFilterDescriptor = returnBoolFilter;
			if (modify != null)
			{
				modify(returnFilter, returnBoolFilter);
			}
			return returnFilter;
		}

	}
}
