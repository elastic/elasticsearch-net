// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Elastic.Clients.Elasticsearch.QueryDsl;

internal static class BoolQueryAndExtensions
{
	internal static Query CombineAsMust(this Query leftContainer, Query rightContainer)
	{
		var hasLeftBool = leftContainer.TryGet<BoolQuery>(out var leftBool);
		var hasRightBool = rightContainer.TryGet<BoolQuery>(out var rightBool);

		// neither side is a bool, no special handling needed wrap in a bool must
		if (!hasLeftBool && !hasRightBool)
		{
			return CreateMustContainer(new List<Query> { leftContainer, rightContainer });
		}

		else if (TryHandleBoolsWithOnlyShouldClauses(leftContainer, rightContainer, leftBool, rightBool, out var query))
		{
			return query;
		}

		else if (TryHandleUnmergableBools(leftContainer, rightContainer, leftBool, rightBool, out query))
		{
			return query;
		}

		// neither side is unmergable so neither is a bool with should clauses

		var mustNotClauses = OrphanMustNots(leftContainer).EagerConcat(OrphanMustNots(rightContainer));
		var filterClauses = OrphanFilters(leftContainer).EagerConcat(OrphanFilters(rightContainer));
		var mustClauses = OrphanMusts(leftContainer).EagerConcat(OrphanMusts(rightContainer));

		var queryContainer = CreateMustContainer(mustClauses, mustNotClauses, filterClauses);

		return queryContainer;
	}

	/// <summary>
	/// Handles cases where either side is a bool which indicates it can't be merged, yet the other side is mergable.
	/// A side is considered unmergable if its locked (has important metadata) or has should clauses.
	/// Instead of always wrapping these cases in another bool we merge the unmergable side into to others must clause therefore flattening the
	/// generated graph. In such cases, we copy the existing BoolQuery so as not to cause a potentially surprising side-effect. (see https://github.com/elastic/elasticsearch-net/issues/5076).
	/// </summary>
	private static bool TryHandleUnmergableBools(Query leftContainer, Query rightContainer, BoolQuery? leftBool, BoolQuery? rightBool, [NotNullWhen(true)] out Query? query)
	{
		query = null;

		var leftCantMergeAnd = leftBool is not null && !leftBool.CanMergeAnd();
		var rightCantMergeAnd = rightBool is not null && !rightBool.CanMergeAnd();

		if (!leftCantMergeAnd && !rightCantMergeAnd)
		{
			return false;
		}

		if (leftCantMergeAnd && rightCantMergeAnd)
		{
			query = CreateMustContainer(leftContainer, rightContainer);
		}

		// right can't merge but left can and is a bool so we add left to the must clause of right
		else if (!leftCantMergeAnd && leftBool != null && rightCantMergeAnd)
		{
			if (rightContainer is null)
			{
				query = leftContainer;
			}
			else
			{
				// We create a copy here to avoid side-effects on a user provided bool query such as
				// adding a must clause where one did not previously exist.

				var leftBoolCopy = new BoolQuery
				{
					Boost = leftBool.Boost,
					MinimumShouldMatch = leftBool.MinimumShouldMatch,
					QueryName = leftBool.QueryName,
					Should = leftBool.Should,
					Must = leftBool.Must.AddIfNotNull(rightContainer).ToArray(),
					MustNot = leftBool.MustNot,
					Filter = leftBool.Filter
				};

				query = leftBoolCopy;
			}
		}

		// right can't merge and left is not a bool, we forcefully create a wrapped must container
		else if (!leftCantMergeAnd && leftBool is null && rightCantMergeAnd)
		{
			query = CreateMustContainer(leftContainer, rightContainer);
		}

		// left can't merge but right can and is a bool so we add left to the must clause of right
		else if (leftCantMergeAnd && !rightCantMergeAnd && rightBool is not null)
		{
			if (leftContainer is null)
			{
				query = rightContainer;
			}
			else
			{
				var rightBoolCopy = new BoolQuery
				{
					Boost = rightBool.Boost,
					MinimumShouldMatch = rightBool.MinimumShouldMatch,
					QueryName = rightBool.QueryName,
					Should = rightBool.Should,
					Must = rightBool.Must.AddIfNotNull(leftContainer).ToArray(),
					MustNot = rightBool.MustNot,
					Filter = rightBool.Filter
				};

				query = rightBoolCopy;
			}
		}

		//left can't merge and right is not a bool, we forcefully create a wrapped must container
		else if (leftCantMergeAnd && !rightCantMergeAnd && rightBool == null)
		{
			query = CreateMustContainer(new List<Query> { leftContainer, rightContainer });
		}

		return query is not null;
	}

