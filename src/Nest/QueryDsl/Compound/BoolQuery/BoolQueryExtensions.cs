using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	internal static class BoolQueryExtensions
	{
		internal static bool IsBoolQueryWithOnlyMustNots(this IQueryContainer container)
		{
			return container != null && container.Bool != null
				&& !container.Bool.Must.HasAny()
				&& !container.Bool.Should.HasAny()
				&& container.Bool.MustNot.HasAny();
		}

		internal static bool CanMergeMustAndMustNots(this IQueryContainer container)
		{
			return !container.IsStrict && container.Bool.CanMergeMustAndMustNots();
		}

		internal static bool CanMergeMustAndMustNots(this IBoolQuery boolQuery)
		{
			return boolQuery == null || (!boolQuery.Should.HasAny() && boolQuery.MinimumShouldMatch == null);
		}

		internal static bool CanMergeShould(this IQueryContainer container)
		{
			return !container.IsStrict && container.Bool.CanMergeShould();
		}

		internal static bool CanMergeShould(this IBoolQuery boolQuery)
		{
			return boolQuery == null 
				   || (boolQuery.MinimumShouldMatch == null
					&& (
				      (boolQuery.Should.HasAny() && !boolQuery.Must.HasAny() && !boolQuery.MustNot.HasAny())
				       || !boolQuery.Should.HasAny()
				       )
				   );
		}

		internal static QueryContainer MergeMustQueries(this IQueryContainer leftContainer, IQueryContainer rightContainer)
		{
			if (!leftContainer.CanMergeMustAndMustNots() || !rightContainer.CanMergeMustAndMustNots())
			{
				if (rightContainer.IsBoolQueryWithOnlyMustNots()) 
					return CreateMustContainer(new [] { leftContainer }, rightContainer.Bool.MustNot );
				if (leftContainer.IsBoolQueryWithOnlyMustNots()) 
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

		private static IEnumerable<IQueryContainer> CreateMustClauses(IQueryContainer container)
		{
			var boolQuery = container.Bool;
			var hasMustClauses = boolQuery != null && boolQuery.Must.HasAny();
			if (hasMustClauses) return boolQuery.Must;
			if (boolQuery != null && boolQuery.Conditionless)
				return Enumerable.Empty<IQueryContainer>();

			return new[] {container};
		}

		private static IEnumerable<IQueryContainer> OrphanMustNots(IQueryContainer container)
		{
			var lBoolQuery = container.Bool;
			if (lBoolQuery == null || !lBoolQuery.MustNot.HasAny()) return null;
			
			var mustNotQueries = lBoolQuery.MustNot.ToList();
			lBoolQuery.MustNot = null;
			return mustNotQueries;
		}

		internal static QueryContainer MergeShouldQueries(this IQueryContainer leftContainer, IQueryContainer rightContainer)
		{
			if (!leftContainer.CanMergeShould() || !leftContainer.CanMergeShould())
				return CreateShouldContainer(new List<IQueryContainer> { leftContainer, rightContainer }); 

			var lBoolQuery = leftContainer.Bool;
			var rBoolQuery = rightContainer.Bool;

			var lHasShouldQueries = lBoolQuery != null && lBoolQuery.Should.HasAny();
			var rHasShouldQueries = rBoolQuery != null && rBoolQuery.Should.HasAny();

			var lq = lHasShouldQueries ? lBoolQuery.Should : new[] { leftContainer };
			var rq = rHasShouldQueries ? rBoolQuery.Should : new[] { rightContainer };

			var shouldClauses =  lq.EagerConcat(rq);
			return CreateShouldContainer(shouldClauses);
		}

		internal static QueryContainer CreateShouldContainer(IList<IQueryContainer> shouldClauses)
		{
			IQueryContainer q = new QueryContainer();
			q.Bool = new BoolQuery();
			q.Bool.Should = shouldClauses.ToListOrNullIfEmpty();
			return q as QueryContainer;
		}
		
		internal static QueryContainer CreateMustContainer(IList<IQueryContainer> mustClauses, IEnumerable<IQueryContainer> mustNotClauses)
		{
			IQueryContainer q = new QueryContainer();
			q.Bool = new BoolQuery();
			q.Bool.Must = mustClauses.ToListOrNullIfEmpty();
			q.Bool.MustNot = mustNotClauses.ToListOrNullIfEmpty();
			return q as QueryContainer;
		}
	}
}