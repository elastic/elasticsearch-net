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

namespace Tests.Cluster.ClusterHealth
{
	[Collection(IntegrationContext.ReadOnly)]
	public class ClusterHealthApiTests : ApiIntegrationTestBase<IClusterHealthResponse, IClusterHealthRequest, ClusterHealthDescriptor, ClusterHealthRequest>
	{
		public ClusterHealthApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ClusterHealth(),
			fluentAsync: (client, f) => client.ClusterHealthAsync(),
			request: (client, r) => client.ClusterHealth(r),
			requestAsync: (client, r) => client.ClusterHealthAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cluster/health";

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
			r.ClusterName.Should().NotBeNullOrWhiteSpace();
			r.Status.Should().NotBeNullOrWhiteSpace();
			r.TimedOut.Should().BeFalse();
			r.NumberOfNodes.Should().BeGreaterOrEqualTo(1);
			r.NumberOfDataNodes.Should().BeGreaterOrEqualTo(1);
			r.ActivePrimaryShards.Should().BeGreaterOrEqualTo(1);
			r.ActiveShards.Should().BeGreaterOrEqualTo(1);
		});

	}

}
