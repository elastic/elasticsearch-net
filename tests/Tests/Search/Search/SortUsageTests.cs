// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Search.Search;

public class SortUsageTests : SearchUsageTestBase
{
	public SortUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override object ExpectJson =>
		new
		{
			sort = new object[]
			{
				new { startedOn = new { order = "asc" } },
				new { name = new { order = "desc" } }
			}
		};

	// TODO - Update once we code-generate the fluent syntax
	protected override Action<SearchRequestDescriptor<Project>> Fluent => s => s
		.Sort(new[]
		{
			new SortCombinations(SortOptions.Field(Infer.Field<Project>(f => f.StartedOn), new FieldSort { Order = SortOrder.Asc })),
			new SortCombinations(SortOptions.Field(Infer.Field<Project>(f => f.Name), new FieldSort { Order = SortOrder.Desc }))
		});

	protected override SearchRequest<Project> Initializer =>
		new()
		{
			Sort = new[]
			{
				new SortCombinations(SortOptions.Field(Infer.Field<Project>(f => f.StartedOn), new FieldSort { Order = SortOrder.Asc })),
				new SortCombinations(SortOptions.Field(Infer.Field<Project>(f => f.Name), new FieldSort { Order = SortOrder.Desc }))
			}
		};
}
