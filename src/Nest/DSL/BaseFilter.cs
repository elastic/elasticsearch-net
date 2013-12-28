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
	public class BaseFilter
	{
		[JsonProperty(PropertyName = "bool")]
		internal BoolBaseFilterDescriptor BoolFilterDescriptor { get; set; }

		private static readonly IEnumerable<BaseFilter> Empty = Enumerable.Empty<BaseFilter>();

		internal virtual bool IsConditionless { get; set; }
		internal bool IsStrict { get; set; }
		internal bool IsVerbatim { get; set; }
		
		/// <summary>
		/// AND's two BaseFilters
		/// </summary>
		/// <returns>A new basefilter that represents the AND of the two</returns>
		public static BaseFilter operator &(BaseFilter leftFilter, BaseFilter rightFilter)
		{
			var defaultFilter = new BaseFilter() { IsConditionless = true };
			leftFilter = leftFilter ?? defaultFilter;
			rightFilter = rightFilter ?? defaultFilter;
			var combined = new[] { leftFilter, rightFilter };

			//if any of the queries is conditionless return the first one that is not
			//or return the defaultQuery
			if (combined.Any(bf => bf.IsConditionless))
				return combined.FirstOrDefault(bf=>!bf.IsConditionless) ?? defaultFilter;

			//return simple combination of the two if merging is not possible/necessary
			var noMergeFilter = CombineIfNoMergeIsNecessary(leftFilter, rightFilter, combined);
			if (noMergeFilter != null)
				return noMergeFilter;

			//if the left is a strict bool try to merge right on left first 
			var joinStrictLeft = StrictSingleSideAndMerge(leftFilter, rightFilter);
			if (joinStrictLeft != null)
				return joinStrictLeft;

			// if the right side is a strict bool try to merge left on right
			var joinStrictRight = StrictSingleSideAndMerge(rightFilter, leftFilter);
			if (joinStrictRight != null)
				return joinStrictRight;

			// if the left side is a normal bool try to merge right on left
			var joinLeft = SingleSideAndMerge(leftFilter, rightFilter);
			if (joinLeft != null)
				return joinLeft;

			// if the right side is a normal bool try to merge lefft on right
			var joinRight = SingleSideAndMerge(rightFilter, leftFilter);
			return joinRight ?? defaultFilter;
		}

		public static BaseFilter operator |(BaseFilter leftFilter, BaseFilter rightFilter)
		{
			var defaultFilter = new BaseFilter() { IsConditionless = true };
			leftFilter = leftFilter ?? defaultFilter;
			rightFilter = rightFilter ?? defaultFilter;
			var combined = new[] { leftFilter, rightFilter };

			if (combined.Any(bf => bf.IsConditionless))
				return combined.FirstOrDefault(bf => !bf.IsConditionless) ?? defaultFilter;

			var leftBoolFilter = leftFilter.BoolFilterDescriptor;
			var rightBoolFilter = rightFilter.BoolFilterDescriptor;
		

			var f = new BaseFilter();
			var fq = new BoolBaseFilterDescriptor();
			fq._ShouldFilters = new[] { leftFilter, rightFilter };
			f.BoolFilterDescriptor = fq;

			//if neither the left nor the right side represent a bool filter join them
			if (leftFilter.BoolFilterDescriptor == null && rightFilter.BoolFilterDescriptor == null)
			{
				fq._ShouldFilters = leftFilter.MergeShouldFilters(rightFilter);
				return f;
			}
			//if the left or right sight already is a bool filter determine join the non bool query side of the 
			//of the operation onto the other.
			if (leftFilter.BoolFilterDescriptor != null && rightFilter.BoolFilterDescriptor == null && !leftFilter.IsStrict)
			{
				JoinShouldOnSide(leftFilter, rightFilter, fq);
			}
			else if (rightFilter.BoolFilterDescriptor != null && leftFilter.BoolFilterDescriptor == null && !rightFilter.IsStrict)
			{
				JoinShouldOnSide(rightFilter, leftFilter, fq);
			}
			//both sides already represent a bool filter
			else
			{
				//both sides report that we may merge the shoulds
				if (!leftFilter.IsStrict && !rightFilter.IsStrict
					&& leftBoolFilter.CanJoinShould()
					&& rightBoolFilter.CanJoinShould())
					fq._ShouldFilters = leftFilter.MergeShouldFilters(rightFilter);
				else
					//create a new nested bool with two separate should bool sections
					fq._ShouldFilters = new[] { leftFilter, rightFilter };
			}
			return f;
		}

		public static BaseFilter operator !(BaseFilter lbq)
		{
			if (lbq == null || lbq.IsConditionless)
				return new BaseFilter { IsConditionless = true };

			var f = new BaseFilter();
			var fq = new BoolBaseFilterDescriptor();
			fq._MustNotFilters = new[] { lbq };

			f.BoolFilterDescriptor = fq;
			return f;
		}

		public static bool operator false(BaseFilter a)
		{
			return false;
		}

		public static bool operator true(BaseFilter a)
		{
			return false;
		}

		private static void JoinShouldOnSide(BaseFilter lbq, BaseFilter rbq, BoolBaseFilterDescriptor bq)
		{
			bq._ShouldFilters = lbq.MergeShouldFilters(rbq);
		}

		private static BaseFilter CombineIfNoMergeIsNecessary(
			BaseFilter leftFilter, 
			BaseFilter rightFilter, 
			BaseFilter[] combined)
		{
			var leftBoolFilter = leftFilter.BoolFilterDescriptor;
			var rightBoolFilter = rightFilter.BoolFilterDescriptor;
			//if neither side is already a boolfilter 
			//  or if all boolfilters are strict.
			//  or if one side is strict and the other is null
			//no merging is needed just return the combination
			if (
				(leftBoolFilter == null && rightBoolFilter == null)
				|| (leftFilter.IsStrict && rightFilter.IsStrict)
				|| (leftFilter.IsStrict && rightBoolFilter == null)
				|| (rightFilter.IsStrict && leftBoolFilter == null))
			{
				return CreateReturnFilter((returnFilter, returnBoolFilter) => returnBoolFilter._MustFilters = combined);
			}
			return null;
		}

		private static BaseFilter StrictSingleSideAndMerge(BaseFilter targetFilter, BaseFilter mergeFilter)
		{
			//if the target is not strict return
			if (!targetFilter.IsStrict) return null;

			var mergeBoolFilter = mergeFilter.BoolFilterDescriptor;

			return CreateReturnFilter((returnFilter, returnBoolFilter) =>
			{
				if (mergeBoolFilter._MustNotFilters.HasAny())
				{
					returnBoolFilter._MustNotFilters = mergeBoolFilter._MustNotFilters;
					mergeBoolFilter._MustNotFilters = null;
				}

				returnBoolFilter._MustFilters = new[] { targetFilter }.Concat(mergeBoolFilter._MustFilters ?? Empty);
			});
		}

		private static BaseFilter SingleSideAndMerge(BaseFilter targetFilter, BaseFilter mergeFilter)
		{
			var targetBoolFilter = targetFilter.BoolFilterDescriptor;
			var mergeBoolFilter = mergeFilter.BoolFilterDescriptor;

			if (targetBoolFilter == null) return null;

			var combined = new[] { targetFilter, mergeFilter };
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
						: new[] {mergeFilter})
					.NullIfEmpty();
				returnBoolFilter._MustNotFilters = (targetBoolFilter._MustNotFilters ?? Empty)
					.Concat(mergeBoolFilter != null
						? (mergeBoolFilter._MustNotFilters ?? Empty)
						: Empty
					).NullIfEmpty();
					
			});
		}

		public static BaseFilter CreateReturnFilter(Action<BaseFilter, BoolBaseFilterDescriptor> modify = null)
		{
			var returnFilter = new BaseFilter();
			var returnBoolFilter = new BoolBaseFilterDescriptor() { };
			returnFilter.BoolFilterDescriptor = returnBoolFilter;
			if (modify != null)
			{
				modify(returnFilter, returnBoolFilter);
			}
			return returnFilter;
		}

	}
}
