using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework;
using Tests.Framework.Integration;
using static Nest.Infer;

namespace Tests.Indices.IndexManagement.DeleteIndex
{
	public class DeleteIndexApiTests
		: ApiIntegrationAgainstNewIndexTestBase
			<WritableCluster, IDeleteIndexResponse, IDeleteIndexRequest, DeleteIndexDescriptor, DeleteIndexRequest>
	{
		public DeleteIndexApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteIndexRequest Initializer => new DeleteIndexRequest(CallIsolatedValue);
		protected override string UrlPath => $"/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.DeleteIndex(CallIsolatedValue),
			(client, f) => client.DeleteIndexAsync(CallIsolatedValue),
			(client, r) => client.DeleteIndex(r),
			(client, r) => client.DeleteIndexAsync(r)
		);
	}

	public class DeleteAllIndicesApiTests
		: ApiTestBase<WritableCluster, IDeleteIndexResponse, IDeleteIndexRequest, DeleteIndexDescriptor, DeleteIndexRequest>
	{
		public DeleteAllIndicesApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override HttpMethod HttpMethod => HttpMethod.DELETE;

		protected override DeleteIndexRequest Initializer => new DeleteIndexRequest(AllIndices);
		protected override string UrlPath => $"/_all";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.DeleteIndex(AllIndices),
			(client, f) => client.DeleteIndexAsync(AllIndices),
			(client, r) => client.DeleteIndex(r),
			(client, r) => client.DeleteIndexAsync(r)
		);
	}
}
