using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Xunit;

namespace Tests.Cluster.NodesUsage
{
	public class NodesUsageApiTests : ApiIntegrationTestBase<ReadOnlyCluster, INodesUsageResponse, INodesUsageRequest, NodesUsageDescriptor, NodesUsageRequest>
	{
		public NodesUsageApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.NodesUsage(),
			fluentAsync: (client, f) => client.NodesUsageAsync(),
			request: (client, r) => client.NodesUsage(r),
			requestAsync: (client, r) => client.NodesUsageAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_nodes/usage";

		protected override void ExpectResponse(INodesUsageResponse response)
		{
			response.ClusterName.Should().NotBeEmpty();

			response.NodesMetaData.Should().NotBeNull();
			response.NodesMetaData.Total.Should().Be(1);
			response.NodesMetaData.Successful.Should().Be(1);
			response.NodesMetaData.Failed.Should().Be(0);

			response.Nodes.Should().NotBeNull();
			response.Nodes.Should().HaveCount(1);

			response.Nodes.First().Value.Timestamp.Should().BeBefore(DateTimeOffset.UtcNow);
			response.Nodes.First().Value.Since.Should().BeBefore(DateTimeOffset.UtcNow);
			response.Nodes.First().Value.RestActions.Should().NotBeNull();
		}
	}

}
