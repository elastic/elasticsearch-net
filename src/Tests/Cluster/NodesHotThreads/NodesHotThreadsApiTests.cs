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

namespace Tests.Cluster.NodesHotThreads
{
	[Collection(IntegrationContext.ReadOnly)]
	public class NodesHotThreadsApiTests : ApiIntegrationTestBase<INodesHotThreadsResponse, INodesHotThreadsRequest, NodesHotThreadsDescriptor, NodesHotThreadsRequest>
	{
		public NodesHotThreadsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.NodesHotThreads(),
			fluentAsync: (client, f) => client.NodesHotThreadsAsync(),
			request: (client, r) => client.NodesHotThreads(r),
			requestAsync: (client, r) => client.NodesHotThreadsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cluster/nodes/hotthreads";

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
			r.HotThreads.Should().NotBeEmpty();
			var t = r.HotThreads.First();
			t.NodeId.Should().NotBeNullOrWhiteSpace();
			t.NodeName.Should().NotBeNullOrWhiteSpace();
			t.Hosts.Should().NotBeEmpty();
		});

	}

}
