// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Nest
{
	using Containers = List<QueryContainer>;

	internal static class BoolQueryAndExtensions
	{
		internal static QueryContainer CombineAsMust(this QueryContainer leftContainer, QueryContainer rightContainer)
		{
			QueryContainer c;
			var leftBool = leftContainer.Self()?.Bool;
			var rightBool = rightContainer.Self()?.Bool;

			//neither side is a bool, no special handling needed wrap in a bool must
			if (leftBool == null && rightBool == null)
				return CreateMustContainer(new Containers { leftContainer, rightContainer });

			else if (TryHandleBoolsWithOnlyShouldClauses(leftContainer, rightContainer, leftBool, rightBool, out c)) return c;
			else if (TryHandleUnmergableBools(leftContainer, rightContainer, leftBool, rightBool, out c)) return c;

			//neither side is unmergable so neither is a bool with should clauses

			var mustNotClauses = OrphanMustNots(leftContainer).EagerConcat(OrphanMustNots(rightContainer));
			var filterClauses = OrphanFilters(leftContainer).EagerConcat(OrphanFilters(rightContainer));
			var mustClauses = OrphanMusts(leftContainer).EagerConcat(OrphanMusts(rightContainer));

			var container = CreateMustContainer(mustClauses, mustNotClauses, filterClauses);
			return container;
		}

		/// <summary>
		/// Handles cases where either side is a bool which indicates it can't be merged yet the other side is mergable.
		/// A side is considered unmergable if its locked (has important metadata) or has should clauses.
		/// Instead of always wrapping these cases in another bool we merge to unmergable side into to others must clause therefor flattening the
		/// generated graph
		/// </summary>
		[SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")] // there for clarity
		private static bool TryHandleUnmergableBools(
			QueryContainer leftContainer, QueryContainer rightContainer, IBoolQuery leftBool, IBoolQuery rightBool, out QueryContainer c
		)
		{
			c = null;
			var leftCantMergeAnd = leftBool != null && !leftBool.CanMergeAnd();
			var rightCantMergeAnd = rightBool != null && !rightBool.CanMergeAnd();
			if (!leftCantMergeAnd && !rightCantMergeAnd) return false;

			if (leftCantMergeAnd && rightCantMergeAnd)
				c = CreateMustContainer(leftContainer, rightContainer);
			//right can't merge but left can and is a bool so we add left to the must clause of right
			else if (!leftCantMergeAnd && leftBool != null && rightCantMergeAnd)
			{
				leftBool.Must = leftBool.Must.AddIfNotNull(rightContainer);
				c = leftContainer;
			}
			//right can't merge and left is not a bool, we forcefully create a wrapped must container
			else if (!leftCantMergeAnd && leftBool == null && rightCantMergeAnd)
				c = CreateMustContainer(leftContainer, rightContainer);
			//left can't merge but right can and is a bool so we add left to the must clause of right
			else if (leftCantMergeAnd && !rightCantMergeAnd && rightBool != null)
			{
				rightBool.Must = rightBool.Must.AddIfNotNull(leftContainer);
				c = rightContainer;
			}
			//left can't merge and right is not a bool, we forcefully create a wrapped must container
			else if (leftCantMergeAnd && !rightCantMergeAnd && rightBool == null)
				c = CreateMustContainer(new Containers { leftContainer, rightContainer });
			return c != null;
		}

		/// <summary>
		/// Both Sides are bools, but one of them has only should clauses so we should wrap into a new container.
		/// Unless we know one of the sides is a bool with only a must who's clauses are all bools with only shoulds.
		/// This is a piece of metadata we set at the bools creation time so we do not have to itterate the clauses on each combination
		/// In this case we can optimize the generated graph by merging and preventing stack overflows
		/// </summary>
		private static bool TryHandleBoolsWithOnlyShouldClauses(
			QueryContainer leftContainer, QueryContainer rightContainer, IBoolQuery leftBool, IBoolQuery rightBool, out QueryContainer c
		)
		{
			c = null;
			var leftHasOnlyShoulds = leftBool.HasOnlyShouldClauses();
			var rightHasOnlyShoulds = rightBool.HasOnlyShouldClauses();
			if (!leftHasOnlyShoulds && !rightHasOnlyShoulds) return false;

			if (leftContainer.HoldsOnlyShouldMusts && rightHasOnlyShoulds)
			{
				leftBool.Must = leftBool.Must.AddIfNotNull(rightContainer);
				c = leftContainer;
			}
			else if (rightContainer.HoldsOnlyShouldMusts && leftHasOnlyShoulds)
			{
				rightBool.Must = rightBool.Must.AddIfNotNull(leftContainer);
				c = rightContainer;
			}
			else
			{
				c = CreateMustContainer(new Containers { leftContainer, rightContainer });
				c.HoldsOnlyShouldMusts = rightHasOnlyShoulds && leftHasOnlyShoulds;
			}
			return true;
		}

		private static QueryContainer CreateMustContainer(QueryContainer left, QueryContainer right) =>
			CreateMustContainer(new Containers { left, right });

		private static QueryContainer CreateMustContainer(List<QueryContainer> mustClauses) =>
			new QueryContainer(new BoolQuery() { Must = mustClauses.ToListOrNullIfEmpty() });

		private static QueryContainer CreateMustContainer(
			List<QueryContainer> mustClauses,
			List<QueryContainer> mustNotClauses,
			List<QueryContainer> filters
		) => new QueryContainer(new BoolQuery
		{
			Must = mustClauses.ToListOrNullIfEmpty(),
			MustNot = mustNotClauses.ToListOrNullIfEmpty(),
			Filter = filters.ToListOrNullIfEmpty()
		});

		private static bool CanMergeAnd(this IBoolQuery boolQuery) =>
			boolQuery != null && !boolQuery.Locked && !boolQuery.Should.HasAny();

		private static IEnumerable<QueryContainer> OrphanMusts(QueryContainer container)
		{
			var lBoolQuery = container.Self().Bool;
			if (lBoolQuery == null) return new[] { container };

			return lBoolQuery.Must?.AsInstanceOrToListOrNull();
		}

		private static IEnumerable<QueryContainer> OrphanMustNots(IQueryContainer container) => container.Bool?.MustNot?.AsInstanceOrToListOrNull();

		private static IEnumerable<QueryContainer> OrphanFilters(IQueryContainer container) => container.Bool?.Filter?.AsInstanceOrToListOrNull();
	}
}