	/// <summary>
	/// Both Sides are bools, but one of them has only should clauses so we should wrap into a new container.
	/// Unless we know one of the sides is a bool with only a must who's clauses are all bools with only should clauses.
	/// This is a piece of metadata we set at the bools creation time so we do not have to iterate the clauses on each combination
	/// In this case we can optimize the generated graph by merging and preventing stack overflows
	/// </summary>
	private static bool TryHandleBoolsWithOnlyShouldClauses(Query leftContainer, Query rightContainer, BoolQuery leftBool, BoolQuery rightBool, out Query query)
	{
		query = null;

		var leftHasOnlyShoulds = leftBool.HasOnlyShouldClauses();
		var rightHasOnlyShoulds = rightBool.HasOnlyShouldClauses();

		if (!leftHasOnlyShoulds && !rightHasOnlyShoulds)
		{
			return false;
		}

		if (leftContainer.HoldsOnlyShouldMusts && rightHasOnlyShoulds)
		{
			leftBool.Must = leftBool.Must.AddIfNotNull(rightContainer).ToArray();
			query = leftContainer;
		}
		else if (rightContainer.HoldsOnlyShouldMusts && leftHasOnlyShoulds)
		{
			rightBool.Must = rightBool.Must.AddIfNotNull(leftContainer).ToArray();
			query = rightContainer;
		}
		else
		{
			query = CreateMustContainer(new List<Query> { leftContainer, rightContainer });
			query.HoldsOnlyShouldMusts = rightHasOnlyShoulds && leftHasOnlyShoulds;
		}

		return true;
	}

	private static Query CreateMustContainer(Query left, Query right) =>
		CreateMustContainer(new List<Query> { left, right });

	private static Query CreateMustContainer(List<Query> mustClauses) =>
		Query.Bool(new BoolQuery() { Must = mustClauses.ToListOrNullIfEmpty() });

	private static Query CreateMustContainer(
		List<Query> mustClauses,
		List<Query> mustNotClauses,
		List<Query> filters
		) => Query.Bool(new BoolQuery
		{
			Must = mustClauses.ToListOrNullIfEmpty(),
			MustNot = mustNotClauses.ToListOrNullIfEmpty(),
			Filter = filters.ToListOrNullIfEmpty()
		});

	private static bool CanMergeAnd(this BoolQuery boolQuery) =>
		boolQuery != null && !boolQuery.Locked && !boolQuery.Should.HasAny();

	private static IEnumerable<Query> OrphanMusts(Query container)
	{
		if (!container.TryGet<BoolQuery>(out var lBoolQuery))
			return new[] { container };

		return lBoolQuery.Must?.AsInstanceOrToListOrNull();
	}

	private static IEnumerable<Query> OrphanMustNots(Query container) =>
		!container.TryGet<BoolQuery>(out var boolQuery) ? null : (IEnumerable<Query>)(boolQuery.MustNot?.AsInstanceOrToListOrNull());

	private static IEnumerable<Query> OrphanFilters(Query container) =>
		!container.TryGet<BoolQuery>(out var boolQuery) ? null : (IEnumerable<Query>)(boolQuery.Filter?.AsInstanceOrToListOrNull());
}
