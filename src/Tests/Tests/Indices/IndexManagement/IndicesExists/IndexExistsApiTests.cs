using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Indices.IndexManagement.IndicesExists
{
	public class IndexExistsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ExistsResponse, IIndexExistsRequest, IndexExistsDescriptor, IndexExistsRequest>
	{
		public IndexExistsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.HEAD;

		protected override IndexExistsRequest Initializer => new IndexExistsRequest(Index<Project>());
		protected override string UrlPath => $"/project";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.Exists(Index<Project>()),
			(client, f) => client.Indices.ExistsAsync(Index<Project>()),
			(client, r) => client.Indices.Exists(r),
			(client, r) => client.Indices.ExistsAsync(r)
		);

		protected override void ExpectResponse(ExistsResponse response) => response.Exists.Should().BeTrue();
	}
}
