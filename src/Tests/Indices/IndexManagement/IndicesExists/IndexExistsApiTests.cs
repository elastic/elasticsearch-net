using Elasticsearch.Net_5_2_0;
using Nest_5_2_0;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest_5_2_0.Infer;

namespace Tests.Indices.IndexManagement.IndicesExists
{
	public class IndexExistsApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IExistsResponse, IIndexExistsRequest, IndexExistsDescriptor, IndexExistsRequest>
	{
		public IndexExistsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.IndexExists(Index<Project>()),
			fluentAsync: (client, f) => client.IndexExistsAsync(Index<Project>()),
			request: (client, r) => client.IndexExists(r),
			requestAsync: (client, r) => client.IndexExistsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.HEAD;
		protected override string UrlPath => $"/project";

		protected override IndexExistsRequest Initializer => new IndexExistsRequest(Index<Project>());
	}
}
