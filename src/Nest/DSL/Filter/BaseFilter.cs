using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.DSL.Visitor;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using Elasticsearch.Net;
using System.Linq.Expressions;

namespace Nest
{
	public class BaseFilterDescriptor : IFilterDescriptor, ICustomJson
	{
		public string _Name { get; set; }
		
		public string _CacheKey { get; set; }
		
		public bool? _Cache { get; set; }
		
		public string RawFilter { get; set; }
		
		[JsonIgnore]
		public virtual bool IsConditionless { get; internal set; }
		
		IBoolFilter IFilterDescriptor.Bool { get; set; }
		
		IExistsFilter IFilterDescriptor.Exists { get; set; }

		IMissingFilter IFilterDescriptor.Missing { get; set; }

		IIdsFilter IFilterDescriptor.Ids { get; set; }

		IGeoBoundingBoxFilter IFilterDescriptor.GeoBoundingBox { get; set; }

		IGeoDistanceFilter IFilterDescriptor.GeoDistance { get; set; }

		IGeoDistanceRangeFilter IFilterDescriptor.GeoDistanceRange { get; set; }

		IGeoPolygonFilter IFilterDescriptor.GeoPolygon { get; set; }

		IGeoShapeBaseFilter IFilterDescriptor.GeoShape { get; set; }

		ILimitFilter IFilterDescriptor.Limit { get; set; }

		ITypeFilter IFilterDescriptor.Type { get; set; }

		IMatchAllFilter IFilterDescriptor.MatchAll { get; set; }

		IHasChildFilter IFilterDescriptor.HasChild { get; set; }

		IHasParentFilter IFilterDescriptor.HasParent { get; set; }

		IRangeFilter IFilterDescriptor.Range { get; set; }

		IPrefixFilter IFilterDescriptor.Prefix { get; set; }

		ITermFilter IFilterDescriptor.Term { get; set; }

		ITermsBaseFilter IFilterDescriptor.Terms { get; set; }

		IQueryFilter IFilterDescriptor.Query { get; set; }

		IAndFilter IFilterDescriptor.And { get; set; }

		IOrFilter IFilterDescriptor.Or { get; set; }

		INotFilter IFilterDescriptor.Not{ get; set; }

		IScriptFilter IFilterDescriptor.Script { get; set; }

		INestedFilterDescriptor IFilterDescriptor.Nested { get; set; }

		IRegexpFilter IFilterDescriptor.Regexp { get; set; }
		
		
		public void Accept(IQueryVisitor visitor)
		{
			var walker = new QueryFilterWalker();
			walker.Walk(this, visitor);

			
		}

		private static readonly IEnumerable<BaseFilterDescriptor> Empty = Enumerable.Empty<BaseFilterDescriptor>();

		
		[JsonIgnore]
		public bool IsStrict { get; internal set; }
		[JsonIgnore]
		public bool IsVerbatim { get; internal set; }
		object ICustomJson.GetCustomJson()
		{
			var f = ((IFilterDescriptor)this);
			if (f.RawFilter.IsNullOrEmpty()) return f; 
			return new RawJson(f.RawFilter);
		}
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

			var leftBoolFilter = ((IFilterDescriptor)leftFilterDescriptor).Bool;
			var rightBoolFilter = ((IFilterDescriptor)rightFilterDescriptor).Bool;
		

			var f = new BaseFilterDescriptor();
			var fq = new BoolBaseFilterDescriptor();
			var bfq = (IBoolFilter)fq;
			bfq.Should = new[] { leftFilterDescriptor, rightFilterDescriptor };
			((IFilterDescriptor)f).Bool = fq;

