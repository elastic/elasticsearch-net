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

namespace Tests.Cluster.ClusterStats
{
	[Collection(IntegrationContext.ReadOnly)]
	public class ClusterStatsApiTests : ApiTestBase<IClusterStatsResponse, IClusterStatsRequest, ClusterStatsDescriptor, ClusterStatsRequest>
	{
		public ClusterStatsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.ClusterStats(),
			fluentAsync: (client, f) => client.ClusterStatsAsync(),
			request: (client, r) => client.ClusterStats(r),
			requestAsync: (client, r) => client.ClusterStatsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/_cluster/stats";

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
			r.ClusterName.Should().NotBeNullOrWhiteSpace();
			r.Nodes.Should().NotBeNull();
			r.Nodes.Count.Should().NotBeNull();
			r.Nodes.Count.MasterData.Should().BeGreaterOrEqualTo(1);

			r.Indices.Should().NotBeNull();
			r.Indices.Count.Should().BeGreaterThan(0);

			r.Indices.Docs.Should().NotBeNull();
			r.Indices.Docs.Count.Should().BeGreaterThan(0);
		});

	}

}
