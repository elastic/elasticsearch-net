// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Search.Search;

public class BasicSortUsageTests : SearchUsageTestBase
{
	public BasicSortUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override object ExpectJson =>
		new
		{
			sort = "startedOn"
		};

	protected override Action<SearchRequestDescriptor<Project>> Fluent => s => s
		.Sort(s => s.Field(f => f.StartedOn));

	protected override SearchRequest<Project> Initializer =>
		new()
		{
			Sort = new [] { SortOptions.Field("startedOn") }
		};
}

public class FieldSortWithOrderUsageTests : SearchUsageTestBase
{
	public FieldSortWithOrderUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override object ExpectJson =>
		new
		{
			sort = new Dictionary<string, object>
			{
				{
					"startedOn", new
					{
						order = "desc"
					}
				}
			}
		};

	protected override Action<SearchRequestDescriptor<Project>> Fluent => s => s
		.Sort(s => s.Field(f => f.StartedOn, fldsrt => fldsrt.Order(SortOrder.Desc)));

	protected override SearchRequest<Project> Initializer =>
		new()
		{
			Sort = new[] { SortOptions.Field(Infer.Field<Project>(f => f.StartedOn), new FieldSort { Order = SortOrder.Desc }) }
		};
}

public class BasicScoreSortUsageTests : SearchUsageTestBase
{
	public BasicScoreSortUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override object ExpectJson =>
		new
		{
			sort = new { _score = new { order = "desc" }}
		};

	protected override Action<SearchRequestDescriptor<Project>> Fluent => s => s
		.Sort(s => s.Score(sc => sc.Order(SortOrder.Desc)));

	protected override SearchRequest<Project> Initializer =>
		new()
		{
			Sort = new[] { SortOptions.Score(new() { Order = SortOrder.Desc }) }
		};
}
