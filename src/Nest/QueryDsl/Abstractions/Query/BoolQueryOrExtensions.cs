using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	using Containers = System.Collections.Generic.List<QueryContainer>;
	internal static class BoolQueryOrExtensions
	{
		internal static QueryContainer CombineAsShould(this QueryContainer leftContainer, QueryContainer rightContainer)
		{
			QueryContainer c = null;
			var leftBool = leftContainer.Self()?.Bool;
			var rightBool = rightContainer.Self()?.Bool;
			if (TryFlattenShould(leftContainer, rightContainer, leftBool, rightBool, out c))
				return c;

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
			var leftCanMerge = leftContainer.CanMergeShould();
			var rightCanMerge = rightContainer.CanMergeShould();
			if (!leftCanMerge && !rightCanMerge) c = CreateShouldContainer(new Containers { leftContainer, rightContainer });

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

		private static bool CanMergeShould(this IQueryContainer container) => container.Bool.CanMergeShould();

		private static bool CanMergeShould(this IBoolQuery boolQuery) =>
			boolQuery != null && boolQuery.IsWritable && !boolQuery.Locked && boolQuery.HasOnlyShouldClauses();

		private static QueryContainer CreateShouldContainer(List<QueryContainer> shouldClauses) =>
			new BoolQuery
			{
				Should = shouldClauses.ToListOrNullIfEmpty()
			};

	}
}
