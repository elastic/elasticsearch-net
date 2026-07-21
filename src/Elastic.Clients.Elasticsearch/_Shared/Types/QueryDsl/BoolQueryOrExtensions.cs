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
		var leftBool = leftContainer.Bool;
		var hasLeftBool = (leftBool is not null);
		var rightBool = rightContainer.Bool;
		var hasRightBool = (rightBool is not null);

		if (TryFlattenShould(leftContainer, rightContainer, leftBool, rightBool, out var c))
			return c;

		var lHasShouldQueries = hasLeftBool && leftBool.Should.HasAny();
		var rHasShouldQueries = hasRightBool && rightBool.Should.HasAny();

		var lq = lHasShouldQueries ? leftBool.Should : [leftContainer];
		var rq = rHasShouldQueries ? rightBool.Should : [rightContainer];

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
		container.Bool?.CanMergeShould() ?? false;

	private static bool CanMergeShould(this BoolQuery boolQuery) =>
		boolQuery is not null && !boolQuery.Locked && boolQuery.HasOnlyShouldClauses();

	private static Query CreateShouldContainer(List<Query> shouldClauses) =>
		new()
		{
			Bool = new() { Should = shouldClauses.ToListOrNullIfEmpty() }
		};
}
