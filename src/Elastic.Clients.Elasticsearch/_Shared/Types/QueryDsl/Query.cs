// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;

namespace Elastic.Clients.Elasticsearch.QueryDsl;

public partial class Query
{
	internal bool HoldsOnlyShouldMusts { get; set; }

	public static bool operator false(Query _) => false;
	public static bool operator true(Query _) => false;

	public static Query operator &(Query leftContainer, Query rightContainer) =>
		And(leftContainer, rightContainer);

	internal static Query And(Query leftContainer, Query rightContainer)
	{
		if (leftContainer is null && rightContainer is null)
		{
			throw new ArgumentException("Queries to combine should not both be null.");
		}

		if (rightContainer is null)
			return leftContainer;

		if (leftContainer is null)
			return rightContainer;

		return leftContainer.CombineAsMust(rightContainer);
	}

	public static Query operator |(Query leftContainer, Query rightContainer) => Or(leftContainer, rightContainer);

	internal static Query Or(Query leftContainer, Query rightContainer)
	{
		if (leftContainer is null && rightContainer is null)
		{
			throw new ArgumentException("Queries to combine should not both be null.");
		}

		if (rightContainer is null)
			return leftContainer;

		if (leftContainer is null)
			return rightContainer;

		return leftContainer.CombineAsShould(rightContainer);
	}

	public static Query operator !(Query queryContainer) => queryContainer is null
			? null
			: Query.Bool(new BoolQuery { MustNot = new[] { queryContainer } });

	public static Query operator +(Query queryContainer) => queryContainer is null
		? null
		: Query.Bool(new BoolQuery { Filter = new[] { queryContainer } });
}
