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

namespace Tests.Cluster.NodesInfo
{
	[Collection(IntegrationContext.ReadOnly)]
	public class NodesInfoApiTests : ApiIntegrationTestBase<INodesInfoResponse, INodesInfoRequest, NodesInfoDescriptor, NodesInfoRequest>
	{
		public NodesInfoApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.NodesInfo(),
			fluentAsync: (client, f) => client.NodesInfoAsync(),
			request: (client, r) => client.NodesInfo(r),
			requestAsync: (client, r) => client.NodesInfoAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_nodes";

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
			r.ClusterName.Should().NotBeNullOrWhiteSpace();
			r.Nodes.Should().NotBeEmpty().And.HaveCount(1);
			var node = r.Nodes.First();
			node.Key.Should().NotBeNullOrWhiteSpace();
		});

	}

}
