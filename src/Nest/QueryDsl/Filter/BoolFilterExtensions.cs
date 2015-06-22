using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	internal static class BoolFilterExtensions
	{
		internal static bool IsBoolFilterWithOnlyMustNots(this IFilterContainer container)
		{
			return container != null && container.Bool != null
				&& !container.Bool.Must.HasAny()
				&& !container.Bool.Should.HasAny()
				&& container.Bool.MustNot.HasAny();
		}

		internal static bool CanMergeMustAndMustNots(this IFilterContainer container)
		{
			return !container.IsStrict && container.Bool.CanMergeMustAndMustNots();
		}

		internal static bool CanMergeMustAndMustNots(this IBoolFilter boolFilter)
		{
			return boolFilter == null || !boolFilter.Should.HasAny();

		}

		internal static bool CanMergeShould(this IFilterContainer container)
		{
			return !container.IsStrict && container.Bool.CanMergeShould();
		}

		internal static bool CanMergeShould(this IBoolFilter boolFilter)
		{
			return boolFilter == null 
				   ||  (
				      (boolFilter.Should.HasAny() && !boolFilter.Must.HasAny() && !boolFilter.MustNot.HasAny())
				       || !boolFilter.Should.HasAny()
				   );
		}

		internal static FilterContainer MergeMustFilters(this IFilterContainer leftContainer, IFilterContainer rightContainer)
		{
			if (!leftContainer.CanMergeMustAndMustNots() || !rightContainer.CanMergeMustAndMustNots())
			{
				if (rightContainer.IsBoolFilterWithOnlyMustNots()) 
					return CreateMustContainer(new [] { leftContainer }, rightContainer.Bool.MustNot );
				if (leftContainer.IsBoolFilterWithOnlyMustNots()) 
					return CreateMustContainer(new [] { rightContainer }, leftContainer.Bool.MustNot );
				return CreateMustContainer(new [] { leftContainer, rightContainer }, null);
			}
			
			var mustNots = OrphanMustNots(leftContainer)
				.EagerConcat(OrphanMustNots(rightContainer));
			
			var leftClauses = CreateMustClauses(leftContainer);
			var rightClauses = CreateMustClauses(rightContainer);
			
			var mustClauses = leftClauses.EagerConcat(rightClauses);
			var container = CreateMustContainer(mustClauses, mustNots);
			return container;
		}

		private static IEnumerable<IFilterContainer> CreateMustClauses(IFilterContainer container)
		{
			var boolFilter = container.Bool;
			var hasMustClauses = boolFilter != null && boolFilter.Must.HasAny();
			if (hasMustClauses) return boolFilter.Must;
			if (boolFilter != null && boolFilter.IsConditionless)
				return Enumerable.Empty<IFilterContainer>();

			return new[] {container};
		}

		private static IEnumerable<IFilterContainer> OrphanMustNots(IFilterContainer container)
		{
			var lBoolFilter = container.Bool;
			if (lBoolFilter == null || !lBoolFilter.MustNot.HasAny()) return null;
			
			var mustNotFilters = lBoolFilter.MustNot.ToList();
			lBoolFilter.MustNot = null;
			return mustNotFilters;
		}

		internal static FilterContainer MergeShouldFilters(this IFilterContainer leftContainer, IFilterContainer rightContainer)
		{
			if (!leftContainer.CanMergeShould() || !leftContainer.CanMergeShould())
				return CreateShouldContainer(new List<IFilterContainer> { leftContainer, rightContainer }); 

			var lBoolFilter = leftContainer.Bool;
			var rBoolFilter = rightContainer.Bool;

			var lHasShouldFilters = lBoolFilter != null && lBoolFilter.Should.HasAny();
			var rHasShouldFilters = rBoolFilter != null && rBoolFilter.Should.HasAny();

			var lq = lHasShouldFilters ? lBoolFilter.Should : new[] { leftContainer };
			var rq = rHasShouldFilters ? rBoolFilter.Should : new[] { rightContainer };

			var shouldClauses =  lq.EagerConcat(rq);
			return CreateShouldContainer(shouldClauses);
		}

		internal static FilterContainer CreateShouldContainer(IList<IFilterContainer> shouldClauses)
		{
			IFilterContainer q = new FilterContainer();
			q.Bool = new BoolBaseFilterDescriptor();
			q.Bool.Should = shouldClauses.NullIfEmpty();
			return q as FilterContainer;
		}
		
		internal static FilterContainer CreateMustContainer(IList<IFilterContainer> mustClauses, IEnumerable<IFilterContainer> mustNotClauses)
		{
			IFilterContainer q = new FilterContainer();
			q.Bool = new BoolBaseFilterDescriptor();
			q.Bool.Must = mustClauses.NullIfEmpty();
			q.Bool.MustNot = mustNotClauses.NullIfEmpty();
			return q as FilterContainer;
		}
	}
}