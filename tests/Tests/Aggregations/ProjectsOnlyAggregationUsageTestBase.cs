// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Threading.Tasks;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Aggregations;

public abstract class ProjectsOnlyAggregationUsageTestBase : AggregationUsageTestBase<ReadOnlyCluster>
{
	protected ProjectsOnlyAggregationUsageTestBase(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

	protected override Indices AgainstIndex => DefaultSeeder.ProjectsAliasFilter;
	protected override string ExpectedUrlPathAndQuery => $"/{DefaultSeeder.ProjectsAliasFilter}/_search";

	[U] protected override Task HitsTheCorrectUrl() => base.HitsTheCorrectUrl();

	[U] protected override Task UsesCorrectHttpMethod() => base.UsesCorrectHttpMethod();

	[U] protected override void SerializesInitializer() => base.SerializesInitializer();

	[U] protected override void SerializesFluent() => base.SerializesFluent();

	[I] public override Task ReturnsExpectedStatusCode() => base.ReturnsExpectedResponse();

	[I] public override Task ReturnsExpectedIsValid() => base.ReturnsExpectedIsValid();

	[I] public override Task ReturnsExpectedResponse() => base.ReturnsExpectedResponse();
}
