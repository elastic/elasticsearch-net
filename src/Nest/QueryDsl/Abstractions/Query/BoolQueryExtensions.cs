using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	internal static class BoolQueryExtensions
	{
		internal static QueryContainer CombineAsMust(this QueryContainer leftContainer, QueryContainer rightContainer)
		{
			var leftBool = (leftContainer as IQueryContainer)?.Bool;
			var rightBool = (rightContainer as IQueryContainer)?.Bool;
			if (leftBool == null && rightBool == null)
				return CreateMustContainer(new[] { leftContainer, rightContainer }, null, null);

			if ((leftBool?.Locked).GetValueOrDefault() || (rightBool?.Locked).GetValueOrDefault())
				return CreateMustContainer(new[] { leftContainer, rightContainer }, null, null);

			if (leftBool.HasOnlyShouldClauses() || rightBool.HasOnlyShouldClauses())
				return CreateMustContainer(new[] { leftContainer, rightContainer }, null, null);

			var mustNotClauses = OrphanMustNots(leftContainer).EagerConcat(OrphanMustNots(rightContainer));
			var filterClauses = OrphanFilters(leftContainer).EagerConcat(OrphanFilters(rightContainer));
			var mustClauses = MustClausesOrSelf(leftContainer).EagerConcat(MustClausesOrSelf(rightContainer));

			var container = CreateMustContainer(mustClauses, mustNotClauses, filterClauses);
			return container;
		}

		internal static QueryContainer CombineAsShould(this QueryContainer leftContainer, QueryContainer rightContainer)
		{
			if ((!leftContainer.CanMergeShould() || !rightContainer.CanMergeShould()))
				return CreateShouldContainer(new List<QueryContainer> { leftContainer, rightContainer });

			var lBoolQuery = leftContainer.Self().Bool;
			var rBoolQuery = rightContainer.Self().Bool;

			var lHasShouldQueries = lBoolQuery != null && lBoolQuery.Should.HasAny();
			var rHasShouldQueries = rBoolQuery != null && rBoolQuery.Should.HasAny();

			var lq = lHasShouldQueries ? lBoolQuery.Should : new[] { leftContainer };
			var rq = rHasShouldQueries ? rBoolQuery.Should : new[] { rightContainer };

			var shouldClauses = lq.EagerConcat(rq);
			return CreateShouldContainer(shouldClauses);
		}

		private static IQueryContainer Self(this QueryContainer q) => q;


		private static bool HasOnlyShouldClauses(this IBoolQuery boolQuery) =>
			boolQuery != null && boolQuery.IsWritable &&  (
				boolQuery.Should.HasAny()
				&& !boolQuery.Must.HasAny()
				&& !boolQuery.MustNot.HasAny()
				&& !boolQuery.Filter.HasAny()
			);

		private static bool HasOnlyFilterClauses(this IBoolQuery boolQuery) =>
			boolQuery != null && boolQuery.IsWritable && !boolQuery.Locked && (
				!boolQuery.Should.HasAny()
				&& !boolQuery.Must.HasAny()
				&& !boolQuery.MustNot.HasAny()
				&& boolQuery.Filter.HasAny()
			);

		private static bool HasOnlyMustNotClauses(this IBoolQuery boolQuery) =>
			boolQuery != null && boolQuery.IsWritable && !boolQuery.Locked && (
				!boolQuery.Should.HasAny()
				&& !boolQuery.Must.HasAny()
				&& boolQuery.MustNot.HasAny()
			&& !boolQuery.Filter.HasAny()
		);

		private static bool CanMergeShould(this IQueryContainer container) => container.Bool.CanMergeShould();

		private static bool CanMergeShould(this IBoolQuery boolQuery) =>
			boolQuery == null || (!boolQuery.Locked
				&& (boolQuery.HasOnlyShouldClauses() || boolQuery.HasOnlyMustNotClauses() || boolQuery.HasOnlyFilterClauses())
			);

		private static IEnumerable<QueryContainer> MustClausesOrSelf(QueryContainer container)
		{
			var boolQuery = container.Self().Bool;
			if (boolQuery != null && boolQuery.Must.HasAny()) return boolQuery.Must;

			return boolQuery != null && !boolQuery.IsWritable ? Enumerable.Empty<QueryContainer>() : new[] { container };
		}

		private static IEnumerable<QueryContainer> OrphanMustNots(IQueryContainer container)
		{
			var lBoolQuery = container.Bool;
			if (lBoolQuery == null || !lBoolQuery.MustNot.HasAny()) return null;

			var mustNotQueries = lBoolQuery.MustNot.ToList();
			lBoolQuery.MustNot = null;
			return mustNotQueries;
		}

		private static IEnumerable<QueryContainer> OrphanFilters(IQueryContainer container)
		{
			var lBoolQuery = container.Bool;
			if (lBoolQuery == null || !lBoolQuery.Filter.HasAny()) return null;

			var filters = lBoolQuery.Filter.ToList();
			lBoolQuery.Filter = null;
			return filters;
		}

		private static QueryContainer CreateShouldContainer(IList<QueryContainer> shouldClauses) =>
			new BoolQuery
			{
				Should = shouldClauses.ToListOrNullIfEmpty()
			};

		private static QueryContainer CreateMustContainer(IList<QueryContainer> mustClauses, IEnumerable<QueryContainer> mustNotClauses, IEnumerable<QueryContainer> filters) =>
			new BoolQuery
			{
				Must = mustClauses.ToListOrNullIfEmpty(),
				MustNot = mustNotClauses.ToListOrNullIfEmpty(),
				Filter = filters.ToListOrNullIfEmpty()
			};
	}
}
