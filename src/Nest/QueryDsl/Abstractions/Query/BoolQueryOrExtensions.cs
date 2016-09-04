using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	internal static class BoolQueryOrExtensions
	{
		internal static QueryContainer CombineAsShould(this QueryContainer leftContainer, QueryContainer rightContainer)
		{
			QueryContainer c = null;
			var leftBool = leftContainer.Self()?.Bool;
			var rightBool = rightContainer.Self()?.Bool;
			if (TryFlattenShould(leftContainer, rightContainer, leftBool, rightBool, out c)) return c;

			//if (leftContainer.FlattenShould(rightContainer)) return leftContainer;

			var lBoolQuery = leftContainer.Self().Bool;
			var rBoolQuery = rightContainer.Self().Bool;

			var lHasShouldQueries = lBoolQuery != null && lBoolQuery.Should.HasAny();
			var rHasShouldQueries = rBoolQuery != null && rBoolQuery.Should.HasAny();

			var lq = lHasShouldQueries ? lBoolQuery.Should : new[] { leftContainer };
			var rq = rHasShouldQueries ? rBoolQuery.Should : new[] { rightContainer };

			var shouldClauses = lq.EagerConcat(rq);
			return CreateShouldContainer(shouldClauses);
		}
		private static bool TryFlattenShould(
			QueryContainer leftContainer, QueryContainer rightContainer, IBoolQuery leftBool, IBoolQuery rightBool, out QueryContainer c)
		{
			c = null;
			var leftCanMerge = leftContainer.CanMergeShould2();
			var rightCanMerge = rightContainer.CanMergeShould2();
			if (!leftCanMerge && !rightCanMerge) c = CreateShouldContainer(new[] { leftContainer, rightContainer });

			//left can merge but right's bool can not instead of wrapping into a new bool we inject the whole bool into left
			else if (leftCanMerge && !rightCanMerge && rightBool != null)
			{
				leftBool.Should = leftBool.Should.AddIfNotNull(rightContainer);
				c = leftContainer;
			}
			else if (rightCanMerge && !leftCanMerge && leftBool != null)
			{
				rightBool.Should = rightBool.Should.AddIfNotNull(leftContainer);
				c = rightContainer;
			}
			return c != null;

		}

		private static bool FlattenShould(this IQueryContainer leftContainer, IQueryContainer rightContainer)
		{
			// if the left operand can merge but the right operand can not because its also a bool with a must/must_not clause
			// We can still lift the whole right operand into the lefts should

			// this prevents many (q |= <bool with non should clauses>) assignments
			// making a deeply nested tree of paired shoulds.

			if (leftContainer.Bool.CanMergeShould() && rightContainer.Bool != null && !rightContainer.Bool.CanMergeShould())
			{
				var x = new List<QueryContainer>(leftContainer?.Bool?.Should ?? Enumerable.Empty<QueryContainer>());
				x.AddIfNotNull<QueryContainer>(rightContainer as QueryContainer);
				leftContainer.Bool.Should = x;
				return true;
			}
			return false;
		}

		private static bool CanMergeShould2(this IQueryContainer container) => container.Bool.CanMergeShould2();

		private static bool CanMergeShould2(this IBoolQuery boolQuery) =>
			boolQuery != null && boolQuery.IsWritable && !boolQuery.Locked && boolQuery.HasOnlyShouldClauses();

		private static bool CanMergeShould(this IQueryContainer container) => container.Bool.CanMergeShould();

		private static bool CanMergeShould(this IBoolQuery boolQuery) =>
			boolQuery == null || (!boolQuery.Locked
				&& (boolQuery.HasOnlyShouldClauses() || boolQuery.HasOnlyMustNotClauses() || boolQuery.HasOnlyFilterClauses())
			);

		private static QueryContainer CreateShouldContainer(IList<QueryContainer> shouldClauses) =>
			new BoolQuery
			{
				Should = shouldClauses.ToListOrNullIfEmpty()
			};

	}
}
