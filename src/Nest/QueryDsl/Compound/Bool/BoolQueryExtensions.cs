using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	internal static class BoolQueryExtensions
	{
		internal static IQueryContainer Self(this QueryContainer q) => q;

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

		internal static QueryContainer MergeMustQueries(this QueryContainer leftContainer, QueryContainer rightContainer)
		{
			if (!leftContainer.CanMergeMustAndMustNots() || !rightContainer.CanMergeMustAndMustNots())
			{
				if (rightContainer.IsBoolQueryWithOnlyMustNots()) 
					return CreateMustContainer(new [] { leftContainer }, rightContainer.Self().Bool.MustNot );
				if (leftContainer.IsBoolQueryWithOnlyMustNots()) 
					return CreateMustContainer(new [] { rightContainer }, leftContainer.Self().Bool.MustNot );
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

		private static IEnumerable<QueryContainer> CreateMustClauses(QueryContainer container)
		{
			var boolQuery = container.Self().Bool;
			var hasMustClauses = boolQuery != null && boolQuery.Must.HasAny();
			if (hasMustClauses) return boolQuery.Must;
			if (boolQuery != null && boolQuery.Conditionless)
				return Enumerable.Empty<QueryContainer>();

			return new[] {container};
		}

		private static IEnumerable<QueryContainer> OrphanMustNots(IQueryContainer container)
		{
			var lBoolQuery = container.Bool;
			if (lBoolQuery == null || !lBoolQuery.MustNot.HasAny()) return null;
			
			var mustNotQueries = lBoolQuery.MustNot.ToList();
			lBoolQuery.MustNot = null;
			return mustNotQueries;
		}

		internal static QueryContainer MergeShouldQueries(this QueryContainer leftContainer, QueryContainer rightContainer)
		{
			if (!leftContainer.CanMergeShould() || !leftContainer.CanMergeShould())
				return CreateShouldContainer(new List<QueryContainer> { leftContainer, rightContainer }); 

			var lBoolQuery = leftContainer.Self().Bool;
			var rBoolQuery = rightContainer.Self().Bool;

			var lHasShouldQueries = lBoolQuery != null && lBoolQuery.Should.HasAny();
			var rHasShouldQueries = rBoolQuery != null && rBoolQuery.Should.HasAny();

			var lq = lHasShouldQueries ? lBoolQuery.Should : new[] { leftContainer };
			var rq = rHasShouldQueries ? rBoolQuery.Should : new[] { rightContainer };

			var shouldClauses =  lq.EagerConcat(rq);
			return CreateShouldContainer(shouldClauses);
		}

		internal static QueryContainer CreateShouldContainer(IList<QueryContainer> shouldClauses)
		{
			IQueryContainer q = new QueryContainer();
			q.Bool = new BoolQuery();
			q.Bool.Should = shouldClauses.ToListOrNullIfEmpty();
			return q as QueryContainer;
		}
		
		internal static QueryContainer CreateMustContainer(IList<QueryContainer> mustClauses, IEnumerable<QueryContainer> mustNotClauses)
		{
			IQueryContainer q = new QueryContainer();
			q.Bool = new BoolQuery();
			q.Bool.Must = mustClauses.ToListOrNullIfEmpty();
			q.Bool.MustNot = mustNotClauses.ToListOrNullIfEmpty();
			return q as QueryContainer;
		}
	}
}