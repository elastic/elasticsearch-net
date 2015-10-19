using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Cluster.NodesStats
{
	[Collection(IntegrationContext.ReadOnly)]
	public class NodesStatsApiTests : ApiIntegrationTestBase<INodesStatsResponse, INodesStatsRequest, NodesStatsDescriptor, NodesStatsRequest>
	{
		public NodesStatsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.NodesStats(),
			fluentAsync: (client, f) => client.NodesStatsAsync(),
			request: (client, r) => client.NodesStats(r),
			requestAsync: (client, r) => client.NodesStatsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_nodes/stats";

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
			r.ClusterName.Should().NotBeNullOrWhiteSpace();
			r.Nodes.Should().NotBeEmpty().And.HaveCount(1);
			var node = r.Nodes.First();
			node.Key.Should().NotBeNullOrWhiteSpace();
		});

	}

}
