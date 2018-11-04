using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using static Nest.Infer;

namespace Tests.Indices.IndexManagement.IndicesExists
{
	public class IndexExistsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IExistsResponse, IIndexExistsRequest, IndexExistsDescriptor, IndexExistsRequest>
	{
		public IndexExistsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.HEAD;

		protected override IndexExistsRequest Initializer => new IndexExistsRequest(Index<Project>());
		protected override string UrlPath => $"/project";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.IndexExists(Index<Project>()),
			(client, f) => client.IndexExistsAsync(Index<Project>()),
			(client, r) => client.IndexExists(r),
			(client, r) => client.IndexExistsAsync(r)
		);
	}
}
