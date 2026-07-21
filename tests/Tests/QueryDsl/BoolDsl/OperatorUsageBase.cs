// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace Tests.QueryDsl.BoolDsl;

public abstract class OperatorUsageBase
{
	protected static readonly TermQuery NullQuery = null;
	protected static readonly TermQuery TermQuery = new("x") { Value = "y" };

	protected static void ReturnsNull(Query combined)
	{
		combined.Should().BeNull();
	}

	protected static void ReturnsBool(Query combined, Action<BoolQuery> boolQueryAssert)
	{
		combined.Should().NotBeNull();
		combined.TryGet<BoolQuery>(out var boolQuery).Should().BeTrue();
		boolQuery.Should().NotBeNull();
		boolQueryAssert(boolQuery);
	}

	protected static void ReturnsSingleQuery<T>(Query combined, Action<T> queryAssert) where T : Query
	{
		combined.Should().NotBeNull();
		combined.TryGet<T>(out var query).Should().BeTrue();
		query.Should().NotBeNull();
		queryAssert(query);
	}
}
