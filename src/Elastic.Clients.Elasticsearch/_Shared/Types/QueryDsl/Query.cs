// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

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
			: new() { Bool = new() { MustNot = [queryContainer] } };

	public static Query operator +(Query queryContainer) => queryContainer is null
		? null
		: new() { Bool = new() { Filter = [queryContainer] } };
}

public readonly partial struct QueryDescriptor
{
	public static bool operator false(QueryDescriptor _) => false;

	public static bool operator true(QueryDescriptor _) => false;

	public static QueryDescriptor operator &(QueryDescriptor leftContainer, QueryDescriptor rightContainer) =>
		new(leftContainer.Instance & rightContainer.Instance);

	public static QueryDescriptor operator |(QueryDescriptor leftContainer, QueryDescriptor rightContainer) =>
		new(leftContainer.Instance | rightContainer.Instance);

	public static QueryDescriptor operator !(QueryDescriptor queryContainer) => new(!queryContainer.Instance);

	public static QueryDescriptor operator +(QueryDescriptor queryContainer) => new(+queryContainer.Instance);
}

public readonly partial struct QueryDescriptor<TDocument>
{
	public static bool operator false(QueryDescriptor<TDocument> _) => false;

	public static bool operator true(QueryDescriptor<TDocument> _) => false;

	public static QueryDescriptor<TDocument> operator &(QueryDescriptor<TDocument> leftContainer, QueryDescriptor<TDocument> rightContainer) =>
		new(leftContainer.Instance & rightContainer.Instance);

	public static QueryDescriptor<TDocument> operator |(QueryDescriptor<TDocument> leftContainer, QueryDescriptor<TDocument> rightContainer) =>
		new(leftContainer.Instance | rightContainer.Instance);

	public static QueryDescriptor<TDocument> operator !(QueryDescriptor<TDocument> queryContainer) => new(!queryContainer.Instance);

	public static QueryDescriptor<TDocument> operator +(QueryDescriptor<TDocument> queryContainer) => new(+queryContainer.Instance);
}
