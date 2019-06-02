using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Indices.IndexManagement.OpenCloseIndex.CloseIndex
{
	public class CloseIndexApiTests
		: ApiIntegrationAgainstNewIndexTestBase<WritableCluster, CloseIndexResponse, ICloseIndexRequest, CloseIndexDescriptor, CloseIndexRequest>
	{
		public CloseIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override CloseIndexRequest Initializer => new CloseIndexRequest(CallIsolatedValue);
		protected override string UrlPath => $"/{CallIsolatedValue}/_close";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Indices.Close(CallIsolatedValue),
			(client, f) => client.Indices.CloseAsync(CallIsolatedValue),
			(client, r) => client.Indices.Close(r),
			(client, r) => client.Indices.CloseAsync(r)
		);
	}
}
