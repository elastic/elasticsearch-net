// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;

namespace Elastic.Clients.Elasticsearch.QueryDsl;

internal static class BoolQueryOrExtensions
{
	internal static Query CombineAsShould(this Query leftContainer, Query rightContainer)
	{
		var hasLeftBool = leftContainer.TryGet<BoolQuery>(out var leftBool);
		var hasRightBool = rightContainer.TryGet<BoolQuery>(out var rightBool);

		if (TryFlattenShould(leftContainer, rightContainer, leftBool, rightBool, out var c))
			return c;

		var lHasShouldQueries = hasLeftBool && leftBool.Should.HasAny();
		var rHasShouldQueries = hasRightBool && rightBool.Should.HasAny();

		var lq = lHasShouldQueries ? leftBool.Should : new[] { leftContainer };
		var rq = rHasShouldQueries ? rightBool.Should : new[] { rightContainer };

		var shouldClauses = lq.EagerConcat(rq);

		return CreateShouldContainer(shouldClauses);
	}

	private static bool TryFlattenShould(Query leftContainer, Query rightContainer, BoolQuery leftBool, BoolQuery rightBool, out Query query)
	{
		query = null;

		var leftCanMerge = leftContainer.CanMergeShould();
		var rightCanMerge = rightContainer.CanMergeShould();

		if (!leftCanMerge && !rightCanMerge)
			query = CreateShouldContainer(new List<Query> { leftContainer, rightContainer });

		// Left can merge but right's bool can not. instead of wrapping into a new bool we inject the whole bool into left

		else if (leftCanMerge && !rightCanMerge && rightBool is not null)
		{
			leftBool.Should = leftBool.Should.AddIfNotNull(rightContainer).ToArray();
			query = leftContainer;
		}
		else if (rightCanMerge && !leftCanMerge && leftBool is not null)
		{
			rightBool.Should = rightBool.Should.AddIfNotNull(leftContainer).ToArray();
			query = rightContainer;
		}

		return query != null;
	}

	private static bool CanMergeShould(this Query container) =>
		container.TryGet<BoolQuery>(out var boolQuery) && boolQuery.CanMergeShould();

	private static bool CanMergeShould(this BoolQuery boolQuery) =>
		boolQuery is not null && !boolQuery.Locked && boolQuery.HasOnlyShouldClauses();

	private static Query CreateShouldContainer(List<Query> shouldClauses) =>
		new BoolQuery
		{
			Should = shouldClauses.ToListOrNullIfEmpty()
		};
}
