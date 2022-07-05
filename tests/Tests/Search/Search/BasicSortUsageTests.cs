// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
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
		.Sort(new Sort
		{
			new SortCombinations("startedOn")
		});

	protected override SearchRequest<Project> Initializer =>
		new()
		{
			Sort = new Sort
			{
				new SortCombinations("startedOn")
			}
		};
}
