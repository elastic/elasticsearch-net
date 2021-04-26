/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System.Collections.Generic;

namespace Nest
{
	using Containers = List<QueryContainer>;

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
			QueryContainer leftContainer, QueryContainer rightContainer, IBoolQuery leftBool, IBoolQuery rightBool, out QueryContainer c
		)
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
