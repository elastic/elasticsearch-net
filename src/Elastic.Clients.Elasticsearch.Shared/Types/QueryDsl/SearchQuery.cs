// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

#if ELASTICSEARCH_SERVERLESS
namespace Elastic.Clients.Elasticsearch.Serverless.QueryDsl;
#else
namespace Elastic.Clients.Elasticsearch.QueryDsl;
#endif

/// <summary>
/// A base type for all query variants.
/// </summary>
public abstract partial class SearchQuery
{
	// Always evaluate to false so that each side of && equation is evaluated
	public static bool operator false(SearchQuery _) => false;

	// Always evaluate to false so that each side of && equation is evaluated
	public static bool operator true(SearchQuery _) => false;

	public static SearchQuery? operator &(SearchQuery? leftQuery, SearchQuery? rightQuery) => Combine(leftQuery, rightQuery, (l, r) => l && r);

	public static SearchQuery? operator |(SearchQuery? leftQuery, SearchQuery? rightQuery) => Combine(leftQuery, rightQuery, (l, r) => l || r);

	public static SearchQuery? operator !(SearchQuery? query) => query is null
		? null
		: new BoolQuery { MustNot = new Query[] { query } };

	public static SearchQuery? operator +(SearchQuery? query) => query is null
		? null
		: new BoolQuery { Filter = new Query[] { query } };

	private static SearchQuery? Combine(SearchQuery? leftQuery, SearchQuery? rightQuery, Func<Query?, Query?, Query> combine)
	{
		// If both sides are null, return null
		if (leftQuery is null && rightQuery is null)
		{
			return null;
		}

		// If the left side is null, return the right side
		if (leftQuery is null)
		{
			return rightQuery;
		}

		// If the right side is null, return the left side
		if (rightQuery is null)
		{
			return leftQuery;
		}

		var container = combine(leftQuery, rightQuery);

		if (container.TryGet<BoolQuery>(out var boolQuery))
		{
			return boolQuery;
		}

		throw new Exception("Unable to combine queries.");
	}

	public static implicit operator Query?(SearchQuery? query) => query is null ? null : new Query(query);

	// We no longer (currently) support verbatim/strict query containers so this may be simplified to a direct abstract method in the future.
	internal void WrapInContainer(Query container) => InternalWrapInContainer(container);

	internal abstract void InternalWrapInContainer(Query container);
}
