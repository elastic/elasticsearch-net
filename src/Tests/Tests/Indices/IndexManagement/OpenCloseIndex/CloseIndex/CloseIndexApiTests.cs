using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Indices.IndexManagement.OpenCloseIndex.CloseIndex
{
	public class CloseIndexApiTests
		: ApiIntegrationAgainstNewIndexTestBase<WritableCluster, ICloseIndexResponse, ICloseIndexRequest, CloseIndexDescriptor, CloseIndexRequest>
	{
		public CloseIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override CloseIndexRequest Initializer => new CloseIndexRequest(CallIsolatedValue);
		protected override string UrlPath => $"/{CallIsolatedValue}/_close";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.CloseIndex(CallIsolatedValue),
			(client, f) => client.CloseIndexAsync(CallIsolatedValue),
			(client, r) => client.CloseIndex(r),
			(client, r) => client.CloseIndexAsync(r)
		);
	}
}
