// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Cluster.NodesUsage
{
	public class NodesUsageApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, NodesUsageResponse, INodesUsageRequest, NodesUsageDescriptor, NodesUsageRequest>
	{
		public NodesUsageApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_nodes/usage";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			var searchResponse = client.Search<Project>(s => s
				.Size(0)
				.Aggregations(a => a
					.Average("avg_commits", avg => avg
						.Field(f => f.NumberOfCommits)
					)
				)
			);

			if (!searchResponse.IsValid)
				throw new Exception($"Exception when setting up {nameof(NodesUsageApiTests)}: {searchResponse.DebugInformation}");
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Nodes.Usage(),
			(client, f) => client.Nodes.UsageAsync(),
			(client, r) => client.Nodes.Usage(r),
			(client, r) => client.Nodes.UsageAsync(r)
		);

		protected override void ExpectResponse(NodesUsageResponse response)
		{
			response.ClusterName.Should().NotBeEmpty();

			response.NodeStatistics.Should().NotBeNull();
			response.NodeStatistics.Total.Should().Be(1);
			response.NodeStatistics.Successful.Should().Be(1);
			response.NodeStatistics.Failed.Should().Be(0);

			response.Nodes.Should().NotBeNull();
			response.Nodes.Should().HaveCount(1);

			var firstNode = response.Nodes.First();
			firstNode.Value.Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);
			firstNode.Value.Since.Should().BeBefore(DateTimeOffset.UtcNow);
			firstNode.Value.RestActions.Should().NotBeNull();

			if (TestClient.Configuration.InRange(">=7.8.0"))
				firstNode.Value.Aggregations.Should().NotBeNull().And.ContainKey("avg");

		}
	}
}
