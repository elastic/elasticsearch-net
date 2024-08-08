// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Aggregations;

public abstract class ProjectsOnlyAggregationUsageTestBase : AggregationUsageTestBase<ReadOnlyCluster>
{
	protected ProjectsOnlyAggregationUsageTestBase(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override Indices AgainstIndex => DefaultSeeder.ProjectsAliasFilter;

	protected override string ExpectedUrlPathAndQuery => $"/{DefaultSeeder.ProjectsAliasFilter}/_search";
}
