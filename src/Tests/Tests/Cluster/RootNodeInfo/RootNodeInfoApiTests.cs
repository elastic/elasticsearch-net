using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Cluster.RootNodeInfo
{
	public class RootNodeInfoApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IRootNodeInfoResponse, IRootNodeInfoRequest, RootNodeInfoDescriptor, RootNodeInfoRequest>
	{
		public RootNodeInfoApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => "/";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.RootNodeInfo(),
			(client, f) => client.RootNodeInfoAsync(),
			(client, r) => client.RootNodeInfo(r),
			(client, r) => client.RootNodeInfoAsync(r)
		);

		protected override void ExpectResponse(IRootNodeInfoResponse response)
		{
			response.Version.Should().NotBeNull();
			response.Version.LuceneVersion.Should().NotBeNullOrWhiteSpace();
			response.Tagline.Should().NotBeNullOrWhiteSpace();
			response.Name.Should().NotBeNullOrWhiteSpace();
		}
	}
}