			//if neither the left nor the right side represent a bool filter join them
			if (((IFilterDescriptor)leftFilterDescriptor).Bool == null && ((IFilterDescriptor)rightFilterDescriptor).Bool == null)
			{
				bfq.Should = leftFilterDescriptor.MergeShouldFilters(rightFilterDescriptor);
				return f;
			}
			//if the left or right sight already is a bool filter determine join the non bool query side of the 
			//of the operation onto the other.
			if (((IFilterDescriptor)leftFilterDescriptor).Bool != null && ((IFilterDescriptor)rightFilterDescriptor).Bool == null && !leftFilterDescriptor.IsStrict)
			{
				JoinShouldOnSide(leftFilterDescriptor, rightFilterDescriptor, fq);
			}
			else if (((IFilterDescriptor)rightFilterDescriptor).Bool != null && ((IFilterDescriptor)leftFilterDescriptor).Bool == null && !rightFilterDescriptor.IsStrict)
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
					bfq.Should = leftFilterDescriptor.MergeShouldFilters(rightFilterDescriptor);
				else
					//create a new nested bool with two separate should bool sections
					bfq.Should = new[] { leftFilterDescriptor, rightFilterDescriptor };
			}
			return f;
		}

		public static BaseFilterDescriptor operator !(BaseFilterDescriptor lbq)
		{
			if (lbq == null || lbq.IsConditionless)
				return new BaseFilterDescriptor { IsConditionless = true };

			var f = new BaseFilterDescriptor();
			var fq = new BoolBaseFilterDescriptor();
			((IBoolFilter)fq).MustNot = new[] { lbq };

			((IFilterDescriptor)f).Bool = fq;
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
			((IBoolFilter)bq).Should = lbq.MergeShouldFilters(rbq);
		}

		private static BaseFilterDescriptor CombineIfNoMergeIsNecessary(
			BaseFilterDescriptor leftFilterDescriptor, 
			BaseFilterDescriptor rightFilterDescriptor, 
			BaseFilterDescriptor[] combined)
		{
			var leftBoolFilter = ((IFilterDescriptor)leftFilterDescriptor).Bool;
			var rightBoolFilter = ((IFilterDescriptor)rightFilterDescriptor).Bool;
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
				return CreateReturnFilter((returnFilter, returnBoolFilter) => ((IBoolFilter)returnBoolFilter).Must = combined);
			}
			return null;
		}

		private static BaseFilterDescriptor StrictSingleSideAndMerge(BaseFilterDescriptor targetFilterDescriptor, BaseFilterDescriptor mergeFilterDescriptor)
		{
			//if the target is not strict return
			if (!targetFilterDescriptor.IsStrict) return null;

			IBoolFilter mergeBoolFilter = ((IFilterDescriptor)mergeFilterDescriptor).Bool;

			return CreateReturnFilter((returnFilter, returnBoolFilter) =>
			{
				if (mergeBoolFilter.MustNot.HasAny())
				{
					((IBoolFilter)returnBoolFilter).MustNot = mergeBoolFilter.MustNot;
					mergeBoolFilter.MustNot = null;
				}

				((IBoolFilter)returnBoolFilter).Must = new[] { targetFilterDescriptor }.Concat(mergeBoolFilter.Must ?? Empty);
			});
		}

		private static BaseFilterDescriptor SingleSideAndMerge(BaseFilterDescriptor targetFilterDescriptor, BaseFilterDescriptor mergeFilterDescriptor)
		{
			var targetBoolFilter = ((IFilterDescriptor)targetFilterDescriptor).Bool;
			var mergeBoolFilter = ((IFilterDescriptor)mergeFilterDescriptor).Bool;

			if (targetBoolFilter == null) return null;

			var combined = new[] { targetFilterDescriptor, mergeFilterDescriptor };
			return CreateReturnFilter((returnFilter, returnBoolFilter) =>
			{
				if (!targetBoolFilter.CanMergeMustAndMustNots() || !mergeBoolFilter.CanMergeMustAndMustNots())
				{
					((IBoolFilter)returnBoolFilter).Must = combined;
					return;
				}

				((IBoolFilter)returnBoolFilter).Must = (((IBoolFilter)targetBoolFilter).Must ?? Empty)
					.Concat(mergeBoolFilter != null
						? (((IBoolFilter)mergeBoolFilter).Must ?? Empty)
						: new[] {mergeFilterDescriptor})
					.NullIfEmpty();
				((IBoolFilter)returnBoolFilter).MustNot = (((IBoolFilter)targetBoolFilter).MustNot ?? Empty)
					.Concat(mergeBoolFilter != null
						? (((IBoolFilter)mergeBoolFilter).MustNot ?? Empty)
						: Empty
					).NullIfEmpty();
					
			});
		}

		public static BaseFilterDescriptor CreateReturnFilter(Action<BaseFilterDescriptor, BoolBaseFilterDescriptor> modify = null)
		{
			var returnFilter = new BaseFilterDescriptor();
			var returnBoolFilter = new BoolBaseFilterDescriptor() { };
			((IFilterDescriptor)returnFilter).Bool = returnBoolFilter;
			if (modify != null)
			{
				modify(returnFilter, returnBoolFilter);
			}
			return returnFilter;
		}

	}
}
