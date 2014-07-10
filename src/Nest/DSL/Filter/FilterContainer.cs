using System;
using System.Collections.Generic;
using System.Linq;
using Nest.DSL.Visitor;
using Newtonsoft.Json;

namespace Nest
{
	public class FilterContainer : IFilterContainer, ICustomJson
	{
		private static readonly IEnumerable<FilterContainer> Empty = Enumerable.Empty<FilterContainer>();

		string IFilterContainer._FilterName { get; set; }
		
		string IFilterContainer._CacheKey { get; set; }
		
		bool? IFilterContainer._Cache { get; set; }
		
		string IFilterContainer.RawFilter { get; set; }
		
		[JsonIgnore]
		public virtual bool IsConditionless { get; internal set; }
		
		[JsonIgnore]
		bool IFilterContainer.IsStrict { get; set; }

		[JsonIgnore]
		bool IFilterContainer.IsVerbatim { get; set; }
		
		IBoolFilter IFilterContainer.Bool { get; set; }
		
		IExistsFilter IFilterContainer.Exists { get; set; }

		IMissingFilter IFilterContainer.Missing { get; set; }

		IIdsFilter IFilterContainer.Ids { get; set; }

		IGeoBoundingBoxFilter IFilterContainer.GeoBoundingBox { get; set; }

		IGeoDistanceFilter IFilterContainer.GeoDistance { get; set; }

		IGeoDistanceRangeFilter IFilterContainer.GeoDistanceRange { get; set; }

		IGeoPolygonFilter IFilterContainer.GeoPolygon { get; set; }

		IGeoShapeBaseFilter IFilterContainer.GeoShape { get; set; }

		ILimitFilter IFilterContainer.Limit { get; set; }

		ITypeFilter IFilterContainer.Type { get; set; }

		IMatchAllFilter IFilterContainer.MatchAll { get; set; }

		IHasChildFilter IFilterContainer.HasChild { get; set; }

		IHasParentFilter IFilterContainer.HasParent { get; set; }

		IRangeFilter IFilterContainer.Range { get; set; }

		IPrefixFilter IFilterContainer.Prefix { get; set; }

		ITermFilter IFilterContainer.Term { get; set; }

		ITermsBaseFilter IFilterContainer.Terms { get; set; }

		IQueryFilter IFilterContainer.Query { get; set; }

		IAndFilter IFilterContainer.And { get; set; }

		IOrFilter IFilterContainer.Or { get; set; }

		INotFilter IFilterContainer.Not{ get; set; }

		IScriptFilter IFilterContainer.Script { get; set; }

		INestedFilter IFilterContainer.Nested { get; set; }

		IRegexpFilter IFilterContainer.Regexp { get; set; }
		
		public FilterContainer() {}
		public FilterContainer(PlainFilter filter)
		{
			PlainFilter.ToContainer(filter, this);
		}
		public static FilterContainer From(PlainFilter filter)
		{
			return PlainFilter.ToContainer(filter);
		}
		public void Accept(IQueryVisitor visitor)
		{
			var walker = new QueryFilterWalker();
			if (visitor.Scope == VisitorScope.Unknown) visitor.Scope = VisitorScope.Filter;
			walker.Walk(this, visitor);

			
		}


		
		object ICustomJson.GetCustomJson()
		{
			var f = ((IFilterContainer)this);
			if (f.RawFilter.IsNullOrEmpty()) return f; 
			return new RawJson(f.RawFilter);
		}
		/// <summary>
		/// AND's two BaseFilters
		/// </summary>
		/// <returns>A new basefilter that represents the AND of the two</returns>
		public static FilterContainer operator &(FilterContainer leftFilterDescriptor, FilterContainer rightFilterDescriptor)
		{
			var defaultFilter = new FilterContainer() { IsConditionless = true };
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

		public static FilterContainer operator |(FilterContainer leftFilterDescriptor, FilterContainer rightFilterDescriptor)
		{
			var defaultFilter = new FilterContainer() { IsConditionless = true };
			leftFilterDescriptor = leftFilterDescriptor ?? defaultFilter;
			rightFilterDescriptor = rightFilterDescriptor ?? defaultFilter;
			var combined = new[] { leftFilterDescriptor, rightFilterDescriptor };

			if (combined.Any(bf => bf.IsConditionless))
				return combined.FirstOrDefault(bf => !bf.IsConditionless) ?? defaultFilter;

			var leftDescriptor = ((IFilterContainer)leftFilterDescriptor);
			var leftBoolFilter = leftDescriptor.Bool;
			var rightDescriptor = ((IFilterContainer)rightFilterDescriptor);
			var rightBoolFilter = rightDescriptor.Bool;
		

			var f = new FilterContainer();
			var fq = new BoolBaseFilterDescriptor();
			var bfq = (IBoolFilter)fq;
			bfq.Should = new[] { leftFilterDescriptor, rightFilterDescriptor };
			((IFilterContainer)f).Bool = fq;

			//if neither the left nor the right side represent a bool filter join them
			if (leftDescriptor.Bool == null && rightDescriptor.Bool == null)
			{
				bfq.Should = leftFilterDescriptor.MergeShouldFilters(rightFilterDescriptor);
				return f;
			}
			//if the left or right sight already is a bool filter determine join the non bool query side of the 
			//of the operation onto the other.
			if (leftDescriptor.Bool != null && rightDescriptor.Bool == null && !leftDescriptor.IsStrict)
			{
				JoinShouldOnSide(leftFilterDescriptor, rightFilterDescriptor, fq);
			}
			else if (rightDescriptor.Bool != null && leftDescriptor.Bool == null && !rightDescriptor.IsStrict)
			{
				JoinShouldOnSide(rightFilterDescriptor, leftFilterDescriptor, fq);
			}
			//both sides already represent a bool filter
			else
			{
				//both sides report that we may merge the shoulds
				if (!leftDescriptor.IsStrict && !rightDescriptor.IsStrict
					&& leftBoolFilter.CanJoinShould()
					&& rightBoolFilter.CanJoinShould())
					bfq.Should = leftFilterDescriptor.MergeShouldFilters(rightFilterDescriptor);
				else
					//create a new nested bool with two separate should bool sections
					bfq.Should = new[] { leftFilterDescriptor, rightFilterDescriptor };
			}
			return f;
		}

		public static FilterContainer operator !(FilterContainer lbq)
		{
			if (lbq == null || lbq.IsConditionless)
				return new FilterContainer { IsConditionless = true };

			var f = new FilterContainer();
			var fq = new BoolBaseFilterDescriptor();
			((IBoolFilter)fq).MustNot = new[] { lbq };

			((IFilterContainer)f).Bool = fq;
			return f;
		}

		public static bool operator false(FilterContainer a)
		{
			return false;
		}

		public static bool operator true(FilterContainer a)
		{
			return false;
		}

		private static void JoinShouldOnSide(IFilterContainer lbq, IFilterContainer rbq, IBoolFilter bq)
		{
			bq.Should = lbq.MergeShouldFilters(rbq);
		}

		private static FilterContainer CombineIfNoMergeIsNecessary(
			IFilterContainer leftFilterDescriptor, 
			IFilterContainer rightFilterDescriptor, 
			IEnumerable<FilterContainer> combined)
		{
			var leftBoolFilter = leftFilterDescriptor.Bool;
			var rightBoolFilter = rightFilterDescriptor.Bool;
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

		private static FilterContainer StrictSingleSideAndMerge(IFilterContainer targetFilterDescriptor, IFilterContainer mergeFilterDescriptor)
		{
			//if the target is not strict return
			if (!targetFilterDescriptor.IsStrict) return null;

			var mergeBoolFilter = mergeFilterDescriptor.Bool;

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

		private static FilterContainer SingleSideAndMerge(IFilterContainer targetFilterDescriptor, IFilterContainer mergeFilterDescriptor)
		{
			var targetBoolFilter = targetFilterDescriptor.Bool;
			var mergeBoolFilter = mergeFilterDescriptor.Bool;

			if (targetBoolFilter == null) return null;

			var combined = new[] { targetFilterDescriptor, mergeFilterDescriptor };
			return CreateReturnFilter((returnFilter, returnBoolFilter) =>
			{
				var boolFilter = ((IBoolFilter)returnBoolFilter);
				if (!targetBoolFilter.CanMergeMustAndMustNots() || !mergeBoolFilter.CanMergeMustAndMustNots())
				{
					boolFilter.Must = combined;
					return;
				}

				boolFilter.Must = (targetBoolFilter.Must ?? Empty)
					.Concat(mergeBoolFilter != null
						? (mergeBoolFilter.Must ?? Empty)
						: new[] {mergeFilterDescriptor})
					.NullIfEmpty();
				boolFilter.MustNot = (targetBoolFilter.MustNot ?? Empty)
					.Concat(mergeBoolFilter != null
						? (mergeBoolFilter.MustNot ?? Empty)
						: Empty
					).NullIfEmpty();
					
			});
		}

		public static FilterContainer CreateReturnFilter(Action<FilterContainer, BoolBaseFilterDescriptor> modify = null)
		{
			var returnFilter = new FilterContainer();
			var returnBoolFilter = new BoolBaseFilterDescriptor() { };
			((IFilterContainer)returnFilter).Bool = returnBoolFilter;
			if (modify != null)
			{
				modify(returnFilter, returnBoolFilter);
			}
			return returnFilter;
		}

	}
}
